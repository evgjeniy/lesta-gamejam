using System.Collections;
using System.Collections.Generic;
using Code.Scripts.Services;
using Code.Scripts.Shoot;
using Code.Scripts.Util;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Scripts.Controllers
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField] private List<WeaponTypeScriptable> weaponType;
        [SerializeField] private EnergySystem.EnergySystem energySystem;
        [SerializeField] private Renderer renderer;
        [SerializeField] private Transform projectileSpawnPoint;
        [SerializeField] private float switchInactiveTime;
        [SerializeField, Min(0.0f)] private float shootCooldown = 0.5f;
        [SerializeField, Min(0.0f)] private float shootEnergy = 25f;

        private PlayerInputBehaviour _inputBehaviour;
        private bool _canSwitch = true;
        private int _currentProjectileType;
        private float _shootTimer;

        private int CurrentType
        {
            get => _currentProjectileType;
            set
            {
                if (!_canSwitch) return;
                
                if (value < 0)
                    _currentProjectileType = weaponType.Count - 1;
                else if (value > weaponType.Count - 1)
                    _currentProjectileType = 0;
                else
                    _currentProjectileType = value;
                renderer.material.color = weaponType[CurrentType].ProjectileColor;
                
                StartCoroutine(WaitForSwitch());
            }
        }

        private void Awake() => _inputBehaviour = GetComponent<PlayerInputBehaviour>();

        private void Update() => _shootTimer += Time.deltaTime;

        private void OnEnable()
        {
            _inputBehaviour.Shoot.performed += Shoot;
            _inputBehaviour.ChangeWeapon.performed += ChangeWeapon;
        }

        private void OnDisable()
        {
            _inputBehaviour.Shoot.performed -= Shoot;
            _inputBehaviour.ChangeWeapon.performed -= ChangeWeapon;
        }

        private void ChangeWeapon(InputAction.CallbackContext context)
        {
            switch (context.ReadValue<float>())
            {
                case > 0.6f: CurrentType++; break;
                case < -0.6f: CurrentType--; break;
            }
        }
        
        private void Shoot(InputAction.CallbackContext context)
        {
            if (_shootTimer < shootCooldown) return;
            _shootTimer = 0.0f;

            if (Camera.main == null) return;

            if (!energySystem.SpendEnergy(shootEnergy)) return;

            var spawnPosition = projectileSpawnPoint.position;
            
            SpawnProjectile(Camera.main.GetMousePosition(spawnPosition) - spawnPosition);
        }

        private Projectile SpawnProjectile(Vector3 direction)
        {
            var result = Instantiate(weaponType[CurrentType].ProjectileType, projectileSpawnPoint.position, Quaternion.identity);
            result.MoveDirection = direction;
            result.energySystem = energySystem;
            return result;
        }

        private IEnumerator WaitForSwitch()
        {
            _canSwitch = false;
            yield return new WaitForSeconds(switchInactiveTime);
            _canSwitch = true;
        }
    }
}

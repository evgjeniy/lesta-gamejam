using System.Collections;
using System.Collections.Generic;
using Code.Scripts.Shoot;
using UnityEngine;

namespace Code.Scripts.Controllers
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField] private List<WeaponTypeScriptable> weaponType;
        [SerializeField] private EnergySystem.EnergySystem energySystem;
        [SerializeField] private Renderer renderer;
        [SerializeField] private float switchInactiveTime;
        
        private bool _canSwitch = true;
        private int _currentProjectileType;

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

        public void ChangeWeaponType(float mouseInput)
        {
            switch (mouseInput)
            {
                case > 0.6f:
                    CurrentType++;
                    break;
                case < -0.6f:
                    CurrentType--;
                    break;
            }
        }
    
        public void Shoot(Vector3 direction)
        {
            SpawnProjectile(direction);
        }

        private Projectile SpawnProjectile(Vector3 direction)
        {
            var result = Instantiate(weaponType[CurrentType].ProjectileType, transform.position, Quaternion.identity);
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

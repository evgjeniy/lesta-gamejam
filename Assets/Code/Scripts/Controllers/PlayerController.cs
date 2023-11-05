using Code.Scripts.Services;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Scripts.Controllers
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(PlayerInputBehaviour))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField, Min(0.0f)] private float speed = 5.0f;
        [SerializeField, Min(0.0f)] private float jumpVelocityY = 5.0f;
        [SerializeField, Min(0.0f)] private float shootCooldown = 0.5f;
        [SerializeField, Min(0.0f)] private float shootEnergy = 25f;
        [Header("Overlap")]
        [SerializeField, Min(0.0f)] private float groundCheckRadius = 0.1f;
        [SerializeField] private Transform groundCheck;
        [SerializeField] private LayerMask groundMask;
        
        [SerializeField] private WeaponController weaponController;
        [SerializeField] private EnergySystem.EnergySystem energySystem;
        private PlayerInputBehaviour _inputBehaviour;
        private Rigidbody _rigidbody;

        private float _shootTimer;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _inputBehaviour = GetComponent<PlayerInputBehaviour>();
        }

        private void OnEnable()
        {
            _inputBehaviour.Jump.performed += Jump;
            _inputBehaviour.Shoot.performed += Shoot;
            _inputBehaviour.ChangeWeapon.performed += ChangeWeapon;
            _inputBehaviour.DisableAbility.performed += DisableAbility;
        }

        private void OnDisable()
        {
            _inputBehaviour.Jump.performed -= Jump;
            _inputBehaviour.Shoot.performed -= Shoot;
            _inputBehaviour.ChangeWeapon.performed -= ChangeWeapon;
            _inputBehaviour.DisableAbility.performed -= DisableAbility;
        }

        private void Update() => _shootTimer += Time.deltaTime;

        private void FixedUpdate()
        {
            var moveAxis = _inputBehaviour.GetMoveAxis();
            var moveDirection = Vector3.right * moveAxis;
            _rigidbody.position += moveDirection * (speed * Time.fixedDeltaTime);
            
            if (moveAxis != 0.0f) _rigidbody.rotation = Quaternion.LookRotation(moveDirection);
        }

        private void DisableAbility(InputAction.CallbackContext context)
        {
            energySystem.RemoveAllSpenders();
        }

        private void ChangeWeapon(InputAction.CallbackContext context)
        {
            weaponController.ChangeWeaponType(context.ReadValue<float>());
        }

        private void Jump(InputAction.CallbackContext context)
        {
            if (!Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundMask.value)) return;
            
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, jumpVelocityY, 0.0f);
        }

        private void Shoot(InputAction.CallbackContext context)
        {
            if (_shootTimer < shootCooldown) return;
            _shootTimer = 0.0f;

            if (Camera.main == null) return;

            if (!energySystem.SpendEnergy(shootEnergy)) return;

            var movementDirection = Input.mousePosition;
            movementDirection.z = Mathf.Abs(transform.position.z - Camera.main.transform.position.z);

            weaponController.Shoot(Camera.main.ScreenToWorldPoint(movementDirection) - transform.position);
        }
    }
}
using Code.Scripts.Overlap;
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

        [SerializeField] private CheckSphereOverlap groundCheckSphere;
        [SerializeField] private EnergySystem.EnergySystem energySystem;
        
        private PlayerInputBehaviour _inputBehaviour;
        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _inputBehaviour = GetComponent<PlayerInputBehaviour>();
        }

        private void OnEnable()
        {
            _inputBehaviour.Jump.performed += Jump;
            _inputBehaviour.DisableAbility.performed += DisableAbility;
        }

        private void OnDisable()
        {
            _inputBehaviour.Jump.performed -= Jump;
            _inputBehaviour.DisableAbility.performed -= DisableAbility;
        }

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

        private void Jump(InputAction.CallbackContext context)
        {
            if (!groundCheckSphere.Check()) return;
            
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, jumpVelocityY, 0.0f);
        }

        private void OnDrawGizmosSelected() => groundCheckSphere.TryDrawGizmos();
    }
}
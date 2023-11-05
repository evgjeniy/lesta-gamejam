using System;
using Code.Scripts.Overlap;
using Code.Scripts.Services;
using Code.Scripts.Util;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Scripts.Controllers
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(PlayerInputBehaviour))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField, Min(0.0f)] private float jumpVelocityY = 5.0f;

        [SerializeField] private CheckSphereOverlap groundCheckSphere;
        [SerializeField] private EnergySystem.EnergySystem energySystem;
        
        private PlayerInputBehaviour _inputBehaviour;
        private Rigidbody _rigidbody;
        private Animator _animator;
        
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");
        private static readonly int MoveAxis = Animator.StringToHash("MoveAxis");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
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
            var lookVector = Camera.main.GetMousePosition(transform.position);
            
            var sign = Mathf.Sign(lookVector.x - transform.position.x);
            _rigidbody.rotation = Quaternion.Lerp(_rigidbody.rotation, Quaternion.LookRotation(Vector3.right * sign), 10 * Time.fixedDeltaTime);

            var moveAxis = _inputBehaviour.GetMoveAxis();
            var isMoving = moveAxis != 0.0f;
            _animator.SetBool(IsMoving, isMoving);
            if (isMoving) _animator.SetFloat(MoveAxis, moveAxis * sign);
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
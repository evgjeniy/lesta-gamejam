using System;
using System.Collections;
using Code.Scripts.Energy;
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
        [SerializeField, Min(0.0f)] private float jumpMoveSpeedX = 3.0f;
        [SerializeField, Range(1.0f, 30.0f)] private float animationSmoothValue = 10.0f;

        [SerializeField] private CheckSphereOverlap groundCheckSphere;
        [SerializeField] private EnergySystem energySystem;
        
        private PlayerInputBehaviour _inputBehaviour;
        private Rigidbody _rigidbody;
        private Animator _animator;
        
        private static readonly int MoveAxis = Animator.StringToHash("MoveAxis");
        private static readonly int IsJumping = Animator.StringToHash("IsJumping");

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
            var lerpTime = Time.fixedDeltaTime * animationSmoothValue;
            
            _rigidbody.rotation = Quaternion.Lerp(_rigidbody.rotation, Quaternion.LookRotation(Vector3.right * sign), lerpTime);

            var moveAxis = _inputBehaviour.GetMoveAxis();
            _animator.SetFloat(MoveAxis, Mathf.Lerp(_animator.GetFloat(MoveAxis), moveAxis * sign, lerpTime));
        }

        private void DisableAbility(InputAction.CallbackContext context)
        {
            energySystem.RemoveAllSpenders();
        }

        private void Jump(InputAction.CallbackContext context)
        {
            if (!groundCheckSphere.Check()) return;
            StartCoroutine(JumpCoroutine());
        }

        // need's to be fixed, Jump is stop the movement in the end
        private IEnumerator JumpCoroutine()
        {
            _animator.applyRootMotion = false;
            yield return new WaitForFixedUpdate();
            
            _animator.SetBool(IsJumping, true);

            while (groundCheckSphere.Check())
            {
                var moveAxis = _inputBehaviour.GetMoveAxis();
                _rigidbody.velocity = new Vector3(moveAxis * jumpMoveSpeedX, jumpVelocityY, 0.0f);
                yield return new WaitForFixedUpdate();
            }
            
            while (!groundCheckSphere.Check())
            {
                var moveAxis = _inputBehaviour.GetMoveAxis();
                _rigidbody.velocity = new Vector3(moveAxis * jumpMoveSpeedX, _rigidbody.velocity.y, 0.0f);
                yield return new WaitForFixedUpdate();
            }
            
            _animator.SetBool(IsJumping, false);
            _animator.applyRootMotion = true;
        }

        private void OnDrawGizmosSelected() => groundCheckSphere.TryDrawGizmos();
    }
}
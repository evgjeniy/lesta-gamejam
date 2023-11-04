using Code.Scripts.Services;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Scripts
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(PlayerInputBehaviour))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField, Min(0.0f)] private float speed = 5.0f;
        [SerializeField, Min(0.0f)] private float jumpVelocityY = 5.0f;
        
        [Header("Overlap")]
        [SerializeField, Min(0.0f)] private float groundCheckRadius;
        [SerializeField] private Transform groundCheck;
        [SerializeField] private LayerMask groundMask;

        [SerializeField] private Projectile projectileRef;

        private Rigidbody _rigidbody;
        private PlayerInputBehaviour _inputBehaviour;

        private Projectile.Factory _factory;

        private void Awake()
        {
            _factory = new Projectile.Factory(projectileRef);
            _rigidbody = GetComponent<Rigidbody>();
            _inputBehaviour = GetComponent<PlayerInputBehaviour>();
        }

        private void Start()
        {
            _inputBehaviour.ShootAction.performed += OnShootAction;
        }

        private void OnEnable()
        {
          //  _inputBehaviour.ShootAction.performed += OnShootAction;
        }

        private void OnDisable()
        {
            _inputBehaviour.ShootAction.performed -= OnShootAction;
        }

        private void OnShootAction(InputAction.CallbackContext context)
        {
            _factory.Create(Vector3.left).transform.position = transform.position;
        }

        private void FixedUpdate()
        {
            var inputDirection = _inputBehaviour.GetMoveDirection();
            _rigidbody.velocity = inputDirection * speed;
            transform.rotation = Quaternion.LookRotation(inputDirection);

            if (_inputBehaviour.IsJumping()) Jump();
        }
        
        private void Jump()
        {
            if (!Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundMask.value)) return;
            
            var currentVelocity = _rigidbody.velocity;
            var jumpVelocity = new Vector3(currentVelocity.x, jumpVelocityY, currentVelocity.z);
            _rigidbody.velocity = jumpVelocity;
        }
    }
}
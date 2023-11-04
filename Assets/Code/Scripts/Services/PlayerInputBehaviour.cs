using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Scripts.Services
{
    public class PlayerInputBehaviour : MonoBehaviour
    {
        private InputActions _inputActions;

        private void Awake() => _inputActions = new InputActions();

        private void OnEnable() => _inputActions.Enable();

        private void OnDisable() => _inputActions.Disable();

        public InputAction ShootAction => _inputActions.Player.Shoot;

        public Vector3 GetMoveDirection()
        {
            var moveAxis = _inputActions.Player.Move.ReadValue<float>();
            return new Vector3(moveAxis, 0.0f, 0.0f);
        }

        public bool IsJumping() => _inputActions.Player.Jump.ReadValue<float>() != 0.0f;
    }
}
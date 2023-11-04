using Code.Scripts.Contracts;
using UnityEngine;

namespace Code.Scripts.Services
{
    public class PlayerInputBehaviour : IPlayerInputService
    {
        private readonly InputActions _inputActions;

        public PlayerInputBehaviour(InputActions inputActions) => _inputActions = inputActions;

        public void Enable() => _inputActions.Enable();

        public void Disable() => _inputActions.Disable();

        public Vector3 GetMoveDirection()
        {
            var inputDirection = _inputActions.Player.Move.ReadValue<Vector2>();
            return new Vector3(inputDirection.x, 0.0f, inputDirection.y);
        }
    }
}
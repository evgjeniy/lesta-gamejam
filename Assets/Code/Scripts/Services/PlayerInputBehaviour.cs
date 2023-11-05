using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Scripts.Services
{
    [DefaultExecutionOrder(-100)] // Unity Execution Order is terrible (must be initialized before everything else)
    public class PlayerInputBehaviour : MonoBehaviour
    {
        private InputActions _inputActions;

        public InputAction Shoot => _inputActions.Player.Shoot;
        public InputAction Jump => _inputActions.Player.Jump;
        public InputAction ChangeWeapon => _inputActions.Player.ChangeWeapon;

        private void Awake() => _inputActions = new InputActions();

        private void OnEnable() => _inputActions.Enable();

        private void OnDisable() => _inputActions.Disable();

        public float GetMoveAxis() => _inputActions.Player.Move.ReadValue<float>();
    }
}
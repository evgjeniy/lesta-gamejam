using UnityEngine;

namespace Code.Scripts.Contracts
{
    public interface IPlayerInputService
    {
        public void Enable();
        public void Disable();
        public Vector3 GetMoveDirection();
    }
}
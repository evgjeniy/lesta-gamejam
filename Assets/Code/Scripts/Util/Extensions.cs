using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Scripts.Util
{
    public static class Extensions
    {
        public static Vector3 GetMousePosition(this Camera camera, Vector3 targetPosition)
        {
            if (camera == null)
                throw new NullReferenceException($"Local variable {nameof(camera)} in method {nameof(GetMousePosition)} is null");
            
            Vector3 mousePosition = Mouse.current.position.value;
            mousePosition.z = Mathf.Abs(targetPosition.z - camera.transform.position.z);

            return camera.ScreenToWorldPoint(mousePosition);
        }
    }
}
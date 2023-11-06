using Code.Scripts.Util;
using UnityEngine;

namespace Code.Scripts.Shoot
{
    public class BulletTrajectory : MonoBehaviour
    {
        [SerializeField] private LineRenderer lr;
        [SerializeField] private Transform startLinePoint;
        [SerializeField] private float maxLen;
        
        private Camera _camera;

        private void Start() => _camera = Camera.main;

        private void LateUpdate()
        {
            if (_camera == null) return;
            
            var firstPoint = startLinePoint.position;    
            lr.SetPosition(0, firstPoint);

            var direction = Vector3.ClampMagnitude(_camera.GetMousePosition(firstPoint) - firstPoint, maxLen);
            lr.SetPosition(1, firstPoint + direction);
        }
    }
}

using UnityEngine;

namespace Code.Scripts.Overlap
{
    [System.Serializable]
    public class CheckSphereOverlap : CheckOverlap
    {
        [SerializeField] private Transform root;
        [SerializeField] private Vector3 offset;
        [SerializeField, Min(0.0f)] private float radius = 1.0f;
        
        private Vector3 Center => root == null ? offset : root.position + offset;

        public override bool Check() => Physics.CheckSphere(Center, radius);
        public override void DrawWireGizmos() => Gizmos.DrawWireSphere(Center, radius);
        public override void DrawSolidGizmos() => Gizmos.DrawSphere(Center, radius);
    }
}
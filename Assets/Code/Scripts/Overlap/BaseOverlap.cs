using Code.Scripts.Util;
using UnityEngine;

namespace Code.Scripts.Overlap
{
    [System.Serializable]
    public abstract class BaseOverlap
    {
        [SerializeField] private GizmosData gizmos;

        public void TryDrawGizmos()
        {
            if (gizmos.drawType == GizmosData.DrawType.None) return;
            
            Gizmos.color = gizmos.gizmosColor;
            
            if ((gizmos.drawType & GizmosData.DrawType.Wire) != 0) DrawWireGizmos();
            if ((gizmos.drawType & GizmosData.DrawType.Solid) != 0) DrawSolidGizmos();
        }
        
        public virtual void DrawWireGizmos() {}
        public virtual void DrawSolidGizmos() {}
    }
}
using UnityEngine;

namespace Code.Scripts.Shoot
{
    public interface IProjectileHit
    {
        public void OnHitStatic(GameObject obj, Collision context);
        public void OnHitDynamic(GameObject obj, Collision context);
        public void OnHitDestroyable(GameObject obj, Collision context);
    }
}

using UnityEngine;

namespace Code.Scripts.Shoot
{
    public class HitResolver : MonoBehaviour
    {
        public enum HitType { Static, Dynamic, Destroyable }

        [SerializeField] private HitType hitType;

        public void ResolveHit(IProjectileHit hit, Collision context)
        {
            switch (hitType)
            {
                case HitType.Static:
                    hit.OnHitStatic(gameObject, context);
                    break;
                case HitType.Dynamic:
                    hit.OnHitDynamic(gameObject, context);
                    break;
                case HitType.Destroyable:
                    hit.OnHitDestroyable(gameObject, context);
                    break;
            }
        }
    }
}

using UnityEngine;

namespace Code.Scripts.Shoot
{
    public class KinematicProjectile : Projectile
    {

        public override void OnHitDestroyable(GameObject obj, Collision context)
        {
            OnHit();
            Destroy(obj);
            Destroy(gameObject);
        }

        public override void OnHitDynamic(GameObject obj, Collision context)
        {
            OnHit();
            Destroy(gameObject);
        }

        public override void OnHitStatic(GameObject obj, Collision context)
        {
            OnHit();
            MoveDirection = Vector3.Reflect(MoveDirection, context.contacts[0].normal);
        }
    }
}

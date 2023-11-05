using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicProjectile : Projectile
{
    public override void OnHitDestroyable(GameObject obj, Collision context)
    {
        Destroy(obj);
        Destroy(gameObject);
    }

    public override void OnHitDynamic(GameObject obj, Collision context)
    {
        Destroy(gameObject);
    }

    public override void OnHitStatic(GameObject obj, Collision context)
    {
        MoveDirection = Vector3.Reflect(MoveDirection, context.contacts[0].normal);
    }
}

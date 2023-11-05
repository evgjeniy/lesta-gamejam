using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeReverseProjectile : Projectile
{
    public override void OnHitDestroyable(GameObject obj, Collision context)
    {
        Destroy(gameObject);
    }

    public override void OnHitDynamic(GameObject obj, Collision context)
    {
        obj.TryGetComponent<Timeline>(out var timeline);
        timeline?.StartReverse();
        Destroy(gameObject);
    }

    public override void OnHitStatic(GameObject obj, Collision context)
    {
        Destroy(gameObject);
    }
}

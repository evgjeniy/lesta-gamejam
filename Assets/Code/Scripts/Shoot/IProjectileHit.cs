using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProjectileHit
{
    public void OnHitStatic(GameObject obj, Collision context);
    public void OnHitDynamic(GameObject obj, Collision context);
    public void OnHitDestroyable(GameObject obj, Collision context);
}

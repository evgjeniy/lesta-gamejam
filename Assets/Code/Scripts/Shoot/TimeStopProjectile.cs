using Code.Scripts.EnergySystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeStopProjectile : Projectile
{
    [SerializeField] private EnergySpender energySpender;

    public override void OnHitDestroyable(GameObject obj, Collision context)
    {
        Destroy(gameObject);
    }

    public override void OnHitDynamic(GameObject obj, Collision context)
    {
        obj.TryGetComponent<Timeline>(out var timeline);
        var energy = Instantiate(energySpender);
        energy.OnDestroy += () =>
        {
            timeline.StopReverse();
            timeline.StartTime();
        };
        energySystem.AddSpender(energy);
        timeline?.StopTime();
        Destroy(gameObject);
    }

    public override void OnHitStatic(GameObject obj, Collision context)
    {
        Destroy(gameObject);
    }
}

using Code.Scripts.Energy;
using UnityEngine;

namespace Code.Scripts.Shoot
{
    public class TimeReverseProjectile : Projectile
    {
        [SerializeField] private EnergySpender energySpender;

        public override void OnHitDestroyable(GameObject obj, Collision context)
        {
            Destroy(gameObject);
        }

        public override void OnHitDynamic(GameObject obj, Collision context)
        {
            obj.TryGetComponent<Timeline.Timeline>(out var timeline);

            if (!timeline.IsReversed)
            {
                var energy = Instantiate(energySpender);
                energy.OnDestroy += () =>
                {
                    timeline.StopReverse();
                    timeline.StartTime();
                };
                energySystem.AddSpender(energy);

                timeline.StartReverse();
            }
            Destroy(gameObject);
        }

        public override void OnHitStatic(GameObject obj, Collision context)
        {
            Destroy(gameObject);
        }
    }
}

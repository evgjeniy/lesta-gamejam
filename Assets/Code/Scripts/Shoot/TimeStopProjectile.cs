using Code.Scripts.Energy;
using UnityEngine;

namespace Code.Scripts.Shoot
{
    public class TimeStopProjectile : Projectile
    {
        [SerializeField] private EnergySpender energySpender;

        public override void OnHitDestroyable(GameObject obj, Collision context)
        {
            Destroy(gameObject);
        }

        public override void OnHitDynamic(GameObject obj, Collision context)
        {
            obj.TryGetComponent<Timeline.Timeline>(out var timeline);

            if (!timeline.IsStoped)
            {
                var energy = Instantiate(energySpender);
                energy.OnDestroy += () =>
                {
                    timeline.StopReverse();
                    timeline.StartTime();
                };

                energySystem.AddSpender(energy);

                timeline?.StopTime();
            }

            Destroy(gameObject);
        }

        public override void OnHitStatic(GameObject obj, Collision context)
        {
            Destroy(gameObject);
        }
    }
}

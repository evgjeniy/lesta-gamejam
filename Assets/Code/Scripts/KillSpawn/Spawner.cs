using System;
using UnityEngine;

namespace Code.Scripts.KillSpawn
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] Destroyable _ref;

        public event Action<IDestroyable> Spawned;

        public IDestroyable Spawn()
        {
            var obj = Instantiate(_ref);
            obj.transform.position = transform.position;
            obj.Destroyed += (IDestroyable destroyable) =>
            {
                Spawn();
            };
            Spawned?.Invoke(obj);
            return obj;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, 1);
        }
    }
}

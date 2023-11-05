using System;
using UnityEngine;

namespace Code.Scripts.KillSpawn
{
    public class Destroyable : MonoBehaviour, IDestroyable
    {
        public event Action<IDestroyable> Destroyed;
        
        public void KillObj()
        {
            Destroyed?.Invoke(this);
            Destroy(gameObject);
        }
    }
}

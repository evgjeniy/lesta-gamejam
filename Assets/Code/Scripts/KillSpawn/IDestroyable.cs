using System;

namespace Code.Scripts.KillSpawn
{
    public interface IDestroyable
    {
        public event Action<IDestroyable> Destroyed;
        public void KillObj();
    }
}
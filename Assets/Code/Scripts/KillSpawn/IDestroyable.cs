using System;
using UnityEngine;

public interface IDestroyable
{
    public event Action<IDestroyable> Destroyed;
    public void KillObj();
}

public class DestroyablePlayer : MonoBehaviour, IDestroyable
{
    public event Action<IDestroyable> Destroyed;
    public void KillObj()
    {
        Destroyed?.Invoke(this);
        Destroy(gameObject);
    }
}
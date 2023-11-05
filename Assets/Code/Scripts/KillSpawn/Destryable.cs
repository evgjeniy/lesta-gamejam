using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable : MonoBehaviour, IDestroyable
{
    public event Action<IDestroyable> Destroyed;
    public void KillObj()
    {
        Destroyed?.Invoke(this);
        Destroy(gameObject);
    }
}

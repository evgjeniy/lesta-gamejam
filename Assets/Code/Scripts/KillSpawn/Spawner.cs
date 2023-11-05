using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

}

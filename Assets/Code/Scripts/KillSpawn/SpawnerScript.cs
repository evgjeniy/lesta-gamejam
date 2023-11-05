using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    [SerializeField] IDestroyable _ref;
    [SerializeField] Code.Scripts.PlayerController _charContr;
    public event Action<Code.Scripts.PlayerController> Spawned;

    public Code.Scripts.PlayerController Spawn()
    {
        var character = Instantiate(_charContr);
        character.transform.position = transform.position;
        Spawned?.Invoke(character);
        return character;
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levelend : MonoBehaviour
{
    public event Action LevelCompleted;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 3)
        {
            LevelCompleted?.Invoke();
        }
    }
}

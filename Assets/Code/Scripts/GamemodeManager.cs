using Code.Scripts.EnergySystem;
using Code.Scripts.KillSpawn;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-101)]
public class GamemodeManager : MonoBehaviour
{
    [SerializeField] Spawner playerSpawner;
    [SerializeField] List<Spawner> spawners;
    [SerializeField] EnergyUI energyUI;

    private void OnEnable()
    {
        playerSpawner.Spawned += BindPlayer; 
        playerSpawner.Spawn();
        foreach (Spawner spawner in spawners) { spawner.Spawn(); }
    }

    private void BindPlayer(IDestroyable destroyable)
    {
        var d = ((Destroyable)destroyable);
        var s = d.GetComponent<EnergySystem>();
        energyUI.BindPlayer(s);
    }
}

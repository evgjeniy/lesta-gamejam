using Code.Scripts.Energy;
using Code.Scripts.KillSpawn;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-101)]
public class GamemodeManager : MonoBehaviour
{
    [SerializeField] Spawner playerSpawner;
    [SerializeField] List<Spawner> spawners;
    [SerializeField] EnergyUI energyUI;
    [SerializeField] Levelend levelend;
    [SerializeField] SceneSwitcher nextlevel;

    private void OnEnable()
    {
        levelend.LevelCompleted += CompleteLevel;

        playerSpawner.Spawned += BindPlayer; 
        playerSpawner.Spawn();
        foreach (Spawner spawner in spawners) { spawner.Spawn(); }
    }

    public void CompleteLevel()
    {
        nextlevel.LoadNextLevel();
    }

    private void BindPlayer(IDestroyable destroyable)
    {
        var d = ((Destroyable)destroyable);
        var s = d.GetComponent<EnergySystem>();
        energyUI.BindPlayer(s);
    }
}

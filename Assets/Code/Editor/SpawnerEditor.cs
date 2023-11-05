using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Spawner))]
public class SpawnerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Spawner spawner = (Spawner)target;

        DrawDefaultInspector();

        if (GUILayout.Button("Spawn"))
            spawner.Spawn();
    }
}

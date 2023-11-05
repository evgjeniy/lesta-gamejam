using Code.Scripts.KillSpawn;
using UnityEditor;
using UnityEngine;

namespace Code.Editor
{
    [CustomEditor(typeof(Spawner))]
    public class SpawnerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            Spawner spawner = (Spawner)target;

            DrawDefaultInspector();

            if (GUILayout.Button("Spawn"))
                spawner.Spawn();
        }
    }
}

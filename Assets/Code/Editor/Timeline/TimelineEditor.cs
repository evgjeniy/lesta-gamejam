using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TransformTimeline))]
public class TimelineEditor : Editor
{
    public override void OnInspectorGUI()
    {
        TransformTimeline timeline = (TransformTimeline)target;

        DrawDefaultInspector();

        EditorGUILayout.Space(10);
        GUIStyle headerStyle = new GUIStyle(GUI.skin.label);
        headerStyle.fontStyle = FontStyle.Bold;
        EditorGUILayout.LabelField("My Custom Header", headerStyle);

        if (!timeline.IsStoped)
        {
            if (GUILayout.Button("Stop Time"))
                timeline.StopTime();
        }
        else
        {
            if (GUILayout.Button("Start Time"))
            {
                timeline.StartTime();
                if(timeline.IsReversed)
                {
                    timeline.StopReverse();
                }
            }
        }

        if (!timeline.IsReversed)
        {
            if (GUILayout.Button("Reverse"))
                timeline.StartReverse();

        }
        else 
            if (GUILayout.Button("Stop Reverse"))
                timeline.StopReverse();


    }
}

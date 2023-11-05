using Code.Scripts.Timeline;
using UnityEditor;
using UnityEngine;

namespace Code.Editor.Timeline
{
    [CustomEditor(typeof(TransformTimeline))]
    public class TimelineEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            var timeline = (TransformTimeline)target;

            DrawDefaultInspector();

            EditorGUILayout.Space(10);
            var headerStyle = new GUIStyle(GUI.skin.label) { fontStyle = FontStyle.Bold };
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
            else if (GUILayout.Button("Stop Reverse"))
            {
                timeline.StopReverse();
            }
        }
    }
}

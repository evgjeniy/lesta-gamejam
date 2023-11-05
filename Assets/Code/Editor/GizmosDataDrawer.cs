using UnityEditor;
using UnityEngine;
using Code.Scripts.Util;

namespace Code.Editor
{
    [CustomPropertyDrawer(typeof(GizmosData))]
    public class GizmosDataDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            
            EditorGUI.PrefixLabel(position, label);
            
            var kWidth = new[] { 0.1f, 0.01f, 0.26f, 0.01f, 0.10f, 0.01f, 0.50f };
            var inputWidth = EditorGUIUtility.currentViewWidth - EditorGUIUtility.labelWidth;

            var labelWidth = position.x + EditorGUIUtility.labelWidth;
            var isOnToggleLabel = new Rect(labelWidth, position.y, inputWidth * kWidth[0], position.height);

            labelWidth += isOnToggleLabel.width + inputWidth * kWidth[1];
            var isOnToggleRect = new Rect(labelWidth, position.y, inputWidth * kWidth[2], position.height);

            labelWidth += isOnToggleRect.width + inputWidth * kWidth[3];
            var colorLabel = new Rect(labelWidth, position.y, inputWidth * kWidth[4], position.height);

            labelWidth += colorLabel.width + inputWidth * kWidth[5];
            var colorRect = new Rect(labelWidth, position.y, inputWidth * kWidth[6], position.height);

            EditorGUI.LabelField(isOnToggleLabel, "Type");
            EditorGUI.PropertyField(isOnToggleRect, property.FindPropertyRelative($"{nameof(GizmosData.drawType)}"), GUIContent.none);
            EditorGUI.LabelField(colorLabel, "Color");
            EditorGUI.PropertyField(colorRect, property.FindPropertyRelative($"{nameof(GizmosData.gizmosColor)}"), GUIContent.none);

            EditorGUI.EndProperty();
        }
    }
}
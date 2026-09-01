using UnityEditor;
using UnityEngine;

namespace Chunity.Data
{
    [CustomPropertyDrawer(typeof(VariableParams))]
    public class VariableParamsDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var height = property.FindPropertyRelative("_params");
            return EditorGUI.GetPropertyHeight(height, true);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            EditorGUI.LabelField(position, label.text);
            SerializedProperty paramsProperty = property.FindPropertyRelative("_params");
            EditorGUI.PropertyField(position, paramsProperty, label, true);
            EditorGUI.EndProperty();
        }
    }
}


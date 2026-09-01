using UnityEditor;
using UnityEngine;

namespace Chunity.Data
{
    [CustomPropertyDrawer(typeof(VariableParam))]
    public class VariableParamDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            const float spacing = 5f;

            float typeWidth = position.width * 0.37f;
            float valueWidth = position.width * 0.57f;

            Rect typeRect = new(position.x, position.y, typeWidth, position.height);
            Rect valueRect = new(position.x + typeWidth + spacing, position.y, valueWidth, position.height);

            SerializedProperty type = property.FindPropertyRelative("Type");

            EditorGUI.BeginProperty(position, label, property);
            EditorGUI.PropertyField(typeRect, type, GUIContent.none);

            switch ((ParameterType)type.enumValueIndex)
            {
                case ParameterType.Int:
                    property.FindPropertyRelative("IntValue").intValue =
                        EditorGUI.IntField(valueRect, property.FindPropertyRelative("IntValue").intValue);
                    break;
                case ParameterType.Float:
                    property.FindPropertyRelative("FloatValue").floatValue =
                        EditorGUI.FloatField(valueRect, property.FindPropertyRelative("FloatValue").floatValue);
                    break;
                case ParameterType.Bool:
                    property.FindPropertyRelative("BoolValue").boolValue =
                        EditorGUI.Toggle(valueRect, property.FindPropertyRelative("BoolValue").boolValue);
                    break;
                case ParameterType.String:
                    property.FindPropertyRelative("StringValue").stringValue =
                        EditorGUI.TextField(valueRect, property.FindPropertyRelative("StringValue").stringValue);
                    break;
                case ParameterType.Vector2:
                    EditorGUI.PropertyField(valueRect, property.FindPropertyRelative("Vector2Value"), GUIContent.none);
                    break;
                case ParameterType.Vector3:
                    EditorGUI.PropertyField(valueRect, property.FindPropertyRelative("Vector3Value"), GUIContent.none);
                    break;
                case ParameterType.Script:
                    SerializedProperty script = property.FindPropertyRelative("Script");
                    script.objectReferenceValue =
                        EditorGUI.ObjectField(valueRect, script.objectReferenceValue, typeof(MonoBehaviour), true);
                    break;
            }

            EditorGUI.EndProperty();
        }
    }
}
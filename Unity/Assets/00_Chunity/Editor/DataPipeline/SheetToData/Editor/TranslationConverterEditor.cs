using UnityEditor;
using UnityEngine;

namespace Chunity.DataPipeline
{
    [CustomEditor(typeof(TranslationConverter))]
    public class TranslationConverterEditor : Editor
    {
        private TranslationConverter _converter;
        private SerializedProperty _googleLoader;
        private SerializedProperty _config;

        private void OnEnable()
        {
            _converter = (TranslationConverter)target;
            _googleLoader = serializedObject.FindProperty("GoogleLoader");
            _config = serializedObject.FindProperty("Config");
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(_googleLoader, new GUIContent("Google Sheets"), true);
            EditorGUILayout.Space();
            DrawGenerateLocalization();
            EditorGUILayout.Space();
            DrawConfig();

            serializedObject.ApplyModifiedProperties();
        }

        private void DrawGenerateLocalization()
        {
            GUILayout.Space(5);
            if (GUILayout.Button("Export DB To Json"))
            {
                _converter.ExportDbToJson();
                AssetDatabase.Refresh();
            }
        }

        private void DrawConfig()
        {
            EditorGUILayout.PropertyField(_config, new GUIContent("Config"), true);
        }
    }
}
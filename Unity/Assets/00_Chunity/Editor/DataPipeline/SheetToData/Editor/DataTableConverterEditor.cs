using UnityEditor;
using UnityEngine;

namespace Chunity.DataPipeline
{
    [CustomEditor(typeof(DataTableConverter))]
    public class DataTableConverterEditor : Editor
    {
        private DataTableConverter _converter;
        private SerializedProperty _googleLoader;
        private SerializedProperty _convertSetting;
        private SerializedProperty _tableByName;

        private void OnEnable()
        {
            _googleLoader = serializedObject.FindProperty("GoogleLoader");
            _convertSetting = serializedObject.FindProperty("Config");
            _tableByName = serializedObject.FindProperty("TableByName");


            _converter = (DataTableConverter)target;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(_googleLoader, new GUIContent("Google Sheets"), true);
            GUILayout.Space(10);
            if (GUILayout.Button("Download & Parse"))
                LoadGoogleSheets();
            if (GUILayout.Button("Create DTO Class"))
                GenerateClass();
            if (GUILayout.Button("To Json"))
                TableToJson();

            GUILayout.Space(10);
            EditorGUILayout.PropertyField(_tableByName, new GUIContent("Sheets"), true);
            EditorGUILayout.PropertyField(_convertSetting, new GUIContent("Config"), true);

            if (GUI.changed)
                EditorUtility.SetDirty(_converter);

            serializedObject.ApplyModifiedProperties();
        }

        public async void LoadGoogleSheets()
        {
            await _converter.LoadGoogleSheets();
            Repaint();
        }

        public async void GenerateClass()
        {
            await _converter.GenerateClass();
            AssetDatabase.Refresh();
        }

        public async void TableToJson()
        {
            await _converter.TableToJson();
            AssetDatabase.Refresh();
        }
    }
}

using UnityEditor;
using UnityEngine;

namespace Chunity.DataPipeline
{
    [CustomPropertyDrawer(typeof(GoogleSheetsLoader))]
    public class GoogleSheetsLoaderDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var loader = fieldInfo.GetValue(property.serializedObject.targetObject) as GoogleSheetsLoader;
            float line = EditorGUIUtility.singleLineHeight;

            Rect titleRect = new(position.x, position.y, position.width, line);
            Rect idRect = new(position.x + 10, position.y + line + 2, position.width, line);
            Rect exportRect = new(position.x + 10, position.y + (line + 2) * 2, position.width, line);
            Rect buttonRect = new(position.x + 10, position.y + (line + 2) * 3, 120, line);
            Rect stateRect = new(position.x + 140, position.y + (line + 2) * 3, position.width - 130, line);

            EditorGUI.BeginProperty(position, label, property);

            EditorGUI.LabelField(titleRect, "GoogleSheetsLoader");

            loader.GoogleSheetID = EditorGUI.TextField(idRect, "Sheet ID", loader.GoogleSheetID);
            loader.Export = (DefaultAsset)EditorGUI.ObjectField(exportRect, "Export", loader.Export, typeof(DefaultAsset), false);

            EditorGUI.BeginDisabledGroup(loader.IsLoading);
            if (GUI.Button(buttonRect, loader.IsLoading ? "Downloading..." : "Download"))
                _ = loader.RequestExcelFile();

            EditorGUI.EndDisabledGroup();
            EditorGUI.LabelField(stateRect, string.IsNullOrEmpty(loader.LastSync) == true ? "No Data" : $"Last Sync : {loader.LastSync}");

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight * 4 + 5;
        }
    }
}
using UnityEditor;
using UnityEngine;

namespace Whatever.GameSystems
{
    [CustomEditor(typeof(WorldProfile))]
    public class WorldProfileEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var profile = (WorldProfile)target;

            EditorGUILayout.Space();

            if (GUILayout.Button("Update Faction Table"))
            {
                profile.FactionTable.UpdateFaction();

                EditorUtility.SetDirty(profile);
                AssetDatabase.SaveAssets();
            }
        }
    }
}

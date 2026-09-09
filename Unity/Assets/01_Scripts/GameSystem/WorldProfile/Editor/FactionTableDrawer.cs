using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Whatever.GameSystems
{
    [CustomPropertyDrawer(typeof(FactionTable))]
    public class FactionTableDrawer : PropertyDrawer
    {
        private Dictionary<string, int> _selectedFactionIndex = new();

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var table = fieldInfo.GetValue(property.serializedObject.targetObject) as FactionTable;
            if (table == null)
                return;

            position.height = EditorGUIUtility.singleLineHeight;

            property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, label, true);

            if (!property.isExpanded)
                return;

            EditorGUI.indentLevel++;

            var values = (FactionType[])Enum.GetValues(typeof(FactionType));
            var names = Enum.GetNames(typeof(FactionType));

            if (!_selectedFactionIndex.TryGetValue(property.propertyPath, out int selected))
                selected = 0;

            position.y += EditorGUIUtility.singleLineHeight + 2;

            selected = EditorGUI.Popup(position, "Faction", selected, names);
            _selectedFactionIndex[property.propertyPath] = selected;

            var from = values[selected];

            position.y += EditorGUIUtility.singleLineHeight + 4;

            EditorGUI.LabelField(position, $"{from} Relations", EditorStyles.boldLabel);

            foreach (var to in values)
            {
                position.y += EditorGUIUtility.singleLineHeight + 2;

                const float leftWidth = 120f;

                EditorGUI.LabelField(
                    new Rect(position.x + 15, position.y, leftWidth, EditorGUIUtility.singleLineHeight),
                    to.ToString());

                var oldValue = table[from][(int)to];

                var newValue = (FactionRelation)EditorGUI.EnumPopup(
                    new Rect(position.x + 15 + leftWidth, position.y,
                        position.width - leftWidth - 15,
                        EditorGUIUtility.singleLineHeight),
                    oldValue);

                if (newValue != oldValue)
                {
                    Undo.RecordObject(property.serializedObject.targetObject, "Change Faction Relation");

                    table.SetRelation(from, to, newValue);

                    EditorUtility.SetDirty(property.serializedObject.targetObject);
                }
            }

            EditorGUI.indentLevel--;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (!property.isExpanded)
                return EditorGUIUtility.singleLineHeight;

            int count = Enum.GetValues(typeof(FactionType)).Length;

            // Foldout + Popup + Header + 각 Relation
            return (count + 3) * (EditorGUIUtility.singleLineHeight + 2);
        }
    }
}

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Framework.Editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(UnityEngine.Object), true)]
    public class CustomInspectorEditor : UnityEditor.Editor
    {
        private List<SerializedProperty> _serializedProperties;

        private void OnEnable()
        {
            _serializedProperties = GetSerializedProperties();
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            foreach (var property in _serializedProperties)
            {
                if (property.name.Equals("m_Script", System.StringComparison.Ordinal))
                {
                    GUI.enabled = false;
                    EditorGUILayout.PropertyField(property);
                    GUI.enabled = true;
                }
                else
                {
                    if (property.isArray && property.propertyType != SerializedPropertyType.String)
                    {
                        ReorderableListPropertyDrawer.Draw(property);
                    }
                    else
                    {
                        EditorGUILayout.PropertyField(property, true);
                    }
                }
            }

            serializedObject.ApplyModifiedProperties();
        }

        private List<SerializedProperty> GetSerializedProperties()
        {
            var serializedProperties = new List<SerializedProperty>();

            using (var iterator = serializedObject.GetIterator())
            {
                if (iterator.NextVisible(true))
                {
                    do
                    {
                        serializedProperties.Add(serializedObject.FindProperty(iterator.name));
                    } while (iterator.NextVisible(false));
                }
            }

            return serializedProperties;
        }

        private void OnDisable()
        {
            foreach (var property in _serializedProperties)
            {
                if (property.isArray && property.propertyType != SerializedPropertyType.String)
                {
                    ReorderableListPropertyDrawer.Dispose(property);
                }
            }
        }
    }
}
// Dependancies : 
using UnityEngine;
using UnityEditor;
using AbilitySystem.Status;

namespace AbilitySystem.Editor
{
    [CustomEditor(typeof(StatusEffectMixingDatabase))]
    public class StatusEffectMixingDatabaseEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            // Draw the default inspector first
            DrawDefaultInspector();

            // Add some spacing
            GUILayout.Space(10);

            // Reference to the target database
            StatusEffectMixingDatabase db = (StatusEffectMixingDatabase)target;

            // Create the button
            if (GUILayout.Button("Generate Component Lists"))
            {
                db.GenerateAllComponents();

                // Mark the asset as dirty so Unity saves the changes
                EditorUtility.SetDirty(db);
                AssetDatabase.SaveAssets();

                Debug.Log("Component lists regenerated.");
            }
        }
    }
}

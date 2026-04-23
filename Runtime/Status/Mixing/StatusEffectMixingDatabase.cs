using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// Static Functions ( Singleton Data )

namespace AbilitySystem
{
    [System.Serializable]
    public class MixRule
    {
        public StatusData effectA;
        public StatusData effectB;
        public StatusData result;

        public void GenerateComponents()
        {
            HashSet<StatusData> set = new HashSet<StatusData>();

            // Add A and its components
            set.Add(effectA);
            foreach (var c in effectA.components)
                set.Add(c);

            // Add B and its components
            set.Add(effectB);
            foreach (var c in effectB.components)
                set.Add(c);

            // Assign flattened list to result
            result.components = new List<StatusData>(set);
        }
    }

    [CreateAssetMenu(menuName = "Abilities/Status/Mixing Database", order = 1)]
    public class StatusEffectMixingDatabase : ScriptableObject
    {
        public List<MixRule> mixRules;

        public StatusData TryMix(StatusData a, StatusData b)
        {
            foreach (var rule in mixRules)
            {
                if ((rule.effectA == a && rule.effectB == b) ||
                    (rule.effectA == b && rule.effectB == a))
                    return rule.result;
            }
            return null;
        }

        public void GenerateAllComponents()
        {
            foreach (var rule in mixRules)
            {
                rule.GenerateComponents();
            }
        }

        #if UNITY_EDITOR
        [ContextMenu("Generate Component Lists")]
        private void GenerateComponentLists()
        {
            GenerateAllComponents();
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
        }
        #endif


    }
}

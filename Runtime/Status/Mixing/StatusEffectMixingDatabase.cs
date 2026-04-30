
// Dependancies : 
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace AbilitySystem
{
    /// <summary>
    /// Defines a mixing rule between two status effects. If <b>effectA</b> and <b>effectB</b> are present,
    /// they combine into <b>result</b>. Also generates flattened component lists for the resulting effect. <br/><br/>
    /// Contains : <br/>
    /// <b>- effectA :</b> The first effect in the mix pair, <br/>
    /// <b>- effectB :</b> The second effect in the mix pair, <br/>
    /// <b>- result :</b> The resulting combined StatusData. <br/>
    /// </summary>
    [System.Serializable]
    public class MixRule
    {
        // Data Members :
        public StatusData effectA;
        public StatusData effectB;
        public StatusData result;

        #region Data Methods

        /// <summary>
        /// Generates a flattened component list for the resulting effect by combining
        /// the components of <b>effectA</b>, <b>effectB</b>, and their own components. <br/><br/>
        /// </summary>
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

        #endregion Data Methods
    }

    /// <summary>
    /// Database asset containing all <b>MixRule</b> definitions for the status system. <br/>
    /// Used by <b>StatusEffectMixer</b> to determine how effects combine. <br/><br/>
    /// Contains : <br/>
    /// <b>- mixRules :</b> A list of all defined effect mixing rules. <br/><br/>
    /// Responsible for : <br/>
    /// <b>- Mixing Logic :</b> Provides lookup functionality for determining combined effects, <br/>
    /// <b>- Component Generation :</b> Builds component lists for all mix results. <br/>
    /// </summary>
    [CreateAssetMenu(menuName = "Abilities/Status/Mixing Database", order = 1)]
    public class StatusEffectMixingDatabase : ScriptableObject
    {
        // Data Members :
        public List<MixRule> mixRules;

        #region Data Methods

        /// <summary>
        /// Attempts to mix two status effects using the defined mix rules. <br/><br/>
        /// </summary>
        /// <param name="a">The first effect.</param>
        /// <param name="b">The second effect.</param>
        /// <returns>The resulting StatusData if a rule matches, otherwise null.</returns>
        public StatusData TryMix(StatusData a, StatusData b)
        {
            foreach (MixRule currentRule in mixRules)
            {
                if ((currentRule.effectA == a && currentRule.effectB == b) ||
                    (currentRule.effectA == b && currentRule.effectB == a))
                    return currentRule.result;
            }
            return null;
        }

        /// <summary>
        /// Generates component lists for all mix rules in the database. <br/><br/>
        /// </summary>
        public void GenerateAllComponents()
        {
            foreach (MixRule currentRule in mixRules)
            {
                currentRule.GenerateComponents();
            }
        }

        #endregion Data Methods

        #region Unity Methods

        #if UNITY_EDITOR
        /// <summary>
        /// Editor-only context menu action for regenerating all component lists. <br/>
        /// Useful when modifying mix rules in the inspector. 
        /// </summary>
        [ContextMenu("Generate Component Lists")]
        private void GenerateComponentLists()
        {
            GenerateAllComponents();
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
        }
        #endif

        #endregion Unity Methods
    }
}

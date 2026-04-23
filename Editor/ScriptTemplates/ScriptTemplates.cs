using UnityEditor;

namespace AbilitySystem.Editor 
{
    public class ScriptTemplates 
    {
        [MenuItem("Assets/Create/Abilities/Effects/Create New Effect", priority = 0)]
        public static void CreateEffectScript()
        {
            ProjectWindowUtil.CreateScriptAssetFromTemplateFile(
                "Packages/AbilitySystem/Editor/ScriptTemplates/Effect Script Template.txt",
                "New Effect.cs"
            );
        }

        [MenuItem("Assets/Create/Abilities/Status/Create New Status", priority = 0)]
        public static void CreatStatusScript()
        {
            ProjectWindowUtil.CreateScriptAssetFromTemplateFile(
                "Packages/AbilitySystem/Editor/ScriptTemplates/Status Script Template.txt",
                "New Status.cs"
            );
        }

    }
}

using UnityEditor;

namespace AbilitySystem.Editor 
{
public class ScriptTemplates 
    {
        [MenuItem("Assets/Create/Abilities/Scripts/Effect Script", priority = 2)]
        public static void CreateEffectScript()
        {
            ProjectWindowUtil.CreateScriptAssetFromTemplateFile(
                "Packages/AbilitySystem/Edtor/ScriptTemplates/Effect Script Template.txt",
                "New Effect.cs"
            );
        }
    }
}

using DialogueSystem;
using Editor.AutoItems;
using UnityEditor;

namespace Editor
{
    public static class RuntimeAssetsReloader
    {
        private static readonly string[] DialoguesFolderPaths = { $"Assets/Resources/Dialogues" };
        
        [InitializeOnEnterPlayMode]
        [MenuItem("КефирMiddle/Reload Runtime Assets")]
        public static void ReloadRuntimeAssets()
        {
            RuntimeAssetsScanner<Dialogues, Dialogue>.UpdateAssetsReferences(DialoguesFolderPaths);
        }
    }
}
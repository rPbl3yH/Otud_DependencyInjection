using UnityEditor;
using UnityEngine.UIElements;

namespace Modules.Dialogues
{
    public static class DialogueHelper
    {
        public static StyleSheet LoadNodeStyleSheet()
        {
            return AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Modules/Dialogues/Styles/DialogueNode.uss");
        }
        
        public static StyleSheet LoadGridStyleSheet()
        {
            return AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Modules/Dialogues/Styles/DialogueGrid.uss");
        }
    }
}
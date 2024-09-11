using UnityEditor;

namespace Modules.Dialogues
{
    public static class DialogueMenu
    {
        [MenuItem("Window/Dialogues")]
        public static void ShowWindow()
        {
            EditorWindow.GetWindow<DialogueWindow>("Dialogue");
        }
    }
}
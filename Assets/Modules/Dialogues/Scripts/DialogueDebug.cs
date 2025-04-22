using Sirenix.OdinInspector;
using UnityEngine;

namespace Modules.Dialogues
{
    public class DialogueDebug : MonoBehaviour
    {
        public DialogueConfig DialogueConfig;
        private Dialogue _dialogue;

        private void Awake()
        {
            _dialogue = new Dialogue(DialogueConfig);
        }

        [Button]
        public void ShowDialogue(int choiceIndex)
        {
            _dialogue.MoveNext(choiceIndex);
            
            Debug.Log("--------Next message----------");
            Debug.Log($"{_dialogue.CurrentMessage}");

            for (var index = 0; index < _dialogue.CurrentChoices.Length; index++)
            {
                string choice = _dialogue.CurrentChoices[index];
                Debug.Log($"Answer {index + 1} = {choice}");
            }
        }
    }
}
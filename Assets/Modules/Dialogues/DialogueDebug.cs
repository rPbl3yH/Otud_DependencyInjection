using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Modules.Dialogues
{
    public class DialogueDebug : MonoBehaviour
    {
        [SerializeField] private DialogueConfig _dialogueConfig;

        private Dialogue _dialogue;
        
        private void Start()
        {
            _dialogue = new Dialogue(_dialogueConfig);
            Debug.Log($"Message = {_dialogue.CurrentMessage}");
        }

        [Button]
        private void MoveNext(int answerIndex)
        {
            _dialogue.MoveNext(answerIndex);
            Debug.Log($"Message = {_dialogue.CurrentMessage}");
        }
    }
}
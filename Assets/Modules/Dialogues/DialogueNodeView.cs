using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Modules.Dialogues
{
    public sealed class DialogueNodeView : Node
    {
        private readonly string _id;
        private readonly List<DialogueChoiceView> _choices;
        
        private TextField _textMessage;
        private Port _inputPort;
        private bool _isRoot;
        
        public DialogueNodeView(string id)
        {
            _id = id;
            title = "Dialogue Node";
            _choices = new List<DialogueChoiceView>();
            
            CreateTextMessage();
            CreatePortInput();
            CreateButtonAddChoice();
            ApplyStyles();
            RefreshExpandedState();
        }
        
        public string GetId()
        {
            return _id;
        }

        public string GetMessage()
        {
            return _textMessage.value;
        }

        public void SetMessage(string message)
        {
            _textMessage.value = message;
        }

        public DialogueChoiceView[] GetChoices()
        {
            return _choices.ToArray();
        }
        
        public void CreateChoice(string answer)
        {
            DialogueChoiceView choice = new DialogueChoiceView(answer);
            choice.OnDelete += DeleteChoice;
            _choices.Add(choice);
            outputContainer.Add(choice);
            RefreshExpandedState();
        }

        public void DeleteChoice(DialogueChoiceView choice)
        {
            choice.OnDelete -= DeleteChoice;
            _choices.Remove(choice);
            outputContainer.Remove(choice);
            RefreshExpandedState();
        }

        private void ApplyStyles()
        {
            mainContainer.AddToClassList("dialogue_node_main-container");
        }

        private void CreateButtonAddChoice()
        {
            Button button = new Button
            {
                text = "Add Choice",
                clickable = new Clickable(OnCreateChoiceClicked)
            };
            
            button.AddToClassList("dialogue-node-add-choice-button");
            mainContainer.Add(button);
        }

        private void OnCreateChoiceClicked()
        {
           CreateChoice("Enter Answer...");
        }
        
       
        private void CreatePortInput()
        {
            _inputPort = Port.Create<DialogueEdgeView>(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, null);
            _inputPort.portName = "Input";
            inputContainer.Add(_inputPort);
        }

        private void CreateTextMessage()
        {
            _textMessage = new TextField
            {
                value = "Enter Message",
                multiline = true
            };
            
            _textMessage.AddToClassList("dialogue-node-text-message");
            extensionContainer.Add(_textMessage);
        }

        public int IndexOfOutputPort(Port port)
        {
            for (var i = 0; i < _choices.Count; i++)
            {
                var choice = _choices[i];
                if (choice.IsPort(port))
                {
                    return i;
                }
            }

            throw new Exception("Index of port is not found!");
        }

        public Port GetOutputPort(int index)
        {
            return _choices[index].GetPort();
        }

        public Port GetInputPort()
        {
            return _inputPort;
        }

        public void SetRoot(bool isRoot)
        {
            _isRoot = isRoot;
            style.backgroundColor = isRoot 
                ? new Color(0.92f, 0.76f, 0f) 
                : new Color(0.53f, 0.53f, 0.56f);
        }

        public bool IsRoot()
        {
            return _isRoot;
        }
    }
}
using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Modules.Dialogues
{
    public sealed class DialogueNodeView : Node
    {
        private readonly string id;
        private readonly List<DialogueChoiceView> choices;
        
        private TextField textMessage;
        private Port inputPort;
        private bool isRoot;
        
        public DialogueNodeView(string id)
        {
            this.id = id;
            this.title = "Dialogue Node";
            this.choices = new List<DialogueChoiceView>();
            
            this.CreateTextMessage();
            this.CreatePortInput();
            this.CreateButtonAddChoice();
            this.ApplyStyles();
            this.RefreshExpandedState();
        }
        
        public string GetId()
        {
            return this.id;
        }

        public string GetMessage()
        {
            return this.textMessage.value;
        }

        public void SetMessage(string message)
        {
            this.textMessage.value = message;
        }

        public DialogueChoiceView[] GetChoices()
        {
            return this.choices.ToArray();
        }
        
        public void CreateChoice(string answer)
        {
            DialogueChoiceView choice = new DialogueChoiceView(answer);
            choice.OnDelete += this.DeleteChoice;
            this.choices.Add(choice);
            this.outputContainer.Add(choice);
            this.RefreshExpandedState();
        }
        
        public void DeleteChoice(DialogueChoiceView choice)
        {
            choice.OnDelete -= this.DeleteChoice;
            this.choices.Remove(choice);
            this.outputContainer.Remove(choice);
            this.RefreshExpandedState();
        }

        private void ApplyStyles()
        {
            this.mainContainer.AddToClassList("dialogue_node_main-container");
        }

        private void CreateButtonAddChoice()
        {
            Button button = new Button
            {
                text = "Add Choice",
                clickable = new Clickable(this.OnCreateChoiceClicked)
            };
            
            button.AddToClassList("dialogue-node-add-choice-button");
            this.mainContainer.Add(button);
        }

        private void OnCreateChoiceClicked()
        {
           this.CreateChoice("Enter Answer...");
        }
        
        private void CreatePortInput()
        {
            //TODO:
            this.inputPort = Port.Create<DialogueEdgeView>(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, null);
            this.inputPort.portName = "Input";
            this.inputContainer.Add(this.inputPort);
        }

        private void CreateTextMessage()
        {
            this.textMessage = new TextField
            {
                value = "Enter Message",
                multiline = true
            };
            
            this.textMessage.AddToClassList("dialogue-node-text-message");
            this.extensionContainer.Add(this.textMessage);
        }

        public int IndexOfOutputPort(Port port)
        {
            for (var i = 0; i < this.choices.Count; i++)
            {
                var choice = this.choices[i];
                if (choice.IsPort(port))
                {
                    return i;
                }
            }
        
            throw new Exception("Index of port is not found!");
        }
        
        public Port GetOutputPort(int index)
        {
            return this.choices[index].GetPort();
        }
        
        public Port GetInputPort()
        {
            return this.inputPort;
        }

        public void SetRoot(bool isRoot)
        {
            this.isRoot = isRoot;
            this.style.backgroundColor = isRoot 
                ? new Color(0.92f, 0.76f, 0f) 
                : new Color(0.53f, 0.53f, 0.56f);
        }

        public bool IsRoot()
        {
            return this.isRoot;
        }
    }
}
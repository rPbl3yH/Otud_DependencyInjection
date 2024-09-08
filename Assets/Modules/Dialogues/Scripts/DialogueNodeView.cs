using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Modules.Dialogues
{
    public class DialogueNodeView : Node
    {
        private readonly List<DialogueChoiceView> _choices = new();

        private TextField _textMessage;

        public DialogueNodeView()
        {
            CreateTitle();
            CreateInputPort();
            CreateChoices();
            // CreateMessage();
            CreateTextMessage();
            RefreshExpandedState();
        }

        private void CreateInputPort()
        {
            var inputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, null);
            inputPort.portName = "Input";
            inputContainer.Add(inputPort);
        }

        private void CreateChoices()
        {
            CreateButtonAddChoice();
        }

        private void CreateButtonAddChoice()
        {
            Button button = new Button
            {
                text = "Add Choice",
                clickable = new Clickable(OnCreateChoiceClicked)
            };

            button.AddToClassList("dialogue-node-add-choice-button");
            mainContainer.Insert(1, button);
        }

        private void OnCreateChoiceClicked()
        {
            Debug.Log("Add choice clicked");
            CreateChoice("Answer");
        }

        private void CreateChoice(string answer)
        {
            DialogueChoiceView choice = new DialogueChoiceView(answer);
            choice.OnDelete += DeleteChoice;
            _choices.Add(choice);
            outputContainer.Add(choice);
            RefreshExpandedState();
        }

        private void DeleteChoice(DialogueChoiceView choice)
        {
            choice.OnDelete -= DeleteChoice;
            _choices.Remove(choice);
            outputContainer.Remove(choice);
            RefreshExpandedState();
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

        private void CreateTitle()
        {
            TextField textField = new TextField()
            {
                value = "Dialogue node",
                multiline = false,
            };

            titleContainer.Insert(0, textField);
        }
    }
}
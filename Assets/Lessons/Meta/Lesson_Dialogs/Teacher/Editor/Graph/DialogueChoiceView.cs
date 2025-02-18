// using System;
// using UnityEditor.Experimental.GraphView;
// using UnityEngine;
// using UnityEngine.UIElements;
//
// namespace Modules.Dialogues
// {
//     public sealed class DialogueChoiceView : VisualElement
//     {
//         public event Action<DialogueChoiceView> OnDelete;
//
//         private TextField textAnswer;
//         private Port port;
//
//         public DialogueChoiceView(string answer)
//         {
//             this.CreateButtonDelete();
//             this.CreateTextAnswer(answer);
//             this.CreatePortOutput();
//             this.style.flexDirection = FlexDirection.Row;
//         }
//
//         private void CreateButtonDelete()
//         {
//             Button button = new Button
//             {
//                 text = "X",
//                 clickable = new Clickable(this.OnDeleteClicked)
//             };
//             button.AddToClassList("dialogue-node-remove-choice-button");
//             this.Add(button);
//         }
//
//         private void CreateTextAnswer(string answer)
//         {
//             this.textAnswer = new TextField
//             {
//                 value = answer,
//                 multiline = false,
//                 style =
//                 {
//                     width = 128
//                 }
//             };
//             this.Add(this.textAnswer);
//         }
//
//         public Port GetPort()
//         {
//             return this.port;
//         }
//
//         public bool IsPort(Port port)
//         {
//             return this.port == port;
//         }
//
//         private void CreatePortOutput()
//         {
//             this.port = Port.Create<DialogueEdgeView>(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, null);
//             this.port.portColor = Color.yellow;
//             this.Add(port);
//         }
//
//         private void OnDeleteClicked()
//         {
//             this.OnDelete?.Invoke(this);
//         }
//
//         public string GetText()
//         {
//             return this.textAnswer.value;
//         }
//     }
// }
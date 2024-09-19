// using UnityEditor;
// using UnityEngine.UIElements;
//
// namespace Modules.Dialogues
// {
//     public sealed class DialogueWindow : EditorWindow
//     {
//         private DialogueGraphView graphView;
//         
//          private void CreateGUI()
//          {
//              this.CreateGraph();
//              this.CreateToolbar();
//          }
//
//          private void CreateGraph()
//          {
//              this.graphView = new DialogueGraphView();
//              this.graphView.StretchToParentSize();
//              this.rootVisualElement.Insert(0, graphView);
//          }
//          
//          private void CreateToolbar()
//          {
//              DialogueToolbar toolbar = new DialogueToolbar(this.graphView);
//              this.rootVisualElement.Add(toolbar);
//          }
//     }
// }
//
//

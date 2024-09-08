using UnityEditor;
using UnityEngine.UIElements;

namespace Modules.Dialogues
{
    public class DialogueWindow : EditorWindow
    {
        private DialogueGraphView _graphView;
         
         private void CreateGUI()
         {
             this.CreateGraph();
             // this.CreateToolbar();
         }

         private void CreateGraph()
         {
             this._graphView = new DialogueGraphView();
             this._graphView.StretchToParentSize();
             this.rootVisualElement.Insert(0, _graphView);
         }
    }
}

using UnityEditor;
using UnityEngine.UIElements;

namespace Modules.Dialogues
{
    public class DialogueWindow : EditorWindow
    {
        private DialogueGraphView _graphView;

        private void CreateGUI()
        {
            CreateGraph();
            CreateToolbar();
        }

        private void CreateToolbar()
        {
            DialogueToolbar toolbar = new DialogueToolbar(_graphView);
            rootVisualElement.Add(toolbar);
        }

        private void CreateGraph()
        {
            _graphView = new DialogueGraphView();
            _graphView.StretchToParentSize();
            rootVisualElement.Insert(0, _graphView);
        }
    }
}
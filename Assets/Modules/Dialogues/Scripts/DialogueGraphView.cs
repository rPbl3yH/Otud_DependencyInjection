using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace Modules.Dialogues
{
    public class DialogueGraphView : GraphView
    {
        public DialogueGraphView()
        {
            CreateBackground();
            CreateManipulators();

            CreateDialogueNode();
            ApplyStyles();
        }

        private void CreateDialogueNode()
        {
            var node = new DialogueNodeView();
            // Insert(1, node);
            AddElement(node);
        }

        private void ApplyStyles()
        {
            styleSheets.Add(
                AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Modules/Dialogues/Styles/DialogueGrid.uss"));
            styleSheets.Add(
                AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Modules/Dialogues/Styles/DialogueNode.uss"));
        }

        private void CreateBackground()
        {
            GridBackground gridBackground = new GridBackground();
            gridBackground.StretchToParentSize();
            Insert(0, gridBackground);
        }

        private void CreateManipulators()
        {
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new ContentZoomer());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());
            
            this.AddManipulator(new ContextualMenuManipulator(this.OnContextMenuClicked));
        }

        private void OnContextMenuClicked(ContextualMenuPopulateEvent menuEvent)
        {
            menuEvent.menu.AppendAction("Create node", OnCreateNode);
        }

        private void OnCreateNode(DropdownMenuAction menuAction)
        {
            
        }
    }
}
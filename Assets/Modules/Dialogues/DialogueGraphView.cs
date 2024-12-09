using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Modules.Dialogues
{
    public sealed class DialogueGraphView : GraphView
    {
        public DialogueGraphView()
        {
            this.CreateBackground();
            this.CreateManipulators();
            this.ApplyStyles();
        }

        public DialogueNodeView[] GetNodes()
        {
            List<DialogueNodeView> nodes = new List<DialogueNodeView>();
            foreach (Node node in this.nodes)
            {
                nodes.Add((DialogueNodeView) node);
            }
            
            return nodes.ToArray();
        }

        public DialogueEdgeView[] GetEdges()
        {
            List<Edge> edges = this.edges.ToList();
            DialogueEdgeView[] result = new DialogueEdgeView[edges.Count];

            for (int i = 0; i < edges.Count; i++)
            {
                result[i] = (DialogueEdgeView) edges[i];
            }

            return result;
        }
        
        public void ResetState()
        {
            foreach (Node node in this.nodes)
            {
                this.RemoveElement(node);
            }

            foreach (Edge edge in this.edges)
            {
                this.RemoveElement(edge);
            }
        }

        public DialogueNodeView CreateNode(string id, Vector2 position)
        {
            DialogueNodeView node = new DialogueNodeView(id);
            node.SetPosition(new Rect(position, Vector2.zero));
            this.AddElement(node);
            return node;
        }

        public void CreateEdge(DialogueNodeView outputNode, int outputIndex, DialogueNodeView inputNode)
        {
            Port outputPort = outputNode.GetOutputPort(outputIndex);
            Port inputPort = inputNode.GetInputPort();

            DialogueEdgeView edge = new DialogueEdgeView
            {
                input = inputPort,
                output = outputPort
            };

            this.AddElement(edge);
        }

        private void CreateManipulators()
        {
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new ContentZoomer());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());
            this.AddManipulator(new ContextualMenuManipulator(this.OnContextMenuClicked));
        }
        
        private void CreateBackground()
        {
            GridBackground gridBackground = new GridBackground();
            gridBackground.StretchToParentSize();
            this.Insert(0, gridBackground);
        }

        private void ApplyStyles()
        {
            this.styleSheets.Add(DialogueHelper.LoadNodeStyleSheet());
            this.styleSheets.Add(DialogueHelper.LoadGridStyleSheet());
        }

        private void OnContextMenuClicked(ContextualMenuPopulateEvent menuEvent)
        { 
            menuEvent.menu.AppendAction("Create Node", this.OnCreateNode);
            
            if (menuEvent.target is DialogueNodeView selectedNode)
            {
                menuEvent.menu.AppendAction("Set As Root", _ => this.SetRootNode(selectedNode));
            }
        }

        private void OnCreateNode(DropdownMenuAction menuAction)
        {
            Vector2 mousePosition = menuAction.eventInfo.localMousePosition;
            Vector2 worldPosition = this.ChangeCoordinatesTo(this.parent, mousePosition);
            Vector2 fixedLocalPosition = contentViewContainer.WorldToLocal(worldPosition);
            
            string nodeId = Guid.NewGuid().ToString();
            DialogueNodeView nodeView = this.CreateNode(nodeId, fixedLocalPosition);

            List<Node> nodes = this.nodes.ToList();
            nodeView.SetRoot(nodes.Count == 1);
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            List<Port> result = new List<Port>();
            
            foreach (Port port in this.ports)
            {
                if (port == startPort)
                {
                    continue;
                }
        
                if (port.node == startPort.node)
                {
                    continue;
                }
        
                if (port.direction == startPort.direction)
                {
                    continue;
                }
                
                result.Add(port);
            }
        
            return result;
        }
        
        public void SetRootNode(string rootNodeId)
        {
            foreach (Node node in this.nodes)
            {
                DialogueNodeView dialogueNode = (DialogueNodeView) node;
                dialogueNode.SetRoot(dialogueNode.GetId() == rootNodeId);
            }
        }
        
        public void SetRootNode(DialogueNodeView nodeView)
        {
            foreach (Node node in this.nodes)
            {
                DialogueNodeView dialogueNode = (DialogueNodeView) node;
                dialogueNode.SetRoot(dialogueNode == nodeView);
            }
        }

        public bool TryGetRootNode(out DialogueNodeView result)
        {
            foreach (var node in this.nodes)
            {
                result = (DialogueNodeView) node;
                if (result.IsRoot())
                {
                    return true;
                }
            }

            result = default;
            return false;
        }
    }
}

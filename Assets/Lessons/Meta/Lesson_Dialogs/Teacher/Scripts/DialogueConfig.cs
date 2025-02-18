// using System;
// using UnityEngine;
//
// namespace Modules.Dialogues
// {
//     [CreateAssetMenu(
//         fileName = "DialogueConfig",
//         menuName = "Dialogues/New DialogueConfig"
//     )]
//     public sealed class DialogueConfig : ScriptableObject
//     {
//         public string rootId;
//         
//         public Node[] nodes;
//         public Edge[] edges;
//         
//         [Serializable]
//         public struct Node
//         {
//             public string id;
//             public string message;
//             public string[] choices;
//             public Vector2 editorPosition;
//         }
//         
//         [Serializable]
//         public struct Edge
//         {
//             public string inputNodeId;
//             public string outputNodeId;
//             public int outputIndex;
//         }
//         
//         public bool FindRootNode(out Node node)
//         {
//             return this.FindNode(this.rootId, out node);
//         }
//
//         private bool FindNode(string id, out Node result)
//         {
//             for (int i = 0, count = this.nodes.Length; i < count; i++)
//             {
//                 Node node = this.nodes[i];
//                 if (node.id == id)
//                 {
//                     result = node;
//                     return true;
//                 }
//             }
//
//             result = default;
//             return false;
//         }
//
//         public bool FindNextNode(string currentNodeId, int choiceIndex, out Node nextNode)
//         {
//             for (int i = 0, count = this.edges.Length; i < count; i++)
//             {
//                 Edge edge = this.edges[i];
//                 
//                 if (edge.outputNodeId == currentNodeId && edge.outputIndex == choiceIndex)
//                 {
//                     if (this.FindNode(edge.inputNodeId, out nextNode))
//                     {
//                         return true;
//                     }
//         
//                     return false;
//                 }
//             }
//
//             nextNode = default;
//             return false;
//         }
//     }
// }
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Modules.Dialogues
{
    public sealed class DialogueToolbar : Toolbar
    {
        internal DialogueToolbar(DialogueGraphView graphView)
        {
            ObjectField configField = CreateConfigField();
            Button loadButton = CreateButtonLoad(graphView, configField);
            Button saveButton = CreateButtonSave(graphView, configField);
            Button resetButton = CreateButtonReset(graphView, configField);

            this.Add(configField);
            this.Add(loadButton);
            this.Add(saveButton);
            this.Add(resetButton);
        }

        private static ObjectField CreateConfigField()
        {
            return new ObjectField("Selected Dialog")
            {
                objectType = typeof(DialogueConfig),
                allowSceneObjects = false
            };
        }

        private static Button CreateButtonSave(DialogueGraphView graphView, ObjectField configField)
        {
            return new Button
            {
                text = "Save Config",
                clickable = new Clickable(() =>
                {
                    DialogueConfig config = configField.value as DialogueConfig;
                    
                    if (config != null)
                    {
                        DialogueSaver.SaveDialog(graphView, config);
                    }
                    else
                    {
                        DialogueSaver.SaveDialogAsNew(graphView, out config);
                        configField.value = config;
                    }
                })
            };
        }

        private static Button CreateButtonLoad(DialogueGraphView graphView, ObjectField configField)
        {
            return new Button
            {
                text = "Load Config",
                clickable = new Clickable(() =>
                {
                    DialogueLoader.LoadDialog(configField.value as DialogueConfig, graphView);
                })
            };
        }

        private static Button CreateButtonReset(DialogueGraphView graphView, ObjectField configField)
        {
            return new Button
            {
                text = "Reset Dialog",
                clickable = new Clickable(() =>
                {
                    graphView.ResetState();
                    configField.value = null;
                })
            };
        }
    }
}
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UIElements;

public class BuildIndexSceneLoader : EditorWindow
    {
        private EditorBuildSettingsScene[] _editorScenes;
        private string _path;
        private VisualElement _root;
        private ScrollView _scrollView;
        private bool _isInitialized = false;

        
        [MenuItem("Tools/Build Index Scenes Loader")]
        public static void ShowWindow()
        {
            BuildIndexSceneLoader wnd = GetWindow<BuildIndexSceneLoader>();
            wnd.titleContent = new GUIContent("Build Index Scenes Loader");
        }

        public void CreateGUI()
        {
            _root = rootVisualElement;
            _scrollView = new ScrollView();
            _root.Add(_scrollView);
            InitializeSceneList();
        }

        private void InitializeSceneList()
        {
            _isInitialized = true;
            _editorScenes = EditorBuildSettings.scenes;
            _scrollView.Clear();
            
            for (int i = 0; i < _editorScenes.Length; i++)
            {
                AddSceneButton(_scrollView, i);
            }
        }

        private void AddSceneButton(VisualElement parent, int index)
        {
            _path = _editorScenes[index].path;
            string sceneName = _path.Substring(0, _path.Length - 6).Substring(_path.LastIndexOf('/') + 1);
            
            Button btn = new Button(() => 
            {
                if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                {
                    EditorSceneManager.OpenScene(_editorScenes[index].path);
                }
            })
            {
                text = $"{index} - {sceneName}"
            };
            
            parent.Add(btn);
        }

        private void RefreshUI()
        {
            if (_root != null && _isInitialized)
            {
                _root.Clear();
                _scrollView = new ScrollView();
                _root.Add(_scrollView);
                InitializeSceneList();
            }
        }

        private void OnFocus()
        {
            RefreshUI();
        }

        private void OnEnable()
        {
            RefreshUI();
        }

        private void OnDisable()
        {
            _root?.Clear();
        }
    }

using System.IO;
using TaskList.Controllers.Utilities;
using TaskList.Models;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace TaskList.EditorWindows
{
    public class TaskListEditorWindow : EditorWindow
    {
        private TaskListAsset TaskListAsset
        {
            get
            {
                if (_taskListAsset is null)
                {
                    _taskListAsset = CreateInstance<TaskListAsset>();

                    var assetPath = AssetDatabaseUtility.CombinePath(Constants.ResourcesPath,
                        Constants.TaskListAssetsResourcesPath);

                    if (!AssetDatabase.IsValidFolder(assetPath))
                    {
                        AssetDatabaseUtility.CreateDirectory(assetPath);
                    }
                    
                    AssetDatabase.CreateAsset(_taskListAsset, $"{assetPath}/{nameof(Models.TaskListAsset)}.asset");
                    SaveTaskListAsset();

                    //_savedTaskListObjectField.SetValueWithoutNotify(_taskListAsset);
                }

                return _taskListAsset;
            }
            set => _taskListAsset = value;
        }
        
        private ObjectField _savedTaskListObjectField;
        private TextField _taskTitleTextField;
        private Button _addTaskButton;
        private ScrollView _currentTasksScrollView;

        private TaskListAsset _taskListAsset;
        
        [MenuItem(Constants.TaskListEditorWindowMenuItemName)]
        private static void ShowWindow()
        {
            var window = GetWindow<TaskListEditorWindow>();
            window.titleContent = new GUIContent(Constants.TaskListEditorWindowTitle);
            window.Show();
        }

        private void CreateGUI()
        {
            var structure =
                Resources.Load<VisualTreeAsset>(AssetDatabaseUtility.CombinePath(Constants.StructuresResourcesPath,
                    nameof(TaskListEditorWindow)));
            var style = Resources.Load<StyleSheet>(AssetDatabaseUtility.CombinePath(Constants.StyleSheetsResourcesPath,
                nameof(TaskListEditorWindow)));
            
            rootVisualElement.Add(structure.Instantiate());
            rootVisualElement.styleSheets.Add(style);

            _savedTaskListObjectField =
                rootVisualElement.Q<ObjectField>(NamingUtility.NameOf(nameof(_savedTaskListObjectField)));
            _savedTaskListObjectField.objectType = typeof(TaskListAsset);
            _savedTaskListObjectField.RegisterCallback<ChangeEvent<TaskListAsset>>(LoadTaskList);

            _taskTitleTextField = rootVisualElement.Q<TextField>(NamingUtility.NameOf(nameof(_taskTitleTextField)));
            _taskTitleTextField.RegisterCallback<KeyDownEvent>(AddTask);
            
            _addTaskButton = rootVisualElement.Q<Button>(NamingUtility.NameOf(nameof(_addTaskButton)));
            _addTaskButton.clicked += AddTask;
            
            _currentTasksScrollView =
                rootVisualElement.Q<ScrollView>(NamingUtility.NameOf(nameof(_currentTasksScrollView)));
        }

        private void AddTask()
        {
            if (!string.IsNullOrEmpty(_taskTitleTextField.value))
            {
                AddTaskToggle(_taskTitleTextField.value);
                
                TaskListAsset.AddTask(_taskTitleTextField.value);
                SaveTaskListAsset();
                
                _taskTitleTextField.value = string.Empty;
            }
        }

        private void AddTask(KeyDownEvent eventData)
        {
            if (eventData.keyCode == KeyCode.Return)
            {
                AddTask();
                _taskTitleTextField.Focus();
            }
        }

        private void AddTaskToggle(string task)
        {
            var taskToggle = new Toggle
            {
                text = task
            };
            _currentTasksScrollView.Add(taskToggle);
        }

        private void LoadTaskList(ChangeEvent<TaskListAsset> eventData)
        {
            TaskListAsset = eventData.newValue;

            if (TaskListAsset is not null)
            {
                _currentTasksScrollView.Clear();
                
                TaskListAsset.Tasks.ForEach(AddTaskToggle);
            }
        }

        private void SaveTaskListAsset()
        {
            EditorUtility.SetDirty(TaskListAsset);
            AssetDatabase.SaveAssetIfDirty(TaskListAsset);
            AssetDatabase.Refresh();
        }
    }
}
using TaskList.Controllers.Utilities;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace TaskList.EditorWindows
{
    public class TaskListEditorWindow : EditorWindow
    {
        private TextField _taskTitleTextField;
        private Button _addTaskButton;
        private ScrollView _currentTasksScrollView;
        
        [MenuItem("Custom tools/TaskList")]
        private static void ShowWindow()
        {
            var window = GetWindow<TaskListEditorWindow>();
            window.titleContent = new GUIContent("Task list");
            window.Show();
        }

        private void CreateGUI()
        {
            var structure = Resources.Load<VisualTreeAsset>($"Structures/{nameof(TaskListEditorWindow)}");
            var style = Resources.Load<StyleSheet>($"StyleSheets/{nameof(TaskListEditorWindow)}");
            
            rootVisualElement.Add(structure.Instantiate());
            rootVisualElement.styleSheets.Add(style);

            _taskTitleTextField = rootVisualElement.Q<TextField>(NamingUtility.NameOf(nameof(_taskTitleTextField)));
            _taskTitleTextField.RegisterCallback<KeyDownEvent>(AddTaskOnTextFieldSubmitted);
            
            _addTaskButton = rootVisualElement.Q<Button>(NamingUtility.NameOf(nameof(_addTaskButton)));
            _addTaskButton.clicked += AddTask;
            
            _currentTasksScrollView =
                rootVisualElement.Q<ScrollView>(NamingUtility.NameOf(nameof(_currentTasksScrollView)));
        }

        private void AddTask()
        {
            if (string.IsNullOrEmpty(_taskTitleTextField.value)) return;
            
            var taskToggle = new Toggle
            {
                text = _taskTitleTextField.value
            };

            _taskTitleTextField.value = string.Empty;
            _currentTasksScrollView.Add(taskToggle);
        }

        private void AddTaskOnTextFieldSubmitted(KeyDownEvent eventData)
        {
            if (eventData.keyCode == KeyCode.Return)
            {
                AddTask();
                _taskTitleTextField.Focus();
            }
        }
    }
}
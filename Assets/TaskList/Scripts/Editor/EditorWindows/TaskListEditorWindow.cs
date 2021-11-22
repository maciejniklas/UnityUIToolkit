using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace TaskList.TaskList.Scripts.Editor.EditorWindows
{
    public class TaskListEditorWindow : EditorWindow
    {
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
        }
    }
}
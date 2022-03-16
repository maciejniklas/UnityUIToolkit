using System.Collections.Generic;
using UnityEngine;

namespace TaskList.Models
{
    public class TaskListAsset : ScriptableObject
    {
        [SerializeField] private List<string> tasks = new();

        public List<string> Tasks => tasks;

        public void AddTask(string task)
        {
            tasks.Add(task);
        }
    }
}

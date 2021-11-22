using System.Collections.Generic;
using UnityEngine;

namespace TaskList.Models
{
    public class TasksAsset : ScriptableObject
    {
        [SerializeField] private List<string> tasks;

        public List<string> Tasks => tasks;
    }
}

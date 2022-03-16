using System.Linq;
using UnityEditor;

namespace TaskList.Controllers.Utilities
{
    public static class AssetDatabaseUtility
    {
        public static void CreateDirectory(string directory)
        {
            var directoryItems = directory.Split('/');

            directoryItems.Aggregate((parentFolder, subdirectory) =>
            {
                var nextFolder = $"{parentFolder}/{subdirectory}";

                if (!AssetDatabase.IsValidFolder(nextFolder))
                {
                    AssetDatabase.CreateFolder(parentFolder, subdirectory);
                }

                return nextFolder;
            });
        }

        public static string CombinePath(params string[] items)
        {
            return items.Aggregate((first, second) => $"{first}/{second}");
        }
    }
}
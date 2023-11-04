using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;

namespace Code.Editor
{
    public static class BaseFoldersCreator
    {
        [MenuItem("Assets/Create/Base Folders", priority = 10)]
        private static void CreateAllFolders()
        {
            CreateFolders("Assets/", new List<string> { "Art", "Audio", "Code", "Packages", "Levels" });
            
            CreateFolders("Assets/Art/", new List<string> { "Animations", "Materials", "Models", "Textures", "UI" });
            CreateFolders("Assets/Art/Animations/", new List<string> { "Animators", "Clips" });
            CreateFolders("Assets/Art/UI/", new List<string> { "Assets", "Fonts", "Icons" });
            
            CreateFolders("Assets/Audio/", new List<string> { "Music", "Sounds" });
            CreateFolders("Assets/Code/", new List<string> { "Editor", "Input System", "Scripts", "Shaders" });
            CreateFolders("Assets/Levels/", new List<string> { "Prefabs", "Scenes" });

            AssetDatabase.Refresh();
        }

        private static void CreateFolders(string rootFolder, IEnumerable<string> subfolders)
        {
            foreach (var subfolder in subfolders.Where(subfolder => !Directory.Exists(rootFolder + subfolder))) 
                Directory.CreateDirectory(rootFolder + subfolder);
        }
    }
}
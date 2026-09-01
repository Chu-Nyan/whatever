using System.IO;
using UnityEngine;

namespace Chunity.DataPipeline
{
    public static class ExternalFolderHandler
    {
#if UNITY_EDITOR
        public static readonly string ExternalFolder = Path.Combine(Application.dataPath, "00_Chunity/Runtime/Settings", ExternalFolderName);
#elif UNITY_IOS || UNITY_ANDROID
    public static readonly string ExternalFolder = Application.persistentDataPath;
#else
    public static readonly string ExternalFolder = Path.Combine(Directory.GetParent(Application.dataPath)?.FullName, ExternalFolderName);
#endif
        public const string ExternalFolderName = "External_Data";
        public const string LanguagesFolder = "Languages";

        public static string GetLanguagesTextFile(string name)
        {
            name = Path.Combine(ExternalFolder, LanguagesFolder, $"{name}.json");
            return File.ReadAllText(name);
        }
    }
}

using System;
using System.IO;
using UnityEditor.AddressableAssets;
using UnityEngine;

namespace Chunity.Utility
{
    public static class FileUtility
    {
        public static void GenerateFile(string path, string fileName, string text)
        {
            if (Directory.Exists(path) == false)
                Directory.CreateDirectory(path);

            File.WriteAllText(Path.Combine(path, fileName), text);
        }

        public static void WriteTextFileWithLf(string path, string fileName, string text)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

#if UNITY_EDITOR
            bool hasCrLf = text.Contains("\r\n");
            bool hasCr = text.Contains("\r");
            if (hasCrLf || hasCr)
                Debug.LogWarning($"{fileName}: Invalid line break detected \ntext : {text}");
#endif

            text = text.Replace("\r\n", "\n").Replace("\r", "\n");
            File.WriteAllText(Path.Combine(path, fileName), text);
        }

        public static string GetAddressablePath(UnityEngine.Object obj)
        {
            string path = UnityEditor.AssetDatabase.GetAssetPath(obj);
            string guid = UnityEditor.AssetDatabase.AssetPathToGUID(path);

            var settings = AddressableAssetSettingsDefaultObject.Settings;
            var entry = settings.FindAssetEntry(guid);

            if (entry != null)
                return entry.address;
            else
                throw new Exception($"{obj.name}, No Addressable Asset");
        }
    }
}

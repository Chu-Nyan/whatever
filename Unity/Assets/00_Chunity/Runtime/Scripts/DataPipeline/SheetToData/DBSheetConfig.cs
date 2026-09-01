using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Chunity.DataPipeline
{
    [Serializable]
    public class DBSheetConfig
    {
        public readonly Dictionary<string, string> NameSpaceByType = new()
        {
            { "ShapeType", "Chunity.GameSystem.Physics" }
        };

        [Header("Data Sheet Config")]
        [SerializeField] private int _nameRow = 1;
        [SerializeField] private int _typeRow = 2;
        [SerializeField] private int _dataStartRow = 4;

        public DefaultAsset DefaultDtoScriptPath;
        public DefaultAsset DefaultDtoJsonPath;
        public string DefaultDtoNameSpace = "Chunity.DTO";
        public string ScriptSuffix = "Dto";
        public string JsonSuffix = "Data";

        public int DBNameRow => _nameRow - 1;
        public int DBTypeRow => _typeRow - 1;
        public int DBDataStartedRow => _dataStartRow - 1;

        public string GetScriptFileName(string name)
        {
            return $"{name}{ScriptSuffix}";
        }

        public string GetJsonFileName(string name)
        {
            return $"{name}{JsonSuffix}";
        }
    }
}

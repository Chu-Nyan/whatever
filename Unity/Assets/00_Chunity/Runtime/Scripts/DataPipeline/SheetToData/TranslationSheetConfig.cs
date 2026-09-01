using System;
using UnityEditor;
using UnityEngine;

namespace Chunity.DataPipeline
{
    [Serializable]
    public class TranslationSheetConfig
    {
        [SerializeField] private int _headerRow = 1;
        [SerializeField] private int _keyColumn = 1;
        [SerializeField] private int _firstDataRow = 2;
        public DefaultAsset GeneratePath;

        public int HeaderRow => _headerRow - 1;
        public int KeyColumn => _keyColumn - 1;
        public int FirstDataRow => _firstDataRow - 1;
    }
}

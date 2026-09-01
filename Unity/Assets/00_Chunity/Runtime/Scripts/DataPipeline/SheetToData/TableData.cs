using System;
using System.Collections.Generic;
using System.Data;
using UnityEditor;

namespace Chunity.DataPipeline
{
    [Serializable]
    public class TableData
    {
        private DataTable _table;
        private List<(string, string)> _fieldNameByType;

        public string NameSpace;
        public DefaultAsset ScriptPath;
        public DefaultAsset JsonPath;

        public DataTable Table
        {
            get => _table;
        }

        public List<(string, string)> FieldNameByType
        {
            get => _fieldNameByType;
        }

        public void Setup(DataTable table, DBSheetConfig config)
        {
            _table = table;
            _fieldNameByType = new();
            for (int i = 0; i < table.Columns.Count; i++)
            {
                string name = table.Rows[config.DBNameRow][i].ToString();
                if (GoogleSheetsLoader.HasIgnoreSymbol(name) == true)
                    continue;

                _fieldNameByType.Add((name, table.Rows[config.DBTypeRow][i].ToString()));
            }
        }

        public string GetPascalCaseName()
        {
            string cleanName = Table.TableName
                .Replace("_", "")
                .Replace("-", "")
                .Replace(" ", "");

            return cleanName;
        }
    }
}

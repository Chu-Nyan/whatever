using ExcelDataReader;
using System;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

namespace Chunity.DataPipeline
{
    [Serializable]
    public class GoogleSheetsLoader
    {
        private const string _googleDownloadURL = "https://docs.google.com/spreadsheets/d/{0}/export?format=xlsx";
        public const char IgnoreSymbol = '#';

        public string GoogleSheetID;
        public string LastSync;
        public DefaultAsset Export;
        [NonSerialized]
        public bool IsLoading;
        [NonSerialized]
        public DataTableCollection Sheets;

        public async Task RequestExcelFile()
        {
            if (IsLoading == true)
                return;

            IsLoading = true;
            using var www = UnityWebRequest.Get(string.Format(_googleDownloadURL, GoogleSheetID));

            var operation = www.SendWebRequest();

            while (!operation.isDone)
                await Task.Yield();

            if (www.result != UnityWebRequest.Result.Success)
                Debug.LogError("실패: " + www.error);
            else
            {
                using var stream = new MemoryStream(www.downloadHandler.data);
                var path = Path.Combine(AssetDatabase.GetAssetPath(Export), $"{GoogleSheetID}.cache");
                File.WriteAllBytes(path, www.downloadHandler.data);
                LoadSheets(stream);

                LastSync = DateTime.Now.ToString();
            }

            IsLoading = false;
        }

        public void TryLoadSaveSheets()
        {
            var path = Path.Combine(AssetDatabase.GetAssetPath(Export), $"{GoogleSheetID}.cache");
            if (File.Exists(path) == false)
                throw new Exception("File not found");

            LoadSheets(File.OpenRead(path));
        }

        private void LoadSheets(Stream stream)
        {
            Sheets = ExcelReaderFactory.CreateReader(stream).AsDataSet().Tables;
        }

        public static bool HasIgnoreSymbol(string text)
        {
            return text.Length == 0 || text[0] == IgnoreSymbol;
        }
    }
}

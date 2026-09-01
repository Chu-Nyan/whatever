using System.Collections.Generic;
using System.IO;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace Chunity.DataPipeline
{
    public class PostBuildProcessor : IPostprocessBuildWithReport
    {
        private HashSet<string> _ignoreExtension = new() { ".meta" };

        public int callbackOrder
        {
            get => 100;
        }

        public void OnPostprocessBuild(BuildReport report)
        {
            var outputPath = Path.GetDirectoryName(report.summary.outputPath);
            outputPath = Path.Combine(outputPath, "External_Data");
            var sourcePath = ExternalFolderHandler.ExternalFolder;

            CopyFolder(outputPath, sourcePath);
        }

        private void CopyFolder(string outputPath, string sourcePath)
        {
            if (!Directory.Exists(outputPath))
                Directory.CreateDirectory(outputPath);

            var files = Directory.GetFiles(sourcePath);
            var folders = Directory.GetDirectories(sourcePath);

            foreach (var file in files)
            {
                if (_ignoreExtension.Contains(Path.GetExtension(file)) == true)
                    continue;

                var fileName = Path.GetFileName(file);
                var destFile = Path.Combine(outputPath, fileName);
                File.Copy(file, destFile, true);
            }

            foreach (var nextSourcePath in folders)
            {
                var folderName = Path.GetFileName(nextSourcePath);
                var nextDir = Path.Combine(outputPath, folderName);
                CopyFolder(nextDir, nextSourcePath);
            }
        }
    }
}

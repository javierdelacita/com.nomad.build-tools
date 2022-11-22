using System.IO;
using Newtonsoft.Json;
using UnityEditor;
using UnityEditor.Build.Reporting;

namespace Nomad.BuildTools.Editor.PreprocessorSteps
{
    public static class BuildReportExporter
    {
        private const string LastBuildReportFilename = "LastBuild.buildreport";
        private static readonly string LastBuildReportPath = $"Library/{LastBuildReportFilename}";
        private const string BuildReportDir = "Assets/BuildReports";
        
        public static void ExportLastReport() 
        {
            if (!File.Exists(LastBuildReportPath)) 
            {
                return;
            }
            
            Directory.CreateDirectory(BuildReportDir);

            var assetPath = $"{BuildReportDir}/{LastBuildReportFilename}";
            File.Copy(LastBuildReportPath, assetPath, true);
            
            AssetDatabase.ImportAsset(assetPath);
            
            var buildReport = AssetDatabase.LoadAssetAtPath<BuildReport>(assetPath);
            if (buildReport == null) 
            {
                return;
            }

            ExportUnityBuildReport(buildReport);
        }
        
        public static void ExportUnityBuildReport(BuildReport buildReport) 
        {
            // TODO: This seems is not working anymore and throws an error
            /*
            var buildReportPath = AssetDatabase.GetAssetPath(buildReport);
            var exportFileName = Path.ChangeExtension(buildReportPath, ".json");

            var json = JsonConvert.SerializeObject(buildReport, Formatting.Indented);
            json = json.Replace("\r\n", "\n");
            File.WriteAllText(exportFileName, json);
            */
        }
    }
}
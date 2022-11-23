using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Build.Reporting;

using Debug = UnityEngine.Debug;

namespace Nomad.BuildTools.Editor
{
    public class UnityPlayerBuilder
    {
        private readonly IBuildPreprocessor _buildPreprocessor;

        private readonly IBuildPostprocessor _buildPostprocessor;
        
        public UnityPlayerBuilder(IBuildPreprocessor preprocessor, IBuildPostprocessor postprocessor)
        {
            _buildPreprocessor = preprocessor;
            _buildPostprocessor = postprocessor;
        }

        public void Build(BuildConfiguration buildConfig)
        {
            if (buildConfig == null) 
            {
                throw new ArgumentNullException(nameof(buildConfig));
            }

            var buildTimer = new Stopwatch();
            buildTimer.Start();

            _buildPreprocessor?.PreprocessStep(buildConfig);

            PrepareExportFolder(buildConfig);
            BuildUnityPlayer(buildConfig);

            _buildPostprocessor?.PostprocessStep(buildConfig);

            buildTimer.Stop();
            Debug.Log($"Build finished in {buildTimer.Elapsed} seconds");
        }

        private static void PrepareExportFolder(BuildConfiguration buildConfig)
        {
            if (string.IsNullOrEmpty(buildConfig.ExportPath)) 
            {
                throw new ArgumentException("Non defined export path!");
            }
            
            // Clean Export Folder
            if (Directory.Exists(buildConfig.ExportPath)) 
            {
                Debug.Log($"Cleaning export folder: {buildConfig.ExportPath}");
                var subdirectories = Directory.GetDirectories(buildConfig.ExportPath);
                foreach (var directory in subdirectories) 
                {
                    Debug.Log($"Cleaning directory: {directory}");
                    Directory.Delete(directory, true);
                }
            }
            
            // Create Export Folder if needed
            Directory.CreateDirectory(buildConfig.ExportPath);
        }

        private static void BuildUnityPlayer(BuildConfiguration buildConfig)
        {
            var outputPath = buildConfig.ExportPath;
            
            // Android generates a file, while the other platforms export to a folder
            if (buildConfig.Target == BuildTarget.Android)
            {
                var fileExtension = EditorUserBuildSettings.buildAppBundle ? ".aab" : ".apk";
                outputPath = Path.Combine(
                    outputPath,
                    PlayerSettings.productName.Replace(" ", "") + fileExtension);
            }

            var buildOptions = buildConfig.Options;
            
            // When building an iOS or Android project, we can add the AcceptExternalModificationsToPlayer in order to 
            // support incremental builds
            if (BuildPipeline.BuildCanBeAppended(buildConfig.Target, outputPath) == CanAppendBuild.Yes)
            {
                buildOptions |= BuildOptions.AcceptExternalModificationsToPlayer;
            }

            var buildPlayerOptions = new BuildPlayerOptions
            {
                scenes = EditorBuildSettings.scenes.Select(scene => scene.path).ToArray(),
                locationPathName = outputPath,
                target = buildConfig.Target,
                targetGroup = BuildPipeline.GetBuildTargetGroup(buildConfig.Target),
                options = buildOptions
            };
            
            var buildReport = BuildPipeline.BuildPlayer(buildPlayerOptions);
            Debug.Log($"Build completed with result: {buildReport.summary.result}");
        }
    }
}
using System;
using UnityEditor;

namespace Nomad.BuildTools.Editor.PreprocessorSteps
{
    public class DefineSymbolsBuildPreprocessor
    {
        public void SetDefineSymbols(BuildConfiguration buildConfig)
        {
            var buildTargetGroup = GetBuildTargetGroupFromBuildConfiguration(buildConfig);
            var defineSymbolsString = GetDefineSymbolsStringFromBuildConfiguration(buildConfig);
            PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup, defineSymbolsString);
        }

        private BuildTargetGroup GetBuildTargetGroupFromBuildConfiguration(BuildConfiguration buildConfig)
        {
            switch (buildConfig.Target)
            {
                case BuildTarget.Android: return BuildTargetGroup.Android;
                case BuildTarget.iOS: return BuildTargetGroup.iOS;
                default:
                    throw new NotImplementedException($"{buildConfig.Target} platform is not supported");
            }
        }

        private string GetDefineSymbolsStringFromBuildConfiguration(BuildConfiguration buildConfig)
        {
            return string.Join(";", buildConfig.DefineSymbols);
        }
    }
}
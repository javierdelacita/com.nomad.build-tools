using UnityEditor;

namespace Nomad.BuildTools.Editor
{
    public class BuildConfiguration
    {
        public string ExportPath { get; set; }

        public BuildTarget Target { get; set; }

        public BuildType BuildType { get; set; }

        public BuildOptions Options { get; set; }
        
        public string[] DefineSymbols { get; set; }
    }
}
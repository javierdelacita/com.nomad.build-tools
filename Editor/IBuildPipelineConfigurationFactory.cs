using UnityEditor;

namespace Nomad.BuildTools.Editor
{
    public interface IBuildPipelineConfigurationFactory
    {
        BuildConfiguration CreateBuildConfig(BuildTarget target, BuildType buildType);

        UnityPlayerBuilder CreatePlayerBuilder();
    }
}
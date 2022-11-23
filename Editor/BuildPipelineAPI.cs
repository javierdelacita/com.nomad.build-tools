using System;
using Nomad.BuildTools.Editor;
using UnityEditor;

namespace Nomad.BuildTools.Editor
{
    public static class BuildPipelineAPI
    {
        private static IBuildPipelineConfigurationFactory _registeredConfigurationFactory;

        public static void RegisterConfigurationFactory(IBuildPipelineConfigurationFactory factory)
        {
            _registeredConfigurationFactory = factory;
        }

        public static void BuildAndroidDevelopment()
        {
            if (_registeredConfigurationFactory == null)
            {
                throw new NullReferenceException($"There is no registered IBuildPipelineConfigurationFactory, hence I don't know how to build your game :(");
            }
            
            var buildConfig = _registeredConfigurationFactory.CreateBuildConfig(BuildTarget.Android, BuildType.Development);
            Build(buildConfig);
        }

        public static void BuildAndroidRelease()
        {
            if (_registeredConfigurationFactory == null)
            {
                throw new NullReferenceException($"There is no registered IBuildPipelineConfigurationFactory, hence I don't know how to build your game :(");
            }
            
            var buildConfig = _registeredConfigurationFactory.CreateBuildConfig(BuildTarget.Android, BuildType.Release);
            Build(buildConfig);
        }

        public static void BuildIosDevelopment()
        {
            if (_registeredConfigurationFactory == null)
            {
                throw new NullReferenceException($"There is no registered IBuildPipelineConfigurationFactory, hence I don't know how to build your game :(");
            }
            
            var buildConfig = _registeredConfigurationFactory.CreateBuildConfig(BuildTarget.iOS, BuildType.Development);
            Build(buildConfig);
        }

        public static void BuildIosRelease()
        {
            if (_registeredConfigurationFactory == null)
            {
                throw new NullReferenceException($"There is no registered IBuildPipelineConfigurationFactory, hence I don't know how to build your game :(");
            }
            
            var buildConfig = _registeredConfigurationFactory.CreateBuildConfig(BuildTarget.iOS, BuildType.Release);
            Build(buildConfig);
        }

        private static void Build(BuildConfiguration buildConfig)
        {
            var builder = _registeredConfigurationFactory.CreatePlayerBuilder();
            builder.Build(buildConfig);
        }
    }
}

using UnityEditor;

namespace Nomad.BuildTools.Editor
{
    public static class BuildToolsMenu
    {
        [MenuItem("Nomad/Build Tools/Android/Development build")]
        public static void BuildAndroidDevelopment()
        {
            BuildToolsAPI.BuildUnityClient(BuildPlatform.Android, BuildType.Development);
        }
        
        [MenuItem("Nomad/Build Tools/Android/Release build")]
        public static void BuildAndroidRelease()
        {
            BuildToolsAPI.BuildUnityClient(BuildPlatform.Android, BuildType.Release);
        }
        
        [MenuItem("Nomad/Build Tools/iOS/Development build")]
        public static void BuildIosDevelopment()
        {
            BuildToolsAPI.BuildUnityClient(BuildPlatform.Ios, BuildType.Development);
        }
        
        [MenuItem("Nomad/Build Tools/iOS/Release build")]
        public static void BuildIosRelease()
        {
            BuildToolsAPI.BuildUnityClient(BuildPlatform.Ios, BuildType.Release);
        }
    }
}
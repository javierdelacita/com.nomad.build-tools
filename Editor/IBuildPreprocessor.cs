namespace Nomad.BuildTools.Editor
{
    public interface IBuildPreprocessor
    {
        void PreprocessStep(BuildConfiguration buildConfig);
    }
}
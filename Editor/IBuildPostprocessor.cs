namespace Nomad.BuildTools.Editor
{
    public interface IBuildPostprocessor
    {
        void PostprocessStep(BuildConfiguration buildConfig);
    }
}
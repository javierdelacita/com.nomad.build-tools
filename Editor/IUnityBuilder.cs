namespace Nomad.BuildTools.Editor
{
    public interface IUnityBuilder
    {
        IBuildPreprocessor Preprocessor { get; }
        
        IBuildPostprocessor Postprocessor { get; }

        void Build();
    }
}
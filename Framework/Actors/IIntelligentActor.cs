namespace Framework.Actors
{
    public interface IIntelligentActor : IActor
    {
        int IntelligentFixation(double[] state);
    }
}
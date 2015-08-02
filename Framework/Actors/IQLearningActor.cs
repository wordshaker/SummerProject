namespace Framework.Actors
{
    public interface IQLearningActor
    {
        int Fixate();
        int IntelligentGuess(double[] state);
    }
}
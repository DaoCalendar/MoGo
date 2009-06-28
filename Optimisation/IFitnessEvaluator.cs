using MoGo.World;

namespace MoGo.Optimisation
{
    public interface IFitnessEvaluator
    {
        GeneScore Evaluate(Gene gene);

        void Initialise(OptimiserParameters parameters);
    }
}
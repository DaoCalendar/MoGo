using System;

namespace MoGo.Optimisation
{
    public class IterationCompleteEventArgs : EventArgs
    {
        private readonly int _iterationNumber;
        private readonly int _iterationsRemaining;

        private readonly GeneScore _score;

        public IterationCompleteEventArgs(GeneScore score, int iterationNumber, int iterationsRemaining)
        {
            _score = score;
            _iterationNumber = iterationNumber;
            _iterationsRemaining = iterationsRemaining;
        }

        public GeneScore Score
        {
            get { return _score; }
        }

        public int IterationNumber
        {
            get { return _iterationNumber; }
        }

        public int IterationsRemaining
        {
            get { return _iterationsRemaining; }
        }

        public bool Stop { get; set; }
    }
}
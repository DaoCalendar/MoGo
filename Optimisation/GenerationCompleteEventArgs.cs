using System;
using System.Collections.Generic;

namespace MoGo.Optimisation
{
    public class GenerationCompleteEventArgs : EventArgs
    {
        private readonly int _generationNumber;
        private readonly IList<GeneScore> _scores;

        public GenerationCompleteEventArgs(IList<GeneScore> scores, int generationNumber)
        {
            _scores = scores;
            _generationNumber = generationNumber;
        }

        public IList<GeneScore> Scores
        {
            get { return _scores; }
        }

        public int GenerationNumber
        {
            get { return _generationNumber; }
        }
    }
}
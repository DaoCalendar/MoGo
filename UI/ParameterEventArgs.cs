using System;
using MoGo.Optimisation;

namespace MoGo.UI
{
    public class ParameterEventArgs : EventArgs
    {
        private readonly OptimiserParameters _parameters;

        public ParameterEventArgs(OptimiserParameters parameters)
        {
            _parameters = parameters;
        }

        public OptimiserParameters Parameters
        {
            get { return _parameters; }
        }
    }
}
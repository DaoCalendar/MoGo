using System.Collections.Generic;

namespace MoGo.World
{
    public class CompositeGeneValidator : IGeneValidator
    {
        private readonly IList<IGeneValidator> _geneValidators;

        public CompositeGeneValidator(params IGeneValidator[] geneValidators)
        {
            _geneValidators = geneValidators;
        }


        #region IGeneValidator Members

        public bool Valid(Gene gene)
        {
            foreach (var validator in _geneValidators)
            {
                if (!validator.Valid(gene))
                {
                    return false;
                }
            }

            return true;
        }

        #endregion
    }
}
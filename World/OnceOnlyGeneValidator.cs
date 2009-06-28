using System.Collections.Generic;

namespace MoGo.World
{
    public class OnceOnlyGeneValidator : IGeneValidator
    {
        private readonly IDictionary<Gene, bool> _seenGenes = new Dictionary<Gene, bool>();


        #region IGeneValidator Members

        public bool Valid(Gene gene)
        {
            var valid = !_seenGenes.ContainsKey(gene);

            if (valid)
            {
                _seenGenes.Add(gene, true);
            }

            return valid;
        }

        #endregion
    }
}
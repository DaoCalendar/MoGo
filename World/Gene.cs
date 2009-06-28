using System.Collections.Generic;
using System.Text;
using MoGo.ChromosomeTypes;

namespace MoGo.World
{
    public class Gene
    {
        private readonly List<object> _chromosomes = new List<object>();

        public IList<object> Chromosomes
        {
            get { return _chromosomes; }
        }

        public override int GetHashCode()
        {
            var hashCode = 0;

            _chromosomes.ForEach(delegate(object chromosome) { hashCode ^= chromosome.GetHashCode(); });

            return hashCode;
        }

        public override bool Equals(object obj)
        {
            var other = obj as Gene;
            var result = true;

            if (other != null)
            {
                for (var i = 0; i < _chromosomes.Count; i++)
                {
                    if (!Equals(Chromosomes[i], other.Chromosomes[i]))
                    {
                        result = false;
                        break;
                    }
                }
            }
            else
            {
                result = false;
            }

            return result;
        }

        public override string ToString()
        {
            return string.Join(", ", new List<object>(_chromosomes).ConvertAll(o => o.ToString()).ToArray());
        }

        public string ToString(IList<BaseChromosomeType> chromosomeTypes)
        {
            var sb = new StringBuilder();

            for (var i = 0; i < Chromosomes.Count; i++)
            {
                sb.Append(string.Format("{0}={1:#,##0.###}, ", chromosomeTypes[i].Name, Chromosomes[i]));
            }

            return sb.ToString().TrimEnd(',', ' ');
        }
    }
}
using System;

namespace MoGo.ChromosomeTypes
{
    public class EnumChromosomeType : BaseChromosomeType
    {
        private readonly Type _enumType;
        private readonly Array _enumValues;

        public EnumChromosomeType(string name, Type enumType) : base(name)
        {
            _enumType = enumType;
            _enumValues = Enum.GetValues(enumType);
        }

        public override Type Type
        {
            get { return _enumType; }
        }

        public override object GetRandomValue(Random random)
        {
            return _enumValues.GetValue((int) GetRandomNumber(0, _enumValues.Length - 1, 1, random));
        }

        public override object Mutate(object value, Random random)
        {
            return GetRandomValue(random);
        }
    }
}
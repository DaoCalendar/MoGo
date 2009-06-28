using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using MoGo.ChromosomeTypes;

namespace MoGo.World
{
    internal class GeneValidatorFactory
    {
        private readonly IList<BaseChromosomeType> _chromosomeTypes;

        private readonly Regex _regex = new Regex(@"(?<![\.a-z\d_""])[a-z_][a-z\d_]*",
                                                  RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture |
                                                  RegexOptions.Compiled);

        public GeneValidatorFactory(IList<BaseChromosomeType> chromosomeTypes)
        {
            _chromosomeTypes = chromosomeTypes;
        }

        public IGeneValidator GetGeneValidator(string conditionString)
        {
            var codeDomProvider = CodeDomProvider.CreateProvider("CSharp");
            var compilerParameters = new CompilerParameters
                                         {
                                             GenerateInMemory = false,
                                             WarningLevel = 3,
                                             CompilerOptions = "/optimize",
                                             TreatWarningsAsErrors = true,
                                         };

            compilerParameters.ReferencedAssemblies.Add("System.dll");
            compilerParameters.ReferencedAssemblies.Add(Assembly.GetExecutingAssembly().Location);

            var codeString =
                @"using System;
                using System.IO;
                using MoGo.ChromosomeTypes;

                namespace MoGo.World {

                    public class CompiledGeneValidator : IGeneValidator {

                        public bool Valid(Gene gene) { return " +
                GetMassagedConditionString(conditionString) + @"; }
                    }
            }";

            var compilerResults = codeDomProvider.CompileAssemblyFromSource(compilerParameters, codeString);

            if (compilerResults.Errors.HasErrors)
            {
                //var errorMsg = compiledAssembly.Errors.Count + " Errors:";

                //for (var x = 0; x < compiledAssembly.Errors.Count; x++)
                //{
                //    errorMsg += "\r\nLine: " + compiledAssembly.Errors[x].Line + " - " + compiledAssembly.Errors[x].ErrorText;
                //}

                return null;
            }

            return (IGeneValidator) compilerResults.CompiledAssembly.CreateInstance("MoGo.World.CompiledGeneValidator");
        }

        private string GetMassagedConditionString(string conditionString)
        {
            foreach (Match match in _regex.Matches(conditionString))
            {
                int chromosomeIndex;
                var type = GetChromosomeTypeByName(match.Value, out chromosomeIndex);

                if (type != null)
                {
                    conditionString = conditionString.Replace(match.Value,
                                                              string.Format("({0})gene.Chromosomes[{1}]", type.Type,
                                                                            chromosomeIndex));
                }
            }
            return conditionString;
        }

        private BaseChromosomeType GetChromosomeTypeByName(string name, out int index)
        {
            BaseChromosomeType result = null;
            index = -1;

            for (var i = 0; i < _chromosomeTypes.Count; i++)
            {
                if (_chromosomeTypes[i].Name == name)
                {
                    result = _chromosomeTypes[i];
                    index = i;

                    break;
                }
            }

            return result;
        }
    }
}
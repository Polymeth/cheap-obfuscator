using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using kinda_bad_obfuscator.Obsfuscator.Utility;
using dnlib.DotNet;
using dnlib.DotNet.Emit;


namespace kinda_bad_obfuscator.Obsfuscator.Protection
{
    public class Renamer
    {
        public ModuleDefMD module;

        public void Start(ModuleDefMD _module)
        {
            module = _module;
            Rename();
        }

        private void Rename()
        {
            Analyzer analyzer = new Analyzer();
            Randomizer randomizer = new Randomizer();

            foreach (TypeDef type in module.Types)
            {
                if (analyzer.AnalyzeType(type))
                {
                    type.Name = randomizer.GetAlphanumericRandom();
                }

                foreach (FieldDef field in type.Fields)
                {
                    if (analyzer.AnalyzeField(field))
                    {
                        field.Name = randomizer.GetAlphanumericRandom();
                    }
                }
                
                foreach (PropertyDef property in type.Properties)
                {
                    if (analyzer.analyzeProperty(property))
                    {
                        property.Name = randomizer.GetAlphanumericRandom();
                    }
                }

                foreach (MethodDef method in type.Methods)
                {
                    if (analyzer.analyzeMethod(method))
                    {
                        method.Name = randomizer.GetAlphanumericRandom();
                    }
                    foreach (Parameter parameter in method.Parameters)
                    {
                        parameter.Name = randomizer.GetAlphanumericRandom();
                    }
                }
                
            }

        }
    }
}

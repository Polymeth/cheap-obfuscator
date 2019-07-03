using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dnlib.DotNet;
using dnlib.DotNet.Emit;

namespace kinda_bad_obfuscator.Obsfuscator.Utility
{
    class Analyzer
    {
        // TODO: Optimize ueh
        public bool AnalyzeType(TypeDef type)
        {
            if (type.IsRuntimeSpecialName)
            {
                return false;
            }
            return true;
        }

        public bool AnalyzeField(FieldDef field)
        {
            if (field.IsRuntimeSpecialName)
            {
                return false;
            }
            if (field.IsLiteral)
            {
                return false;
            }
            return true;
        }

        public bool analyzeProperty(PropertyDef property)
        {
            if (property.IsRuntimeSpecialName)
            {
                return false;
            }
            if (property.IsEmpty)
            {
                return false;
            }
            return true;
        }

        public bool analyzeMethod(MethodDef method)
        {
            if (method.IsRuntimeSpecialName)
            {
                return false;
            }
            return true;
        }

    }
}

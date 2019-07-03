using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dnlib.DotNet;
using dnlib.DotNet.Emit;

namespace kinda_bad_obfuscator.Obsfuscator.Protection
{
    public class StringEncryption
    {

        public void Inject(ModuleDefMD module)
        {
            MethodDefUser decode = new MethodDefUser("Decode", MethodSig.CreateStatic(module.CorLibTypes.String, module.CorLibTypes.String), MethodImplAttributes.IL | MethodImplAttributes.Managed, MethodAttributes.Public | MethodAttributes.Static | MethodAttributes.HideBySig | MethodAttributes.ReuseSlot);
            module.GlobalType.Methods.Add(decode);
            CilBody body = new CilBody();
            decode.Body = body;

            body.Instructions.Add(OpCodes.Nop.ToInstruction());
            body.Instructions.Add(OpCodes.Call.ToInstruction(module.Import(typeof(System.Text.Encoding).GetMethod("get_UTF8", new Type[] { }))));
            body.Instructions.Add(OpCodes.Ldarg_0.ToInstruction());
            body.Instructions.Add(OpCodes.Call.ToInstruction(module.Import(typeof(System.Convert).GetMethod("FromBase64String", new Type[] { typeof(string) }))));
            body.Instructions.Add(OpCodes.Callvirt.ToInstruction(module.Import(typeof(System.Text.Encoding).GetMethod("GetString", new Type[] { typeof(byte[]) }))));
            body.Instructions.Add(OpCodes.Ret.ToInstruction());

            foreach (TypeDef type in module.Types)
            {
                if (type.Name != "Resources" || type.Name != "Settings")
                {
                    foreach(MethodDef method in type.Methods)
                {
                        if (!method.HasBody)
                            continue;

                        for (int i = 0; i < method.Body.Instructions.Count(); i++)
                        {
                            if (method.Body.Instructions[i].OpCode == OpCodes.Ldstr)
                            {
                                method.Body.Instructions[i].Operand = Convert.ToBase64String(UTF8Encoding.UTF8.GetBytes(method.Body.Instructions[i].Operand.ToString()));
                                method.Body.Instructions.Insert(i + 1, new Instruction(OpCodes.Call, decode));
                                i += 1;
                            }
                        }
                        method.Body.SimplifyBranches();
                        method.Body.OptimizeBranches();

                    }
                }

                
            }
        }

        public static string Encode(string str)
        {
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(str));
        }
    }
}

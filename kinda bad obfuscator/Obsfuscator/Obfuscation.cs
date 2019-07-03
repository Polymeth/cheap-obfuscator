using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using kinda_bad_obfuscator.Obsfuscator.Protection;
using kinda_bad_obfuscator.Obsfuscator.Utility;
using dnlib.DotNet;
using dnlib.DotNet.Emit;

namespace kinda_bad_obfuscator.Obsfuscator
{
    public class Obfuscation
    {
        // change path lol
        public void Start(string _path)
        {
            ModuleDefMD module = ModuleDefMD.Load(@"source.exe");

            StringEncryption stringEncryption = new StringEncryption();
            stringEncryption.Inject(module);
            Renamer renamer = new Renamer();
            renamer.Start(module);
            module.Write(Environment.CurrentDirectory + @"\final.exe");
        }
    }
}

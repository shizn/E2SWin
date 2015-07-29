using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace E2SWin
{
    class CSharpGenerator
    {
        public void WriteToCSharp(SheetDataInfo dataInfo, string cSharpFullPath)
        {
            StreamWriter sw = new StreamWriter(cSharpFullPath, false, Encoding.UTF8);
            
            // C# begin



            // C# end
            sw.Close();
        }
    }
}

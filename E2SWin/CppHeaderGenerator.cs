using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace E2SWin
{
    class CppHeaderGenerator
    {
        public void WriteToCppHeader(SheetDataInfo dataInfo, string cppHeaderFullPath)
        {
            StreamWriter sw = new StreamWriter(cppHeaderFullPath, false, Encoding.UTF8);
            // .h begin



            // .h end
            sw.Close();
        }
    }
}

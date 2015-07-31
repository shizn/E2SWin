using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace E2SWin
{
    [Serializable]
    public class Config
    {
        private const string configFile = "config.bin";

        public string excelFolderPath = string.Empty;
        public string exportFolderPath = string.Empty;
        public string mapFolderPath = string.Empty;

        public Config()
        {
        }

        static public Config LoadConfig()
        {
            if (!File.Exists(configFile))
            {
                return null;
            }

            Stream fStream = new FileStream(configFile, FileMode.Open, FileAccess.Read);
            BinaryFormatter binFormat = new BinaryFormatter();//创建二进制序列化器
            Config config = (Config)binFormat.Deserialize(fStream);//反序列化对象
            fStream.Close();

            return config;
        }

        public void SaveConfig()
        {
            Stream fStream = new FileStream(configFile, FileMode.Create, FileAccess.ReadWrite);
            BinaryFormatter binFormat = new BinaryFormatter();//创建二进制序列化器
            binFormat.Serialize(fStream, this);
            fStream.Flush();
            fStream.Close();
        }
    }
}

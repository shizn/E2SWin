using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;

namespace E2SWin
{
    public class StructInfo
    {
        public string name = string.Empty;
        public string version = string.Empty;

        public Hashtable structInfos = new Hashtable();
    }

    public class EntryInfo
    {
        public string name = string.Empty;
        public string type = string.Empty;
        public string cname = string.Empty;
        public string desc = string.Empty;
        public string unique = string.Empty;
        public string macrosgroup = string.Empty;
    }

    public class MacrosGroupInfo
    {
        public string name = string.Empty;
        public string version = string.Empty;
        public string desc = string.Empty;

        public Hashtable macrosGroupInfos = new Hashtable();
    }

    public class MacroInfo
    {
        public string cname = string.Empty;
        public string value = string.Empty;
        public string name = string.Empty;
        public string desc = string.Empty;
    }
    public class XmlParser
    {
        public Hashtable metalibMap = new Hashtable();

        public void LoadXmlFile(string xmlFullPath)
        {
            if(!File.Exists(xmlFullPath))
            {
                throw new Exception("找不到字段映射文件：" + xmlFullPath);
            }

            // 清空
            metalibMap.Clear();
            // 载入XML文件
            XDocument doc = XDocument.Load(xmlFullPath);
            XElement metalib = doc.Element("metalib");
            foreach(XElement meta in metalib.Elements())
            {
                if(meta.Name == "struct")
                {
                    // 是一个struct
                    string structName = meta.Attribute("name").Value;
                    string structVersion = meta.Attribute("version").Value;
                    // 检查struct是否重复
                    if(metalibMap[structName]!=null)
                    {
                        throw new Exception("存在重复的struct:" + structName + "\t请检查xml文件" + xmlFullPath);
                    }
                    StructInfo structInfo = new StructInfo();
                    // 记录值
                    structInfo.name = structName;
                    structInfo.version = structVersion;
                    // 遍历每个entry
                    foreach(XElement entry in meta.Elements())
                    {
                        string entryName = entry.Attribute("name").Value;
                        string entryType = entry.Attribute("type").Value;
                        string entryCname = entry.Attribute("cname").Value;
                        string entryMacrosgroup = entry.Attribute("macrosgroup").Value;
                        string entryDesc = entry.Attribute("desc").Value;
                        string entryUnique = entry.Attribute("unique").Value;
                        // 检查entry是否重复 只检查了name
                        if (structInfo.structInfos[entryName] != null)
                        {
                            throw new Exception("存在重复的entry：" + entryName + "在struct：" + structName + "\t请检查xml文件" + xmlFullPath);
                        }
                        EntryInfo entryInfo = new EntryInfo();
                        entryInfo.name = entryName;
                        entryInfo.type = entryType;
                        entryInfo.cname = entryCname;
                        entryInfo.macrosgroup = entryMacrosgroup;
                        entryInfo.desc = entryDesc;
                        entryInfo.unique = entryUnique;
                        // 用entry name作为Key
                        structInfo.structInfos.Add(entryName, entryInfo);
                    }
                    // 将整理好的内容放入Hashtable
                    metalibMap.Add(structName, structInfo);
                }
                else if(meta.Name =="macrosgroup")
                {
                    // 是一个macrosgroup
                    string macrosgroupName = meta.Attribute("name").Value;
                    string macrosgroupVersion = meta.Attribute("version").Value;
                    string macrosgroupDesc = meta.Attribute("desc").Value;
                    // 检查macrosgroup是否重复
                    if(metalibMap[macrosgroupName]!=null)
                    {
                        throw new Exception("存在重复的macrosgroup:" + macrosgroupName + "\t请检查xml文件" + xmlFullPath);
                    }
                    MacrosGroupInfo macrosgroupInfo = new MacrosGroupInfo();
                    macrosgroupInfo.name = macrosgroupName;
                    macrosgroupInfo.version = macrosgroupVersion;
                    macrosgroupInfo.desc = macrosgroupDesc;
                    foreach(XElement macro in meta.Elements())
                    {
                        string macroCname = macro.Attribute("cname").Value;
                        string macroValue = macro.Attribute("value").Value;
                        string macroName = macro.Attribute("name").Value;
                        string macroDesc = macro.Attribute("desc").Value;
                        // 检查macro是否重复
                        if (macrosgroupInfo.macrosGroupInfos[macroCname] != null)
                        {
                            throw new Exception("存在重复的macro：" + macroCname + "在macrosgroup：" + macrosgroupName + "\t请检查xml文件" + xmlFullPath);
                        }
                        MacroInfo macroInfo = new MacroInfo();
                        macroInfo.cname = macroCname;
                        macroInfo.value = macroValue;
                        macroInfo.name = macroName;
                        macroInfo.desc = macroDesc;

                        // 用macro cname作为key
                        macrosgroupInfo.macrosGroupInfos.Add(macroCname, macroInfo);
                    }
                    // 将整理好的内容放入Hashtable
                    metalibMap.Add(macrosgroupInfo, macrosgroupName);
                }
            }
        }


        public void CheckSheetDataInfo(SheetDataInfo sheetInfo)
        {

        }
        
    }
}

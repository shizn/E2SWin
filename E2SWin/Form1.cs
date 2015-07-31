using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Collections;

namespace E2SWin
{
    public partial class MainForm : Form
    {
        public Encoder encoder = new Encoder();
        public XmlParser xmlParser = new XmlParser();
        public MainForm()
        {
            InitializeComponent();
        }

        private void button_excelFolderPath_Click(object sender, EventArgs e)
        {
            // 文件夹选取窗口
            FolderBrowserDialog fDialog = new FolderBrowserDialog();
            fDialog.SelectedPath = this.textBox_excelFolderPath.Text;
            fDialog.Description = "请选取Excel存放目录";
            if(fDialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }
            // 得到excel文件夹路径并更新textbox
            this.textBox_excelFolderPath.Text = fDialog.SelectedPath;

            // 更新设置
        }

        private void XlsxFilePrinter(List<SheetDataInfo> sheetList)
        {
            // 供测试用
            foreach (var r in sheetList)
            {
                this.textBox_log.Text += r.sheetName.ToString();
                this.textBox_log.Text += Environment.NewLine;
                foreach (var x in r.contentRow)
                {
                    this.textBox_log.Text += x.ToString();
                    this.textBox_log.Text += "\t";
                }
                this.textBox_log.Text += Environment.NewLine;
                foreach (var y in r.chsDescription)
                {
                    this.textBox_log.Text += y.ToString();
                    this.textBox_log.Text += "\t";
                }
                this.textBox_log.Text += Environment.NewLine;
                foreach (var z in r.tableData)
                {
                    foreach (var zz in z)
                    {
                        this.textBox_log.Text += zz.ToString();
                        this.textBox_log.Text += "\t";
                    }
                    this.textBox_log.Text += Environment.NewLine;
                }
                this.textBox_log.Text += Environment.NewLine;
            }
            // 供测试用
        }

        private void button_export_Click(object sender, EventArgs e)
        {
            // 清空log
            this.textBox_log.Text = string.Empty;
            textBox_log.Text += DateTime.Now.ToString() + "\t---开始处理--- \r\n";
            // 
            
            string[] xlsFilesString = Directory.GetFiles(this.textBox_excelFolderPath.Text, "*.xls", SearchOption.AllDirectories);
            string[] xlsxFilesString = Directory.GetFiles(this.textBox_excelFolderPath.Text, "*.xlsx", SearchOption.AllDirectories);
            string[] xmlFilesString = Directory.GetFiles(this.textBox_mapFolderPath.Text, "*.xml", SearchOption.AllDirectories);


            // 将文件名转为List
            List<string> xlsFiles = new List<string>(xlsFilesString);
            List<string> xlsxFiles = new List<string>(xlsxFilesString);
            List<string> xmlFiles = new List<string>(xmlFilesString);
           
            // 罗列文件
            textBox_log.Text += DateTime.Now.ToString() + "\tExcel03文件共计：" + xlsFiles.Count.ToString() + "个。\r\n";
            textBox_log.Text += DateTime.Now.ToString() + "\tExcel07文件共计：" + xlsxFiles.Count.ToString() + "个。\r\n";
            textBox_log.Text += DateTime.Now.ToString() + "\txml文件共计：" + xmlFiles.Count.ToString() + "个。\r\n";

            List<SheetDataInfo> ret = new List<SheetDataInfo>();

            //定义excel有效数据开始行数
            int excelTableContentStartLine = 2;
            foreach(string xlsxFile in xlsxFiles)
            {
                // 将当前Excel文件读取内容放入ret中
                ret = ExcelParser.parseXlsx(xlsxFile, excelTableContentStartLine);
                // 对该内容进行比对处理
                foreach(string xmlFile in xmlFiles)
                {
                    string xmlFileNameWithoutExtention = Path.GetFileNameWithoutExtension(xmlFile);
                    // 比较XML文件名和Excel表命
                    foreach (SheetDataInfo sheet in ret)
                    {
                        if (sheet.sheetName == xmlFileNameWithoutExtention)
                        {
                            textBox_log.Text += DateTime.Now.ToString() + "\t正在处理表：" + xmlFileNameWithoutExtention + "\r\n";
                            xmlParser.LoadXmlFile(xmlFile);
                            // macro替换
                            xmlParser.CheckSheetDataInfo(sheet);
                            textBox_log.Text += DateTime.Now.ToString() + "\t表：" + xmlFileNameWithoutExtention + " 处理完毕\r\n";
                            // 对于此三重循环，有很多无意义的遍历，可以加速
                        }
                    }
                }
                // 导出
                encoder.Export(ret, this.textBox_exportFolderPath.Text, false);
            }
            // 开始导出到SQLite
            //encoder.Export(this.textBox_excelFolderPath.Text, this.textBox_exportFolderPath.Text, false);
        }

        private delegate void NameCallBack(string varText);
        public void UpdateTextBox(string input)
        {
            if (InvokeRequired)
            {
                textBox_log.BeginInvoke(new NameCallBack(UpdateTextBox), new object[] { input });
            }
            else
            {
                textBox_log.Text = input;
                // textBox.Text = textBox.Text + Environment.NewLine + input // This might work as append in next line but haven't tested so not sure
            }
        }
    }
}

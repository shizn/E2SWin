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

        private void button_export_Click(object sender, EventArgs e)
        {
            // 清空log
            this.textBox_log.Text = string.Empty;
            textBox_log.Text += DateTime.Now.ToString() + "\t---开始处理--- \r\n";
            // 
            string[] xlsFiles = Directory.GetFiles(this.textBox_excelFolderPath.Text, "*.xls", SearchOption.AllDirectories);
            string[] xlsxFiles = Directory.GetFiles(this.textBox_excelFolderPath.Text, "*.xlsx", SearchOption.AllDirectories);


            // 罗列文件
            textBox_log.Text += DateTime.Now.ToString() + "\tExcel03文件共计：" + xlsFiles.Length.ToString() + "个。\r\n";
            textBox_log.Text += DateTime.Now.ToString() + "\tExcel07文件共计：" + xlsxFiles.Length.ToString() + "个。\r\n";


            foreach(string xlsxFile in xlsxFiles)
            {

                List<SheetDataStruct> ret = ExcelParser.parseXlsx(xlsxFile, 2);

                foreach (var r in ret)
                {
                    this.textBox_log.Text += r.sheetName.ToString();
                    this.textBox_log.Text += Environment.NewLine;
                    foreach(var x in r.contentRow)
                    {
                        this.textBox_log.Text += x.ToString();
                        this.textBox_log.Text += "\t";
                    }
                    this.textBox_log.Text += Environment.NewLine;
                    foreach(var y in r.chsDescription)
                    {
                        this.textBox_log.Text += y.ToString();
                        this.textBox_log.Text += "\t";
                    }
                    this.textBox_log.Text += Environment.NewLine;
                    foreach (var z in r.tableData)
                    {
                        foreach(var zz in z)
                        {
                            this.textBox_log.Text += zz.ToString();
                            this.textBox_log.Text += "\t";
                        }
                        this.textBox_log.Text += Environment.NewLine;
                    }
                    this.textBox_log.Text += Environment.NewLine;
                }
            }


            // 开始处理
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

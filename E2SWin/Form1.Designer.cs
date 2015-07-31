namespace E2SWin
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.button_excelFolderPath = new System.Windows.Forms.Button();
            this.textBox_excelFolderPath = new System.Windows.Forms.TextBox();
            this.button_export = new System.Windows.Forms.Button();
            this.textBox_log = new System.Windows.Forms.TextBox();
            this.textBox_mapFolderPath = new System.Windows.Forms.TextBox();
            this.button_mapFolder = new System.Windows.Forms.Button();
            this.textBox_exportFolderPath = new System.Windows.Forms.TextBox();
            this.button_exportFolder = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button_excelFolderPath
            // 
            this.button_excelFolderPath.Location = new System.Drawing.Point(505, 10);
            this.button_excelFolderPath.Name = "button_excelFolderPath";
            this.button_excelFolderPath.Size = new System.Drawing.Size(75, 23);
            this.button_excelFolderPath.TabIndex = 0;
            this.button_excelFolderPath.Text = "Excel目录";
            this.button_excelFolderPath.UseVisualStyleBackColor = true;
            this.button_excelFolderPath.Click += new System.EventHandler(this.button_excelFolderPath_Click);
            // 
            // textBox_excelFolderPath
            // 
            this.textBox_excelFolderPath.Location = new System.Drawing.Point(12, 12);
            this.textBox_excelFolderPath.Name = "textBox_excelFolderPath";
            this.textBox_excelFolderPath.Size = new System.Drawing.Size(487, 21);
            this.textBox_excelFolderPath.TabIndex = 1;
            // 
            // button_export
            // 
            this.button_export.Location = new System.Drawing.Point(505, 93);
            this.button_export.Name = "button_export";
            this.button_export.Size = new System.Drawing.Size(75, 23);
            this.button_export.TabIndex = 2;
            this.button_export.Text = "导出";
            this.button_export.UseVisualStyleBackColor = true;
            this.button_export.Click += new System.EventHandler(this.button_export_Click);
            // 
            // textBox_log
            // 
            this.textBox_log.Location = new System.Drawing.Point(12, 122);
            this.textBox_log.Multiline = true;
            this.textBox_log.Name = "textBox_log";
            this.textBox_log.ReadOnly = true;
            this.textBox_log.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_log.Size = new System.Drawing.Size(568, 210);
            this.textBox_log.TabIndex = 3;
            // 
            // textBox_mapFolderPath
            // 
            this.textBox_mapFolderPath.Location = new System.Drawing.Point(12, 39);
            this.textBox_mapFolderPath.Name = "textBox_mapFolderPath";
            this.textBox_mapFolderPath.Size = new System.Drawing.Size(487, 21);
            this.textBox_mapFolderPath.TabIndex = 4;
            // 
            // button_mapFolder
            // 
            this.button_mapFolder.Location = new System.Drawing.Point(505, 37);
            this.button_mapFolder.Name = "button_mapFolder";
            this.button_mapFolder.Size = new System.Drawing.Size(75, 23);
            this.button_mapFolder.TabIndex = 5;
            this.button_mapFolder.Text = "映射目录";
            this.button_mapFolder.UseVisualStyleBackColor = true;
            // 
            // textBox_exportFolderPath
            // 
            this.textBox_exportFolderPath.Location = new System.Drawing.Point(12, 66);
            this.textBox_exportFolderPath.Name = "textBox_exportFolderPath";
            this.textBox_exportFolderPath.Size = new System.Drawing.Size(487, 21);
            this.textBox_exportFolderPath.TabIndex = 6;
            // 
            // button_exportFolder
            // 
            this.button_exportFolder.Location = new System.Drawing.Point(505, 64);
            this.button_exportFolder.Name = "button_exportFolder";
            this.button_exportFolder.Size = new System.Drawing.Size(75, 23);
            this.button_exportFolder.TabIndex = 7;
            this.button_exportFolder.Text = "导出目录";
            this.button_exportFolder.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 344);
            this.Controls.Add(this.button_exportFolder);
            this.Controls.Add(this.textBox_exportFolderPath);
            this.Controls.Add(this.button_mapFolder);
            this.Controls.Add(this.textBox_mapFolderPath);
            this.Controls.Add(this.textBox_log);
            this.Controls.Add(this.button_export);
            this.Controls.Add(this.textBox_excelFolderPath);
            this.Controls.Add(this.button_excelFolderPath);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Excel To Sqlite";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_excelFolderPath;
        private System.Windows.Forms.TextBox textBox_excelFolderPath;
        private System.Windows.Forms.Button button_export;
        private System.Windows.Forms.TextBox textBox_log;
        private System.Windows.Forms.TextBox textBox_mapFolderPath;
        private System.Windows.Forms.Button button_mapFolder;
        private System.Windows.Forms.TextBox textBox_exportFolderPath;
        private System.Windows.Forms.Button button_exportFolder;
    }
}


namespace OS_Simulation
{
    partial class Form_CFile
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.checkBox_readonly = new System.Windows.Forms.CheckBox();
            this.checkBox_readwrite = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_content = new System.Windows.Forms.TextBox();
            this.textBox_Createname = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.checkBox_readonly);
            this.panel1.Controls.Add(this.checkBox_readwrite);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.textBox_content);
            this.panel1.Controls.Add(this.textBox_Createname);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(451, 356);
            this.panel1.TabIndex = 0;
            // 
            // checkBox_readonly
            // 
            this.checkBox_readonly.AutoSize = true;
            this.checkBox_readonly.Location = new System.Drawing.Point(381, 31);
            this.checkBox_readonly.Name = "checkBox_readonly";
            this.checkBox_readonly.Size = new System.Drawing.Size(48, 16);
            this.checkBox_readonly.TabIndex = 6;
            this.checkBox_readonly.Text = "只读";
            this.checkBox_readonly.UseVisualStyleBackColor = true;
            // 
            // checkBox_readwrite
            // 
            this.checkBox_readwrite.AutoSize = true;
            this.checkBox_readwrite.Location = new System.Drawing.Point(321, 31);
            this.checkBox_readwrite.Name = "checkBox_readwrite";
            this.checkBox_readwrite.Size = new System.Drawing.Size(54, 16);
            this.checkBox_readwrite.TabIndex = 6;
            this.checkBox_readwrite.Text = "读/写";
            this.checkBox_readwrite.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "输入文件内容：";
            // 
            // textBox_content
            // 
            this.textBox_content.Location = new System.Drawing.Point(15, 79);
            this.textBox_content.Multiline = true;
            this.textBox_content.Name = "textBox_content";
            this.textBox_content.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_content.Size = new System.Drawing.Size(420, 227);
            this.textBox_content.TabIndex = 4;
            // 
            // textBox_Createname
            // 
            this.textBox_Createname.Location = new System.Drawing.Point(90, 26);
            this.textBox_Createname.MaxLength = 3;
            this.textBox_Createname.Name = "textBox_Createname";
            this.textBox_Createname.Size = new System.Drawing.Size(206, 21);
            this.textBox_Createname.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(12, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "文件名：";
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(275, 312);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(85, 31);
            this.button2.TabIndex = 1;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(103, 312);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(85, 31);
            this.button1.TabIndex = 0;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form_CFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button2;
            this.ClientSize = new System.Drawing.Size(482, 380);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Name = "Form_CFile";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "创建文件";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox_content;
        private System.Windows.Forms.TextBox textBox_Createname;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBox_readonly;
        private System.Windows.Forms.CheckBox checkBox_readwrite;
    }
}
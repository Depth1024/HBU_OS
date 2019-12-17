namespace OS_Simulation
{
    partial class Form_EFile
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
            this.textBox_name = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_content = new System.Windows.Forms.TextBox();
            this.button_确定 = new System.Windows.Forms.Button();
            this.button_取消 = new System.Windows.Forms.Button();
            this.radioButton_readwrite = new System.Windows.Forms.RadioButton();
            this.radioButton_readonly = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.button_clear = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox_name
            // 
            this.textBox_name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_name.Location = new System.Drawing.Point(70, 23);
            this.textBox_name.MaxLength = 3;
            this.textBox_name.Name = "textBox_name";
            this.textBox_name.Size = new System.Drawing.Size(145, 21);
            this.textBox_name.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "文件名";
            // 
            // textBox_content
            // 
            this.textBox_content.Location = new System.Drawing.Point(25, 67);
            this.textBox_content.Multiline = true;
            this.textBox_content.Name = "textBox_content";
            this.textBox_content.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_content.Size = new System.Drawing.Size(332, 209);
            this.textBox_content.TabIndex = 6;
            // 
            // button_确定
            // 
            this.button_确定.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button_确定.Location = new System.Drawing.Point(37, 291);
            this.button_确定.Name = "button_确定";
            this.button_确定.Size = new System.Drawing.Size(75, 23);
            this.button_确定.TabIndex = 7;
            this.button_确定.Text = "确定";
            this.button_确定.UseVisualStyleBackColor = true;
            this.button_确定.Click += new System.EventHandler(this.button_确定_Click);
            // 
            // button_取消
            // 
            this.button_取消.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_取消.Location = new System.Drawing.Point(152, 291);
            this.button_取消.Name = "button_取消";
            this.button_取消.Size = new System.Drawing.Size(75, 23);
            this.button_取消.TabIndex = 7;
            this.button_取消.Text = "取消";
            this.button_取消.UseVisualStyleBackColor = true;
            this.button_取消.Click += new System.EventHandler(this.button_取消_Click);
            // 
            // radioButton_readwrite
            // 
            this.radioButton_readwrite.AutoSize = true;
            this.radioButton_readwrite.Location = new System.Drawing.Point(230, 28);
            this.radioButton_readwrite.Name = "radioButton_readwrite";
            this.radioButton_readwrite.Size = new System.Drawing.Size(53, 16);
            this.radioButton_readwrite.TabIndex = 8;
            this.radioButton_readwrite.TabStop = true;
            this.radioButton_readwrite.Text = "读/写";
            this.radioButton_readwrite.UseVisualStyleBackColor = true;
            this.radioButton_readwrite.CheckedChanged += new System.EventHandler(this.radioButton_readwrite_CheckedChanged);
            // 
            // radioButton_readonly
            // 
            this.radioButton_readonly.AutoSize = true;
            this.radioButton_readonly.Location = new System.Drawing.Point(298, 28);
            this.radioButton_readonly.Name = "radioButton_readonly";
            this.radioButton_readonly.Size = new System.Drawing.Size(47, 16);
            this.radioButton_readonly.TabIndex = 9;
            this.radioButton_readonly.TabStop = true;
            this.radioButton_readonly.Text = "只读";
            this.radioButton_readonly.UseVisualStyleBackColor = true;
            this.radioButton_readonly.CheckedChanged += new System.EventHandler(this.radioButton_readonly_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 11;
            this.label2.Text = "文件内容：";
            // 
            // button_clear
            // 
            this.button_clear.Location = new System.Drawing.Point(274, 291);
            this.button_clear.Name = "button_clear";
            this.button_clear.Size = new System.Drawing.Size(71, 23);
            this.button_clear.TabIndex = 12;
            this.button_clear.Text = "清空";
            this.button_clear.UseVisualStyleBackColor = true;
            this.button_clear.Click += new System.EventHandler(this.button_clear_Click);
            // 
            // Form_EFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(383, 326);
            this.ControlBox = false;
            this.Controls.Add(this.button_clear);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.radioButton_readonly);
            this.Controls.Add(this.radioButton_readwrite);
            this.Controls.Add(this.button_取消);
            this.Controls.Add(this.button_确定);
            this.Controls.Add(this.textBox_content);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_name);
            this.Name = "Form_EFile";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form_EFile";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_name;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_content;
        private System.Windows.Forms.Button button_确定;
        private System.Windows.Forms.Button button_取消;
        private System.Windows.Forms.RadioButton radioButton_readwrite;
        private System.Windows.Forms.RadioButton radioButton_readonly;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button_clear;
    }
}
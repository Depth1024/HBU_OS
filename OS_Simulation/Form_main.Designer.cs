namespace OS_Simulation
{
    partial class Form_main
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox_power = new System.Windows.Forms.GroupBox();
            this.button_power = new System.Windows.Forms.Button();
            this.groupBox_storage = new System.Windows.Forms.GroupBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.groupBox_power.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox_power
            // 
            this.groupBox_power.Controls.Add(this.button_power);
            this.groupBox_power.Location = new System.Drawing.Point(1093, 12);
            this.groupBox_power.Name = "groupBox_power";
            this.groupBox_power.Size = new System.Drawing.Size(95, 64);
            this.groupBox_power.TabIndex = 0;
            this.groupBox_power.TabStop = false;
            this.groupBox_power.Text = "电源";
            // 
            // button_power
            // 
            this.button_power.BackColor = System.Drawing.Color.Aquamarine;
            this.button_power.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button_power.Location = new System.Drawing.Point(6, 14);
            this.button_power.Name = "button_power";
            this.button_power.Size = new System.Drawing.Size(83, 44);
            this.button_power.TabIndex = 0;
            this.button_power.Text = "开机";
            this.button_power.UseVisualStyleBackColor = false;
            this.button_power.Click += new System.EventHandler(this.button_power_Click);
            // 
            // groupBox_storage
            // 
            this.groupBox_storage.Location = new System.Drawing.Point(835, 98);
            this.groupBox_storage.Name = "groupBox_storage";
            this.groupBox_storage.Size = new System.Drawing.Size(337, 690);
            this.groupBox_storage.TabIndex = 1;
            this.groupBox_storage.TabStop = false;
            this.groupBox_storage.Text = "内存占用情况";
            this.groupBox_storage.Enter += new System.EventHandler(this.groupBox_storage_Enter);
            // 
            // Form_main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 800);
            this.Controls.Add(this.groupBox_storage);
            this.Controls.Add(this.groupBox_power);
            this.Name = "Form_main";
            this.Text = "Form_main";
            this.Load += new System.EventHandler(this.Form_main_Load);
            this.groupBox_power.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox_power;
        private System.Windows.Forms.Button button_power;
        private System.Windows.Forms.GroupBox groupBox_storage;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}


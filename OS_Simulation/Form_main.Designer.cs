﻿namespace OS_Simulation
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
            this.components = new System.ComponentModel.Container();
            this.groupBox_power = new System.Windows.Forms.GroupBox();
            this.button_power = new System.Windows.Forms.Button();
            this.groupBox_storage = new System.Windows.Forms.GroupBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.groupBox_result = new System.Windows.Forms.GroupBox();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.groupBox_device = new System.Windows.Forms.GroupBox();
            this.deviceA3state = new System.Windows.Forms.Label();
            this.deviceB2state = new System.Windows.Forms.Label();
            this.deviceA2state = new System.Windows.Forms.Label();
            this.ProcessnameA3 = new System.Windows.Forms.Label();
            this.ProcessnameB1 = new System.Windows.Forms.Label();
            this.ProcessnameA1 = new System.Windows.Forms.Label();
            this.ProcessnameB2 = new System.Windows.Forms.Label();
            this.ProcessnameA2 = new System.Windows.Forms.Label();
            this.deviceB1state = new System.Windows.Forms.Label();
            this.deviceA1state = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.deviceB2 = new System.Windows.Forms.Label();
            this.deviceA3 = new System.Windows.Forms.Label();
            this.deviceB1 = new System.Windows.Forms.Label();
            this.deviceA2 = new System.Windows.Forms.Label();
            this.deviceA1 = new System.Windows.Forms.Label();
            this.btn_createProcess1 = new System.Windows.Forms.Button();
            this.btn_createProcess2 = new System.Windows.Forms.Button();
            this.btn_createProcess3 = new System.Windows.Forms.Button();
            this.btn_createProcess4 = new System.Windows.Forms.Button();
            this.btn_createProcess5 = new System.Windows.Forms.Button();
            this.btn_createProcess6 = new System.Windows.Forms.Button();
            this.btn_createProcess7 = new System.Windows.Forms.Button();
            this.btn_createProcess8 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.SystmTimerLabel = new System.Windows.Forms.Label();
            this.groupBox_power.SuspendLayout();
            this.groupBox_device.SuspendLayout();
            this.groupBox1.SuspendLayout();
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
            this.groupBox_storage.Size = new System.Drawing.Size(337, 441);
            this.groupBox_storage.TabIndex = 1;
            this.groupBox_storage.TabStop = false;
            this.groupBox_storage.Text = "内存占用情况";
            this.groupBox_storage.Enter += new System.EventHandler(this.groupBox_storage_Enter);
            // 
            // groupBox_result
            // 
            this.groupBox_result.Location = new System.Drawing.Point(491, 98);
            this.groupBox_result.Name = "groupBox_result";
            this.groupBox_result.Size = new System.Drawing.Size(314, 232);
            this.groupBox_result.TabIndex = 2;
            this.groupBox_result.TabStop = false;
            this.groupBox_result.Text = "运行结果显示";
            this.groupBox_result.Enter += new System.EventHandler(this.groupBox_result_Enter);
            // 
            // groupBox_device
            // 
            this.groupBox_device.Controls.Add(this.deviceA3state);
            this.groupBox_device.Controls.Add(this.deviceB2state);
            this.groupBox_device.Controls.Add(this.deviceA2state);
            this.groupBox_device.Controls.Add(this.ProcessnameA3);
            this.groupBox_device.Controls.Add(this.ProcessnameB1);
            this.groupBox_device.Controls.Add(this.ProcessnameA1);
            this.groupBox_device.Controls.Add(this.ProcessnameB2);
            this.groupBox_device.Controls.Add(this.ProcessnameA2);
            this.groupBox_device.Controls.Add(this.deviceB1state);
            this.groupBox_device.Controls.Add(this.deviceA1state);
            this.groupBox_device.Controls.Add(this.label9);
            this.groupBox_device.Controls.Add(this.label8);
            this.groupBox_device.Controls.Add(this.label3);
            this.groupBox_device.Controls.Add(this.label7);
            this.groupBox_device.Controls.Add(this.label2);
            this.groupBox_device.Controls.Add(this.label1);
            this.groupBox_device.Controls.Add(this.deviceB2);
            this.groupBox_device.Controls.Add(this.deviceA3);
            this.groupBox_device.Controls.Add(this.deviceB1);
            this.groupBox_device.Controls.Add(this.deviceA2);
            this.groupBox_device.Controls.Add(this.deviceA1);
            this.groupBox_device.Location = new System.Drawing.Point(499, 575);
            this.groupBox_device.Name = "groupBox_device";
            this.groupBox_device.Size = new System.Drawing.Size(673, 178);
            this.groupBox_device.TabIndex = 3;
            this.groupBox_device.TabStop = false;
            this.groupBox_device.Text = "设备状态";
            this.groupBox_device.Enter += new System.EventHandler(this.groupBox_device_Enter);
            // 
            // deviceA3state
            // 
            this.deviceA3state.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.deviceA3state.Location = new System.Drawing.Point(119, 140);
            this.deviceA3state.Name = "deviceA3state";
            this.deviceA3state.Size = new System.Drawing.Size(50, 20);
            this.deviceA3state.TabIndex = 3;
            this.deviceA3state.Click += new System.EventHandler(this.deviceA3state_Click);
            // 
            // deviceB2state
            // 
            this.deviceB2state.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.deviceB2state.Location = new System.Drawing.Point(480, 100);
            this.deviceB2state.Name = "deviceB2state";
            this.deviceB2state.Size = new System.Drawing.Size(50, 20);
            this.deviceB2state.TabIndex = 3;
            this.deviceB2state.Click += new System.EventHandler(this.deviceA2state_Click);
            // 
            // deviceA2state
            // 
            this.deviceA2state.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.deviceA2state.Location = new System.Drawing.Point(119, 100);
            this.deviceA2state.Name = "deviceA2state";
            this.deviceA2state.Size = new System.Drawing.Size(50, 20);
            this.deviceA2state.TabIndex = 3;
            this.deviceA2state.Click += new System.EventHandler(this.deviceA2state_Click);
            // 
            // ProcessnameA3
            // 
            this.ProcessnameA3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ProcessnameA3.Location = new System.Drawing.Point(213, 140);
            this.ProcessnameA3.Name = "ProcessnameA3";
            this.ProcessnameA3.Size = new System.Drawing.Size(50, 20);
            this.ProcessnameA3.TabIndex = 3;
            this.ProcessnameA3.Click += new System.EventHandler(this.deviceA1state_Click);
            // 
            // ProcessnameB1
            // 
            this.ProcessnameB1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ProcessnameB1.Location = new System.Drawing.Point(574, 61);
            this.ProcessnameB1.Name = "ProcessnameB1";
            this.ProcessnameB1.Size = new System.Drawing.Size(50, 20);
            this.ProcessnameB1.TabIndex = 3;
            this.ProcessnameB1.Click += new System.EventHandler(this.deviceA1state_Click);
            // 
            // ProcessnameA1
            // 
            this.ProcessnameA1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ProcessnameA1.Location = new System.Drawing.Point(213, 61);
            this.ProcessnameA1.Name = "ProcessnameA1";
            this.ProcessnameA1.Size = new System.Drawing.Size(50, 20);
            this.ProcessnameA1.TabIndex = 3;
            this.ProcessnameA1.Click += new System.EventHandler(this.deviceA1state_Click);
            // 
            // ProcessnameB2
            // 
            this.ProcessnameB2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ProcessnameB2.Location = new System.Drawing.Point(574, 100);
            this.ProcessnameB2.Name = "ProcessnameB2";
            this.ProcessnameB2.Size = new System.Drawing.Size(50, 20);
            this.ProcessnameB2.TabIndex = 3;
            this.ProcessnameB2.Click += new System.EventHandler(this.deviceA1state_Click);
            // 
            // ProcessnameA2
            // 
            this.ProcessnameA2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ProcessnameA2.Location = new System.Drawing.Point(213, 100);
            this.ProcessnameA2.Name = "ProcessnameA2";
            this.ProcessnameA2.Size = new System.Drawing.Size(50, 20);
            this.ProcessnameA2.TabIndex = 3;
            this.ProcessnameA2.Click += new System.EventHandler(this.deviceA1state_Click);
            // 
            // deviceB1state
            // 
            this.deviceB1state.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.deviceB1state.Location = new System.Drawing.Point(480, 61);
            this.deviceB1state.Name = "deviceB1state";
            this.deviceB1state.Size = new System.Drawing.Size(50, 20);
            this.deviceB1state.TabIndex = 3;
            this.deviceB1state.Click += new System.EventHandler(this.deviceA1state_Click);
            // 
            // deviceA1state
            // 
            this.deviceA1state.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.deviceA1state.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.deviceA1state.Location = new System.Drawing.Point(119, 61);
            this.deviceA1state.Name = "deviceA1state";
            this.deviceA1state.Size = new System.Drawing.Size(50, 20);
            this.deviceA1state.TabIndex = 3;
            this.deviceA1state.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.deviceA1state.Click += new System.EventHandler(this.deviceA1state_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(561, 28);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(82, 15);
            this.label9.TabIndex = 2;
            this.label9.Text = "占用进程名";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(470, 28);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(67, 15);
            this.label8.TabIndex = 2;
            this.label8.Text = "设备状态";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(200, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "占用进程名";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(376, 28);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 15);
            this.label7.TabIndex = 1;
            this.label7.Text = "设备名称";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(109, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "设备状态";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "设备名称";
            // 
            // deviceB2
            // 
            this.deviceB2.BackColor = System.Drawing.Color.Yellow;
            this.deviceB2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.deviceB2.Location = new System.Drawing.Point(391, 100);
            this.deviceB2.Name = "deviceB2";
            this.deviceB2.Size = new System.Drawing.Size(23, 23);
            this.deviceB2.TabIndex = 0;
            this.deviceB2.Text = "B2";
            this.deviceB2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // deviceA3
            // 
            this.deviceA3.BackColor = System.Drawing.Color.Yellow;
            this.deviceA3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.deviceA3.Location = new System.Drawing.Point(30, 140);
            this.deviceA3.Name = "deviceA3";
            this.deviceA3.Size = new System.Drawing.Size(23, 23);
            this.deviceA3.TabIndex = 0;
            this.deviceA3.Text = "A3";
            this.deviceA3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // deviceB1
            // 
            this.deviceB1.BackColor = System.Drawing.Color.Yellow;
            this.deviceB1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.deviceB1.Location = new System.Drawing.Point(391, 61);
            this.deviceB1.Name = "deviceB1";
            this.deviceB1.Size = new System.Drawing.Size(23, 23);
            this.deviceB1.TabIndex = 0;
            this.deviceB1.Text = "B1";
            this.deviceB1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // deviceA2
            // 
            this.deviceA2.BackColor = System.Drawing.Color.Yellow;
            this.deviceA2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.deviceA2.Location = new System.Drawing.Point(30, 100);
            this.deviceA2.Name = "deviceA2";
            this.deviceA2.Size = new System.Drawing.Size(23, 23);
            this.deviceA2.TabIndex = 0;
            this.deviceA2.Text = "A2";
            this.deviceA2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // deviceA1
            // 
            this.deviceA1.BackColor = System.Drawing.Color.Yellow;
            this.deviceA1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.deviceA1.Location = new System.Drawing.Point(30, 61);
            this.deviceA1.Name = "deviceA1";
            this.deviceA1.Size = new System.Drawing.Size(23, 23);
            this.deviceA1.TabIndex = 0;
            this.deviceA1.Text = "A1";
            this.deviceA1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_createProcess1
            // 
            this.btn_createProcess1.Location = new System.Drawing.Point(532, 346);
            this.btn_createProcess1.Name = "btn_createProcess1";
            this.btn_createProcess1.Size = new System.Drawing.Size(90, 43);
            this.btn_createProcess1.TabIndex = 4;
            this.btn_createProcess1.Text = "Create Process1";
            this.btn_createProcess1.UseVisualStyleBackColor = true;
            this.btn_createProcess1.Click += new System.EventHandler(this.btn_createProcess1_Click);
            // 
            // btn_createProcess2
            // 
            this.btn_createProcess2.Location = new System.Drawing.Point(663, 346);
            this.btn_createProcess2.Name = "btn_createProcess2";
            this.btn_createProcess2.Size = new System.Drawing.Size(90, 43);
            this.btn_createProcess2.TabIndex = 4;
            this.btn_createProcess2.Text = "Create Process2";
            this.btn_createProcess2.UseVisualStyleBackColor = true;
            this.btn_createProcess2.Click += new System.EventHandler(this.btn_createProcess2_Click);
            // 
            // btn_createProcess3
            // 
            this.btn_createProcess3.Location = new System.Drawing.Point(532, 395);
            this.btn_createProcess3.Name = "btn_createProcess3";
            this.btn_createProcess3.Size = new System.Drawing.Size(90, 43);
            this.btn_createProcess3.TabIndex = 4;
            this.btn_createProcess3.Text = "Create Process3";
            this.btn_createProcess3.UseVisualStyleBackColor = true;
            this.btn_createProcess3.Click += new System.EventHandler(this.btn_createProcess3_Click);
            // 
            // btn_createProcess4
            // 
            this.btn_createProcess4.Location = new System.Drawing.Point(663, 395);
            this.btn_createProcess4.Name = "btn_createProcess4";
            this.btn_createProcess4.Size = new System.Drawing.Size(90, 43);
            this.btn_createProcess4.TabIndex = 4;
            this.btn_createProcess4.Text = "Create Process4";
            this.btn_createProcess4.UseVisualStyleBackColor = true;
            this.btn_createProcess4.Click += new System.EventHandler(this.btn_createProcess4_Click);
            // 
            // btn_createProcess5
            // 
            this.btn_createProcess5.Location = new System.Drawing.Point(532, 444);
            this.btn_createProcess5.Name = "btn_createProcess5";
            this.btn_createProcess5.Size = new System.Drawing.Size(90, 43);
            this.btn_createProcess5.TabIndex = 4;
            this.btn_createProcess5.Text = "Create Process5";
            this.btn_createProcess5.UseVisualStyleBackColor = true;
            this.btn_createProcess5.Click += new System.EventHandler(this.btn_createProcess5_Click);
            // 
            // btn_createProcess6
            // 
            this.btn_createProcess6.Location = new System.Drawing.Point(663, 444);
            this.btn_createProcess6.Name = "btn_createProcess6";
            this.btn_createProcess6.Size = new System.Drawing.Size(90, 43);
            this.btn_createProcess6.TabIndex = 4;
            this.btn_createProcess6.Text = "Create Process6";
            this.btn_createProcess6.UseVisualStyleBackColor = true;
            this.btn_createProcess6.Click += new System.EventHandler(this.btn_createProcess6_Click);
            // 
            // btn_createProcess7
            // 
            this.btn_createProcess7.Location = new System.Drawing.Point(532, 493);
            this.btn_createProcess7.Name = "btn_createProcess7";
            this.btn_createProcess7.Size = new System.Drawing.Size(90, 43);
            this.btn_createProcess7.TabIndex = 4;
            this.btn_createProcess7.Text = "Create Process7";
            this.btn_createProcess7.UseVisualStyleBackColor = true;
            this.btn_createProcess7.Click += new System.EventHandler(this.btn_createProcess7_Click);
            // 
            // btn_createProcess8
            // 
            this.btn_createProcess8.Location = new System.Drawing.Point(663, 493);
            this.btn_createProcess8.Name = "btn_createProcess8";
            this.btn_createProcess8.Size = new System.Drawing.Size(90, 43);
            this.btn_createProcess8.TabIndex = 4;
            this.btn_createProcess8.Text = "Create Process8";
            this.btn_createProcess8.UseVisualStyleBackColor = true;
            this.btn_createProcess8.Click += new System.EventHandler(this.btn_createProcess8_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.SystmTimerLabel);
            this.groupBox1.Location = new System.Drawing.Point(835, 20);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(148, 55);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "系统运行时间";
            // 
            // SystmTimerLabel
            // 
            this.SystmTimerLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.SystmTimerLabel.Location = new System.Drawing.Point(26, 21);
            this.SystmTimerLabel.Name = "SystmTimerLabel";
            this.SystmTimerLabel.Size = new System.Drawing.Size(100, 23);
            this.SystmTimerLabel.TabIndex = 0;
            this.SystmTimerLabel.Click += new System.EventHandler(this.label4_Click);
            // 
            // Form_main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 800);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_createProcess8);
            this.Controls.Add(this.btn_createProcess7);
            this.Controls.Add(this.btn_createProcess6);
            this.Controls.Add(this.btn_createProcess5);
            this.Controls.Add(this.btn_createProcess4);
            this.Controls.Add(this.btn_createProcess3);
            this.Controls.Add(this.btn_createProcess2);
            this.Controls.Add(this.btn_createProcess1);
            this.Controls.Add(this.groupBox_device);
            this.Controls.Add(this.groupBox_result);
            this.Controls.Add(this.groupBox_storage);
            this.Controls.Add(this.groupBox_power);
            this.Name = "Form_main";
            this.Text = "Form_main";
            this.Load += new System.EventHandler(this.Form_main_Load);
            this.groupBox_power.ResumeLayout(false);
            this.groupBox_device.ResumeLayout(false);
            this.groupBox_device.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox_power;
        private System.Windows.Forms.Button button_power;
        private System.Windows.Forms.GroupBox groupBox_storage;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.GroupBox groupBox_result;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private System.Windows.Forms.GroupBox groupBox_device;
        private System.Windows.Forms.Label deviceA3state;
        private System.Windows.Forms.Label deviceA2state;
        private System.Windows.Forms.Label deviceA1state;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label deviceA3;
        private System.Windows.Forms.Label deviceA2;
        private System.Windows.Forms.Label deviceA1;
        private System.Windows.Forms.Label deviceB2state;
        private System.Windows.Forms.Label ProcessnameA3;
        private System.Windows.Forms.Label ProcessnameB1;
        private System.Windows.Forms.Label ProcessnameA1;
        private System.Windows.Forms.Label ProcessnameB2;
        private System.Windows.Forms.Label ProcessnameA2;
        private System.Windows.Forms.Label deviceB1state;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label deviceB2;
        private System.Windows.Forms.Label deviceB1;
        private System.Windows.Forms.Button btn_createProcess1;
        private System.Windows.Forms.Button btn_createProcess2;
        private System.Windows.Forms.Button btn_createProcess3;
        private System.Windows.Forms.Button btn_createProcess4;
        private System.Windows.Forms.Button btn_createProcess5;
        private System.Windows.Forms.Button btn_createProcess6;
        private System.Windows.Forms.Button btn_createProcess7;
        private System.Windows.Forms.Button btn_createProcess8;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label SystmTimerLabel;
    }
}


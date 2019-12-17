using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Drawing.Imaging;
using System.Threading;
using System.Diagnostics;
namespace OS_Simulation
{
    partial class Form_File
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_File));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.D6 = new System.Windows.Forms.Timer(this.components);
            this.D5 = new System.Windows.Forms.Timer(this.components);
            this.D4 = new System.Windows.Forms.Timer(this.components);
            this.D3 = new System.Windows.Forms.Timer(this.components);
            this.D2 = new System.Windows.Forms.Timer(this.components);
            this.D1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.button_execute = new System.Windows.Forms.Button();
            this.pictureBox257 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox258 = new System.Windows.Forms.PictureBox();
            this.pictureBox259 = new System.Windows.Forms.PictureBox();
            this.textBox_command = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBox_path = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.createToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createDirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pastFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cutFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.delFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.processAllocate = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox257)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox258)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox259)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.processAllocate);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.treeView1);
            this.groupBox1.Controls.Add(this.button_execute);
            this.groupBox1.Controls.Add(this.pictureBox257);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.pictureBox258);
            this.groupBox1.Controls.Add(this.pictureBox259);
            this.groupBox1.Controls.Add(this.textBox_command);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.textBox_path);
            this.groupBox1.Location = new System.Drawing.Point(12, 42);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(666, 372);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "文件管理";
            // 
            // groupBox3
            // 
            this.groupBox3.Location = new System.Drawing.Point(364, 199);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(276, 154);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "本地磁盘D";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(253, 251);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(49, 21);
            this.button1.TabIndex = 12;
            this.button1.Text = "格式化";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(41, 46);
            this.treeView1.Name = "treeView1";
            this.treeView1.ShowNodeToolTips = true;
            this.treeView1.Size = new System.Drawing.Size(252, 165);
            this.treeView1.TabIndex = 0;
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick_1);
            this.treeView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeView1_MouseDown_1);
            this.treeView1.MouseHover += new System.EventHandler(this.treeView1_MouseHover);
            // 
            // button_execute
            // 
            this.button_execute.Location = new System.Drawing.Point(253, 224);
            this.button_execute.Name = "button_execute";
            this.button_execute.Size = new System.Drawing.Size(44, 21);
            this.button_execute.TabIndex = 11;
            this.button_execute.Text = "执行";
            this.button_execute.UseVisualStyleBackColor = true;
            this.button_execute.Click += new System.EventHandler(this.button_execute_Click_1);
            // 
            // pictureBox257
            // 
            this.pictureBox257.BackColor = System.Drawing.Color.Red;
            this.pictureBox257.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox257.Location = new System.Drawing.Point(421, 20);
            this.pictureBox257.Name = "pictureBox257";
            this.pictureBox257.Size = new System.Drawing.Size(15, 15);
            this.pictureBox257.TabIndex = 0;
            this.pictureBox257.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(362, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 10;
            this.label3.Text = "系统使用";
            // 
            // pictureBox258
            // 
            this.pictureBox258.BackColor = System.Drawing.Color.Blue;
            this.pictureBox258.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox258.Location = new System.Drawing.Point(515, 20);
            this.pictureBox258.Name = "pictureBox258";
            this.pictureBox258.Size = new System.Drawing.Size(15, 15);
            this.pictureBox258.TabIndex = 0;
            this.pictureBox258.TabStop = false;
            // 
            // pictureBox259
            // 
            this.pictureBox259.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pictureBox259.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox259.Location = new System.Drawing.Point(601, 19);
            this.pictureBox259.Name = "pictureBox259";
            this.pictureBox259.Size = new System.Drawing.Size(15, 15);
            this.pictureBox259.TabIndex = 0;
            this.pictureBox259.TabStop = false;
            // 
            // textBox_command
            // 
            this.textBox_command.Location = new System.Drawing.Point(41, 224);
            this.textBox_command.Name = "textBox_command";
            this.textBox_command.Size = new System.Drawing.Size(201, 21);
            this.textBox_command.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(554, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "未使用";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(456, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "用户使用";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 228);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "命令";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "路径";
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(364, 41);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(276, 152);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "本地磁盘C";
            // 
            // textBox_path
            // 
            this.textBox_path.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_path.Location = new System.Drawing.Point(41, 19);
            this.textBox_path.Name = "textBox_path";
            this.textBox_path.Size = new System.Drawing.Size(252, 21);
            this.textBox_path.TabIndex = 2;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createToolStripMenuItem,
            this.copyFileToolStripMenuItem,
            this.pastFileToolStripMenuItem,
            this.cutFileToolStripMenuItem,
            this.delFileToolStripMenuItem,
            this.editFileToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(101, 136);
            // 
            // createToolStripMenuItem
            // 
            this.createToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createFileToolStripMenuItem,
            this.createDirToolStripMenuItem});
            this.createToolStripMenuItem.Name = "createToolStripMenuItem";
            this.createToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.createToolStripMenuItem.Text = "创建";
            // 
            // createFileToolStripMenuItem
            // 
            this.createFileToolStripMenuItem.Name = "createFileToolStripMenuItem";
            this.createFileToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.createFileToolStripMenuItem.Text = "创建文件";
            this.createFileToolStripMenuItem.Click += new System.EventHandler(this.createFileToolStripMenuItem_Click);
            // 
            // createDirToolStripMenuItem
            // 
            this.createDirToolStripMenuItem.Name = "createDirToolStripMenuItem";
            this.createDirToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.createDirToolStripMenuItem.Text = "创建目录";
            this.createDirToolStripMenuItem.Click += new System.EventHandler(this.createDirToolStripMenuItem_Click);
            // 
            // copyFileToolStripMenuItem
            // 
            this.copyFileToolStripMenuItem.Name = "copyFileToolStripMenuItem";
            this.copyFileToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.copyFileToolStripMenuItem.Text = "复制";
            this.copyFileToolStripMenuItem.Click += new System.EventHandler(this.copyFileToolStripMenuItem_Click_1);
            // 
            // pastFileToolStripMenuItem
            // 
            this.pastFileToolStripMenuItem.Name = "pastFileToolStripMenuItem";
            this.pastFileToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.pastFileToolStripMenuItem.Text = "粘贴";
            this.pastFileToolStripMenuItem.Click += new System.EventHandler(this.pastFileToolStripMenuItem_Click_1);
            // 
            // cutFileToolStripMenuItem
            // 
            this.cutFileToolStripMenuItem.Name = "cutFileToolStripMenuItem";
            this.cutFileToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.cutFileToolStripMenuItem.Text = "执行";
            this.cutFileToolStripMenuItem.Click += new System.EventHandler(this.cutFileToolStripMenuItem_Click_1);
            // 
            // delFileToolStripMenuItem
            // 
            this.delFileToolStripMenuItem.Name = "delFileToolStripMenuItem";
            this.delFileToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.delFileToolStripMenuItem.Text = "删除";
            this.delFileToolStripMenuItem.Click += new System.EventHandler(this.delFileToolStripMenuItem_Click_1);
            // 
            // editFileToolStripMenuItem
            // 
            this.editFileToolStripMenuItem.Name = "editFileToolStripMenuItem";
            this.editFileToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.editFileToolStripMenuItem.Text = "编辑";
            this.editFileToolStripMenuItem.Click += new System.EventHandler(this.editFileToolStripMenuItem_Click_1);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "computer");
            this.imageList1.Images.SetKeyName(1, "menu");
            this.imageList1.Images.SetKeyName(2, "exe");
            this.imageList1.Images.SetKeyName(3, "txt");
            this.imageList1.Images.SetKeyName(4, "harddisk");
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(39, 315);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 12);
            this.label6.TabIndex = 13;
            this.label6.Text = "当前指令已存放至";
            // 
            // processAllocate
            // 
            this.processAllocate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.processAllocate.Location = new System.Drawing.Point(146, 311);
            this.processAllocate.Name = "processAllocate";
            this.processAllocate.Size = new System.Drawing.Size(100, 23);
            this.processAllocate.TabIndex = 14;
            // 
            // Form_File
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 480);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form_File";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox257)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox258)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox259)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Panel[] panel = new Panel[32];
        #region 自己添加的变量
       
        private bool tool=true;
        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer D6;
        private System.Windows.Forms.Timer D5;
        private System.Windows.Forms.Timer D4;
        private System.Windows.Forms.Timer D3;
        private System.Windows.Forms.Timer D2;
        private System.Windows.Forms.Timer D1;
        private GroupBox groupBox1;
        private TreeView treeView1;
        private Button button_execute;
        private PictureBox pictureBox257;
        private Label label3;
        private PictureBox pictureBox258;
        private PictureBox pictureBox259;
        private TextBox textBox_command;
        private Label label5;
        private Label label4;
        private Label label2;
        private Label label1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private TextBox textBox_path;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem createToolStripMenuItem;
        private ToolStripMenuItem createFileToolStripMenuItem;
        private ToolStripMenuItem createDirToolStripMenuItem;
        private ToolStripMenuItem copyFileToolStripMenuItem;
        private ToolStripMenuItem pastFileToolStripMenuItem;
        private ToolStripMenuItem cutFileToolStripMenuItem;
        private ToolStripMenuItem delFileToolStripMenuItem;
        private ToolStripMenuItem editFileToolStripMenuItem;
        private ImageList imageList1;
        private Button button1;
        private Label processAllocate;
        private Label label6;
    }
}


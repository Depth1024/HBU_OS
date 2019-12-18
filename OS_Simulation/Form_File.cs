using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace OS_Simulation
{
    
    public partial class Form_File : Form
    {
        public Form_File()
        {
            InitializeComponent();

            OS_Simulation.FileFunction newfile = new OS_Simulation.FileFunction();
            newfile.ReadFile(treeView1, contextMenuStrip1, imageList1);
            newfile.DrawDisk(groupBox2, "disk1.txt");  //磁盘显示
            newfile.DrawDisk(groupBox3, "disk2.txt");  //磁盘显示

        }


        private void Form1_Load(object sender, EventArgs e)
        {

            this.groupBox1.Enabled = true;

        }



        #region 成员变量

        public string cpathname1;
        public string cpathname2;
        public int flag = 0;
        public OS_Simulation.FCB buffer = new OS_Simulation.FCB();
        public string filecontent;

        #endregion


        #region 建立文件
        private void createFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OS_Simulation.FileFunction newfile = new OS_Simulation.FileFunction();
            OS_Simulation.Form_CFile formfile = new OS_Simulation.Form_CFile();
            DialogResult result = formfile.ShowDialog();
            if (result == DialogResult.OK)
            {
                string name = formfile.filename;
                byte attribute = formfile.attribute;
                byte[] content = formfile.filecontent;
                string pathname = newfile.GetPathname(treeView1.SelectedNode.FullPath);
                string[] names = pathname.Split(new char[] { '\\', '.' });    // \\表示的是字符"\"（斜杠）。这是一个转义符。
                string harddisk = "";
                if (names[0] == "c:" || names[0] == "C:")
                {
                    harddisk = "disk1.txt";
                }

                if (names[0] == "d:" || names[0] == "D:")
                {
                    harddisk = "disk2.txt";
                }

                string fullpathname = pathname + "\\" + name + ".e";
                int n = content.Length / 64;   //文件所需盘块的数量
                if (content.Length % 64 != 0)
                {
                    n = n + 1;
                }
                int[] disknum = null;
                if (n == 0)
                {
                    disknum = newfile.SearchFAT(1, harddisk);
                }
                else
                {
                    disknum = newfile.SearchFAT(n, harddisk);   //找空盘块,记录盘块号
                }

                if (pathname.Length != 2)
                {
                    if (newfile.Search(pathname, harddisk) == 2)
                    {
                        MessageBox.Show("不能在文件下建立目录！", "注意", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    else
                    {
                        if (n == 0)   //文件没有内容
                        {
                            if (newfile.Search(fullpathname, harddisk) != 2)
                            {
                                newfile.CreateFile(fullpathname, attribute, Convert.ToByte(disknum[0]), Convert.ToChar(1), harddisk);
                                newfile.RecordFileFAT(disknum, harddisk);        //记录FAT表   

                            }
                            else
                            {
                                MessageBox.Show("文件存在！", "注意", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                        }
                        else
                        {

                            if (newfile.Search(fullpathname, harddisk) != 2)
                            {
                                newfile.CreateFile(fullpathname, attribute, Convert.ToByte(disknum[0]), Convert.ToChar(n), harddisk);
                                newfile.RecordFileFAT(disknum, harddisk);   //记录文件的FAT表项
                                newfile.WriteContent(disknum, content, harddisk);  //文件的内容写入磁盘    

                            }
                            else
                            {
                                MessageBox.Show("文件存在！", "注意", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                        }
                    }
                }
                else
                {
                    if (n == 0)    //文件没有内容
                    {

                        if (newfile.Search(fullpathname, harddisk) != 2)
                        {
                            newfile.CreateFile(fullpathname, attribute, Convert.ToByte(disknum[0]), Convert.ToChar(1), harddisk);
                            newfile.RecordFileFAT(disknum, harddisk);        //记录FAT表   

                        }
                        else
                        {
                            MessageBox.Show("文件存在！", "注意", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    else
                    {

                        if (newfile.Search(fullpathname, harddisk) != 2)
                        {
                            newfile.CreateFile(fullpathname, attribute, Convert.ToByte(disknum[0]), Convert.ToChar(disknum.Length), harddisk);
                            newfile.RecordFileFAT(disknum, harddisk);   //记录文件的FAT表项
                            newfile.WriteContent(disknum, content, harddisk);  //文件的内容写入磁盘  

                        }
                    }
                }
                newfile.ReadFile(treeView1, contextMenuStrip1, imageList1);
                newfile.DrawDisk(groupBox2, "disk1.txt");
                newfile.DrawDisk(groupBox3, "disk2.txt");
            }
            if (result == DialogResult.Cancel)
            {

            }

        }
        #endregion


        #region Format(格式化函数)
        public void Format(string diskname)
        {
            //建立两个磁盘disk1和disk2
            FileStream diskCD = new FileStream(diskname, FileMode.OpenOrCreate);
            byte[] disk = new byte[8192];
            for (int i = 0; i < 8192; i++)
            {
                disk[i] = 0;
            }
            diskCD.Seek(0, SeekOrigin.Begin);
            diskCD.Write(disk, 0, 8192);
            byte[] FAT_CD = new byte[128];
            FAT_CD[0] = FAT_CD[1] = FAT_CD[2] = 255;
            for (int i = 3; i < 128; i++)
            {
                FAT_CD[i] = 0;
            }
            diskCD.Seek(0, SeekOrigin.Begin);
            diskCD.Write(FAT_CD, 0, 128);
            diskCD.Close();
            OS_Simulation.FileFunction newfile = new OS_Simulation.FileFunction();
            newfile.ReadFile(treeView1, contextMenuStrip1, imageList1);
        }
        #endregion


        #region 命令接口
        private void button_execute_Click(object sender, EventArgs e)
        {
            if (textBox_command.Text == "")
            {
                MessageBox.Show("请输入命令和文件路径", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //分割路径
            string[] command_pathname = textBox_command.Text.Split(new char[] { ' ' });
            string command = command_pathname[0];
            string pathname = command_pathname[1];
            string[] names = pathname.Split(new char[] { '\\', '.' });
            OS_Simulation.FileFunction newfile = new OS_Simulation.FileFunction();

            string harddisk = "";
            if (names[0] == "c:" || names[0] == "C:")
            {
                harddisk = "disk1.txt";
            }

            if (names[0] == "d:" || names[0] == "D:")
            {
                harddisk = "disk2.txt";
            }

            if (command == "create")
            {
                int[] disknum = newfile.SearchFAT(1, harddisk);  //查找一个空盘块分配给文件


                byte attribute = new byte();
                if (names[names.Length - 1].Length == 1 && Convert.ToChar(names[names.Length - 1]) == 'e')
                {
                    attribute = 2;
                }
                else if (names[names.Length - 1].Length == 1 && Convert.ToChar(names[names.Length - 1]) == 't')
                {
                    attribute = 4;
                }
                else
                {
                    attribute = 8;
                }
                switch (attribute)
                {
                    case 2:
                    case 4:

                        if (newfile.Search(pathname, harddisk) != 2)
                        {
                            newfile.CreateFile(pathname, attribute, Convert.ToByte(disknum[0]), Convert.ToChar(1), harddisk);  //建立exe文件或txt文件
                            newfile.RecordFileFAT(disknum, harddisk);        //记录FAT表    

                        }
                        else
                        {
                            MessageBox.Show("文件存在！", "注意", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        break;
                    case 8:
                        newfile.CreateMenu(pathname, harddisk);             //建立目录

                        break;
                }
            }

            else if (command == "delete")
            {
                if (names[names.Length - 1].Length == 1)
                {
                    newfile.DeleteFile(pathname, harddisk);

                }
                else
                {
                    newfile.DeleteMenu(pathname, harddisk);

                }
            }

            else if (command == "edit")
            {
            found:
                if (names[0] == "c:" || names[0] == "C:")
                {
                    harddisk = "disk1.txt";
                }

                if (names[0] == "d:" || names[0] == "D:")
                {
                    harddisk = "disk2.txt";
                }

                if (pathname.Length == 2)   //在本地C盘和D盘上编辑
                {
                    return;
                }

                string halfpathname = pathname.Remove(pathname.Length - 6);
                char attribute = Convert.ToChar(names[names.Length - 1]);
                int disknum;
                UTF8Encoding utf = new UTF8Encoding();
                byte[] name = utf.GetBytes(names[names.Length - 2]);
                if (names.Length == 3)   //例c:\aaa.t
                {
                    disknum = 3;
                }
                else
                {
                    disknum = newfile.Search(halfpathname, harddisk);
                }
                int item = newfile.FindItem(disknum, name, attribute, harddisk)[0];
                int address = newfile.FindItem(disknum, name, attribute, harddisk)[1];
                buffer = newfile.ReadFCB(disknum, item, harddisk);       //获取文件的FCB信息
                int[] dnums = newfile.FindDiskNumber(buffer.Address, harddisk);  //找到文件占用的盘块返回整型数组
                FileStream Disk = new FileStream(harddisk, FileMode.Open);
                byte[] content = new byte[64 * dnums.Length];
                for (int i = 0; i < dnums.Length; i++)
                {
                    Disk.Seek(64 * (dnums[i] - 1), SeekOrigin.Begin);
                    Disk.Read(content, 64 * i, 64);
                }
                Disk.Close();

                filecontent = utf.GetString(content);
                Form_EFile formfile = new Form_EFile(filecontent, buffer.Attribute, buffer.Name);
                DialogResult result = formfile.ShowDialog();

                if (result == DialogResult.OK && formfile.flag == 1)
                {
                    //修改FCB信息
                    buffer.Name = formfile.buffer.Name;
                    buffer.Type = formfile.buffer.Type;
                    buffer.Attribute = formfile.buffer.Attribute;
                    buffer.Length = Convert.ToChar(formfile.number);

                    string newname = "";
                    if (buffer.Attribute == 2 || buffer.Attribute == 3)
                    {
                        newname = utf.GetString(buffer.Name) + ".exe";
                    }
                    else
                    {
                        newname = utf.GetString(buffer.Name) + ".txt";
                    }
                    if (newfile.SearchPreNode(treeView1.SelectedNode, newname) == 1 || newfile.SearchNextNode(treeView1.SelectedNode, newname) == 1)
                    {
                        return;
                    }
                    newfile.DeleteFile(pathname, harddisk);   //删除原文件
                    int[] newdnums = new int[formfile.number];
                    newdnums = newfile.SearchFAT(formfile.number, harddisk);   //寻找空盘块
                    newfile.RecordFileFAT(newdnums, harddisk);                             //记录FAT 
                    newfile.WriteFile(disknum, item, buffer, harddisk);             //把新的FCB信息写入目录项
                    newfile.WriteContent(newdnums, formfile.filecontent, harddisk);         //文件内容写入磁盘
                    newfile.ReadFile(treeView1, contextMenuStrip1, imageList1);
                    newfile.DrawDisk(groupBox2, "disk1.txt");
                    newfile.DrawDisk(groupBox3, "disk2.txt");
                }
                if (result == DialogResult.OK && formfile.flag == 0)
                {
                    goto found;
                }
                if (result == DialogResult.Cancel)
                {

                }
            }

            else if (command == "copy")
            {
                string cpathname1 = command_pathname[1];
                string cpathname2 = command_pathname[3];
                string[] names1 = cpathname1.Split(new char[] { '\\', '.' });
                string[] names2 = cpathname2.Split(new char[] { '\\', '.' });
                string harddisk1 = "";
                string harddisk2 = "";
                if (names1[0] == "c:" || names1[0] == "C:")
                {
                    harddisk1 = "disk1.txt";
                }

                if (names1[0] == "d:" || names1[0] == "D:")
                {
                    harddisk1 = "disk2.txt";
                }

                if (string.Equals(names1[0], names2[0]))
                {
                    harddisk2 = harddisk1;
                }
                else
                {
                    if (harddisk1 == "disk1.txt")
                    {
                        harddisk2 = "disk2.txt";
                    }
                    else
                    {
                        harddisk2 = "disk1.txt";
                    }
                }
                newfile.DeepCopyFile(command_pathname[1], command_pathname[3], harddisk1, harddisk2);
            }
          
            else if (command == "cut")
            {
                #region rubbish
                /*
              string cpathname1 = command_pathname[1];
              string cpathname2 = command_pathname[3];
              string[] names1 = cpathname1.Split(new char[] { '\\', '.' });
              string[] names2 = cpathname2.Split(new char[] { '\\', '.' });
              string harddisk1 = "";
              string harddisk2 = "";
              int flag = 0;
              if (names1[0] == "c:" || names1[0] == "C:")
              {
                  harddisk1 = "disk1.txt";
              }

              if (names1[0] == "d:" || names1[0] == "D:")
              {
                  harddisk1 = "disk2.txt";
              }

              if (string.Equals(names1[0], names2[0]))
              {
                  harddisk2 = harddisk1;
                  flag = 1;  //盘内剪切
              }
              else
              {
                  flag = 2;     //盘间剪切
                  if (harddisk1 == "disk1.txt")
                  {
                      harddisk2 = "disk2.txt";
                  }
                  else
                  {
                      harddisk2 = "disk1.txt";
                  }
              }
              if (flag == 1)
              {
                  newfile.CutFile(command_pathname[1], command_pathname[3], harddisk1);
              }
              if (flag == 2)
              {
                  newfile.DeepCopyFile(command_pathname[1], command_pathname[3], harddisk1, harddisk2);
                  newfile.DeleteFile(command_pathname[1], harddisk1);
              }
                */
                #endregion
                
            }

            else
                {
                MessageBox.Show("无法识别的命令!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            newfile.ReadFile(treeView1, contextMenuStrip1, imageList1);
            newfile.DrawDisk(groupBox2, "disk1.txt");
            newfile.DrawDisk(groupBox3, "disk2.txt");
            textBox_command.Focus();

        }
        #endregion

        private void textBox_command_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                button_execute.Focus();
            }
        }

        private void treeView1_MouseDown_1(object sender, MouseEventArgs e)
        {
            treeView1.SelectedNode = treeView1.GetNodeAt(e.X, e.Y);
        }

        private void treeView1_NodeMouseClick_1(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (treeView1.SelectedNode == null)
            {
                return;
            }
            textBox_path.Text = treeView1.SelectedNode.FullPath;
        }

        private void treeView1_MouseHover(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode == null)
            {
                return;
            }
            this.treeView1.SelectedNode.ToolTipText = treeView1.SelectedNode.FullPath;
        }

        private void editFileToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
        found:
            OS_Simulation.FileFunction newfile = new OS_Simulation.FileFunction();
            string pathname = newfile.GetPathname(treeView1.SelectedNode.FullPath);
            string[] pnames = pathname.Split(new char[] { '\\', '.' });
            string harddisk = "";
            if (pnames[0] == "c:" || pnames[0] == "C:")
            {
                harddisk = "disk1.txt";
            }

            if (pnames[0] == "d:" || pnames[0] == "D:")
            {
                harddisk = "disk2.txt";
            }

            if (pathname.Length == 2)   //在本地C盘和D盘上编辑
            {
                return;
            }

            string halfpathname = pathname.Remove(pathname.Length - 6);
            char attribute = Convert.ToChar(pnames[pnames.Length - 1]);
            int disknum;
            UTF8Encoding utf = new UTF8Encoding();
            byte[] name = utf.GetBytes(pnames[pnames.Length - 2]);
            if (pnames.Length == 3)   //例c:\aaa.t
            {
                disknum = 3;
            }
            else
            {
                disknum = newfile.Search(halfpathname, harddisk);
            }
            int item = newfile.FindItem(disknum, name, attribute, harddisk)[0];
            int address = newfile.FindItem(disknum, name, attribute, harddisk)[1];
            buffer = newfile.ReadFCB(disknum, item, harddisk);       //获取文件的FCB信息
            int[] dnums = newfile.FindDiskNumber(buffer.Address, harddisk);  //找到文件占用的盘块返回整型数组
            FileStream Disk = new FileStream(harddisk, FileMode.Open);
            byte[] content = new byte[64 * dnums.Length];
            for (int i = 0; i < dnums.Length; i++)
            {
                Disk.Seek(64 * (dnums[i] - 1), SeekOrigin.Begin);
                Disk.Read(content, 64 * i, 64);
            }
            Disk.Close();

            filecontent = utf.GetString(content);

            Form_EFile formfile = new Form_EFile(filecontent, buffer.Attribute, buffer.Name);
            DialogResult result = formfile.ShowDialog();

            if (result == DialogResult.OK && formfile.flag == 1)
            {


                string newname = "";
                if (buffer.Attribute == 2 || buffer.Attribute == 3)
                {
                    newname = utf.GetString(buffer.Name) + ".e";
                }

                if (newfile.SearchPreNode(treeView1.SelectedNode, newname) == 1 || newfile.SearchNextNode(treeView1.SelectedNode, newname) == 1)
                {
                    return;
                }
                newfile.DeleteFile(pathname, harddisk);   //删除原文件
                int[] newdnums = new int[formfile.number];
                newdnums = newfile.SearchFAT(formfile.number, harddisk);   //寻找空盘块

                //修改FCB信息
                buffer.Name = formfile.buffer.Name;
                buffer.Type = formfile.buffer.Type;
                buffer.Attribute = formfile.buffer.Attribute;
                buffer.Length = Convert.ToChar(newdnums.Length);

                newfile.RecordFileFAT(newdnums, harddisk);                             //记录FAT 
                newfile.WriteFile(disknum, item, buffer, harddisk);             //把新的FCB信息写入目录项
                newfile.WriteContent(newdnums, formfile.filecontent, harddisk);         //文件内容写入磁盘
                newfile.ReadFile(treeView1, contextMenuStrip1, imageList1);
                newfile.DrawDisk(groupBox2, "disk1.txt");
                newfile.DrawDisk(groupBox3, "disk2.txt");
            }
            if (result == DialogResult.OK && formfile.flag == 0)
            {
                goto found;
            }
            if (result == DialogResult.Cancel)
            {

            }
        }

       

        private void delFileToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            OS_Simulation.FileFunction delfile = new OS_Simulation.FileFunction();
            string pathname = delfile.GetPathname(treeView1.SelectedNode.FullPath);
            string[] names = pathname.Split(new char[] { '\\', '.' });
            string harddisk = "";
            if (names[0] == "c:" || names[0] == "C:")
            {
                harddisk = "disk1.txt";
            }

            if (names[0] == "d:" || names[0] == "D:")
            {
                harddisk = "disk2.txt";
            }

            string[] name = pathname.Split(new char[] { '\\' });

            if (name[name.Length - 1].Length == 3)
            {
                delfile.DeleteMenu(pathname, harddisk);

            }
            else
            {
                delfile.DeleteFile(pathname, harddisk);

            }
            delfile.ReadFile(treeView1, contextMenuStrip1, imageList1);
            delfile.DrawDisk(groupBox3, "disk2.txt");
            delfile.DrawDisk(groupBox2, "disk1.txt");
        }

        private void copyFileToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            OS_Simulation.FileFunction newfile = new OS_Simulation.FileFunction();
            cpathname1 = newfile.GetPathname(treeView1.SelectedNode.FullPath);
            flag = 1;
        }

        private void pastFileToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            OS_Simulation.FileFunction copyfile = new OS_Simulation.FileFunction();
            cpathname2 = copyfile.GetPathname(treeView1.SelectedNode.FullPath);
            string[] names1 = cpathname1.Split(new char[] { '\\', '.' });
            string[] names2 = cpathname2.Split(new char[] { '\\', '.' });
            string harddisk1 = "";
            string harddisk2 = "";
            if (names1[0] == "c:" || names1[0] == "C:")
            {
                harddisk1 = "disk1.txt";
            }

            if (names1[0] == "d:" || names1[0] == "D:")
            {
                harddisk1 = "disk2.txt";
            }

            if (string.Equals(names1[0], names2[0]))
            {
                harddisk2 = harddisk1;
            }
            else
            {
                if (harddisk1 == "disk1.txt")
                {
                    harddisk2 = "disk2.txt";
                }
                else
                {
                    harddisk2 = "disk1.txt";
                }
            }

            if (flag == 1)
            {
                copyfile.DeepCopyFile(cpathname1, cpathname2, harddisk1, harddisk2);
            }
            if (flag == 2 && string.Equals(names1[0], names2[0]))  //盘内剪切
            {
                copyfile.CutFile(cpathname1, cpathname2, harddisk1);
            }
            if (flag == 2 && !string.Equals(names1[0], names2[0]))  //磁盘间的剪切
            {
                copyfile.DeepCopyFile(cpathname1, cpathname2, harddisk1, harddisk2);
                copyfile.DeleteFile(cpathname1, harddisk1);
            }
            copyfile.ReadFile(treeView1, contextMenuStrip1, imageList1);
            copyfile.DrawDisk(groupBox2, "disk1.txt");
            copyfile.DrawDisk(groupBox3, "disk2.txt");
        }
        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        public string[] instructions = new string[8];
        
        private void cutFileToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            
            OS_Simulation.FileFunction newfile = new OS_Simulation.FileFunction();
            string pathname = newfile.GetPathname(treeView1.SelectedNode.FullPath);
            string[] pnames = pathname.Split(new char[] { '\\', '.' });
            string harddisk = "";
            if (pnames[0] == "c:" || pnames[0] == "C:")
            {
                harddisk = "disk1.txt";
            }

            if (pnames[0] == "d:" || pnames[0] == "D:")
            {
                harddisk = "disk2.txt";
            }

            if (pathname.Length == 2)   //在本地C盘和D盘上编辑
            {
                return;
            }

            string halfpathname = pathname.Remove(pathname.Length - 6);
            char attribute = Convert.ToChar(pnames[pnames.Length - 1]);
            int disknum;
            UTF8Encoding utf = new UTF8Encoding();
            byte[] name = utf.GetBytes(pnames[pnames.Length - 2]);
            if (pnames.Length == 3)   //例c:\aaa.t
            {
                disknum = 3;
            }
            else
            {
                disknum = newfile.Search(halfpathname, harddisk);
            }
            int item = newfile.FindItem(disknum, name, attribute, harddisk)[0];
            int address = newfile.FindItem(disknum, name, attribute, harddisk)[1];
            buffer = newfile.ReadFCB(disknum, item, harddisk);       //获取文件的FCB信息
            int[] dnums = newfile.FindDiskNumber(buffer.Address, harddisk);  //找到文件占用的盘块返回整型数组
            FileStream Disk = new FileStream(harddisk, FileMode.Open);
            byte[] content = new byte[64 * dnums.Length];
            for (int i = 0; i < dnums.Length; i++)
            {
                Disk.Seek(64 * (dnums[i] - 1), SeekOrigin.Begin);
                Disk.Read(content, 64 * i, 64);
            }
            Disk.Close();

            filecontent = utf.GetString(content);
            string str = "";
            int m = 0;
            while(filecontent[m] != '\0')
            {
                str = str + filecontent[m];
                m++;
            }
            

            for(int i = 0; i < 8; i++)
            {
                if(instructions[i] == null)
                {
                    instructions[i] = "null";
                }
            }

            int flag = 0;
            for(int i = 0; i < 8; i++)
            {
                if(instructions[i] == "null")
                {
                    instructions[i] =str;
                    //MessageBox.Show("已分配给Process"+(i+1) , "执行成功", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                    processAllocate.Text = "Process" + (i + 1);
                    flag = 1;
                    break;
                }
            }
            if(flag == 0)
            {
                MessageBox.Show("指令数量达到上限", "执行失败", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            }
        }
        
        /// <summary>
        /// 格式化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click_1(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("警告：格式化将删除磁盘上的所有数据。若想格式化该磁盘，请单击“确定”。若想退出，请单击“取消”。", "格式化 本地磁盘", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.OK)
            {
                Format("disk1.txt");
                Format("disk2.txt");
            }
            OS_Simulation.FileFunction newfile = new OS_Simulation.FileFunction();
            newfile.DrawDisk(groupBox2, "disk1.txt");
            newfile.DrawDisk(groupBox3, "disk2.txt");
        }
        /// <summary>
        /// 创建目录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void createDirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = new DialogResult();
            OS_Simulation.FileFunction newfile = new OS_Simulation.FileFunction();
            OS_Simulation.Form_CMenu formmenu = new OS_Simulation.Form_CMenu();
            result = formmenu.ShowDialog();
            if (result == DialogResult.OK)
            {
                string name = formmenu.menuname;
                string pathname = newfile.GetPathname(treeView1.SelectedNode.FullPath);
                string fullpathname = pathname + "\\" + name;
                string[] names = pathname.Split(new char[] { '\\', '.' });
                string harddisk = "";
                if (names[0] == "c:" || names[0] == "C:")
                {
                    harddisk = "disk1.txt";
                }

                if (names[0] == "d:" || names[0] == "D:")
                {
                    harddisk = "disk2.txt";
                }

                if (pathname.Length != 2)
                {
                    if (newfile.Search(pathname, harddisk) == 2)
                    {
                        MessageBox.Show("不能在文件下建立目录！", "注意", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    else
                    {
                        newfile.CreateMenu(fullpathname, harddisk);

                    }
                }
                else
                {
                    newfile.CreateMenu(fullpathname, harddisk);

                }
                newfile.ReadFile(treeView1, contextMenuStrip1, imageList1);
                newfile.DrawDisk(groupBox2, "disk1.txt");
                newfile.DrawDisk(groupBox3, "disk2.txt");
            }
            if (result == DialogResult.Cancel)
            {

            }
        }


        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_execute_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (textBox_command.Text == "")
                {
                    MessageBox.Show("请输入命令和文件路径", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                //分割路径
                string[] command_pathname = textBox_command.Text.Split(new char[] { ' ' });
                string command = command_pathname[0];
                string pathname = command_pathname[1];
                string[] names = pathname.Split(new char[] { '\\', '.' });
                FileFunction newfile = new FileFunction();

                string harddisk = "";
                if (names[0] == "c:" || names[0] == "C:")
                {
                    harddisk = "disk1.txt";
                }
                if (names[0] == "d:" || names[0] == "D:")
                {
                    harddisk = "disk2.txt";
                }
                if (command == "create")
                {
                    int[] disknum = newfile.SearchFAT(1, harddisk);  //查找一个空盘块分配给文件
                    byte attribute = new byte();
                    if (names[names.Length - 1].Length == 1 && Convert.ToChar(names[names.Length - 1]) == 'e')
                    {
                        attribute = 2;
                    }
                    else
                    {
                        attribute = 8;
                    }
                    switch (attribute)
                    {
                        case 2:
                        case 4:
                            if (newfile.Search(pathname, harddisk) != 2)
                            {
                                newfile.CreateFile(pathname, attribute, Convert.ToByte(disknum[0]), Convert.ToChar(1), harddisk);  //建立exe文件
                                newfile.RecordFileFAT(disknum, harddisk);        //记录FAT表    

                            }
                            else
                            {
                                MessageBox.Show("文件存在！", "注意", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                            break;
                        case 8:
                            newfile.CreateMenu(pathname, harddisk);             //建立目录

                            break;
                    }
                }
                else if (command == "delete")
                {
                    byte attribute = new byte();
                    if (names[names.Length - 1].Length == 1 && Convert.ToChar(names[names.Length - 1]) == 'e')
                    {
                        attribute = 2;
                    }
                    else
                    {
                        attribute = 8;
                    }
                    if (names[names.Length - 1].Length == 1)
                    {
                        newfile.DeleteFile(pathname, harddisk);

                    }
                    else
                    {
                        newfile.DeleteMenu(pathname, harddisk);

                    }
                }

                else if (command == "edit")
                {
                found:
                    if (names[0] == "c:" || names[0] == "C:")
                    {
                        harddisk = "disk1.txt";
                    }

                    if (names[0] == "d:" || names[0] == "D:")
                    {
                        harddisk = "disk2.txt";
                    }

                    if (pathname.Length == 2)   //在本地C盘和D盘上编辑
                    {
                        return;
                    }

                    string halfpathname = pathname.Remove(pathname.Length - 6);
                    char attribute = Convert.ToChar(names[names.Length - 1]);
                    int disknum;
                    UTF8Encoding utf = new UTF8Encoding();
                    byte[] name = utf.GetBytes(names[names.Length - 2]);
                    if (names.Length == 3)   //例c:\aaa.t
                    {
                        disknum = 3;
                    }
                    else
                    {
                        disknum = newfile.Search(halfpathname, harddisk);
                    }
                    int item = newfile.FindItem(disknum, name, attribute, harddisk)[0];
                    int address = newfile.FindItem(disknum, name, attribute, harddisk)[1];
                    buffer = newfile.ReadFCB(disknum, item, harddisk);       //获取文件的FCB信息
                    int[] dnums = newfile.FindDiskNumber(buffer.Address, harddisk);  //找到文件占用的盘块返回整型数组
                    FileStream Disk = new FileStream(harddisk, FileMode.Open);
                    byte[] content = new byte[64 * dnums.Length];
                    for (int i = 0; i < dnums.Length; i++)
                    {
                        Disk.Seek(64 * (dnums[i] - 1), SeekOrigin.Begin);
                        Disk.Read(content, 64 * i, 64);
                    }
                    Disk.Close();

                    filecontent = utf.GetString(content);
                    Form_EFile formfile = new Form_EFile(filecontent, buffer.Attribute, buffer.Name);
                    DialogResult result = formfile.ShowDialog();

                    if (result == DialogResult.OK && formfile.flag == 1)
                    {
                        //修改FCB信息
                        buffer.Name = formfile.buffer.Name;
                        buffer.Type = formfile.buffer.Type;
                        buffer.Attribute = formfile.buffer.Attribute;
                        buffer.Length = Convert.ToChar(formfile.number);

                        string newname = "";
                        if (buffer.Attribute == 2 || buffer.Attribute == 3)
                        {
                            newname = utf.GetString(buffer.Name) + ".e";
                        }
                        if (newfile.SearchPreNode(treeView1.SelectedNode, newname) == 1 || newfile.SearchNextNode(treeView1.SelectedNode, newname) == 1)
                        {
                            return;
                        }
                        newfile.DeleteFile(pathname, harddisk);   //删除原文件
                        int[] newdnums = new int[formfile.number];
                        newdnums = newfile.SearchFAT(formfile.number, harddisk);   //寻找空盘块
                        newfile.RecordFileFAT(newdnums, harddisk);                             //记录FAT 
                        newfile.WriteFile(disknum, item, buffer, harddisk);             //把新的FCB信息写入目录项
                        newfile.WriteContent(newdnums, formfile.filecontent, harddisk);         //文件内容写入磁盘
                        newfile.ReadFile(treeView1, contextMenuStrip1, imageList1);
                        newfile.DrawDisk(groupBox2, "disk1.txt");
                        newfile.DrawDisk(groupBox3, "disk2.txt");
                    }
                    if (result == DialogResult.OK && formfile.flag == 0)
                    {
                        goto found;
                    }
                    if (result == DialogResult.Cancel)
                    {

                    }
                }

                else if (command == "copy")
                {
                    string cpathname1 = command_pathname[1];
                    string cpathname2 = command_pathname[3];
                    string[] names1 = cpathname1.Split(new char[] { '\\', '.' });
                    string[] names2 = cpathname2.Split(new char[] { '\\', '.' });
                    string harddisk1 = "";
                    string harddisk2 = "";
                    if (names1[0] == "c:" || names1[0] == "C:")
                    {
                        harddisk1 = "disk1.txt";
                    }

                    if (names1[0] == "d:" || names1[0] == "D:")
                    {
                        harddisk1 = "disk2.txt";
                    }

                    if (string.Equals(names1[0], names2[0]))
                    {
                        harddisk2 = harddisk1;
                    }
                    else
                    {
                        if (harddisk1 == "disk1.txt")
                        {
                            harddisk2 = "disk2.txt";
                        }
                        else
                        {
                            harddisk2 = "disk1.txt";
                        }
                    }
                    newfile.DeepCopyFile(command_pathname[1], command_pathname[3], harddisk1, harddisk2);
                }

                else if (command == "cut")
                {
                    string cpathname1 = command_pathname[1];
                    string cpathname2 = command_pathname[3];
                    string[] names1 = cpathname1.Split(new char[] { '\\', '.' });
                    string[] names2 = cpathname2.Split(new char[] { '\\', '.' });
                    string harddisk1 = "";
                    string harddisk2 = "";
                    int flag = 0;
                    if (names1[0] == "c:" || names1[0] == "C:")
                    {
                        harddisk1 = "disk1.txt";
                    }

                    if (names1[0] == "d:" || names1[0] == "D:")
                    {
                        harddisk1 = "disk2.txt";
                    }

                    if (string.Equals(names1[0], names2[0]))
                    {
                        harddisk2 = harddisk1;
                        flag = 1;  //盘内剪切
                    }
                    else
                    {
                        flag = 2;     //盘间剪切
                        if (harddisk1 == "disk1.txt")
                        {
                            harddisk2 = "disk2.txt";
                        }
                        else
                        {
                            harddisk2 = "disk1.txt";
                        }
                    }
                    if (flag == 1)
                    {
                        newfile.CutFile(command_pathname[1], command_pathname[3], harddisk1);
                    }
                    if (flag == 2)
                    {
                        newfile.DeepCopyFile(command_pathname[1], command_pathname[3], harddisk1, harddisk2);
                        newfile.DeleteFile(command_pathname[1], harddisk1);
                    }
                }

                else
                {
                    MessageBox.Show("无法识别的命令!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                newfile.ReadFile(treeView1, contextMenuStrip1, imageList1);
                newfile.DrawDisk(groupBox2, "disk1.txt");
                newfile.DrawDisk(groupBox3, "disk2.txt");

                textBox_command.Focus();
            }
            catch
            {
                MessageBox.Show("!!");
            }

        }
    }



}
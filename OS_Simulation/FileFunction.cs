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

    #region FCB
    public class FCB                     //目录结构(文件控制块)
    {
        byte [] name=new byte[3];     //文件名或目录名
        byte type;                    //文件类型(扩展名)
        byte attribute;               //文件属性
        byte address;                 //文件起始盘块号
        char length;                  //文件长度


        #region 构造函数
        public FCB() 
        {
        
        }
        public FCB(byte[]nam,byte typ,byte att,byte add,char len)
        {
            name = nam;
            type = typ;
            attribute = att;
            address = add;
            length = len;
        }
        #endregion

        #region 属性
        public byte[] Name
        {
            get { return name; }
            set { name = value; }
        }

        public char Length
        {
            get { return length; }
            set {length=value;}
        }

        public byte Type
        {
            get { return type; }
            set { type = value; }
        }

        public byte Attribute
        {
            get { return attribute; }
            set { attribute = value; }
        }

        public byte Address
        {
            get { return address;}
            set { address = value; }
        }
        #endregion
    }
    #endregion

    public class FileFunction
    {

        #region FindNullItem(判断是否为空目录项)
        public int FindNullItem(int dnum,string harddisk)
        {
            FileStream Disk = new FileStream(harddisk, FileMode.Open);
            int n = 0;
            int flag=0;
            for (int i = 0; i < 8; i++)
            {
                Disk.Seek(64 * (dnum - 1) + 4 + n, SeekOrigin.Begin);  //读fcb中的attribute
                if (Disk.ReadByte() == 0)
                {
                    flag = i+1;
                    break;
                }
                n = n + 8;
            }
            Disk.Close();
            return flag;
        }
        #endregion


        #region FindFAT(查找FAT)
        public int FindFAT(string harddisk)
        {
            FileStream Disk = new FileStream( harddisk, FileMode.Open);
            byte[] FAT = new byte[128];
            Disk.Seek(0, SeekOrigin.Begin);
            Disk.Read(FAT, 0, FAT.Length);
            int disknum=0;
            for (int i = 3; i < 128; i++)
            {
                if (FAT[i] == 0)
                {
                    disknum = i+1;
                    break;
                }
            }
            Disk.Close();
            return disknum;
        }
        #endregion


        #region WriteFile(向磁盘写入FCB)
        public void WriteFile(int disknumber, int Itemnumber, FCB buffer, string harddisk)
        {
            FileStream Disk = new FileStream(harddisk, FileMode.Open);
            if(disknumber >0&&Itemnumber>0)
            {
                Disk.Seek(64 * (disknumber - 1) + 8 * (Itemnumber - 1), SeekOrigin.Begin);
                Disk.Write(buffer.Name, 0, buffer.Name.Length);
                Disk.Seek(0, SeekOrigin.Current);
                Disk.WriteByte(buffer.Type);
                Disk.Seek(0, SeekOrigin.Current);
                Disk.WriteByte(buffer.Attribute);
                Disk.Seek(0, SeekOrigin.Current);
                Disk.WriteByte(buffer.Address);
                Disk.Seek(0, SeekOrigin.Current);
                Disk.WriteByte(Convert.ToByte(buffer.Length));
            }
            Disk.Close();
        }
        #endregion


        #region CreateFCB(创建FCB)
        public FCB CreateFCB(byte[] name,byte type,byte attribute,byte address,char length)
        {
            FCB buffer = new FCB();
            buffer.Name=name;
            buffer.Type = type;
            buffer.Attribute = attribute;
            buffer.Address = address;
            buffer.Length = length;
            return buffer;
        }
        #endregion


        #region SearchMenu(查找目录路径)
        public int SearchMenu(string pathname,string harddisk)
        {
            FileStream Disk = new FileStream(harddisk, FileMode.Open);
            string[] pnames = pathname.Split(new char[] { '\\', '.' });  //分割路径
            char[] chardata = new char[3];
            byte[] bydata = new byte[3];
            int attribute;
            int address;
            int n, flag;
            int disknum = 3;
            Decoder d = Encoding.UTF8.GetDecoder();

            int i = 1;
            while (i <= pnames.Length - 1)
            {
                n = 0;
                for (int j = 0; j < 8; j++)
                {
                    Disk.Seek(64 * (disknum - 1) + n, SeekOrigin.Begin);
                    Disk.Read(bydata, 0, 3);
                    Disk.Seek(1, SeekOrigin.Current);
                    attribute = Disk.ReadByte();
                    address = Disk.ReadByte();
                    d.GetChars(bydata, 0, bydata.Length, chardata, 0);
                    flag = 0;

                    for (int k = 0; k < chardata.Length; k++)
                    {
                        if (!chardata[k].Equals(pnames[i].ToCharArray()[k]))
                        {
                            break;
                        }
                        flag = k+1;
                    }
                    if (flag == chardata.Length && attribute == 8)
                    {
                        disknum = address;
                        goto found;
                    }
                    n = n + 8;
                }
                Disk.Close();
                return 0;      //未找到
            found:
                i++;
            }
            Disk.Close();
            return disknum;  //找到，返回最后一级目录的盘块号
        }
        #endregion 


        #region SearchFile(查找文件)
        public int SearchFile(string pathname, int disknum, string harddisk)
        {
            FileStream Disk = new FileStream(harddisk, FileMode.Open);
            string[] pnames = pathname.Split(new char[] { '\\', '.' });      //分割路径
            string halfpathname = pathname.Remove(pathname.Length - 6);      //去掉文件名和扩展名
            char[] chardata = new char[3];
            byte[] bydata = new byte[3];
            int attribute;
            int address;
            Decoder d = Encoding.UTF8.GetDecoder();
            int n = 0;
            int flag;
            char typew, typer;

            for (int i = 0; i < 8; i++)
            {
                Disk.Seek(64 * (disknum - 1) + n, SeekOrigin.Begin);
                Disk.Read(bydata, 0, 3);
                Disk.Seek(1, SeekOrigin.Current);
                attribute = Disk.ReadByte();
                address = Disk.ReadByte();
                d.GetChars(bydata, 0, bydata.Length, chardata, 0);
                typew = Convert.ToChar(pnames[pnames.Length - 1]);
                typer = 'e';
                flag = 0;
                for (int k = 0; k < chardata.Length; k++)
                {
                    if (!chardata[k].Equals(pnames[pnames.Length - 2].ToCharArray()[k]))
                    {
                        break;
                    }
                    flag = k+1;
                }
                if (flag == chardata.Length && typer == typew)
                {
                    Disk.Close();
                    return 2;      //文件已经存在 
                }
                n = n + 8;
            }
            Disk.Close();
            return disknum;  //返回上一级目录的盘块号
        }
        #endregion 


        #region Search
        public int Search(string pathname, string harddisk)
        {
            string[] pnames = pathname.Split(new char[] { '\\', '.' });  //分割路径
            int searchresult;
            int result;
            int disknum;

            if (pnames[pnames.Length - 1].Length != 1)  //查找目录
            {
                string halfpathname = pathname.Remove(pathname.Length - 4);      
                if (halfpathname.Length != 2)
                {
                    searchresult = SearchMenu(halfpathname,harddisk);
                    if (searchresult == 0)     //最后一级目录路径之前的路径不存在
                    {
                        return 1;          //最后一级目录之前的路径不存在
                    }
                    else
                    {
                        result = SearchMenu(pathname,harddisk);
                        return result;    //0表示目录不存在，返回一个disknum表示目录存在且返回最后一级目录的盘块号
                    }
                }
                else
                {
                   searchresult = SearchMenu(pathname,harddisk);
                   return searchresult;
                }
            }
            else   //查找文件
            {
                string halfpathname = pathname.Remove(pathname.Length - 6);      //去掉文件名和扩展名
                if (halfpathname.Length != 2)
                {
                    searchresult = SearchMenu(halfpathname,harddisk);
                    if (searchresult == 0)  //文件路径不存在
                    {
                        return 1;       //文件之前的路径不存在
                    }
                    else
                    {
                        disknum = searchresult;
                        result = SearchFile(pathname, disknum,harddisk);
                        return result;             //返回2表示文件存在，返回一个disknum(上一级目录的盘块号)表示文件不存在
                    }
                }
                else
                {
                    disknum = 3;
                    result = SearchFile(pathname, disknum,harddisk);
                    return result;
                }
            }
        }
        #endregion


        #region CreateFile(建立文件)
        public void CreateFile(string pathname, byte attribute, byte address, char length, string harddisk)
        {
            int searchresult = Search(pathname,harddisk);

            if (searchresult==1)
            {
                MessageBox.Show("文件路径不存在！","注意",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                return;
            }
            else if (searchresult == 2)
            {
                MessageBox.Show("文件存在！", "注意", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else   //写FCB
            {
                string[] pnames = pathname.Split(new char[] { '\\', '.' });  //分割路径
                string halfpathname = pathname.Remove(pathname.Length - 6);      //去掉文件名和扩展名
                UTF8Encoding utf = new UTF8Encoding();
                char chtype = Convert.ToChar(pnames[pnames.Length - 1]);     //扩展名转换为char
                byte bytype = Convert.ToByte(chtype);                        //char再转换成byte
                if (FindNullItem(searchresult,harddisk) == 0)    //没有空目录项
                {
                    MessageBox.Show("目录已满！");
                    return;
                }
                WriteFile(searchresult, FindNullItem(searchresult,harddisk), CreateFCB(utf.GetBytes(pnames[pnames.Length - 2]), bytype, attribute, address, length),harddisk);
            } 
        }
        #endregion


        #region RecordMenuFAT(记录目录的FAT表项)
        public void RecordMenuFAT(int disknum, string harddisk)
        {
            FileStream Disk = new FileStream(harddisk, FileMode.Open);
            Disk.Seek(disknum - 1, SeekOrigin.Begin);
            Disk.WriteByte(254);
            Disk.Close();
        }
        #endregion


        #region CreateMenu(建立目录)
        public void CreateMenu(string pathname,string harddisk)
        {
            int searchresult = Search(pathname,harddisk);

            if (searchresult == 1)
            {
                MessageBox.Show("文件路径不存在！","注意",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                return;
            }
            else if (searchresult == 0)
            {
                string[] pnames = pathname.Split(new char[] { '\\' });           //分割路径
                string halfpathname = pathname.Remove(pathname.Length - 4);      //去掉最后一个目录名
                UTF8Encoding utf = new UTF8Encoding();
                int disknum = FindFAT(harddisk);
                byte disknumber = Convert.ToByte(disknum);
                if (halfpathname.Length != 2)
                {
                    if (FindNullItem(Search(halfpathname,harddisk),harddisk) == 0)    //没有空目录项
                    {
                        MessageBox.Show("目录已满！");
                        return;
                    }
                    WriteFile(Search(halfpathname,harddisk), FindNullItem(Search(halfpathname,harddisk),harddisk), CreateFCB(utf.GetBytes(pnames[pnames.Length - 1]), 0, 8, disknumber, Convert.ToChar(0)),harddisk);
                    RecordMenuFAT(disknum,harddisk);
                }
                else
                {
                    if (FindNullItem(3,harddisk) == 0)    //没有空目录项
                    {
                        MessageBox.Show("目录已满！");
                        return;
                    }
                    WriteFile(3, FindNullItem(3,harddisk), CreateFCB(utf.GetBytes(pnames[pnames.Length - 1]), 0, 8, disknumber, Convert.ToChar(0)),harddisk);
                    RecordMenuFAT(disknum,harddisk);
                } 
            }
            else
            {
                MessageBox.Show("文件存在！", "注意", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }
        #endregion


        #region FindItem(找文件的目录项)
        public int[] FindItem(int disknum, byte[] name, char attribute, string harddisk)
        {
            FileStream Disk = new FileStream(harddisk, FileMode.Open);
            byte[] filename = new byte[3];
            byte attr;
            int address;
            int n = 0;
            int flag = 0;
            char type;
            int[] Item_Address = { 0,0};
            for (int i = 0; i < 8; i++)
            {
                Disk.Seek(64 * (disknum - 1) + n, SeekOrigin.Begin);  //fcb中的attribute
                Disk.Read(filename, 0, 3);
                Disk.Seek(1, SeekOrigin.Current);
                attr=Convert.ToByte(Disk.ReadByte());
                address= Disk.ReadByte();
                if (attr == 8)
                    {
                    for (int k = 0; k < 3; k++)
                    {
                        if (name[k] != filename[k])
                        {
                            break;
                        }
                        flag = k + 1;
                    }
                    if (flag == 3)
                    {
                        Item_Address[0] = i + 1;
                        Item_Address[1] = address;
                        Disk.Close();
                        return Item_Address;
                    }
                }
                else
                {
                    type = 'e';
                    for (int k = 0; k < 3; k++)
                    {
                        if (name[k] != filename[k])
                        {
                            break;
                        }
                        flag = k + 1;
                    }
                    if (flag == 3 && type == attribute)
                    {
                        Item_Address[0] = i + 1;
                        Item_Address[1] = address;
                        Disk.Close();
                        return Item_Address;
                    }
                }
                n = n + 8;
            }
            Disk.Close();
            return Item_Address;
        }
        #endregion


        #region DeleteFCB(撤销FCB)
        public void DeleteFCB(int disknum, int item, string harddisk)
        {
            FileStream Disk = new FileStream(harddisk, FileMode.Open);
            byte[] bydata = new byte[8];
            Disk.Seek(64*(disknum-1)+8*(item-1),SeekOrigin.Begin);
            Disk.Write(bydata, 0, 8);
            Disk.Close();
        }
        #endregion


        #region DeleteFile(删除文件)
        public void DeleteFile(string pathname, string harddisk)
        { 
            if(Search(pathname,harddisk)==1)
            {
                MessageBox.Show("文件路径不正确！", "注意", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else if (Search(pathname,harddisk) == 2)   //文件存在
            {
                string[] pnames = pathname.Split(new char[] { '\\', '.' });
                string halfpathname = pathname.Remove(pathname.Length - 6);
                char attribute= Convert.ToChar(pnames[pnames.Length-1]);
                UTF8Encoding utf = new UTF8Encoding();
                byte[] name = utf.GetBytes(pnames[pnames.Length - 2]);
                int disknum;
                if (pnames.Length == 3)
                {
                    disknum = 3;
                }
                else
                {
                    disknum = Search(halfpathname,harddisk);    
                }
                int item = FindItem(disknum, name, attribute,harddisk)[0];
                int address = FindItem(disknum, name, attribute,harddisk)[1];
                byte addr = Convert.ToByte(address);
                DeleteFCB(disknum, item,harddisk);   //删除FCB

                if(addr==0)
                {
                    return;
                }
                else
                {
                    byte[] FAT = new byte[128];
                    FileStream Disk = new FileStream(harddisk, FileMode.Open);
                    Disk.Read(FAT, 0, 128);
                    int count = 1;
                    while (FAT[addr - 1] != 253)
                    {
                        addr = FAT[addr - 1];
                        count++;
                    }
                    byte[] dnums = new byte[count];
                    int i = 1;
                    addr = Convert.ToByte(address);
                    while (FAT[addr-1] != 253)
                    {
                        dnums[i] = FAT[addr-1];
                        addr =FAT[addr-1];
                        i++;
                    }
                    dnums[0] = Convert.ToByte(address);
                    byte[] delcontent = new byte[64];
                    for (int k = 0; k < 64; k++)
                    {
                        delcontent[k] = 0;
                    }
                    for (int j = 0; j < dnums.Length; j++)
                    {
                        Disk.Seek(64 * (dnums[j] - 1), SeekOrigin.Begin);
                        Disk.Write(delcontent, 0, 64);      //删除文件的内容
                    }

                    //将FAT表中文件占用的记为0
                    for (int t = 0; t < dnums.Length; t++)
                    {
                        Disk.Seek(dnums[t] - 1, SeekOrigin.Begin);
                        Disk.WriteByte(0);
                    }
                    Disk.Close();
                }          
            }
            else
            {
                MessageBox.Show("文件不存在！", "注意", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }
        #endregion


        #region DeleteMenu(删除目录)
        public void DeleteMenu(string pathname, string harddisk)
        {
            if (Search(pathname,harddisk) == 1)
            {
                MessageBox.Show("文件路径不正确！", "注意", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else if (Search(pathname,harddisk) == 0)
            {
                MessageBox.Show("文件不存在！", "注意", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else
            {
                string[] pnames = pathname.Split(new char[] { '\\', '.' });
                string halfpathname = pathname.Remove(pathname.Length - 4);
                int lastdnum=Search(pathname,harddisk);
     
                
                int attribute;
                byte[] bytename = new byte[3];
                UTF8Encoding utf8 = new UTF8Encoding();
                int n=0;
                for(int i=0;i<8;i++)
                {
                    FileStream Disk = new FileStream(harddisk, FileMode.Open);
                    Disk.Seek(64 * (lastdnum - 1) + n, SeekOrigin.Begin);
                    Disk.Read(bytename, 0, 3);
                    Disk.Seek(1,SeekOrigin.Current);
                    attribute = Disk.ReadByte();
                    if (attribute == 8)
                    {
                        pathname = pathname + '\\' + utf8.GetString(bytename);
                        Disk.Close();
                        DeleteMenu(pathname,harddisk);
                    }
                    else if (attribute == 2 || attribute == 3)
                    {
                            pathname = pathname + '\\' + utf8.GetString(bytename) + ".e";
                            Disk.Close();
                            DeleteFile(pathname,harddisk);   
                    }
                    else
                    {
                        Disk.Close();
                        break;
                    }
                    n=n+8;
                }        
                int disknum;
                if (pnames.Length == 2)   //c:\aaa
                {
                    disknum = 3;
                }
                else
                {
                    disknum = Search(halfpathname,harddisk);
                }
                UTF8Encoding utf = new UTF8Encoding();
                byte[] name = utf.GetBytes(pnames[pnames.Length - 1]);
                int item = FindItem(disknum, name, Convert.ToChar(8),harddisk)[0];     //目录项
                int address = FindItem(disknum, name, Convert.ToChar(8),harddisk)[1];  //目录起始盘块号
                //byte addr = Convert.ToByte(address);

                DeleteFCB(disknum, item,harddisk);   //删除FCB

               
                byte[] delcontent = new byte[64];
                for (int k = 0; k < 64; k++)
                {
                    delcontent[k] = 0;
                }
                FileStream Disk1 = new FileStream(harddisk, FileMode.Open);
                Disk1.Seek(64 * ( address- 1), SeekOrigin.Begin);
                Disk1.Write(delcontent, 0, 64);
                Disk1.Seek(address - 1, SeekOrigin.Begin);  //记录FAT为0
                Disk1.WriteByte(0);
                Disk1.Close();
            }
        }
        #endregion


        #region ReadFCB(获取文件的FCB信息)
        public FCB ReadFCB(int disknum, int item, string harddisk)
        {
            FileStream Disk = new FileStream(harddisk, FileMode.Open);
            FCB buffer = new FCB();
            Disk.Seek(64 * (disknum - 1) + 8 * (item - 1), SeekOrigin.Begin);
            Disk.Read(buffer.Name, 0, 3);
            buffer.Type = Convert.ToByte(Disk.ReadByte());
            buffer.Attribute =Convert.ToByte(Disk.ReadByte());
            buffer.Address = Convert.ToByte(Disk.ReadByte());
            buffer.Length=Convert.ToChar(Disk.ReadByte());
            Disk.Close();
            return buffer;
        }
        #endregion


        #region CopyFile(复制文件,只复制FCB)
        public void CopyFile(string pathname1, string pathname2, string harddisk)
        {
            string[] pnames = pathname1.Split(new char[] { '\\', '.' });
            string halfpathname = pathname1.Remove(pathname1.Length - 6);
            char attribute = Convert.ToChar(pnames[pnames.Length - 1]);
            UTF8Encoding utf = new UTF8Encoding();
            byte[] name = utf.GetBytes(pnames[pnames.Length - 2]);
            int disknum;
            if (pnames.Length == 3)   //c:\aaa.t
            {
                disknum = 3;
            }
            else
            {
                disknum = Search(halfpathname,harddisk);
            }
            int item = FindItem(disknum, name, attribute,harddisk)[0];
            //int address = FindItem(disknum, name, attribute)[1];
            FCB buffer = ReadFCB(disknum, item,harddisk);

            pathname2 = pathname2 +'\\'+ pnames[pnames.Length-2] +'.'+pnames[pnames.Length - 1];

            CreateFile(pathname2, buffer.Attribute, buffer.Address, buffer.Length,harddisk);
        }
        #endregion


        #region CutFile(移动文件)
        public void CutFile(string pathname1, string pathname2, string harddisk)
        {
            CopyFile(pathname1, pathname2,harddisk);  //复制FCB到新目录下

            string[] pnames = pathname1.Split(new char[] { '\\', '.' });
            string halfpathname = pathname1.Remove(pathname1.Length - 6);
            char attribute = Convert.ToChar(pnames[pnames.Length - 1]);
            UTF8Encoding utf = new UTF8Encoding();
            byte[] name = utf.GetBytes(pnames[pnames.Length - 2]);
            int disknum;
            if (pnames.Length == 3)   //c:\aaa.e
            {
                disknum = 3;
            }
            else
            {
                disknum = Search(halfpathname,harddisk);
            }
            int item = FindItem(disknum, name, attribute,harddisk)[0];

            DeleteFCB(disknum, item,harddisk);   //删除FCB
        }
        #endregion


        #region DeepCopyFile(深度复制)
        public void DeepCopyFile(string pathname1, string pathname2, string harddisk1,string harddisk2)
        {
            string[] pnames = pathname1.Split(new char[] { '\\', '.' });
            string halfpathname = pathname1.Remove(pathname1.Length - 6);
            char attribute = Convert.ToChar(pnames[pnames.Length - 1]);
            UTF8Encoding utf = new UTF8Encoding();
            byte[] name = utf.GetBytes(pnames[pnames.Length - 2]);
            int disknum;
            if (pnames.Length == 3)   //c:\aaa.e
            {
                disknum = 3;
            }
            else
            {
                disknum = Search(halfpathname,harddisk1);
            }
            int item = FindItem(disknum, name, attribute,harddisk1)[0];   //获取文件在目录的第几项
            FCB buffer = ReadFCB(disknum, item,harddisk1);
            int [] dnums = FindDiskNumber(buffer.Address,harddisk1);   //获取文件占用的所有盘块号

            //读取文件的内容
            FileStream Disk = new FileStream(harddisk1, FileMode.Open);
            byte[] content = new byte[64 * dnums.Length];
            for (int i = 0; i < dnums.Length; i++)
            {
                Disk.Seek(64 * (dnums[i] - 1), SeekOrigin.Begin);
                Disk.Read(content, 64 * i, 64);
            }
            Disk.Close();

            pathname2 = pathname2 + '\\' + pnames[pnames.Length - 2] + '.' + pnames[pnames.Length - 1];
            DialogResult result = new DialogResult();
            if (Search(pathname2, harddisk2) == 2)
            {
                result = MessageBox.Show("文件存在，是否覆盖？", "注意", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                if (result == DialogResult.OK)
                {
                    DeleteFile(pathname2, harddisk2);
                    int[] nums = SearchFAT(dnums.Length, harddisk2);  //获取空盘块
                    RecordFileFAT(nums, harddisk2);                  //记录文件FAT
                    WriteContent(nums, content, harddisk2);            //写入文件内容
                    CreateFile(pathname2, buffer.Attribute, Convert.ToByte(nums[0]), buffer.Length, harddisk2);    //FCB写入新目录内    
                }
                if (result == DialogResult.Cancel)
                {
                    return;
                }
            }
            else
            {
                int[] nums =SearchFAT(dnums.Length, harddisk2);  //获取空盘块
                RecordFileFAT(nums, harddisk2);                  //记录文件FAT
                WriteContent(nums, content, harddisk2);            //写入文件内容
                CreateFile(pathname2, buffer.Attribute, Convert.ToByte(nums[0]), buffer.Length, harddisk2);    //FCB写入新目录内 
            }      
        }
        #endregion


        #region ReadFile(读文件画节点)
        public void ReadFile(TreeView treeView, ContextMenuStrip contextMenuStrip,ImageList imageList)
        {
            treeView.Nodes.Clear();        //删除集合中所有树节点
            //重新添加树节点   
            treeView.ImageList = imageList;
            TreeNode root = new TreeNode("计算机", 0, 0);
            TreeNode node_C = new TreeNode("本地磁盘C", 4, 4);
            TreeNode node_D = new TreeNode("本地磁盘D", 4, 4);
            node_C.ContextMenuStrip = contextMenuStrip;
            node_D.ContextMenuStrip = contextMenuStrip;
            treeView.Nodes.Add(root);
            root.Nodes.Add(node_C);
            root.Nodes.Add(node_D);
            DrawTree(node_C, 3,"disk1.txt",contextMenuStrip);
            DrawTree(node_D, 3, "disk2.txt",contextMenuStrip);
            treeView.ExpandAll();
        }
        #endregion


        #region GetPathname(整理路径)
        public string GetPathname(string text)
        {
            string[] s = text.Split(new char[] { '\\' });
            char[] chdata = s[1].ToCharArray();
            string pname = "";
            string tname;
            string pathname;
            if (chdata[chdata.Length - 1] == 'C')
            {
                for (int i = 2; i < s.Length; i++)
                {
                    pname = pname + '\\' + s[i];
                }
                tname = "c:" + pname;
                if (s[s.Length - 1].Length == 3)
                {
                    pathname = tname;
                    return pathname;
                }
                else if (s[s.Length - 1].Length == 5)
                {
                    pathname = tname;
                    return pathname;
                }
                else
                {
                    pathname = tname.Remove(tname.Length - 2);
                    return pathname;
                }
            }

            else
            {
                for (int i = 2; i < s.Length; i++)
                {
                    pname = pname + '\\' + s[i];
                }
                tname = "d:" + pname;
                if (s[s.Length - 1].Length == 3)
                {
                    pathname = tname;
                    return pathname;
                }
                else if (s[s.Length - 1].Length == 5)
                {
                    pathname = tname;
                    return pathname;
                }
                else
                {
                    pathname = tname.Remove(tname.Length - 2);
                    return pathname;
                }
            }
        }
        #endregion


        #region SearchFAT(查找空盘块)
        public int[] SearchFAT(int n, string harddisk)
        {
            FileStream Disk = new FileStream(harddisk, FileMode.Open);
            byte[] FAT = new byte[128];
            Disk.Seek(0, SeekOrigin.Begin);
            Disk.Read(FAT, 0, FAT.Length);
            Disk.Close();
            int k = 0;
            while (k < 128)
            {
                if (FAT[k] == 0)
                {
                    break;
                }
                k++;
            }
            if (k == 128)
            {
                MessageBox.Show("磁盘已满！", "注意");
                return null;
            }
            int[] disknum = new int[n];
            int j = 0;
            for (int i = 0; i < 128; i++)
            {
                if (FAT[i] == 0)
                {
                    while (j < n)
                    {
                        disknum[j] = i + 1;
                        j++;
                        break;
                    }
                    if (j == n)
                    {
                        break;
                    }
                }
            }
            int[] newdisknum = new int[j];
            for (int t = 0; t < j; t++)
            {
                newdisknum[t] = disknum[t];
            }
            if (j < n)
            {
                return newdisknum;
            }
            else
            {
                return disknum;
            }
        }
        #endregion


        #region RecordFileFAT(记录文件的FAT表项)
        public void RecordFileFAT(int[] disknum, string harddisk)
        {
            FileStream Disk = new FileStream(harddisk, FileMode.Open);
            byte[] bydata = new byte[disknum.Length];
            for (int i = 0; i < disknum.Length; i++)
            {
                bydata[i] = Convert.ToByte(disknum[i]);
            }
            int j = 0;
            while (j != disknum.Length - 1)
            {
                Disk.Seek(disknum[j] - 1, SeekOrigin.Begin);
                Disk.WriteByte(bydata[j + 1]);
                j++;
            }
            Disk.Seek(disknum[disknum.Length - 1] - 1, SeekOrigin.Begin);
            Disk.WriteByte(253);
            Disk.Close();
        }
        #endregion


        #region WriteContent(文件内容写入磁盘)
        public void WriteContent(int[] disknum, byte[] content, string harddisk)
        {
            FileStream Disk = new FileStream(harddisk, FileMode.Open);
            for (int i = 0; i < disknum.Length; i++)
            {
                Disk.Seek(64 * (disknum[i] - 1), SeekOrigin.Begin);
                if (i != disknum.Length - 1)
                {
                    Disk.Write(content, 0, 64);
                }
                else
                {
                    if (content.Length - (64 * i) <= 64)
                    {
                        Disk.Write(content, 64 * i, content.Length - (64 * i));
                    }
                    else
                    {
                        Disk.Write(content, 64 * i, 64);
                        MessageBox.Show("磁盘已满！");
                    }
                }
            }
            Disk.Close();
        }
        #endregion


        #region FindDiskNumber(找到文件占用的盘快号)
        public int[] FindDiskNumber(byte address, string harddisk)
        {
            FileStream Disk = new FileStream(harddisk, FileMode.Open);
            byte[] FAT = new byte[128];
            Disk.Seek(0, SeekOrigin.Begin);
            Disk.Read(FAT, 0, FAT.Length);
            int j = Convert.ToInt32(address);
            int count = 1;
            while (FAT[j - 1] != 253)
            {
                j = FAT[j - 1];
                count++;
            }
            int[] disknum = new int[count];
            int i = 1;
            j = Convert.ToInt32(address);
            while (FAT[j - 1] != 253)
            {
                disknum[i] = FAT[j - 1];
                j = FAT[j - 1];
                i++;
            }
            disknum[0] = Convert.ToInt32(address);
            Disk.Close();
            return disknum;
        }
        #endregion


        #region 查找树节点
        public int SearchPreNode(TreeNode node, string newname)
        {
            if (node.PrevNode != null)
            {
                if (newname == node.PrevNode.Text)
                {
                    MessageBox.Show("无法重命名：指定的文件与现有文件重名。请指定另一文件名。", "注意", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return 1;
                }
                else
                {
                    SearchPreNode(node.PrevNode, newname);
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        public int SearchNextNode(TreeNode node, string newname)
        {
            if (node.NextNode != null)
            {
                if (newname == node.NextNode.Text)
                {
                    MessageBox.Show("无法重命名：指定的文件与现有文件重名。请指定另一文件名。", "注意", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return 1;
                }
                else
                {
                    SearchNextNode(node.NextNode, newname);
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }
        #endregion


        #region DrawTree
        public void DrawTree(TreeNode node, int disknum,string harddisk,ContextMenuStrip contextmenustrip)
        {
            char[] charname = new char[3];
            byte[] bytename = new byte[3];
            int n = 0;
            for (int i = 0; i < 8; i++)
            {
                FileStream Disk = new FileStream(harddisk, FileMode.Open);
                Disk.Seek(64 * (disknum - 1) + n, SeekOrigin.Begin);
                Disk.Read(bytename, 0, 3);                              //读文件名
                string name = UTF8Encoding.UTF8.GetString(bytename);    //文件名转换为string类型
                Disk.Seek(1, SeekOrigin.Current);
                int attr = Disk.ReadByte();
                Disk.Close();
                if (attr == 8)                          //目录
                {
                    FileStream Disk1 = new FileStream(harddisk, FileMode.Open);
                    TreeNode m_childnode = new TreeNode(name, 1, 1);
                    m_childnode.ContextMenuStrip = contextmenustrip;
                    node.Nodes.Add(m_childnode);
                    Disk1.Seek(64*(disknum-1)+5+n, SeekOrigin.Begin);
                    int disknumber = Disk1.ReadByte();
                    Disk1.Close();
                    DrawTree(m_childnode, disknumber, harddisk,contextmenustrip);
                    n = n + 8;
                    continue;
                }
                else if (attr == 2 || attr == 3)     
                {
                    name += ".e";
                    TreeNode e_childnode = new TreeNode(name, 2, 2);
                    e_childnode.ContextMenuStrip = contextmenustrip;
                    node.Nodes.Add(e_childnode);
                    n = n + 8;
                    continue;
                }
                else
                {
                    n = n + 8;
                    continue;
                }
            }
        }
        #endregion

       
        #region DrawDisk(读FAT画磁盘)
        public void DrawDisk(GroupBox groupbox,string harddisk)
        {
            groupbox.Controls.Clear();
            FileStream Disk = new FileStream(harddisk, FileMode.Open);
            byte[] FAT = new byte[128];
            Disk.Seek(0, SeekOrigin.Begin);
            Disk.Read(FAT, 0, 128);
            Disk.Close();

            PictureBox[] PB = new PictureBox[128];
            for (int i = 0; i < 3; i++)
            {
                PB[i] = new PictureBox();   //实例化
                PB[i].BorderStyle = BorderStyle.Fixed3D;
                PB[i].BackColor = Color.Red;
                PB[i].Size = new System.Drawing.Size(15, 15);
            }
            for (int i = 3; i < 128; i++)
            {
                PB[i] = new PictureBox();   //实例化
                PB[i].BorderStyle = BorderStyle.Fixed3D;
                PB[i].Size = new System.Drawing.Size(15, 15);
            }

            //初始化picturebox的位置
            int j = 0;
            int k;
            int n = 0;
            for (int i = 0; i < 8; i++)
            {
                k = 0;
                while (j != 16 * (i + 1))
                {
                    PB[j].Location = new System.Drawing.Point(17 + 16 * k, 21 + 16 * n);
                    j++;
                    k++;
                }
                n++;
            }


            for (int i = 3; i < 128;i++ )
            {
                if (FAT[i] != 0)
                {
                    PB[i].BackColor = Color.Blue;        
                }
                if (FAT[i] == 0)
                {
                    PB[i].BackColor = Color.White;
                }
            }
            
            for (int i = 0; i < 128; i++)
            {
                groupbox.Controls.Add(PB[i]);
            }
            
        }
        #endregion

    }

}
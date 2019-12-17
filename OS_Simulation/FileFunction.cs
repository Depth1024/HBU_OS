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
    public class FCB                     //Ŀ¼�ṹ(�ļ����ƿ�)
    {
        byte [] name=new byte[3];     //�ļ�����Ŀ¼��
        byte type;                    //�ļ�����(��չ��)
        byte attribute;               //�ļ�����
        byte address;                 //�ļ���ʼ�̿��
        char length;                  //�ļ�����


        #region ���캯��
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

        #region ����
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

        #region FindNullItem(�ж��Ƿ�Ϊ��Ŀ¼��)
        public int FindNullItem(int dnum,string harddisk)
        {
            FileStream Disk = new FileStream(harddisk, FileMode.Open);
            int n = 0;
            int flag=0;
            for (int i = 0; i < 8; i++)
            {
                Disk.Seek(64 * (dnum - 1) + 4 + n, SeekOrigin.Begin);  //��fcb�е�attribute
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


        #region FindFAT(����FAT)
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


        #region WriteFile(�����д��FCB)
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


        #region CreateFCB(����FCB)
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


        #region SearchMenu(����Ŀ¼·��)
        public int SearchMenu(string pathname,string harddisk)
        {
            FileStream Disk = new FileStream(harddisk, FileMode.Open);
            string[] pnames = pathname.Split(new char[] { '\\', '.' });  //�ָ�·��
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
                return 0;      //δ�ҵ�
            found:
                i++;
            }
            Disk.Close();
            return disknum;  //�ҵ����������һ��Ŀ¼���̿��
        }
        #endregion 


        #region SearchFile(�����ļ�)
        public int SearchFile(string pathname, int disknum, string harddisk)
        {
            FileStream Disk = new FileStream(harddisk, FileMode.Open);
            string[] pnames = pathname.Split(new char[] { '\\', '.' });      //�ָ�·��
            string halfpathname = pathname.Remove(pathname.Length - 6);      //ȥ���ļ�������չ��
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
                    return 2;      //�ļ��Ѿ����� 
                }
                n = n + 8;
            }
            Disk.Close();
            return disknum;  //������һ��Ŀ¼���̿��
        }
        #endregion 


        #region Search
        public int Search(string pathname, string harddisk)
        {
            string[] pnames = pathname.Split(new char[] { '\\', '.' });  //�ָ�·��
            int searchresult;
            int result;
            int disknum;

            if (pnames[pnames.Length - 1].Length != 1)  //����Ŀ¼
            {
                string halfpathname = pathname.Remove(pathname.Length - 4);      
                if (halfpathname.Length != 2)
                {
                    searchresult = SearchMenu(halfpathname,harddisk);
                    if (searchresult == 0)     //���һ��Ŀ¼·��֮ǰ��·��������
                    {
                        return 1;          //���һ��Ŀ¼֮ǰ��·��������
                    }
                    else
                    {
                        result = SearchMenu(pathname,harddisk);
                        return result;    //0��ʾĿ¼�����ڣ�����һ��disknum��ʾĿ¼�����ҷ������һ��Ŀ¼���̿��
                    }
                }
                else
                {
                   searchresult = SearchMenu(pathname,harddisk);
                   return searchresult;
                }
            }
            else   //�����ļ�
            {
                string halfpathname = pathname.Remove(pathname.Length - 6);      //ȥ���ļ�������չ��
                if (halfpathname.Length != 2)
                {
                    searchresult = SearchMenu(halfpathname,harddisk);
                    if (searchresult == 0)  //�ļ�·��������
                    {
                        return 1;       //�ļ�֮ǰ��·��������
                    }
                    else
                    {
                        disknum = searchresult;
                        result = SearchFile(pathname, disknum,harddisk);
                        return result;             //����2��ʾ�ļ����ڣ�����һ��disknum(��һ��Ŀ¼���̿��)��ʾ�ļ�������
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


        #region CreateFile(�����ļ�)
        public void CreateFile(string pathname, byte attribute, byte address, char length, string harddisk)
        {
            int searchresult = Search(pathname,harddisk);

            if (searchresult==1)
            {
                MessageBox.Show("�ļ�·�������ڣ�","ע��",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                return;
            }
            else if (searchresult == 2)
            {
                MessageBox.Show("�ļ����ڣ�", "ע��", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else   //дFCB
            {
                string[] pnames = pathname.Split(new char[] { '\\', '.' });  //�ָ�·��
                string halfpathname = pathname.Remove(pathname.Length - 6);      //ȥ���ļ�������չ��
                UTF8Encoding utf = new UTF8Encoding();
                char chtype = Convert.ToChar(pnames[pnames.Length - 1]);     //��չ��ת��Ϊchar
                byte bytype = Convert.ToByte(chtype);                        //char��ת����byte
                if (FindNullItem(searchresult,harddisk) == 0)    //û�п�Ŀ¼��
                {
                    MessageBox.Show("Ŀ¼������");
                    return;
                }
                WriteFile(searchresult, FindNullItem(searchresult,harddisk), CreateFCB(utf.GetBytes(pnames[pnames.Length - 2]), bytype, attribute, address, length),harddisk);
            } 
        }
        #endregion


        #region RecordMenuFAT(��¼Ŀ¼��FAT����)
        public void RecordMenuFAT(int disknum, string harddisk)
        {
            FileStream Disk = new FileStream(harddisk, FileMode.Open);
            Disk.Seek(disknum - 1, SeekOrigin.Begin);
            Disk.WriteByte(254);
            Disk.Close();
        }
        #endregion


        #region CreateMenu(����Ŀ¼)
        public void CreateMenu(string pathname,string harddisk)
        {
            int searchresult = Search(pathname,harddisk);

            if (searchresult == 1)
            {
                MessageBox.Show("�ļ�·�������ڣ�","ע��",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                return;
            }
            else if (searchresult == 0)
            {
                string[] pnames = pathname.Split(new char[] { '\\' });           //�ָ�·��
                string halfpathname = pathname.Remove(pathname.Length - 4);      //ȥ�����һ��Ŀ¼��
                UTF8Encoding utf = new UTF8Encoding();
                int disknum = FindFAT(harddisk);
                byte disknumber = Convert.ToByte(disknum);
                if (halfpathname.Length != 2)
                {
                    if (FindNullItem(Search(halfpathname,harddisk),harddisk) == 0)    //û�п�Ŀ¼��
                    {
                        MessageBox.Show("Ŀ¼������");
                        return;
                    }
                    WriteFile(Search(halfpathname,harddisk), FindNullItem(Search(halfpathname,harddisk),harddisk), CreateFCB(utf.GetBytes(pnames[pnames.Length - 1]), 0, 8, disknumber, Convert.ToChar(0)),harddisk);
                    RecordMenuFAT(disknum,harddisk);
                }
                else
                {
                    if (FindNullItem(3,harddisk) == 0)    //û�п�Ŀ¼��
                    {
                        MessageBox.Show("Ŀ¼������");
                        return;
                    }
                    WriteFile(3, FindNullItem(3,harddisk), CreateFCB(utf.GetBytes(pnames[pnames.Length - 1]), 0, 8, disknumber, Convert.ToChar(0)),harddisk);
                    RecordMenuFAT(disknum,harddisk);
                } 
            }
            else
            {
                MessageBox.Show("�ļ����ڣ�", "ע��", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }
        #endregion


        #region FindItem(���ļ���Ŀ¼��)
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
                Disk.Seek(64 * (disknum - 1) + n, SeekOrigin.Begin);  //fcb�е�attribute
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


        #region DeleteFCB(����FCB)
        public void DeleteFCB(int disknum, int item, string harddisk)
        {
            FileStream Disk = new FileStream(harddisk, FileMode.Open);
            byte[] bydata = new byte[8];
            Disk.Seek(64*(disknum-1)+8*(item-1),SeekOrigin.Begin);
            Disk.Write(bydata, 0, 8);
            Disk.Close();
        }
        #endregion


        #region DeleteFile(ɾ���ļ�)
        public void DeleteFile(string pathname, string harddisk)
        { 
            if(Search(pathname,harddisk)==1)
            {
                MessageBox.Show("�ļ�·������ȷ��", "ע��", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else if (Search(pathname,harddisk) == 2)   //�ļ�����
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
                DeleteFCB(disknum, item,harddisk);   //ɾ��FCB

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
                        Disk.Write(delcontent, 0, 64);      //ɾ���ļ�������
                    }

                    //��FAT�����ļ�ռ�õļ�Ϊ0
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
                MessageBox.Show("�ļ������ڣ�", "ע��", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }
        #endregion


        #region DeleteMenu(ɾ��Ŀ¼)
        public void DeleteMenu(string pathname, string harddisk)
        {
            if (Search(pathname,harddisk) == 1)
            {
                MessageBox.Show("�ļ�·������ȷ��", "ע��", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else if (Search(pathname,harddisk) == 0)
            {
                MessageBox.Show("�ļ������ڣ�", "ע��", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                int item = FindItem(disknum, name, Convert.ToChar(8),harddisk)[0];     //Ŀ¼��
                int address = FindItem(disknum, name, Convert.ToChar(8),harddisk)[1];  //Ŀ¼��ʼ�̿��
                //byte addr = Convert.ToByte(address);

                DeleteFCB(disknum, item,harddisk);   //ɾ��FCB

               
                byte[] delcontent = new byte[64];
                for (int k = 0; k < 64; k++)
                {
                    delcontent[k] = 0;
                }
                FileStream Disk1 = new FileStream(harddisk, FileMode.Open);
                Disk1.Seek(64 * ( address- 1), SeekOrigin.Begin);
                Disk1.Write(delcontent, 0, 64);
                Disk1.Seek(address - 1, SeekOrigin.Begin);  //��¼FATΪ0
                Disk1.WriteByte(0);
                Disk1.Close();
            }
        }
        #endregion


        #region ReadFCB(��ȡ�ļ���FCB��Ϣ)
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


        #region CopyFile(�����ļ�,ֻ����FCB)
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


        #region CutFile(�ƶ��ļ�)
        public void CutFile(string pathname1, string pathname2, string harddisk)
        {
            CopyFile(pathname1, pathname2,harddisk);  //����FCB����Ŀ¼��

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

            DeleteFCB(disknum, item,harddisk);   //ɾ��FCB
        }
        #endregion


        #region DeepCopyFile(��ȸ���)
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
            int item = FindItem(disknum, name, attribute,harddisk1)[0];   //��ȡ�ļ���Ŀ¼�ĵڼ���
            FCB buffer = ReadFCB(disknum, item,harddisk1);
            int [] dnums = FindDiskNumber(buffer.Address,harddisk1);   //��ȡ�ļ�ռ�õ������̿��

            //��ȡ�ļ�������
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
                result = MessageBox.Show("�ļ����ڣ��Ƿ񸲸ǣ�", "ע��", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                if (result == DialogResult.OK)
                {
                    DeleteFile(pathname2, harddisk2);
                    int[] nums = SearchFAT(dnums.Length, harddisk2);  //��ȡ���̿�
                    RecordFileFAT(nums, harddisk2);                  //��¼�ļ�FAT
                    WriteContent(nums, content, harddisk2);            //д���ļ�����
                    CreateFile(pathname2, buffer.Attribute, Convert.ToByte(nums[0]), buffer.Length, harddisk2);    //FCBд����Ŀ¼��    
                }
                if (result == DialogResult.Cancel)
                {
                    return;
                }
            }
            else
            {
                int[] nums =SearchFAT(dnums.Length, harddisk2);  //��ȡ���̿�
                RecordFileFAT(nums, harddisk2);                  //��¼�ļ�FAT
                WriteContent(nums, content, harddisk2);            //д���ļ�����
                CreateFile(pathname2, buffer.Attribute, Convert.ToByte(nums[0]), buffer.Length, harddisk2);    //FCBд����Ŀ¼�� 
            }      
        }
        #endregion


        #region ReadFile(���ļ����ڵ�)
        public void ReadFile(TreeView treeView, ContextMenuStrip contextMenuStrip,ImageList imageList)
        {
            treeView.Nodes.Clear();        //ɾ���������������ڵ�
            //����������ڵ�   
            treeView.ImageList = imageList;
            TreeNode root = new TreeNode("�����", 0, 0);
            TreeNode node_C = new TreeNode("���ش���C", 4, 4);
            TreeNode node_D = new TreeNode("���ش���D", 4, 4);
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


        #region GetPathname(����·��)
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


        #region SearchFAT(���ҿ��̿�)
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
                MessageBox.Show("����������", "ע��");
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


        #region RecordFileFAT(��¼�ļ���FAT����)
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


        #region WriteContent(�ļ�����д�����)
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
                        MessageBox.Show("����������");
                    }
                }
            }
            Disk.Close();
        }
        #endregion


        #region FindDiskNumber(�ҵ��ļ�ռ�õ��̿��)
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


        #region �������ڵ�
        public int SearchPreNode(TreeNode node, string newname)
        {
            if (node.PrevNode != null)
            {
                if (newname == node.PrevNode.Text)
                {
                    MessageBox.Show("�޷���������ָ�����ļ��������ļ���������ָ����һ�ļ�����", "ע��", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                    MessageBox.Show("�޷���������ָ�����ļ��������ļ���������ָ����һ�ļ�����", "ע��", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                Disk.Read(bytename, 0, 3);                              //���ļ���
                string name = UTF8Encoding.UTF8.GetString(bytename);    //�ļ���ת��Ϊstring����
                Disk.Seek(1, SeekOrigin.Current);
                int attr = Disk.ReadByte();
                Disk.Close();
                if (attr == 8)                          //Ŀ¼
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

       
        #region DrawDisk(��FAT������)
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
                PB[i] = new PictureBox();   //ʵ����
                PB[i].BorderStyle = BorderStyle.Fixed3D;
                PB[i].BackColor = Color.Red;
                PB[i].Size = new System.Drawing.Size(15, 15);
            }
            for (int i = 3; i < 128; i++)
            {
                PB[i] = new PictureBox();   //ʵ����
                PB[i].BorderStyle = BorderStyle.Fixed3D;
                PB[i].Size = new System.Drawing.Size(15, 15);
            }

            //��ʼ��picturebox��λ��
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
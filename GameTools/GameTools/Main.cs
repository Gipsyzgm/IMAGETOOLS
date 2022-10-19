using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameTools
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            this.textBox1.Text = "D:/idiercode/trunk/art/UIProject/assets";
        }

        public delegate void UpdateLogTxt(string msg, Color color);

        private UpdateLogTxt _updateLogTxt;

        private void Form1_Load(object sender, EventArgs e)
        {
            GetExcelFolderFiles();
            Logger.MainForm = this;
            _updateLogTxt = new UpdateLogTxt(UpdateLogTxtMethod);
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void GetExcelFolderFiles()
        {
            this.listBox1.Items.Clear();
            string path = this.textBox1.Text;
            if (path == "")
                return;
            if (!Directory.Exists(path))
            {
                Logger.LogWarning("请检查文件路径" + path);
                return;
            }

            DirectoryInfo di = new DirectoryInfo(path);
            DirectoryInfo[] dis = di.GetDirectories();

            foreach (DirectoryInfo fil in dis)
            {
                this.listBox1.Items.Add(fil.Name);
            }
        }

        public string lastpath = "";

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string curItem = listBox1.SelectedItem.ToString();
            lastpath = this.textBox1.Text + "/" + curItem;
            Logger.Log("路径" + lastpath);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Logger.Log("开始处理");
            Program.ChangeImageSize();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Logger.Log("开始处理");
            Program.ChangeImageSizeByFolder(lastpath);
        }


        private void button3_Click(object sender, EventArgs e)
        {
            Logger.Log("开始清除日志");
            Logger.Clean();
            Logger.Log("清除日志完成");
        }


        public void Log(string str, Color color)
        {
            if (richTextBox1 == null) return;
            if (richTextBox1.InvokeRequired)
            {
                BeginInvoke(_updateLogTxt, str, color);
            }
            else
            {
                UpdateLogTxtMethod(str, color);
            }
        }

        public void UpdateLogTxtMethod(string str, Color color)
        {
            if (str == null) //清空日志
                this.richTextBox1.Text = string.Empty;
            else
            {
                if (this.richTextBox1.Text != string.Empty)
                    str = Environment.NewLine + str;
                richTextBox1.SelectionStart = richTextBox1.TextLength;
                richTextBox1.SelectionLength = 0;
                richTextBox1.SelectionColor = color;
                richTextBox1.AppendText(str);
                richTextBox1.SelectionColor = richTextBox1.ForeColor;
                richTextBox1.Focus();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            GetExcelFolderFiles();
        }
    }
}
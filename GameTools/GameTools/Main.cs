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

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void GetExcelFolderFiles()
        {
            this.listBox1.Items.Clear();
            string path = this.textBox1.Text;
            lastpath = "";
            if (path == "")
                return;
            if (!Directory.Exists(path))
            {
                Logger.LogWarning("请检查文件路径" + path);
                return;
            }

            DirectoryInfo di = new DirectoryInfo(path);
            //DirectoryInfo.GetFiles返回当前目录的文件列表   
            FileInfo[] files = di.GetFiles("*", SearchOption.AllDirectories);
            for (int i = 0; i < files.Length; i++)
            {
                if (!files[i].Name.EndsWith(".png") && !files[i].Name.EndsWith(".jpg")) continue;
                this.listBox1.Items.Add(files[i].Name);
            }
        }

        public string oripath = "";
        public string lastpath = "";
        public int settype = 0;
        public float weight = 0;
        public float high = 0;

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string curItem = listBox1.SelectedItem.ToString();
            lastpath = this.textBox1.Text + "/" + curItem;
            Logger.Log("路径" + lastpath);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (lastpath == "")
            {
                if (oripath == "")
                {
                    Logger.LogWarning("检查输入文件夹");
                    return;
                }

                if (settype == 0)
                {
                    Logger.LogWarning("请选择图片处理方式");
                    return;
                }

                if (weight == 0 || high == 0)
                {
                    Logger.LogWarning("请检查宽高值的输入是否正常，宽高均不可为0");
                    return;
                }

                Logger.Log("开始处理文件夹");
                Program.ChangeImageSizeByFolder(oripath, settype, weight, high);
            }
            else
            {
                if (settype == 0)
                {
                    Logger.LogWarning("请选择图片处理方式");
                    return;
                }

                if (weight == 0 || high == 0)
                {
                    Logger.LogWarning("请检查宽高值的输入是否正常，宽高均不可为0");
                    return;
                }

                Logger.Log("开始处理文件");
                Program.ChangeImageSizeByFile(lastpath, settype, weight, high);
            }
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox) sender;

            // Save the selected employee's name, because we will remove
            // the employee's name from the list.
            string selectedEmployee = (string) comboBox1.SelectedItem;

            if (selectedEmployee.StartsWith("按固定值修改大小"))
            {
                settype = 1;
            }
            else
            {
                settype = 2;
            }

            Logger.Log("选择模式" + selectedEmployee + ":" + settype);
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            weight = float.Parse(textBox4.Text);
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            high = float.Parse(textBox5.Text);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            oripath = textBox1.Text;
        }
    }
}
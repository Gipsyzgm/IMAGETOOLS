using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameTools
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
        }


        //type == 1 固定值修改  
        //type == 2 比例修改  
        public static void ChangeImageSizeByFolder(string path, int type, float weigh, float high)
        {
            if (!Directory.Exists(path))
            {
                Logger.Log("请检查文件路径" + path);
                return;
            }

            Logger.Log("路径" + path);
            DirectoryInfo direction = new DirectoryInfo(path);
            //DirectoryInfo.GetFiles返回当前目录的文件列表   
            FileInfo[] files = direction.GetFiles("*", SearchOption.AllDirectories);
            for (int i = 0; i < files.Length; i++)
            {
                if (!files[i].Name.EndsWith(".png") && !files[i].Name.EndsWith(".jpg")) continue;
                string xmlName = files[i].Name.Split('.')[0];
                Logger.Log("imageName:" + xmlName);
                var myBitmap = new System.Drawing.Bitmap(files[i].FullName);
                var x = myBitmap.Width;
                var y = myBitmap.Height;
                if (type == 1)
                {
                    x = (int) weigh;
                    y = (int) high;
                }
                else
                {
                    x = (int) (myBitmap.Width * weigh);
                    y = (int) (myBitmap.Height * high);
                }

                if (x == 0)
                {
                    x = 1;
                }

                if (y == 0)
                {
                    y = 1;
                }

                var b = new System.Drawing.Bitmap(x, y);
                var g = System.Drawing.Graphics.FromImage(b);
                // 插值算法的质量 
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(myBitmap, new System.Drawing.Rectangle(0, 0, x, y),
                    new System.Drawing.Rectangle(0, 0, myBitmap.Width, myBitmap.Height),
                    System.Drawing.GraphicsUnit.Pixel);
                g.Dispose();
                myBitmap.Dispose();
                b.Save(files[i].FullName);
                b.Dispose();
            }

            Logger.Log("处理完成");
        }

        public static void ChangeImageSizeByFile(string path, int type, float weigh, float high)
        {
            if (!File.Exists(path))
            {
                Logger.Log("请检查文件路径" + path);
                return;
            }

            var myBitmap = new System.Drawing.Bitmap(path);
            var x = myBitmap.Width;
            var y = myBitmap.Height;
            if (type == 1)
            {
                x = (int) weigh;
                y = (int) high;
            }
            else
            {
                x = (int) (myBitmap.Width * weigh);
                y = (int) (myBitmap.Height * high);
            }

            if (x == 0)
            {
                x = 1;
            }

            if (y == 0)
            {
                y = 1;
            }


            var b = new System.Drawing.Bitmap(x, y);
            var g = System.Drawing.Graphics.FromImage(b);
            // 插值算法的质量 
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.DrawImage(myBitmap, new System.Drawing.Rectangle(0, 0, x, y),
                new System.Drawing.Rectangle(0, 0, myBitmap.Width, myBitmap.Height),
                System.Drawing.GraphicsUnit.Pixel);
            g.Dispose();
            myBitmap.Dispose();
            b.Save(path);
            b.Dispose();
            Logger.Log("处理完成");
        }
    }
}
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


        public static void ChangeImageSize()
        {
            var oripath = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            Logger.Log("路径" + oripath);
            string[] pathary = oripath.Replace(@"\", "/").Split('/');
            string path = "";
            for (int i = 0; i < pathary.Length; i++)
            {
                Logger.Log("路径" + pathary[i]);
                if (pathary[i] == "client2")
                {
                    break;
                }
                else
                {
                    path = path + pathary[i] + @"\";
                }
            }

            Logger.Log("路径" + path);
            path = path + @"art\UIProject\assets";
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
                var x = Math.Ceiling(myBitmap.Width * 720 / 1080f);
                var y = Math.Ceiling(myBitmap.Height * 1440 / 2160f);
                var b = new System.Drawing.Bitmap((int) x, (int) y);
                var g = System.Drawing.Graphics.FromImage(b);
                // 插值算法的质量 
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(myBitmap, new System.Drawing.Rectangle(0, 0, (int) x, (int) y),
                    new System.Drawing.Rectangle(0, 0, myBitmap.Width, myBitmap.Height),
                    System.Drawing.GraphicsUnit.Pixel);
                g.Dispose();
                myBitmap.Dispose();
                b.Save(files[i].FullName);
                b.Dispose();
            }

            Logger.Log("处理完成");
        }


        public static void ChangeImageSizeByFolder(string path)
        {
            // var oripath = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            // Logger.Log("路径" + oripath);
            // string[] pathary = oripath.Replace(@"\", "/").Split('/');
            //
            // string path = "";
            //
            //
            // for (int i = 0; i < pathary.Length; i++)
            // {
            //     Logger.Log("路径" + pathary[i]);
            //     if (pathary[i] == "client2")
            //     {
            //         break;
            //     }
            //     else
            //     {
            //         path = path + pathary[i] + @"\";
            //     }
            // }
            //
            // Logger.Log("路径" + path);
            // path = path + @"art\UIProject\assets\" + folder;

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
                var x = Math.Ceiling(myBitmap.Width * 720 / 1080f);
                var y = Math.Ceiling(myBitmap.Height * 1440 / 2160f);
                var b = new System.Drawing.Bitmap((int) x, (int) y);
                var g = System.Drawing.Graphics.FromImage(b);
                // 插值算法的质量 
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(myBitmap, new System.Drawing.Rectangle(0, 0, (int) x, (int) y),
                    new System.Drawing.Rectangle(0, 0, myBitmap.Width, myBitmap.Height),
                    System.Drawing.GraphicsUnit.Pixel);
                g.Dispose();
                myBitmap.Dispose();
                b.Save(files[i].FullName);
                b.Dispose();
            }

            Logger.Log("处理完成");
        }
    }
}
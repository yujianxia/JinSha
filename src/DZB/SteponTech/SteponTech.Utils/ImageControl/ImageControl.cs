using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.DrawingCore;
using System.DrawingCore.Drawing2D;
using System.DrawingCore.Imaging;
using System.IO;
using System.Text;

namespace SteponTech.Utils.ImageControl
{
    public class ImageControl
    {
        public byte[] resizeImage(string filename, string path, int width, int height)
        {

            Image imgToResize = Image.FromFile(path + "\\" + filename);
            var savepath = path + "\\" + System.IO.Path.GetFileNameWithoutExtension(path + "\\" + filename) + width + "×" + height + System.IO.Path.GetExtension(path + "\\" + filename);

            // 要保存到的图片 
            Bitmap b = new Bitmap(width, height);
            try
            {
                if (!File.Exists(savepath))
                {
                    Graphics g = Graphics.FromImage(b);
                    // 插值算法的质量 
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.DrawImage(imgToResize, new Rectangle(0, 0, width, height), new Rectangle(0, 0, imgToResize.Width, imgToResize.Height), GraphicsUnit.Pixel);
                    g.Dispose();
                }
            }
            catch (Exception e)
            {
                return null;
            }


            MemoryStream stream = new MemoryStream();
            b.Save(stream, ImageFormat.Jpeg);
            byte[] data = new byte[stream.Length];
            stream.Seek(0, SeekOrigin.Begin);
            stream.Read(data, 0, Convert.ToInt32(stream.Length));
            return data;

        }
    }
}

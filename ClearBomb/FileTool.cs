using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ClearBomb
{
    public static class FileTool
    {
        //写入高度，宽度，雷数
        public static void WriteFile(String fileName, string content1, string content2, string content3)
        {
            FileStream fs = new FileStream(fileName, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(content1);
            sw.WriteLine(content2);
            sw.WriteLine(content3);
            sw.Close();
            fs.Close();
        }

        public static void ReadFile(string fileName, ref string content1, ref string content2, ref string content3)
        {
            if (!File.Exists(fileName))
            {
                return;
            }
            FileStream fs = new FileStream(fileName, FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            content1 = sr.ReadLine();
            content2 = sr.ReadLine();
            content3 = sr.ReadLine();
            sr.Close();
            fs.Close();
        }

        //写入选择状态，简单/一般/困难/自定义
        public static void WriteFile(String fileName, string content)
        {
            FileStream fs = new FileStream(fileName, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(content);
            sw.Close();
            fs.Close();
        }

        public static string ReadFile(string fileName)
        {
            if (!File.Exists(fileName))
            {
                return string.Empty;
            }
            FileStream fs = new FileStream(fileName, FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            string content = sr.ReadLine();
            sr.Close();
            fs.Close();
            return content;
        }
    }
}

using System.Diagnostics;
using System.IO;
using System;
using System.Windows.Forms;
using System.Threading;

namespace MyMCBBS.Updater
{
    internal class Program
    {
        private static void Main()
        {
            try
            {
                Thread.Sleep(2000);
                if (Process.GetProcessesByName("MyMCBBS").Length > 0)
                {
                    Process.GetProcessesByName("MyMCBBS")[0].Kill();
                }

                foreach (var item in Directory.GetFiles("cache"))
                {
                    if (File.Exists(Path.GetFileName(item)))
                    {
                        File.Delete(Path.GetFileName(item));
                    }

                    File.Move(item, Path.GetFileName(item));
                }

                foreach (var item in Directory.GetDirectories("cache"))
                {
                    if (Directory.Exists(item.Replace(@"cache\", string.Empty)))
                    {
                        Directory.Delete(item.Replace(@"cache\", string.Empty), true);
                    }

                    Directory.Move(item, item.Replace(@"cache\", string.Empty));
                }

                Directory.Delete("cache", true);

                Process.Start("MyMCBBS.exe");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
    }
}
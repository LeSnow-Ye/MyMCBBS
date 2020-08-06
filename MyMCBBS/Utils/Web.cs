namespace MyMCBBS.Utils
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// 网络相关.
    /// </summary>
    internal static class Web
    {
        /// <summary>
        /// 下载网页内容，发生错误则返回"下载失败"，内置多次尝试功能.
        /// </summary>
        /// <param name="url">网址.</param>
        /// <returns>网页内容.</returns>
        internal static string DownloadWebsite(string url)
        {
            using (WebClient webClient = new WebClient())
            {
                webClient.Encoding = Encoding.UTF8;

                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        string result;
                        Task task = new Task(() =>
                        {
                            result = webClient.DownloadString(url);
                        });
                        task.Start();
                        task.Wait(10000);

                        return webClient.DownloadString(url);
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(e.ToString());
                    }
                }

                return "下载失败";
            }
        }

        /// <summary>
        /// 用浏览器打开网页.
        /// </summary>
        /// <param name="url">要打开的网页链接.</param>
        internal static void OpenWithBrowser(string url)
        {
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $"/c start {url.Replace("&", "\"&\"")}",
                UseShellExecute = false,
                RedirectStandardInput = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true,
            };
            process.StartInfo = startInfo;
            process.Start();
        }

        /// <summary>
        /// 下载文件.
        /// </summary>
        /// <param name="url">文件地址.</param>
        /// <param name="path">保存路径.</param>
        /// <returns>false 为失败.</returns>
        internal static bool DownloadFile(string url, string path)
        {
            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(path)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(path));
                }

                using (WebClient webClient = new WebClient())
                {
                    webClient.DownloadFile(url, path);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
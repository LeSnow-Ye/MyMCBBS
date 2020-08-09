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
        /// 用浏览器打开网页.
        /// </summary>
        /// <param name="url">要打开的网页链接.</param>
        internal static void OpenWithBrowser(string url)
        {
            Process.Start(new ProcessStartInfo(url));
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
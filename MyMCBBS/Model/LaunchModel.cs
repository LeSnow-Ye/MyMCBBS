using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using MyMCBBS.Utils;
using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using LeanCloud;
using System.Windows;
using System.Xml.Serialization;

namespace MyMCBBS.Model
{
    public class LaunchModel : ObservableObject
    {
        private string launchInfo = "laoding...";

        public LaunchModel()
        {
            this.Launching();
        }

        private void Launching()
        {
            Task.Run(async () =>
            {
                try
                {
                    this.LaunchInfo = "正在检查更新...";
                    await Task.Delay(1000);

                    string content = new WebClient().DownloadString("https://gitee.com/YeZhi233/MyMCBBS/raw/master/UpdataInfo");
                    byte[] bytes = Encoding.ASCII.GetBytes(content);

                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(UpdateInfoModel));
                    var updateInfo = xmlSerializer.Deserialize(new MemoryStream(bytes)) as UpdateInfoModel;

                    if (updateInfo.Version > App.Version)
                    {
                        this.LaunchInfo = $"检查到更新 v{updateInfo.Version.ToString()[0]}.{updateInfo.Version.ToString()[1]}.{updateInfo.Version.ToString()[2]}";
                        await Task.Delay(1000);
                        this.LaunchInfo = "下载中...";
                        foreach (var item in updateInfo.Files)
                        {
                            Web.DownloadFile(item.Url, item.Path);
                        }

                        this.LaunchInfo = "下载完成...";
                        Process.Start("Updater.exe");
                        Environment.Exit(0);
                    }
                    else
                    {
                        this.LaunchInfo = "准备中...";
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.ToString());
                    this.LaunchInfo = "检查更新出错";
                    await Task.Delay(1000);
                }

                AVClient.Initialize("rYLnugXiHMPlOHVHrTg3SW12-gzGzoHsz","4Otay1pQaK0TPWSrHDuq8cA8", "https://rylnugxi.lc-cn-n1-shared.com");
                AVClient.HttpLog((o) => Debug.WriteLine(o));
                await App.Current.Dispatcher.Invoke(async () =>
                {
                    App.UserModel = new UserModel();
                    await App.UserModel.RefreshAsync();
                    try
                    {
                        AVObject data = new AVObject("Data")
                        {
                            ["UID"] = App.UserModel.UID,
                            ["Credit"] = App.UserModel.Credit,
                            ["Name"] = App.UserModel.Name,
                            ["Version"] = App.Version,
                            ["LaunchTime"] = DateTime.Now.ToString(),
                        };

                        await data.SaveAsync();
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(e.ToString());
                    }
                });

                Messenger.Default.Send(true, "Close");
            });
        }

        /// <summary>
        /// Gets or sets 启动信息.
        /// </summary>
        public string LaunchInfo
        {
            get => this.launchInfo;
            set
            {
                this.launchInfo = value;
                this.RaisePropertyChanged("launchInfo");
            }
        }
    }
}
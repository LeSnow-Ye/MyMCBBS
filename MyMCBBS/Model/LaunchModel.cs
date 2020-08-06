using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using MyMCBBS.Utils;
using MyMCBBS.Model;
using System.Xml.Serialization;
using System.Diagnostics;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using MyMCBBS.Utils;
using System.Data;
using GalaSoft.MvvmLight.Messaging;

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

                    string content = Web.DownloadWebsite("https://gitee.com/YeZhi233/MyMCBBS/raw/master/UpdataInfo");
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

                App.Current.Dispatcher.Invoke(() => App.UserModel = new UserModel());
                await Task.Delay(500);
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

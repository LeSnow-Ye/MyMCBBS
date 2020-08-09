namespace MyMCBBS.ViewModel
{
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using GalaSoft.MvvmLight.Messaging;
    using MyMCBBS.Model;
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using System.Xml.Serialization;

    public class SettingsViewModel : ViewModelBase
    {
        private Config config;

        public SettingsViewModel()
        {
            this.config = new Config();
            this.Load();

            // 开机自启
            IWshRuntimeLibrary.WshShell shell = new IWshRuntimeLibrary.WshShell();
            IWshRuntimeLibrary.IWshShortcut shortcut =
                   (IWshRuntimeLibrary.IWshShortcut)shell.CreateShortcut(
                   Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\" + Application.ProductName + ".lnk");

            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\" + Application.ProductName + ".lnk")
                && shortcut.TargetPath == Application.ExecutablePath)
            {
                this.Config.RunAtStartUp = true;
            }

            Messenger.Default.Register<bool>(this, "Save", (b) =>
            {
                if (b)
                {
                    this.Save();
                }
            });

            this.OkButtonCommand = new RelayCommand<double>((input) =>
            {
                if (this.Config.UID != (int)input)
                {
                    this.Config.UID = (int)input;
                    this.Save();
                }

                App.Current.Dispatcher.BeginInvoke((Action)(async () => await App.UserModel.RefreshAsync()));
                Messenger.Default.Send<bool>(false, "OpenSettingsCommand");
            });
        }

        public RelayCommand<double> OkButtonCommand { get; set; }

        /// <summary>
        /// 加载设置.
        /// </summary>
        public void Load()
        {
            if (File.Exists("MyMCBBS.conf"))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(Config));

                try
                {
                    this.Config = (Config)xmlSerializer.Deserialize(new StreamReader("MyMCBBS.conf"));
                    this.Config.Loaded = true;
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.ToString());
                }
            }
        }

        /// <summary>
        /// 保存设置.
        /// </summary>
        public void Save()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Config));

            try
            {
                xmlSerializer.Serialize(new StreamWriter("MyMCBBS.conf"), this.Config);
                Debug.WriteLine("Saved");
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
        }

        /// <summary>
        /// Gets or sets 配置.
        /// </summary>
        public Config Config
        {
            get => this.config;
            set
            {
                this.config = value;
                this.RaisePropertyChanged("config");
            }
        }
    }
}
namespace MyMCBBS.ViewModel
{
    using GalaSoft.MvvmLight;
    using MyMCBBS.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Serialization;
    using MyMCBBS.Utils;
    using System.IO;
    using System.Diagnostics;

    public class SettingsViewModel : ViewModelBase
    {

        private Config config;

        public SettingsViewModel()
        {
            this.config = new Config();
            this.Load();
        }

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

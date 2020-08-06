using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using GalaSoft.MvvmLight;
using MyMCBBS.Utils;
using System.IO;
using System.Diagnostics;

namespace MyMCBBS.Model
{
    /// <summary>
    /// 设置类.
    /// </summary>
    [Serializable]
    public class Config : ObservableObject
    {
        private int uid = 1249946;

        /// <summary>
        /// Gets or sets UID.
        /// </summary>
        public int UID
        {
            get => this.uid;
            set
            {
                this.uid = value;
                this.RaisePropertyChanged("uid");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 是否加载了配置文件.
        /// </summary>
        public bool Loaded = false;
    }
}

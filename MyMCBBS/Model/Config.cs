using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyMCBBS.Utils;
using System.IO;
using System.Diagnostics;
using System.Windows;

namespace MyMCBBS.Model
{
    /// <summary>
    /// 设置类.
    /// </summary>
    [Serializable]
    public class Config
    {

        /// <summary>
        /// Gets or sets UID.
        /// </summary>
        public int UID { get; set; } = 1249946;

        /// <summary>
        /// Gets or sets a value indicating whether 是否加载了配置文件.
        /// </summary>
        public bool Loaded = false;

        public bool SuperMode = false;

        public bool PlaySound = false;
    }
}

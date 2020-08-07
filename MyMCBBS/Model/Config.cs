using System;
using System.Collections.Generic;

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

        /// <summary>
        /// 问答版
        /// </summary>
        public bool SuperMode = false;

        public bool NotifyQA = false;
        public bool PlaySoundQA = false;

        /// <summary>
        /// 自定义
        /// </summary>
        public bool AutoOpen = false;

        public bool NotifyCuston = false;
        public bool PlaySoundCustom = false;
        public List<string> CustonPartList = new List<string>();
    }
}
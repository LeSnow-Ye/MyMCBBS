using System;
using System.Collections.Generic;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System.IO;

namespace MyMCBBS.Model
{
    /// <summary>
    /// 设置类.
    /// </summary>
    [Serializable]
    public class Config : ObservableObject
    {
        private int uid = 1249946;


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


        private bool runAtStartUp = false;

        /// <summary>
        /// Gets or sets 介绍.
        /// </summary>
        public bool RunAtStartUp
        {
            get => this.runAtStartUp;
            set
            {
                this.runAtStartUp = value;
                Messenger.Default.Send<bool>(true, "Save");
                if (!value)
                {
                    File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\" + System.Windows.Forms.Application.ProductName + ".lnk");
                }
                else
                {
                    IWshRuntimeLibrary.WshShell shell = new IWshRuntimeLibrary.WshShell();
                    IWshRuntimeLibrary.IWshShortcut shortcut =
                           (IWshRuntimeLibrary.IWshShortcut)shell.CreateShortcut(
                           Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\" + System.Windows.Forms.Application.ProductName + ".lnk");
                    shortcut.TargetPath = System.Windows.Forms.Application.ExecutablePath;
                    shortcut.WorkingDirectory = Environment.CurrentDirectory;
                    shortcut.WindowStyle = 1;
                    shortcut.Description = System.Windows.Forms.Application.ProductName + "_lnk";
                    shortcut.Save();
                }

                this.RaisePropertyChanged("RunAtStartUp");
            }
        }
    }
}
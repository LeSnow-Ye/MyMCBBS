using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MyMCBBS.Model;
using MyMCBBS.ViewModel;

namespace MyMCBBS
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public static int Version = 2000;
        public static SettingsViewModel Config = new SettingsViewModel();
        public static UserModel UserModel;
    }
}

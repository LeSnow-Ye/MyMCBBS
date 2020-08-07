using MyMCBBS.Model;
using MyMCBBS.Utils;
using MyMCBBS.ViewModel;
using System.Windows;

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

        public static PostsSpider PostsSpider = new PostsSpider();
    }
}
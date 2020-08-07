using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Data;
using System.Windows.Documents;
using HandyControl.Tools;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using HandyControl.Controls;
using GalaSoft.MvvmLight;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HandyControl.Tools.Extension;

namespace MyMCBBS
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            var stb = this.Resources["FadeOut"] as Storyboard;
            Storyboard.SetTarget(stb, this);
            stb.Completed += (s, o) => Environment.Exit(0);
            stb.Begin();
            this.NotifyIcon.Hide();
        }

        private void Window_Drag(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                try
                {
                    this.DragMove();
                }
                catch
                {
                }
            }
        }

        private void NotifyIcon_Click(object sender, RoutedEventArgs e) => this.Show();
        private void HideWindow_Click(object sender, RoutedEventArgs e) => this.Hide();
    }
}

using HandyControl.Controls;
using HandyControl.Data;
using HandyControl.Tools.Extension;
using MyMCBBS.View;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Notification.Show(new NewPostNotificationView(), ShowAnimation.HorizontalMove, false);
        }
    }
}
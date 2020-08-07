namespace MyMCBBS.View
{
    using GalaSoft.MvvmLight.Messaging;
    using MyMCBBS.ViewModel;
    using System;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Media.Animation;

    /// <summary>
    /// LaunchWindow.xaml 的交互逻辑.
    /// </summary>
    public partial class LaunchingWindow : Window
    {
        public LaunchingWindow()
        {
            if (Process.GetProcessesByName("MyMCBBS").Length > 1)
            {
                MessageBox.Show("MyMCBBS已运行，请检查任务栏");
                Environment.Exit(0);
            }

            Messenger.Default.Register<bool>(this, "Close", (b) =>
            {
                if (b)
                {
                    App.Current.Dispatcher.Invoke(() =>
                    {
                        var mainWindow = new MainWindow();
                        mainWindow.Show();
                        this.CloseWindow();
                    });
                }
            });

            this.InitializeComponent();
            this.DataContext = new LaunchViewModel();
        }

        private void Grid_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.CloseWindow();
        }

        private void CloseWindow()
        {
            var stb = this.Resources["FadeOut"] as Storyboard;
            Storyboard.SetTarget(stb, this);
            stb.Completed += (s, o) => this.Close();
            stb.Begin();
        }
    }
}
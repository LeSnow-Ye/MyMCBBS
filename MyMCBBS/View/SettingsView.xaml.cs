using System.Windows.Controls;

namespace MyMCBBS.View
{
    /// <summary>
    /// SettingsView.xaml 的交互逻辑
    /// </summary>
    public partial class SettingsView : UserControl
    {
        public SettingsView()
        {
            InitializeComponent();
            this.DataContext = App.Config;
        }
    }
}

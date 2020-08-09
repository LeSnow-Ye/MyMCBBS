using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LiveCharts;
using LiveCharts.Wpf;

namespace MyMCBBS.View
{
    /// <summary>
    /// CreditAnalystView.xaml 的交互逻辑
    /// </summary>
    public partial class CreditAnalystView : UserControl
    {
        public CreditAnalystView()
        {
            InitializeComponent();
        }

        private void PieChart_DataHover(object sender, ChartPoint chartPoint)
        {
            var chart = (PieChart)chartPoint.ChartView;

            //clear selected slice.
            foreach (PieSeries series in chart.Series)
            {
                series.Fill.Opacity = 1;
            }

            var selectedSeries = (PieSeries)chartPoint.SeriesView;
            selectedSeries.Fill.Opacity = 0.8;
        }

        private void PieChart_MouseLeave(object sender, MouseEventArgs e)
        {
            foreach (PieSeries series in (sender as PieChart).Series)
            {
                series.Fill.Opacity = 1;
            }
        }
    }
}

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using HandyControl.Controls;
using HandyControl.Data;
using MyMCBBS.Model;
using System;

namespace MyMCBBS.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public RelayCommand<FunctionEventArgs<object>> IndexSelectionChangedCommand => new Lazy<RelayCommand<FunctionEventArgs<object>>>(() =>
            new RelayCommand<FunctionEventArgs<object>>(SwitchItem)).Value;

        private MainWindowModel mainWindowModel;

        /// <summary>
        /// Gets or sets MainWindowModel.
        /// </summary>
        public MainWindowModel MainWindowModel
        {
            get => this.mainWindowModel;
            set
            {
                this.mainWindowModel = value;
                this.RaisePropertyChanged("mainWindowModel");
            }
        }

        public RelayCommand AboutCommand { get; set; } = new RelayCommand(() =>
        {
            Messenger.Default.Send<bool>(true, "OpenAboutCommand");
        });

        private void SwitchItem(FunctionEventArgs<object> info)
        {
            string input = (info.Info as SideMenuItem)?.Header.ToString();
            this.MainWindowModel.Title = input;

            switch (input)
            {
                case "主页":
                    this.MainWindowModel.PresentIndex = MainWindowModel.Index.Home;
                    break;

                case "爱心收割机":
                    this.MainWindowModel.PresentIndex = MainWindowModel.Index.HeartsHarvester;
                    break;

                case "主题抓取器":
                    this.MainWindowModel.PresentIndex = MainWindowModel.Index.PostSpider;
                    break;

                case "收藏":
                    this.MainWindowModel.PresentIndex = MainWindowModel.Index.Collections;
                    break;

                case "积分分析":
                    this.MainWindowModel.PresentIndex = MainWindowModel.Index.CreditAnalist;
                    break;

                case "统计信息":
                    this.MainWindowModel.PresentIndex = MainWindowModel.Index.Statistics;
                    break;

                case "小工具":
                    this.MainWindowModel.PresentIndex = MainWindowModel.Index.Tools;
                    break;

                case "快捷导航":
                    this.MainWindowModel.PresentIndex = MainWindowModel.Index.Sites;
                    break;
            }
        }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            this.mainWindowModel = new MainWindowModel();
        }
    }
}
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
                case "��ҳ":
                    this.MainWindowModel.PresentIndex = MainWindowModel.Index.Home;
                    break;

                case "�����ո��":
                    this.MainWindowModel.PresentIndex = MainWindowModel.Index.HeartsHarvester;
                    break;

                case "����ץȡ��":
                    this.MainWindowModel.PresentIndex = MainWindowModel.Index.PostSpider;
                    break;

                case "�ղ�":
                    this.MainWindowModel.PresentIndex = MainWindowModel.Index.Collections;
                    break;

                case "���ַ���":
                    this.MainWindowModel.PresentIndex = MainWindowModel.Index.CreditAnalist;
                    break;

                case "ͳ����Ϣ":
                    this.MainWindowModel.PresentIndex = MainWindowModel.Index.Statistics;
                    break;

                case "С����":
                    this.MainWindowModel.PresentIndex = MainWindowModel.Index.Tools;
                    break;

                case "��ݵ���":
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
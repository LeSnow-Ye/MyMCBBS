using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MyMCBBS.Model;
using MyMCBBS.Utils;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace MyMCBBS.ViewModel
{
    public class DialogHostViewModel : ViewModelBase
    {
        private DialogHostModel dialogHostModel;

        public DialogHostViewModel()
        {
            Messenger.Default.Register<bool>(this, "OpenAboutCommand", (i) =>
            {
                if (i)
                {
                    this.DialogHostModel.IsAboutDialogOpen = true;
                }
            });

            Messenger.Default.Register<bool>(this, "OpenSettingsCommand", (i) =>
            {
                if (i)
                {
                    this.DialogHostModel.IsSettingsDialogOpen = true;
                }
                else
                {
                    this.DialogHostModel.IsSettingsDialogOpen = false;
                }
            });

            this.dialogHostModel = new DialogHostModel();
            this.Close = new RelayCommand(() =>
            {
                this.DialogHostModel.IsNoticeDialogOpen = false;
                this.DialogHostModel.IsAboutDialogOpen = false;
            });
            this.OkButtonCommand = new RelayCommand<double>((input) =>
            {
                App.Config.Config.UID = (int)input;
                App.Config.Save();

                Task.Run(async () =>
                {
                    await App.Current.Dispatcher.BeginInvoke((Action)(async () => await App.UserModel.RefreshAsync()));
                    this.DialogHostModel.IsNewUserDialogOpen = false;
                });
            });

            Task.Run(async () =>
            {
                if (!App.Config.Config.Loaded)
                {
                    await Task.Delay(1000);
                    this.DialogHostModel.IsNewUserDialogOpen = true;
                }
                else if (File.Exists("Notice.jpg"))
                {
                    try
                    {
                        await Task.Delay(1000);
                        this.DialogHostModel.IsNoticeDialogOpen = true;
                        App.Current.Dispatcher.Invoke(() =>
                        {
                            this.DialogHostModel.NoticeContent.BeginInit();
                            this.DialogHostModel.NoticeContent.CacheOption = System.Windows.Media.Imaging.BitmapCacheOption.OnLoad;
                            this.DialogHostModel.NoticeContent.UriSource = new Uri(Directory.GetCurrentDirectory() + "\\Notice.jpg");
                            this.DialogHostModel.NoticeContent.EndInit();
                            File.Delete("Notice.jpg");
                        });
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(e.ToString());
                    }
                }
            });
        }

        public RelayCommand Close { get; set; }

        public RelayCommand<double> OkButtonCommand { get; set; }

        public RelayCommand<Uri> HyperlinkCommand { get; set; } = new RelayCommand<Uri>((uri) => Web.OpenWithBrowser(uri.ToString()));

        /// <summary>
        /// Gets or sets dialogHostModel.
        /// </summary>
        public DialogHostModel DialogHostModel
        {
            get => this.dialogHostModel;
            set
            {
                this.dialogHostModel = value;
                this.RaisePropertyChanged("dialogHostModel");
            }
        }
    }
}
using GalaSoft.MvvmLight;
using System.Windows.Media.Imaging;

namespace MyMCBBS.Model
{
    public class DialogHostModel : ObservableObject
    {
        private bool isNewUserDialogOpen = false;
        private bool isNoticeDialogOpen = false;
        private bool isAboutDialogOpen = false;
        private bool isSettingsDialogOpen = false;
        private BitmapImage noticeContent = new BitmapImage();

        public bool IsNoticeDialogOpen
        {
            get => this.isNoticeDialogOpen;
            set
            {
                this.isNoticeDialogOpen = value;
                this.RaisePropertyChanged("isNoticeDialogOpen");
            }
        }

        public bool IsNewUserDialogOpen
        {
            get => this.isNewUserDialogOpen;
            set
            {
                this.isNewUserDialogOpen = value;
                this.RaisePropertyChanged("isNewUserDialogOpen");
            }
        }

        public bool IsAboutDialogOpen
        {
            get => this.isAboutDialogOpen;
            set
            {
                this.isAboutDialogOpen = value;
                this.RaisePropertyChanged("IsAboutDialogOpen");
            }
        }

        public bool IsSettingsDialogOpen
        {
            get => this.isSettingsDialogOpen;
            set
            {
                this.isSettingsDialogOpen = value;
                this.RaisePropertyChanged("IsSettingsDialogOpen");
            }
        }

        /// <summary>
        /// Gets or sets 介绍.
        /// </summary>
        public BitmapImage NoticeContent
        {
            get => this.noticeContent;
            set
            {
                this.noticeContent = value;
                this.RaisePropertyChanged("noticeContent");
            }
        }
    }
}
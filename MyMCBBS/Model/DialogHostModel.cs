
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace MyMCBBS.Model
{
    public class DialogHostModel : ObservableObject
    {
        private bool isNoticeDialogOpen = false;
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


        private bool isNewUserDialogOpen = false;

        public bool IsNewUserDialogOpen
        {
            get => this.isNewUserDialogOpen;
            set
            {
                this.isNewUserDialogOpen = value;
                this.RaisePropertyChanged("isNewUserDialogOpen");
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

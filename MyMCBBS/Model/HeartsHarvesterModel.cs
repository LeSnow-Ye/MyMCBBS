using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyMCBBS.Utils;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using MyMCBBS.ViewModel;

namespace MyMCBBS.Model
{
    public class HeartsHarvesterModel : ObservableObject
    {
        private ObservableCollection<Post> qaPosts = new ObservableCollection<Post>();
        private bool superMode = App.Config.Config.SuperMode;
        private bool playSound = App.Config.Config.PlaySoundQA;
        private bool notify = App.Config.Config.NotifyQA;

        public HeartsHarvesterModel()
        {
            this.QAPosts.CollectionChanged += (s, e) =>
            {
                (App.Current.FindResource("Locator") as ViewModelLocator).NewPostNotification.Part = QAPosts[0].PostPart;
                (App.Current.FindResource("Locator") as ViewModelLocator).NewPostNotification.URL = QAPosts[0].Url;
                (App.Current.FindResource("Locator") as ViewModelLocator).NewPostNotification.Title = QAPosts[0].Title;
            };
        }

        /// <summary>
        /// Gets or sets 播放声音.
        /// </summary>
        public bool PlaySound
        {
            get => this.playSound;
            set
            {
                this.playSound = value;
                App.Config.Config.PlaySoundQA = value;
                App.Config.Save();
                this.RaisePropertyChanged("playSound");
            }
        }

        /// <summary>
        /// Gets or sets 超级模式.
        /// </summary>
        public bool SuperMode
        {
            get => this.superMode;
            set
            {
                this.superMode = value;
                App.Config.Config.SuperMode = value;
                App.Config.Save();
                this.RaisePropertyChanged("SuperMode");
            }
        }

        /// <summary>
        /// Gets or sets 消息通知.
        /// </summary>
        public bool Notify
        {
            get => this.notify;
            set
            {
                this.notify = value;
                App.Config.Config.NotifyQA = value;
                App.Config.Save();
                this.RaisePropertyChanged("Notify");
            }
        }

        public ObservableCollection<Post> QAPosts
        {
            get => this.qaPosts;
            set
            {
                this.qaPosts = value;
                this.RaisePropertyChanged("QAPosts");
            }
        }
    }
}

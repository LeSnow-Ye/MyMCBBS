using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyMCBBS.Utils;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;

namespace MyMCBBS.Model
{
    public class HeartsHarvesterModel : ObservableObject
    {
        private ObservableCollection<Post> qaPosts = new ObservableCollection<Post>();


        private bool superMode = App.Config.Config.SuperMode;


        private bool playSound = App.Config.Config.PlaySound;

        /// <summary>
        /// Gets or sets 播放声音.
        /// </summary>
        public bool PlaySound
        {
            get => this.playSound;
            set
            {
                this.playSound = value;
                App.Config.Config.PlaySound = value;
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

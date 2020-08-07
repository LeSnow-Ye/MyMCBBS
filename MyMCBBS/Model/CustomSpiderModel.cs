using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyMCBBS.Utils;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using MyMCBBS.ViewModel;
using GalaSoft.MvvmLight.Command;

namespace MyMCBBS.Model
{
    public class CustomSpiderModel : ObservableObject
    {
        private ObservableCollection<Post> posts = new ObservableCollection<Post>();
        private bool autoOpen = App.Config.Config.AutoOpen;
        private bool playSound = App.Config.Config.PlaySoundCustom;
        private bool notify = App.Config.Config.NotifyCuston;

        private ObservableCollection<string> customParts = new ObservableCollection<string>(App.Config.Config.CustonPartList);

        public ObservableCollection<string> CustomParts
        {
            get => this.customParts;
            set
            {
                this.customParts = value;
                this.RaisePropertyChanged("CustomParts");
            }
        }

        public CustomSpiderModel()
        {
            this.Posts.CollectionChanged += (s, e) =>
            {
                (App.Current.FindResource("Locator") as ViewModelLocator).NewPostNotification.Part = Posts[0].PostPart;
                (App.Current.FindResource("Locator") as ViewModelLocator).NewPostNotification.URL = Posts[0].Url;
                (App.Current.FindResource("Locator") as ViewModelLocator).NewPostNotification.Title = Posts[0].Title;
            };

            this.Save = new RelayCommand(() =>
            {
                App.Config.Config.CustonPartList = this.CustomParts.ToList();
                App.Config.Save();
            });

            this.Add = new RelayCommand<string>((input) =>
            {
                if (!this.CustomParts.ToList().Exists((s) => s == input))
                {
                    this.CustomParts.Add(input);
                }
            });

            this.Delete = new RelayCommand<string>((input) => this.CustomParts.Remove(input));
        }

        public RelayCommand Save { get; set; }
        public RelayCommand<string> Delete { get; set; }
        public RelayCommand<string> Add { get; set; }

        public List<string> PartsList { get; set; } = new List<string>
        {
            "新闻资讯",
            "游戏技巧",
            "软件资源",
            "编程开发",
            "翻译&Wiki",
            "矿工茶馆",
            "视频实况",
            "材质资源",
            "皮肤分享",
            "周边创作",
            "展示&共享",
            "搬运&鉴赏",
            "匠人酒馆",
            "Mod讨论",
            "Mod教程",
            "Mod发布",
            "联机教程",
            "整合包发布",
            "服务端插件",
            "服务端整合包",
            "基岩版作品发布",
            "基岩版技巧教程",
            "基岩版问答",
            "基岩版软件资源",
            "基岩版服务器",
            "基岩版插件&服务端",
            "公告和反馈",
        };

        /// <summary>
        /// Gets or sets 播放声音.
        /// </summary>
        public bool PlaySound
        {
            get => this.playSound;
            set
            {
                this.playSound = value;
                App.Config.Config.PlaySoundCustom = value;
                App.Config.Save();
                this.RaisePropertyChanged("playSound");
            }
        }

        /// <summary>
        /// Gets or sets 自动打开.
        /// </summary>
        public bool AutoOpen
        {
            get => this.autoOpen;
            set
            {
                this.autoOpen = value;
                App.Config.Config.AutoOpen = value;
                App.Config.Save();
                this.RaisePropertyChanged("AutoOpen");
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
                App.Config.Config.NotifyCuston = value;
                App.Config.Save();
                this.RaisePropertyChanged("Notify");
            }
        }

        public ObservableCollection<Post> Posts
        {
            get => this.posts;
            set
            {
                this.posts = value;
                this.RaisePropertyChanged("Posts");
            }
        }
    }
}

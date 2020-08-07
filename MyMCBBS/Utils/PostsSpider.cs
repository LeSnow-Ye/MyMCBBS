using AngleSharp;
using AngleSharp.Dom;
using GalaSoft.MvvmLight.Messaging;
using HandyControl.Controls;
using HandyControl.Data;
using MyMCBBS.Model;
using MyMCBBS.View;
using MyMCBBS.ViewModel;
using System.Diagnostics;
using System.Linq;
using System.Media;
using System.Threading.Tasks;
using System.Timers;

namespace MyMCBBS.Utils
{
    public struct PostMessage
    {
        public Post Post;
        public int Index;
    }

    public class PostsSpider
    {
        private Timer Timer = new Timer(3000);

        public void Start() => this.Timer.Start();

        public PostsSpider()
        {
            this.Timer.Elapsed += (s, e) => this.Refresh();
            this.Refresh(true);
        }

        /// <summary>
        /// 刷新并将帖子送到该去的地方
        /// </summary>
        public void Refresh(bool initialization = false)
        {
            Debug.WriteLine("Refresh");
            Task.Run(async () =>
            {
                // 最新主题失效因此抓取 抢沙发
                string html = Web.DownloadWebsite($"https://www.mcbbs.net/forum.php?mod=guide&view=sofa");
                var pageDocument = await BrowsingContext.New(Configuration.Default).OpenAsync(req => req.Content(html));

                var postsPart = pageDocument.QuerySelectorAll("#threadlist > div.bm_c > table > tbody").ToList(); // 所有帖子的集合

                Post post;
                bool canPlaySound = false;
                for (int i = 0; i < 20; i++)
                {
                    var item = postsPart[i];

                    post = new Post()
                    {
                        Url = "https://www.mcbbs.net/" + item.QuerySelector("tbody > tr > th > a").GetAttribute("href"),
                        Title = item.QuerySelector("tbody > tr > th > a").Text(),
                        Poster = item.QuerySelector("tbody > tr > td > cite > a").Text(),
                        PostPart = item.QuerySelector("tbody > tr > td.by > a").Text(),
                    };

                    #region 爱心收割机

                    // 是否已经抓取过
                    bool? qaExist = false;

                    switch (post.PostPart)
                    {
                        case "原版问答":
                        case "Mod问答":
                        case "周边问答":
                        case "联机问答":
                        case "基岩版问答":
                            // 用Messenger降耦合
                            Messenger.Default.Send<PostMessage>(new PostMessage() { Post = post, Index = i }, "Hearts");
                            Messenger.Default.Register<bool>(this, "HeartsBack", (b) => qaExist = b);
                            break;
                    }

                    if (!initialization && qaExist == null)
                    {
                        if (App.Config.Config.PlaySoundQA)
                        {
                            canPlaySound = true;
                        }

                        if (App.Config.Config.SuperMode)
                        {
                            Web.OpenWithBrowser(post.Url);
                        }

                        if (App.Config.Config.NotifyQA)
                        {
                            App.Current.Dispatcher.Invoke(() => Notification.Show(new NewPostNotificationView(), ShowAnimation.HorizontalMove, false));
                        }
                    }

                    #endregion 爱心收割机

                    #region 自定义

                    // 是否已经抓取过
                    bool? customExist = false;

                    if (App.Config.Config.CustonPartList.Exists(s => post.PostPart == s))
                    {
                        Messenger.Default.Send<PostMessage>(new PostMessage() { Post = post, Index = i }, "Custom");
                        Messenger.Default.Register<bool>(this, "CustomBack", (b) => customExist = b);
                    }

                    if (!initialization && customExist == null)
                    {
                        if (App.Config.Config.PlaySoundCustom)
                        {
                            canPlaySound = true;
                        }

                        if (App.Config.Config.AutoOpen)
                        {
                            Web.OpenWithBrowser(post.Url);
                        }

                        if (App.Config.Config.NotifyCuston)
                        {
                            App.Current.Dispatcher.Invoke(() => Notification.Show(new NewPostNotificationView(), ShowAnimation.HorizontalMove, false));
                        }
                    }

                    #endregion 自定义

                    if (customExist == true && qaExist == true)
                    {
                        break; // 已经循环到存在的帖子即可停止循环以提高性能
                    }
                }

                if (canPlaySound)
                {
                    using (SoundPlayer soundPlayer = new SoundPlayer(Properties.Resources.levelup))
                    {
                        soundPlayer.Play();
                    }
                }
            });
        }
    }
}
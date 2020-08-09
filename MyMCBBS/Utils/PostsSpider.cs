using AngleSharp;
using AngleSharp.Dom;
using HandyControl.Controls;
using HandyControl.Data;
using MyMCBBS.Model;
using MyMCBBS.View;
using MyMCBBS.ViewModel;
using System.Diagnostics;
using System.Linq;
using System;
using System.Net;
using System.Text;
using System.Media;
using System.Threading.Tasks;
using System.Timers;

namespace MyMCBBS.Utils
{
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
                string html = "下载失败";
                using (WebClient webClient = new WebClient())
                {
                    webClient.Encoding = Encoding.UTF8;
                    try
                    {
                        // 最新主题失效因此抓取 抢沙发
                        html = webClient.DownloadString("https://www.mcbbs.net/forum.php?mod=guide&view=sofa");
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(e.ToString());
                    }
                }

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
                            App.Current.Dispatcher.Invoke(() =>
                            {
                                var posts = (App.Current.FindResource("Locator") as ViewModelLocator).HeartsHarvester.HeartsHarvesterModel.QAPosts;
                                if (posts.All(p => p.Url != post.Url))
                                {
                                    if (posts.Count <= i)
                                    {
                                        posts.Add(post);
                                    }
                                    else
                                    {
                                        posts.Insert(i, post);
                                    }

                                    // 最大缓存
                                    if (posts.Count >= 30)
                                    {
                                        posts.RemoveAt(29);
                                    }

                                    qaExist = null;
                                }
                                else
                                {
                                    qaExist = true;
                                }
                            });
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

                    var customSpider = (App.Current.FindResource("Locator") as ViewModelLocator).CustomSpider;
                    if (customSpider.CustomSpiderModel.CustomParts.ToList().Exists(s => post.PostPart == s))
                    {
                        App.Current.Dispatcher.Invoke(() =>
                        {
                            if (customSpider.CustomSpiderModel.Posts.All(p => p.Url != post.Url))
                            {
                                if (customSpider.CustomSpiderModel.Posts.Count <= i)
                                {
                                    customSpider.CustomSpiderModel.Posts.Add(post);
                                }
                                else
                                {
                                    customSpider.CustomSpiderModel.Posts.Insert(i, post);
                                }

                                // 最大缓存
                                if (customSpider.CustomSpiderModel.Posts.Count >= 30)
                                {
                                    customSpider.CustomSpiderModel.Posts.RemoveAt(29);
                                }

                                customExist = null;
                            }
                            else
                            {
                                customExist = true;
                            }
                        });
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
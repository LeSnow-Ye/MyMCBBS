using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyMCBBS.Model;
using AngleSharp;
using System.Media;
using AngleSharp.Dom;
using System.Timers;
using MyMCBBS.ViewModel;
using HandyControl.Controls;
using HandyControl.Data;
using MyMCBBS.View;
using System.Data;
using System.Windows.Markup;
using System.Diagnostics;

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
                            App.Current.Dispatcher.Invoke(() =>
                            {
                                if ((App.Current.FindResource("Locator") as ViewModelLocator).HeartsHarvester.HeartsHarvesterModel.QAPosts.All(p => p.Url != post.Url))
                                {
                                    if ((App.Current.FindResource("Locator") as ViewModelLocator).HeartsHarvester.HeartsHarvesterModel.QAPosts.Count <= i)
                                    {
                                        (App.Current.FindResource("Locator") as ViewModelLocator).HeartsHarvester.HeartsHarvesterModel.QAPosts.Add(post);
                                    }
                                    else
                                    {
                                        (App.Current.FindResource("Locator") as ViewModelLocator).HeartsHarvester.HeartsHarvesterModel.QAPosts.Insert(i, post);
                                    }

                                    // 最大缓存
                                    if ((App.Current.FindResource("Locator") as ViewModelLocator).HeartsHarvester.HeartsHarvesterModel.QAPosts.Count >= 30)
                                    {
                                        (App.Current.FindResource("Locator") as ViewModelLocator).HeartsHarvester.HeartsHarvesterModel.QAPosts.RemoveAt(29);
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
                    #endregion

                    #region 自定义
                    // 是否已经抓取过
                    bool? customExist = false;

                    if ((App.Current.FindResource("Locator") as ViewModelLocator).CustomSpider.CustomSpiderModel.CustomParts.ToList().Exists(s => post.PostPart == s))
                    {
                        App.Current.Dispatcher.Invoke(() =>
                        {
                            if ((App.Current.FindResource("Locator") as ViewModelLocator).CustomSpider.CustomSpiderModel.Posts.All(p => p.Url != post.Url))
                            {
                                if ((App.Current.FindResource("Locator") as ViewModelLocator).CustomSpider.CustomSpiderModel.Posts.Count <= i)
                                {
                                    (App.Current.FindResource("Locator") as ViewModelLocator).CustomSpider.CustomSpiderModel.Posts.Add(post);
                                }
                                else
                                {
                                    (App.Current.FindResource("Locator") as ViewModelLocator).CustomSpider.CustomSpiderModel.Posts.Insert(i, post);
                                }

                                // 最大缓存
                                if ((App.Current.FindResource("Locator") as ViewModelLocator).CustomSpider.CustomSpiderModel.Posts.Count >= 30)
                                {
                                    (App.Current.FindResource("Locator") as ViewModelLocator).CustomSpider.CustomSpiderModel.Posts.RemoveAt(29);
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
                    #endregion

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

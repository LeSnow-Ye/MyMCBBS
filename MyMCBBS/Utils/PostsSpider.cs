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
                string html = Web.DownloadWebsite($"https://www.mcbbs.net/forum.php?mod=guide&view=newthread");
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

                    // 是否已经抓取过
                    bool exist = false;
                    switch (post.PostPart)
                    {
                        case "矿工茶馆":
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
                                }
                                else
                                {
                                    exist = true;
                                }
                            });
                            break;
                    }

                    if (exist)
                    {
                        break; // 已经循环到存在的帖子即可停止循环以提高性能
                    }
                    else if (App.Config.Config.SuperMode && !initialization)
                    {
                        canPlaySound = true;
                        Web.OpenWithBrowser(post.Url);
                    }
                }

                if (canPlaySound && App.Config.Config.PlaySound)
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

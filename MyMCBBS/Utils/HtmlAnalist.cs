namespace MyMCBBS.Utils
{
    using AngleSharp;
    using AngleSharp.Dom;
    using System;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using static System.Convert;

    /// <summary>
    /// 分析网页.
    /// </summary>
    internal static class HtmlAnalist
    {
        /// <summary>
        /// 分析用户积分.
        /// </summary>
        /// <param name="uid">用户UID.</param>
        /// <returns>用户积分.</returns>
        public static async Task<(string Name, string AvatarUrl, int? Credit, int? Popularity, int? GoldNugget, int? Emerald, int? NetherStar, int? Contribution, int? Heart, int? Diamond)> UserAnalistAsync(int uid)
        {
            try
            {
                // 以防 API 失效，采用下载/分析网页方式
                string html = new WebClient() {Encoding=Encoding.UTF8 }.DownloadString($"https://www.mcbbs.net/home.php?mod=space&uid={uid}");
                var pageDocument = await BrowsingContext.New(Configuration.Default).OpenAsync(req => req.Content(html));

                var creditPart = pageDocument.QuerySelectorAll("#psts > ul > li").ToList();

                var result = (pageDocument.QuerySelector("#uhd > div > h2").Text().Replace("\n", string.Empty),
                    pageDocument.QuerySelector("#uhd > div > div > a > img").GetAttribute("src").Replace("small", "big"),
                    ToInt32(Regex.Replace(creditPart[1].Text(), @"[^0-9]+", string.Empty)),
                    ToInt32(Regex.Replace(creditPart[2].Text(), @"[^0-9]+", string.Empty)),
                    ToInt32(Regex.Replace(creditPart[3].Text(), @"[^0-9]+", string.Empty)),
                    ToInt32(Regex.Replace(creditPart[5].Text(), @"[^0-9]+", string.Empty)),
                    ToInt32(Regex.Replace(creditPart[6].Text(), @"[^0-9]+", string.Empty)),
                    ToInt32(Regex.Replace(creditPart[7].Text(), @"[^0-9]+", string.Empty)),
                    ToInt32(Regex.Replace(creditPart[8].Text(), @"[^0-9]+", string.Empty)),
                    ToInt32(Regex.Replace(creditPart[9].Text(), @"[^0-9]+", string.Empty))
                    );

                return result;
            }
            catch (Exception e)
            {
                return ("获取失败", "获取失败", null, null, null, null, null, null, null, null);
            }
        }
    }
}
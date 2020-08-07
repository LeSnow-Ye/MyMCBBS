using GalaSoft.MvvmLight;

namespace MyMCBBS.Model
{
    public class MainWindowModel : ObservableObject
    {
        public enum Index : byte
        {
            /// <summary>
            /// 0 主页
            /// </summary>
            Home,

            /// <summary>
            /// 1 爱心收割机
            /// </summary>
            HeartsHarvester,

            /// <summary>
            /// 2 主题抓取器
            /// </summary>
            PostSpider,

            /// <summary>
            /// 3 收藏
            /// </summary>
            Collections,

            /// <summary>
            /// 4 积分分析
            /// </summary>
            CreditAnalist,

            /// <summary>
            /// 5 统计信息
            /// </summary>
            Statistics,

            /// <summary>
            /// 6 快捷导航
            /// </summary>
            Sites,

            /// <summary>
            /// 7 小工具
            /// </summary>
            Tools,
        }

        private Index index = Index.Home;

        /// <summary>
        /// Gets or sets 当前页面.
        /// </summary>
        public Index PresentIndex
        {
            get => this.index;
            set
            {
                this.index = value;
                this.RaisePropertyChanged("PresentIndex");
            }
        }

        private string title = "主页";

        /// <summary>
        /// Gets or sets 标题.
        /// </summary>
        public string Title
        {
            get => this.title;
            set
            {
                this.title = value;
                this.RaisePropertyChanged("Title");
            }
        }
    }
}
namespace MyMCBBS.Model
{
    using GalaSoft.MvvmLight;
    using MyMCBBS.Utils;
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Media.Imaging;

    /// <summary>
    /// 用户类.
    /// </summary>
    public class UserModel : ObservableObject
    {
        private string name;
        private int uid;
        private int? credit;
        private int? heart;
        private int? popularity;
        private int? diamond;
        private int? emerald;
        private int? contribution;
        private int? netherStar;
        private int? goldNugget;
        private BitmapImage avatar;

        /// <summary>
        /// 刷新用户信息，会同步 Config 和 UserModel 中的UID.
        /// </summary>
        public async Task RefreshAsync()
        {
            try
            {
                this.UID = App.Config.Config.UID;
                var result = await HtmlAnalist.UserAnalistAsync(this.UID);
                this.Name = result.Name;
                this.Credit = result.Credit;
                this.Heart = result.Heart;
                this.Popularity = result.Popularity;
                this.Diamond = result.Diamond;
                this.Emerald = result.Emerald;
                this.Contribution = result.Contribution;
                this.NetherStar = result.NetherStar;
                this.GoldNugget = result.GoldNugget;
                this.Avatar = new BitmapImage(new Uri(result.AvatarUrl));
            }
            catch(Exception e) { Debug.WriteLine(e.ToString()); }
        }

        public UserModel()
        {
            this.RefreshAsync();
        }

        /// <summary>
        /// Gets or sets 用户名.
        /// </summary>
        public string Name
        {
            get => this.name;
            set
            {
                this.name = value;
                this.RaisePropertyChanged("name");
            }
        }

        /// <summary>
        /// Gets or sets UID.
        /// </summary>
        public int UID
        {
            get => this.uid;
            set
            {
                this.uid = value;
                this.RaisePropertyChanged("uid");
            }
        }

        /// <summary>
        /// Gets or sets 介绍.
        /// </summary>
        public BitmapImage Avatar
        {
            get => this.avatar;
            set
            {
                this.avatar = value;
                this.RaisePropertyChanged("avatar");
            }
        }

        #region 积分

        /// <summary>
        /// Gets or sets 积分.
        /// </summary>
        public int? Credit
        {
            get => this.credit;
            set
            {
                this.credit = value;
                this.RaisePropertyChanged("credit");
            }
        }

        /// <summary>
        /// Gets or sets 爱心.
        /// </summary>
        public int? Heart
        {
            get => this.heart;
            set
            {
                this.heart = value;
                this.RaisePropertyChanged("heart");
            }
        }

        /// <summary>
        /// Gets or sets 人气.
        /// </summary>
        public int? Popularity
        {
            get => this.popularity;
            set
            {
                this.popularity = value;
                this.RaisePropertyChanged("popularity");
            }
        }

        /// <summary>
        /// Gets or sets 钻石.
        /// </summary>
        public int? Diamond
        {
            get => this.diamond;
            set
            {
                this.diamond = value;
                this.RaisePropertyChanged("diamond");
            }
        }

        /// <summary>
        /// Gets or sets 绿宝石.
        /// </summary>
        public int? Emerald
        {
            get => this.emerald;
            set
            {
                this.emerald = value;
                this.RaisePropertyChanged("emerald");
            }
        }

        /// <summary>
        /// Gets or sets 贡献.
        /// </summary>
        public int? Contribution
        {
            get => this.contribution;
            set
            {
                this.contribution = value;
                this.RaisePropertyChanged("contribution");
            }
        }

        /// <summary>
        /// Gets or sets 下界之星.
        /// </summary>
        public int? NetherStar
        {
            get => this.netherStar;
            set
            {
                this.netherStar = value;
                this.RaisePropertyChanged("netherStar");
            }
        }

        /// <summary>
        /// Gets or sets 金粒.
        /// </summary>
        public int? GoldNugget
        {
            get => this.goldNugget;
            set
            {
                this.goldNugget = value;
                this.RaisePropertyChanged("goldNugget");
            }
        }

        #endregion 积分
    }
}
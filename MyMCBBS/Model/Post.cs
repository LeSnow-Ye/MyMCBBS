using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace MyMCBBS.Model
{
   public class Post : ObservableObject
    {
        private string poster;
        private string url;
        private string title;

        private string part;

        private bool isNewPost = true;

        public Post()
        {
            Task.Run(() =>
            {
                Thread.Sleep(15 * 1000);
                this.IsNewPost = false;
            });
        }

        public bool IsNewPost
        {
            get => this.isNewPost;
            set
            {
                this.isNewPost = value;
                this.RaisePropertyChanged("isNewPost");
            }
        }

        public string PostPart
        {
            get => this.part;
            set
            {
                this.part = value;
                this.RaisePropertyChanged("part");
            }
        }

        /// <summary>
        /// Gets or sets url.
        /// </summary>
        public string Url
        {
            get => this.url;
            set
            {
                this.url = value;
                this.RaisePropertyChanged("url");
            }
        }

        /// <summary>
        /// Gets or sets 发帖人.
        /// </summary>
        public string Poster
        {
            get => this.poster;
            set
            {
                this.poster = value;
                this.RaisePropertyChanged("poster");
            }
        }

        /// <summary>
        /// Gets or sets 标题.
        /// </summary>
        public string Title
        {
            get => this.title;
            set
            {
                this.title = value;
                this.RaisePropertyChanged("title");
            }
        }
    }
}

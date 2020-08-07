using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MyMCBBS.Model;
using MyMCBBS.Utils;

namespace MyMCBBS.ViewModel
{
    public class HeartsHarvesterViewModel : ViewModelBase
    {
        private HeartsHarvesterModel heartsHarvesterModel;

        public HeartsHarvesterViewModel()
        {
            this.heartsHarvesterModel = new HeartsHarvesterModel();
            App.PostsSpider.Start();
        }

        public HeartsHarvesterModel HeartsHarvesterModel
        {
            get => this.heartsHarvesterModel;
            set
            {
                this.heartsHarvesterModel = value;
                this.RaisePropertyChanged("heartsHarvesterModel");
            }
        }

        public RelayCommand<Post> OpenWithBrowserCommand { get; set; } = new RelayCommand<Post>((post) =>
        {
            Web.OpenWithBrowser(post.Url);
            post.IsNewPost = false;
        });
    }
}

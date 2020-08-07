using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MyMCBBS.Utils;

namespace MyMCBBS.ViewModel
{
    public class NewPostNotificationViewModel : ViewModelBase
    {
        public NewPostNotificationViewModel()
        {
            this.Visit = new RelayCommand(() => Web.OpenWithBrowser(this.URL));
        }

        private string part = "Part";

        public string Part
        {
            get => this.part;
            set
            {
                this.part = value;
                this.RaisePropertyChanged("Part");
            }
        }

        private string title = "Title";

        public string Title
        {
            get => this.title;
            set
            {
                this.title = value;
                this.RaisePropertyChanged("Title");
            }
        }

        public string URL { get; set; } = "lesnow.tk";

        public RelayCommand Visit { get; set; }
    }
}
using GalaSoft.MvvmLight;
using MyMCBBS.Model;

namespace MyMCBBS.ViewModel
{
    public class LaunchViewModel : ViewModelBase
    {
        private LaunchModel launchModel;

        public LaunchViewModel()
        {
            launchModel = new LaunchModel();
        }

        public LaunchModel LaunchModel
        {
            get => this.launchModel;
            set
            {
                this.launchModel = value;
                this.RaisePropertyChanged("LaunchModel");
            }
        }
    }
}
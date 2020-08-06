using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using MyMCBBS.Model;
using GalaSoft.MvvmLight.Command;

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
                this.RaisePropertyChanged("launchModel");
            }
        }
    }
}

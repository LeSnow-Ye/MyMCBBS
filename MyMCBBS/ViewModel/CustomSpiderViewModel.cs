using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using MyMCBBS.Model;

namespace MyMCBBS.ViewModel
{
    public class CustomSpiderViewModel : ViewModelBase
    {
        private CustomSpiderModel customSpiderModel = new CustomSpiderModel();

        public CustomSpiderModel CustomSpiderModel
        {
            get => this.customSpiderModel;
            set
            {
                this.customSpiderModel = value;
                this.RaisePropertyChanged("CustomSpiderModel");
            }
        }
    }
}

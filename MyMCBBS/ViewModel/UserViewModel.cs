using GalaSoft.MvvmLight;
using MyMCBBS.Model;

namespace MyMCBBS.ViewModel
{
    public class UserViewModel : ViewModelBase
    {
        public UserModel UserModel { get => App.UserModel; }
    }
}
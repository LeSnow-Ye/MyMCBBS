using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using CommonServiceLocator;
using MyMCBBS.ViewModel;

namespace MyMCBBS.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<UserViewModel>();
            SimpleIoc.Default.Register<DialogHostViewModel>();
            SimpleIoc.Default.Register<HeartsHarvesterViewModel>();
        }

        public MainViewModel Main { get => ServiceLocator.Current.GetInstance<MainViewModel>(); }
        public UserViewModel User { get => ServiceLocator.Current.GetInstance<UserViewModel>(); }
        public DialogHostViewModel Dialog { get => ServiceLocator.Current.GetInstance<DialogHostViewModel>(); }
        public HeartsHarvesterViewModel HeartsHarvester { get => ServiceLocator.Current.GetInstance<HeartsHarvesterViewModel>(); }


        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}
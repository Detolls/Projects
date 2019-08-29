using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;

namespace Saritasa.JiraDayIssues.WPF.ViewModel
{
    /// <summary>
    /// ViewModel locator.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// <see cref="ViewModelLocator"/> class constructor.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<MainWindowViewModel>();
        }

        public MainWindowViewModel MainViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainWindowViewModel>();
            }
        }
    }
}
using Caliburn.Micro;
using StartCaliburnApp.ViewModels;
using System.Windows;

namespace StartCaliburnApp
{
    public class Bootstrapper : BootstrapperBase //프로그램 부팅할때 필요한 클래스
    {
        public Bootstrapper()
        {
            Initialize(); //초기화
        }


        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ButtonsViewModel>(); // object -> ViewModel
        }
    }
}

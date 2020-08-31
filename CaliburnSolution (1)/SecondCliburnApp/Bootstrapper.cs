using Caliburn.Micro;
using SecondCliburnApp.ViewModels;
using System.Windows;

namespace SecondCliburnApp
{
    public class Bootstrapper : BootstrapperBase //프로그램 부팅할때 필요한 클래스
    {
        public Bootstrapper()
        {
            Initialize(); //초기화
        }


        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            //**View와 ViewModel연결
            DisplayRootViewFor<ShellViewModel>(); // object -> ViewModel
        }
    }
}

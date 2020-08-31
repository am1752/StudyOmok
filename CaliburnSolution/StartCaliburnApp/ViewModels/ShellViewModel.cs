using Caliburn.Micro;
using System.Windows;

namespace StartCaliburnApp.ViewModels
{
    public class ShellViewModel:PropertyChangedBase, IHaveDisplayName
    {
        string name; 
        public string Name 
        {
            get => name;
            set 
            {
                name = value;
                NotifyOfPropertyChange(() => Name); // RaisePropertyChange 바뀌는 값 그대로 Name에 넣겠다.
                NotifyOfPropertyChange(()=> CanSayHello); //=canExcute
            }
        }
        public bool CanSayHello
        {
            get => !string.IsNullOrEmpty(Name);
        }

        public ShellViewModel()
        {
            DisplayName = "Basic Cliburn App";
        }

        public string DisplayName { get; set; }

        //릴레이 커멘드 사용하지 않아도 된다
        public void SayHello()
        {
            MessageBox.Show($"Hello {Name}");
        }
    }
}

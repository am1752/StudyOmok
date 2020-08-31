using Caliburn.Micro;
using SecondCliburnApp.Models;

namespace SecondCliburnApp.ViewModels
{
    public class ShellViewModel : Conductor<object>, IHaveDisplayName
    {
        public override string DisplayName { get; set; }

        public ShellViewModel()
        {
            DisplayName = "Second Caliburn App";
            //FirstName = "Chae";
            People = new BindableCollection<PersonModel>();

            People.Add(new PersonModel { LastName = "Gate", FirstName = "Bill" });
            People.Add(new PersonModel { LastName = "SEO", FirstName = "PARK" });
            People.Add(new PersonModel { LastName = "JOON", FirstName = "PARK" });
        }

        string firstName;

        public string FirstName 
        { 
            get=>firstName;
            set
            {
                firstName = value;
                NotifyOfPropertyChange(() => FirstName);
                NotifyOfPropertyChange(() => FullName);
                NotifyOfPropertyChange(() => CanClearName);
            }
        }

        string lastName;

        public string LastName
        {
            get => lastName;
            set
            {
                lastName = value;
                NotifyOfPropertyChange(() => LastName);
                NotifyOfPropertyChange(() => FullName);
            }
        }

        //string fullName;
        public string FullName
        {
            get => $"{LastName} {FirstName}";
            
        }

       public BindableCollection<PersonModel> People {get; set;}

        PersonModel selectedPerson;

        public PersonModel SelectedPerson
        {
            get => selectedPerson;
            set
            {
                selectedPerson = value;
                this.LastName = selectedPerson.LastName;
                this.FirstName = selectedPerson.FirstName;
                NotifyOfPropertyChange(() => SelectedPerson);
                NotifyOfPropertyChange(() => CanClearName);
            }
        }

        public void ClearName()
        {
            this.FirstName = this.LastName = string.Empty;
            


        }

        public bool CanClearName
        {
            get => !(string.IsNullOrEmpty(LastName) && string.IsNullOrEmpty(FirstName));
        }

        public void LoadPageOne()
        { //UserControl FirstChildView
            ActivateItem(new FirstChildViewModel());

            //ActivateItem
        }

        public void LoadPageTwo()
        { //UserControl SecondChildView
            ActivateItem(new SecondChildViewModel());
        }
    }
}

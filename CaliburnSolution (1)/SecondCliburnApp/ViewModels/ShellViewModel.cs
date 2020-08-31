using Caliburn.Micro;
using MySql.Data.MySqlClient;
using SecondCliburnApp.Helpers;
using SecondCliburnApp.Models;
using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace SecondCliburnApp.ViewModels
{
    public class ShellViewModel : Conductor<object>, IHaveDisplayName
    {
        public override string DisplayName { get; set; }

        string firstName;

        public string FirstName
        {
            get => firstName;
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
                NotifyOfPropertyChange(() => CanClearName);
            }
        }

        //string fullName;
        public string FullName
        {
            get => $"{LastName} {FirstName}";
            set
            {
                //NotifyOfPropertyChange(() => FullName);
            }
        }

        public ShellViewModel()
        {
            DisplayName = "Second Caliburn App";
            //FirstName = "HeeJi";

            People = new BindableCollection<PersonModel>();

            InitCombobox();
            //People.Add(new PersonModel { LastName="Gates", FirstName="bill"});
            //People.Add(new PersonModel { LastName = "Jobs", FirstName = "Steve" });
            //People.Add(new PersonModel { LastName = "Musk", FirstName = "Ellen" });
        }

        private void InitCombobox()
        {
            People.Add(new PersonModel { LastName = "", FirstName = "선택" });
            using (MySqlConnection conn = new MySqlConnection(Commons.STRCONNSTRING))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(Commons.SELECTPEOPLEQUERY, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                
                //읽을 값이 있는동안 계속
                while (reader.Read())
                {
                    var temp = new PersonModel
                    {
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString()
                    };
                    //바인더블 컬렉션
                    People.Add(temp);
                }
            }
            SelectedPerson = People.Where(v => v.FirstName.Contains("선택")).First();
        }



        //**모델에 있는 class를 꺼내 쓰겠다 (모델과 뷰모델 연동)
        //콤보박스 사람 list
        public BindableCollection<PersonModel> People { get; set; }

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
            get => !(string.IsNullOrEmpty(firstName) && string.IsNullOrEmpty(lastName));
            //if (string.IsNullOrEmpty(firstName) && string.IsNullOrEmpty(lastName))
            //    return false;
            //else return true;
        }

        public void LoadPageOne() //UserContrl FirstChildView
        {
            ActivateItem(new FirstChildViewModel());
        }

        public void LoadPageTwo() //UserControl SecondChildView
        {
            ActivateItem(new SecondChildViewModel());
        }
    }
}

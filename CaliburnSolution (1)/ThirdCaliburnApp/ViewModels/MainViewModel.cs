using Caliburn.Micro;
using MvvmDialogs;
using MySql.Data.MySqlClient;
using System;
using System.Windows;
using ThirdCaliburnApp.Models;

namespace ThirdCaliburnApp.ViewModels
{


    public class MainViewModel : Conductor<object>, IHaveDisplayName
    {
        private readonly IWindowManager windowManager;
        private readonly IDialogService nativeDialogService;

        public MainViewModel(IWindowManager windowManager, IDialogService nativeDialogService)
        {
            this.windowManager = windowManager;
            this.nativeDialogService = nativeDialogService;

            GetEmployees();
        }




        #region 속성
        EmployeesModel EmployeesModel;
        int id;
        string empName;
        decimal salary;
        string deptName;
        string destination;

        public int Id {
            get => id;
            set {
                id = value;

                NotifyOfPropertyChange(() => SelectedEmployee);
                NotifyOfPropertyChange(() => Id);
                NotifyOfPropertyChange(() => CanDeleteEmployee);

            }
        }

        public string EmpName {
            get => empName;
            set
            {
                empName = value;
                NotifyOfPropertyChange(() => EmpName);
                NotifyOfPropertyChange(() => CanSaveEmployee);
            }
        }

        public decimal Salary {
            get => salary;
            set {
                salary = value;
                NotifyOfPropertyChange(() => Salary);
                NotifyOfPropertyChange(() => CanSaveEmployee);
            }
        }

        public string DeptName {
            get => deptName;
            set {
                deptName = value;
                NotifyOfPropertyChange(() => DeptName);
                NotifyOfPropertyChange(() => CanSaveEmployee);
            }
        }

        public string Destination {
            get => destination;
            set {
                destination = value;
                NotifyOfPropertyChange(() => Destination);
                NotifyOfPropertyChange(() => CanSaveEmployee);
            }
        }

        private readonly string strQuery = EmployeesTbl.SELECT_EMPLOYEES;

        BindableCollection<EmployeesModel> employees;

        //전체 Employees 리스트
        public BindableCollection<EmployeesModel> Employees {
            get => employees;
            set
            {
                employees = value;
                NotifyOfPropertyChange(() => Employees);
            }
        }


        //리스트 중 선택된 하나
        EmployeesModel selectedEmployee;

        public EmployeesModel SelectedEmployee
        {
            get => selectedEmployee;
            set
            {
                selectedEmployee = value;

                Id = value.Id;
                EmpName = value.EmpName;
                Salary = value.Salary;
                DeptName = value.DeptName;
                Destination = value.Destination;

                NotifyOfPropertyChange(() => SelectedEmployee);
            }
        }

        #endregion

        #region 생성자
        public MainViewModel()
        {
            GetEmployees();
        }
        #endregion

        private void GetEmployees()
        {
            using (MySqlConnection conn = new MySqlConnection(Commons.CONNSTRING))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(strQuery, conn);
                //cmd.Connection = conn;
                MySqlDataReader reader = cmd.ExecuteReader();
                Employees = new BindableCollection<EmployeesModel>();
                while (reader.Read())
                {
                    var temp = new EmployeesModel
                    {
                        Id = (int)reader["id"],
                        EmpName = reader["EmpName"].ToString(),
                        Salary = (decimal)reader["Salary"],
                        DeptName = reader["DeptName"].ToString(),
                        Destination = reader["Destination"].ToString()
                    };
                    Employees.Add(temp);
                }
            }
        }


        

        public void SaveEmployee()
        {
            int ResultRow = 0;
            using (MySqlConnection conn = new MySqlConnection(Commons.CONNSTRING))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;

                if (Id == 0) cmd.CommandText = EmployeesTbl.INSERT_EMPLOYEES;
                else cmd.CommandText = EmployeesTbl.UPDATE_EMPLOYEES;

                MySqlParameter paramEmpName = new MySqlParameter("@EmpName", MySqlDbType.VarChar, 45);
                paramEmpName.Value = EmpName;
                cmd.Parameters.Add(paramEmpName);

                MySqlParameter paramSalary = new MySqlParameter("@Salary", MySqlDbType.Decimal);
                paramSalary.Value = Salary;
                cmd.Parameters.Add(paramSalary);

                MySqlParameter paramDeptName = new MySqlParameter("@DeptName", MySqlDbType.VarChar, 45);
                paramDeptName.Value = DeptName;
                cmd.Parameters.Add(paramDeptName);

                MySqlParameter paramDestination = new MySqlParameter("@Destination", MySqlDbType.VarChar, 45);
                paramDestination.Value = Destination;
                cmd.Parameters.Add(paramDestination);
                        
                if (Id != 0)
                {
                    MySqlParameter paramId = new MySqlParameter("@id", MySqlDbType.Int32);
                    paramId.Value = Id;
                    cmd.Parameters.Add(paramId);
                }

                ResultRow = cmd.ExecuteNonQuery();
            }
                                              
            if (ResultRow > 0)
            {
                //MessageBox.Show("저장했습니다.");
                DialogViewModel dialogVM = new DialogViewModel();
                dialogVM.DisplayName = "저~장";
                bool? success = windowManager.ShowDialog(dialogVM);

                NewEmployee();
                GetEmployees();
            }

        }

       
        public bool CanSaveEmployee
        {
            get
            {
                return (string.IsNullOrEmpty(EmpName) || string.IsNullOrEmpty(Destination) || Salary == 0 || string.IsNullOrEmpty(DeptName)) ? false : true ;
            }
        }

        public bool CanDeleteEmployee
        {
            get => !(Id == 0);
        }

        public void DeleteEmployee()
        {
            int ResultRow = 0;

            using (MySqlConnection conn = new MySqlConnection(Commons.CONNSTRING))
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;

                cmd.CommandText = EmployeesTbl.DELETE_EMPLOYEES;

                MySqlParameter paramId = new MySqlParameter("@id", MySqlDbType.Int32);
                paramId.Value = Id;
                cmd.Parameters.Add(paramId);

                ResultRow = cmd.ExecuteNonQuery();
            }

            if (ResultRow > 0)
            {
                MessageBox.Show("삭제했습니다.");

                GetEmployees();
            }
        }




        public void NewEmployee()
        {
            Id = 0;
            EmpName = String.Empty;
            Salary = 0;
            DeptName = Destination = String.Empty;
        }
    }
}

namespace ThirdCaliburnApp
{
    public class Commons
    {
        internal static readonly string CONNSTRING = 
            "Data source = localhost;Port=3306;Database=testdb;Uid=root;Password=mysql_p@ssw0rd;";
    }

    public class EmployeesTbl
    {
        public static string SELECT_EMPLOYEES = " SELECT id, " +
                                            " EmpName, " +
                                            " Salary, " +
                                            " DeptName, " +
                                            " Destination " +
                                            " FROM testdb.employeestbl; ";
        public static string UPDATE_EMPLOYEES = " UPDATE employeestbl " +
                                                   " SET " +
                                                   " EmpName = @EmpName, " +
                                                   " Salary = @Salary, " +
                                                   " DeptName = @DeptName, "+
                                                   " Destination = @Destination " +
                                                   " WHERE id = @id; ";

        public static string INSERT_EMPLOYEES = " INSERT INTO employeestbl "+
                                                " (EmpName, " + 
                                                " Salary, " + 
                                                " DeptName, "+
                                                " Destination) " +
                                                " VALUES " +
                                                " (@EmpName, " + 
                                                " @Salary, " +
                                                " @DeptName, " +
                                                " @Destination) ";
        internal static string DELETE_EMPLOYEES = " DELETE FROM employeestbl " +
                                                 " WHERE id = @id ";
    }
}

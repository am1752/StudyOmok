using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace OMOK
{
    public partial class SearchForm : Form
    {
        string strConnString = "Server=localhost;Port=3306;" +
            "Database=OMOK;Uid=root;Pwd=mysql_p@ssw0rd";

        public SearchForm()
        {
            InitializeComponent();
        }

        private void SearchForm_Load(object sender, EventArgs e)
        {

        }

        private void TxtID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                SearchButton_Click(sender, new EventArgs());
            }

        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            UpdateData();
        }

        private void SetColumHeaders()
        {
            DataGridViewColumn column;

            column = GrdList.Columns[0];
            column.Width = 100;
            column.HeaderText = "ID";

            column = GrdList.Columns[1];
            column.Width = 120;
            column.HeaderText = "Win";

            column = GrdList.Columns[2];
            column.Width = 120;
            column.HeaderText = "Lose";

        }

        private void UpdateData()
        {
            using (MySqlConnection conn = new MySqlConnection(strConnString))
            {
                conn.Open();
                string strQuery = " SELECT * " +
                                  " FROM scoretbl " +
                                  " Where id = " +
                                  " '"+
                                  TxtID.Text.Trim() +
                                  "';";


                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = strQuery;
                //MySqlParameter paramUserId = new MySqlParameter("@id", MySqlDbType.VarChar, 45);
                //paramUserId.Value = TxtID.Text.Trim(); // 공백 넣는 경우가 아주 많기때문에                    
                //cmd.Parameters.Add(paramUserId);
                //cmd.ExecuteNonQuery();

                //strQuery = " SELECT * " +
                //                  " FROM scoretbl " +
                //                  " Where id = "+
                //                  paramUserId +
                //                  " ;";

                MySqlDataAdapter adapter = new MySqlDataAdapter(strQuery, conn);
                DataSet ds = new DataSet();
                               

                adapter.Fill(ds, "scoretbl");

                GrdList.DataSource = ds;
                GrdList.DataMember = "scoretbl";
            }
            SetColumHeaders();
            //mode = "";
        }
    }
}

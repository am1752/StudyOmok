using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace OMOK
{
    public partial class ROOMFORM : Form
    {
        string strConnString = "Server=localhost;Port=3306;" +
            "Database=OMOK;Uid=root;Pwd=mysql_p@ssw0rd";
        //string mode = "";

        //private Thread thread; // 통신을 위한 쓰레드
        //private TcpClient tcpClient;// TCP 클라이언트
        //private NetworkStream stream;

        //private bool playing;
        //private bool entered;
        //private bool threading;


        Thread thread; // 통신을 위한 쓰레드
        TcpClient tcpClient;// TCP 클라이언트
        NetworkStream stream;

        //string Commons.ROOMName;


        public ROOMFORM()
        {
            
            
            Commons.Mousedown = 0;
            Commons.ROOMName = "";
            InitializeComponent();
            UpdateData();
            CreateButton.Enabled = false;
            IDst.Text = Commons.UserId + "님 안녕하세요.";

            string message = "[Lobby]";
            //string issee = Commons.Mousedown == 0 ? "0" : "1";
            byte[] buf1 = Encoding.ASCII.GetBytes(message + "People" + "," + Commons.UserId);
            LoginForm.stream.Write(buf1, 0, buf1.Length);
        }

        private void Enter_Button_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(label1.Text))
            {
                MessageBox.Show("방을 선택해주세요!!");
                return;
            }
            else if(Commons.roomPerson == 2)
            {
                var Res = MessageBox.Show("방이 꽉 찼습니다.  관전하시겠습니까? ","알림",MessageBoxButtons.YesNo);
                if(Res == DialogResult.Yes)
                {
                    SeeButton_Click(sender, new EventArgs());
                }
              
                else return;
            }
            else
            {
                Commons.roomPerson = 2;
                Updateroom();
                UpdatePerson2();
            }
        }

        private void SetColumHeaders()
        {
            DataGridViewColumn column;

            column = GrdRoom.Columns[0];
            column.Width = 100;
            column.HeaderText = "번호";

            column = GrdRoom.Columns[1];
            column.Width = 120;
            column.HeaderText = "방이름";

            column = GrdRoom.Columns[2];
            column.Width = 120;
            column.HeaderText = "인원수";

        }

        private void UpdateData()
        {
            using (MySqlConnection conn = new MySqlConnection(strConnString))
            {
                conn.Open();
                string strQuery = " SELECT NO, " +
                                  " Name," +
                                  " person " +
                                  " FROM roomtbl; ";

                MySqlCommand cmd = new MySqlCommand(strQuery, conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(strQuery, conn);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "roomtbl");

                GrdRoom.DataSource = ds;
                GrdRoom.DataMember = "roomtbl";
            }
            SetColumHeaders();
            //mode = "";
        }


        private void SEND_MESSAGE()
        {
            string message = "[Room]";
            byte[] buf1 = Encoding.ASCII.GetBytes(message +","+"Create"+ ","+Commons.ROOMName);
            LoginForm.stream.Write(buf1, 0, buf1.Length);
        }

        private void CK_ROOM()
        {
            string strQuery = "SELECT name FROM roomtbl " + //반드시 뒤에 SpaceBar를 넣어줘야됨(안넣으면 userTBLWHERE로 붙어져서 Syntax Error나옴)
                                      " WHERE name = @name ";    //@userID로 사용안하고 직접적 ID(TxtUserID)를 바로 넣으면 SQL Injection으로 해킹위험이 나옴

            try
            {
                using (MySqlConnection conn = new MySqlConnection(strConnString))
                {
                    conn.Open();
                    //MetroMessageBox.Show(this, $"DB접속성공!!");
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = strQuery;
                    MySqlParameter paramName = new MySqlParameter("@name", MySqlDbType.VarChar, 45);
                    cmd.Parameters.Add(paramName);
                    paramName.Value = txtCreate.Text.Trim();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    reader.Read();

                    if (reader.HasRows)
                    {
                        MessageBox.Show("이미 있는 방이름입니다.");
                        CreateButton.Enabled = false;
                        txtCreate.Text = null;
                        txtCreate.Focus();
                    }
                    else
                    {
                        CreateRoom();
                        Commons.ROOMName = paramName.Value.ToString();
                        Updateroom();
                        MessageBox.Show($"{paramName.Value}방이 만들어졌습니다.");
                        //SEND_MESSAGE();
                        GOTOGAME();
                    }
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
            
        }
        private void childForm_Closed(object sender, FormClosedEventArgs e)
        {

            Show();
            //thread.Start();
            byte[] buf = Encoding.ASCII.GetBytes($"[Room]Exit");
            LoginForm.stream.Write(buf, 0, buf.Length);
            //init();
        }

        private void GOTOGAME()
        {
            string message = "[Room]";
            byte[] buf2 = Encoding.ASCII.GetBytes(message+"Join"+","+Commons.ROOMName);
            LoginForm.stream.Write(buf2, 0, buf2.Length);

            BeginInvoke(new Action(() =>
            {
                //thread.Abort();
                Hide();
                MultiPlayForm multiplayForm = new MultiPlayForm();
                multiplayForm.FormClosed += new FormClosedEventHandler(childForm_Closed);
                multiplayForm.Show();
            }));
        }

        private void CreateRoom()
        {
            string strQuery =  " INSERT INTO roomtbl " +
                               " (Name, " + 
                               " person) " +
                               " VALUES " +
                               " (@Name, " +
                               " 1); ";
            using (MySqlConnection conn = new MySqlConnection(strConnString))
            {

                conn.Open();
                //MetroMessageBox.Show(this, $"DB접속성공!!");
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = strQuery;
                MySqlParameter paramname = new MySqlParameter("@name", MySqlDbType.VarChar, 45);
                paramname.Value = txtCreate.Text.Trim(); // 공백 넣는 경우가 아주 많기때문에
                
                cmd.Parameters.Add(paramname);
                cmd.ExecuteNonQuery();
                UpdateData();
            }
        }

        private void Updateroom()
        {
            string strQuery =  " UPDATE omok.member " +
                               " SET " +
                               " room = @room " +
                               " WHERE NO = " +
                               " (SELECT temp.NO FROM " +
                               " (SELECT NO " +
                               " FROM omok.member " +
                               " WHERE id = @id) as temp); ";

            using (MySqlConnection conn = new MySqlConnection(strConnString))
            {

                conn.Open();
                //MetroMessageBox.Show(this, $"DB접속성공!!");
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = strQuery;
                MySqlParameter paramroom = new MySqlParameter("@room", MySqlDbType.VarChar, 45);
                paramroom.Value = Commons.ROOMName; // 공백 넣는 경우가 아주 많기때문에

                MySqlParameter paramid = new MySqlParameter("@id", MySqlDbType.VarChar, 45);
                paramid.Value = Commons.UserId;

                cmd.Parameters.Add(paramid);
                cmd.Parameters.Add(paramroom);
                cmd.ExecuteNonQuery();
            }
        }

        private void txtCreate_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCreate.Text)) CreateButton.Enabled = false;
            else CreateButton.Enabled = true;
        }

        private void txtCreate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                CreateButton_Click(sender, new EventArgs());
            }
        }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            //LoginForm.init();
            CK_ROOM();
        }

        //private void CK_Person()
        //{
        //    string strQuery = " SELECT Name, " +
        //                      "  person " +
        //                      " FROM roomtbl " +
        //                      " Where Name = @Name; ";

        //    using (MySqlConnection conn = new MySqlConnection(strConnString))
        //    {

        //        conn.Open();
        //        //MetroMessageBox.Show(this, $"DB접속성공!!");
        //        MySqlCommand cmd = new MySqlCommand();
        //        cmd.Connection = conn;
        //        cmd.CommandText = strQuery;
        //        MySqlParameter paramname = new MySqlParameter("@name", MySqlDbType.VarChar, 45);
        //        paramname.Value = Commons.ROOMName; // 공백 넣는 경우가 아주 많기때문에
                
        //        cmd.Parameters.Add(paramname);
        //        cmd.ExecuteNonQuery();

        //        MySqlDataReader reader = cmd.ExecuteReader();
        //        reader.Read();

                

        //        UpdateData();
        //    }
        //}

        void UpdatePerson2()
        {
            string strQuery =  " UPDATE omok.roomtbl " +
                               " SET " +
                               " person = 2 " +
                               " WHERE NO = " +
                               " (SELECT temp.NO FROM " +
                               " (SELECT NO " +
                               " FROM omok.roomtbl " +
                               " WHERE Name= @Name) as temp); ";

            using (MySqlConnection conn = new MySqlConnection(strConnString))
            {

                conn.Open();
                //MetroMessageBox.Show(this, $"DB접속성공!!");
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = strQuery;

                MySqlParameter paramname = new MySqlParameter("@Name", MySqlDbType.VarChar,45);
                paramname.Value = label1.Text.Trim(); // 공백 넣는 경우가 아주 많기때문에

                cmd.Parameters.Add(paramname);
                cmd.ExecuteNonQuery();
                                
                UpdateData();
                Commons.roomPerson = 1;
                //SEND_MESSAGE();
                byte[] buf1 = Encoding.ASCII.GetBytes("[Room]" +"," +"Join"+Commons.ROOMName);
                LoginForm.stream.Write(buf1, 0, buf1.Length);
                GOTOGAME();
            }
            
        }

        private void GrdRoom_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow data = GrdRoom.Rows[e.RowIndex];
            label1.Text = data.Cells[1].Value.ToString();
            Commons.ROOMName = label1.Text;
            Commons.roomPerson = int.Parse(data.Cells[2].Value.ToString());
        }

        private void SeeButton_Click(object sender, EventArgs e)
        {
            if(Commons.ROOMName == "")
            {
                MessageBox.Show("관전할 방을 선택해주세요!!");
                return;
            }
            else
            {
                Commons.Mousedown = 1;
                GOTOGAME();
            }
        }

        private void ROOMFORM_FormClosed(object sender, FormClosedEventArgs e)
        {
            closeNetwork();
        }

        private void UpateName_Click(object sender, EventArgs e)
        {
            UpdateData();
        }

        private void ROOMFORM_Load(object sender, EventArgs e)
        {
            //init();
        }

        private void closeNetwork()
        {
            //thread.Abort();
            LoginForm.stream.Close();
            LoginForm.tcpClient.Close();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            closeNetwork();
            Environment.Exit(0);
        }

        private void init()
        {
            tcpClient = LoginForm.tcpClient;
            LoginForm.stream = LoginForm.tcpClient.GetStream();
            //thread.Start();
        }

       
    }

    
}

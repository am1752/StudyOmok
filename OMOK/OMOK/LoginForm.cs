using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OMOK
{
    public partial class LoginForm : Form
    {
        
        string strConnString = "Server=localhost;Port=3306;" +
            "Database=OMOK;Uid=root;Pwd=mysql_p@ssw0rd";
        string PW = "";
        //private TcpClient thread;// TCP 클라이언트
        //private NetworkStream stream;

        public static Thread thread; // 통신을 위한 쓰레드
        public static TcpClient tcpClient;// TCP 클라이언트
        public static NetworkStream stream;

        public LoginForm()
        {
            InitializeComponent();
            
            
        }

        private void init()
        {
            tcpClient = new TcpClient();
            tcpClient.Connect("127.0.0.1", 9876);
            stream = tcpClient.GetStream();
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            LoginProcess();
        }


        private void LoginProcess()
        {
            if (string.IsNullOrEmpty(txtid.Text) || string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show(this, "아이디나 패스워드를 입력하세요!", "로그인", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtid.Text = txtPassword.Text = string.Empty;
                txtid.Focus();
                return;
            }

            string resultId = string.Empty;

            string strQuery = "SELECT id,password FROM member " + //반드시 뒤에 SpaceBar를 넣어줘야됨(안넣으면 userTBLWHERE로 붙어져서 Syntax Error나옴)
                                      " WHERE id = @id " +    //@userID로 사용안하고 직접적 ID(TxtUserID)를 바로 넣으면 SQL Injection으로 해킹위험이 나옴
                                      " AND password = @password ";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(strConnString))
                {

                    conn.Open();
                    //MetroMessageBox.Show(this, $"DB접속성공!!");
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = strQuery;
                    MySqlParameter paramUserId = new MySqlParameter("@id", MySqlDbType.VarChar, 12);
                    paramUserId.Value = txtid.Text.Trim(); // 공백 넣는 경우가 아주 많기때문에
                    MySqlParameter ParamPassword = new MySqlParameter("@password", MySqlDbType.VarChar);
                    PW = txtPassword.Text.Trim();

                    var md5Hash = MD5.Create();
                    var cryptoPassword = Commons.GetMd5Hash(md5Hash, txtPassword.Text.Trim());
                    ParamPassword.Value = cryptoPassword;


                    cmd.Parameters.Add(paramUserId);
                    cmd.Parameters.Add(ParamPassword);

                    MySqlDataReader reader = cmd.ExecuteReader();
                    reader.Read();

                    if (!reader.HasRows)
                    {
                        MessageBox.Show(this, "아이디나 패스워드를 정확히 입력하세요", "로그인실패",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtPassword.Text = txtid.Text = string.Empty;
                        txtid.Focus();
                        return;
                    }

                    else
                    {
                        init();
                        resultId = reader["id"] != null ? reader["id"].ToString() : string.Empty;
                        
                        Commons.UserId = txtid.Text.Trim();
                        Commons.PW = txtPassword.Text.Trim();

                        byte[] buf = new byte[256];
                        buf = Encoding.ASCII.GetBytes("[Login]" + Commons.UserId+ "," + ParamPassword.Value);
                        stream.Write(buf, 0, buf.Length);
                        int bufBytes = stream.Read(buf, 0, buf.Length);

                        string message = Encoding.ASCII.GetString(buf, 0, bufBytes);
                        if(message == "valid")
                        {
                            MessageBox.Show(this, $"{resultId} 로그인 성공");
                            Room_Loading();
                        }
                        else if(message == "already")
                        {
                            MessageBox.Show("이미 접속 중인 아이디 입니다.");
                            Commons.UserId = Commons.PW = "";
                            txtid.Text = txtPassword.Text = null;
                            txtid.Focus();
                        }
                        
                        
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"DB접속에러 : {ex.Message}", "로그인에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //if (string.IsNullOrEmpty(resultId))
            //{
            //    txtid.Text = txtPassword.Text = string.Empty;
            //    txtid.Focus();
            //    return;
            //}
            //else this.Hide();

        }

        private void childForm_Closed(object sender, FormClosedEventArgs e)
        {
            
            Show();
            init();
        }

        private void Room_Loading()
        {
            Hide();
            ROOMFORM roomForm = new ROOMFORM();
            roomForm.FormClosed += new FormClosedEventHandler(childForm_Closed);
            roomForm.Show();
            
            
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                LoginButton_Click(sender, new EventArgs());
            }
        }

        private void SignButton_Click(object sender, EventArgs e)
        {
            Hide();
            JoinForm joinForm = new JoinForm();
            joinForm.Show();
            joinForm.FormClosed += new FormClosedEventHandler(childForm_Closed);
        }

        private void LoginForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void FindButton_Click(object sender, EventArgs e)
        {
            Hide();
            FindForm findForm = new FindForm();
            findForm.Show();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            //init();
        }
    }
}

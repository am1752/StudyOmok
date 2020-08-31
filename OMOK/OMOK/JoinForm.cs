using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OMOK
{
    public partial class JoinForm : Form
    {
        string strConnString = "Server=localhost;Port=3306;" +
            "Database=OMOK;Uid=root;Pwd=mysql_p@ssw0rd";
        int CK = 0;

        public JoinForm()
        {
            InitializeComponent();
            Txt.Focus();
        }

        private void CheckButton_Click(object sender, EventArgs e)
        {
            CHECK_ID();

        }

        private void CHECK_ID()
        {
            string strQuery = "SELECT id FROM member " + //반드시 뒤에 SpaceBar를 넣어줘야됨(안넣으면 userTBLWHERE로 붙어져서 Syntax Error나옴)
                                      " WHERE id = @id ";    //@userID로 사용안하고 직접적 ID(TxtUserID)를 바로 넣으면 SQL Injection으로 해킹위험이 나옴

            try
            {
                using (MySqlConnection conn = new MySqlConnection(strConnString))
                {
                    conn.Open();
                    //MetroMessageBox.Show(this, $"DB접속성공!!");
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = strQuery;
                    MySqlParameter paramUserId = new MySqlParameter("@id", MySqlDbType.VarChar, 45);
                    cmd.Parameters.Add(paramUserId);
                    paramUserId.Value = TxtID.Text.Trim();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    reader.Read();

                    if (reader.HasRows)
                    {
                        MessageBox.Show("이미 등록아이디가 있습니다.");
                        TxtID.Text = null;
                        TxtID.Focus();
                    }
                    else
                    {
                        MessageBox.Show("사용 가능한 아이디 입니다.");
                        CK = 1;
                        txtPassword.Focus();
                    }
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void InsertDataToDB()
        {
            string strQuery = "INSERT INTO member " +
                " (id, password, question, answer) " +
                " VALUES " +
                " (@id, @password, @question, @answer); ";

            if(txtPassword.Text.Trim() != txtPWCHECK.Text.Trim())
            {
                MessageBox.Show("비번이 같지 않습니다.");
                txtPassword.Text = txtPWCHECK.Text = null;
                txtPassword.Focus(); 
            }

            else if(string.IsNullOrEmpty(txtPassword.Text)
                || string.IsNullOrEmpty(txtPWCHECK.Text) || string.IsNullOrEmpty(Txt.Text))
            {
                MessageBox.Show("아이디 비번을 채워주세요.");
                TxtID.Text=txtPassword.Text = txtPWCHECK.Text = null;
                TxtID.Focus();
            }

            else if(CboQ.SelectedIndex == 0 || string.IsNullOrEmpty(txtA.Text))
            {
                MessageBox.Show("질문과 답을 해주세요.");
                CboQ.Focus();
            }



            else
            {
                using (MySqlConnection conn = new MySqlConnection(strConnString))
                {
                    conn.Open();
                    string PW;
                    MySqlCommand cmd = new MySqlCommand(strQuery, conn);
                    MySqlParameter paramId = new MySqlParameter("@id", MySqlDbType.VarChar, 45);
                    paramId.Value = TxtID.Text.Trim().ToString();
                    cmd.Parameters.Add(paramId);
                    MySqlParameter paramPassword = new MySqlParameter("@password", MySqlDbType.VarChar, 45);
                    PW = txtPassword.Text.Trim();
                    var md5Hash = MD5.Create();
                    var cryptoPassword = Commons.GetMd5Hash(md5Hash, txtPassword.Text.Trim());
                    paramPassword.Value = cryptoPassword;
                    cmd.Parameters.Add(paramPassword);
                    MySqlParameter paramQuestion = new MySqlParameter("@question", MySqlDbType.VarChar, 45);
                    paramQuestion.Value = CboQ.SelectedItem.ToString();
                    cmd.Parameters.Add(paramQuestion);
                    MySqlParameter paramAnswer = new MySqlParameter("@answer", MySqlDbType.VarChar, 45);
                    paramAnswer.Value = txtA.Text.Trim();
                    cmd.Parameters.Add(paramAnswer);
                    cmd.ExecuteNonQuery();
                }

                InsertDataToDB1();
                MessageBox.Show("가입되었습니다.");

                Hide();
                LoginForm loginForm = new LoginForm();
                loginForm.Show();
            }


        }

        private void JOIN_Click(object sender, EventArgs e)
        {
            if(CK == 0)
            {
                MessageBox.Show("아이디 중복확인 해주세요!!");
                return;
            }
            else
            {
                InsertDataToDB();
            }
            
        }

        private void InsertDataToDB1()
        {
            string strQuery = "INSERT INTO scoretbl " +
                " (id, win, lose) " +
                " VALUES " +
                " (@id, 0, 0); ";
            using (MySqlConnection conn = new MySqlConnection(strConnString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(strQuery, conn);
                MySqlParameter paramId = new MySqlParameter("@id", MySqlDbType.VarChar, 45);
                paramId.Value = TxtID.Text.Trim().ToString();
                cmd.Parameters.Add(paramId);
                cmd.ExecuteNonQuery();
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                CheckButton_Click(sender, new EventArgs());
            }
        }

        private void txtA_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                JOIN_Click(sender, new EventArgs());
            }
        }
    }


 }



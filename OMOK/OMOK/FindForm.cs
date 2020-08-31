using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OMOK
{
    public partial class FindForm : Form
    {
        string strConnString = "Server=localhost;Port=3306;" +
            "Database=OMOK;Uid=root;Pwd=mysql_p@ssw0rd";
        int CK = 0;

        string id;
        public FindForm()
        {
            InitializeComponent();
            CboQ.IsAccessible = false;
            TxtID.Focus();
            label4.Visible = CboQ.Visible = CheckButton.Visible = true;
            Txt.Text = "ID";
            PWA.Text = "비번 답";
            Find.Text = "찾기";

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
                        MessageBox.Show("등록아이디가 있습니다.");
                        CboQ.Focus();
                        CboQ.IsAccessible = true;
                        
                    }
                    else
                    {
                        MessageBox.Show("없는 아이디 입니다.");
                        TxtID.Text = null;
                        TxtID.Focus();
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void Find_Click(object sender, EventArgs e)
        {
            if(CK == 0)
            {
                string strQuery = " SELECT password " +
                               " FROM member " +
                               " WHERE id = @id " +
                               " AND question = @question " +
                               " AND answer = @answer; ";

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
                        paramUserId.Value = TxtID.Text.Trim(); // 공백 넣는 경우가 아주 많기때문에                    
                        cmd.Parameters.Add(paramUserId);
                        MySqlParameter paramQuestion = new MySqlParameter("@question", MySqlDbType.VarChar, 45);
                        paramQuestion.Value = CboQ.SelectedItem.ToString();
                        cmd.Parameters.Add(paramQuestion);
                        MySqlParameter paramAnswer = new MySqlParameter("@answer", MySqlDbType.VarChar, 45);
                        paramAnswer.Value = txtA.Text.Trim();
                        cmd.Parameters.Add(paramAnswer);

                        //MySqlDataReader reader = cmd.ExecuteReader();
                        //reader.Read();

                        cmd.ExecuteNonQuery();
                        string PW = cmd.ExecuteScalar().ToString();

                        if (string.IsNullOrEmpty(PW))
                        {
                            MessageBox.Show(this, "질문과 답을 정확히 입력하세요", "로그인실패",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtA.Text = string.Empty;
                            txtA.Focus();
                            return;
                        }
                        else
                        {
                            //var md5Hash = MD5.Create();
                            //var decryptoPassword = Commons.VerifyMd5Hash(md5Hash, PW);
                            //PW = decryptoPassword;


                            var res = MessageBox.Show("비번을 새로 입력","새 비번", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                            if (res == DialogResult.Yes)
                            {
                                id = TxtID.Text.Trim();
                                ChangePW();   
                            }
                            //joinForm.FormClosed += new FormClosedEventHandler(childForm_Closed);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                if(string.IsNullOrEmpty(TxtID.Text) || string.IsNullOrEmpty(txtA.Text) || TxtID.Text != txtA.Text)
                {
                    MessageBox.Show("새 비번을 정확히 적어주세요");
                    TxtID.Text = txtA.Text = null;
                    TxtID.Focus();
                }
                else
                {
                    string strQuery = " UPDATE omok.member " +
                               " SET " +
                               " password = @password " +
                               " WHERE NO = " +
                               " (SELECT temp.NO FROM " +
                               " (SELECT NO " +
                               " FROM omok.member " +
                               " WHERE id= @id) as temp); ";

                    using (MySqlConnection conn = new MySqlConnection(strConnString))
                    {

                        conn.Open();
                        //MetroMessageBox.Show(this, $"DB접속성공!!");
                        MySqlCommand cmd = new MySqlCommand();
                        cmd.Connection = conn;
                        cmd.CommandText = strQuery;
                        MySqlParameter paramPW = new MySqlParameter("@password", MySqlDbType.VarChar, 45);
                        string PW = TxtID.Text.Trim();
                        var md5Hash = MD5.Create();
                        var cryptoPassword = Commons.GetMd5Hash(md5Hash, TxtID.Text.Trim());
                        paramPW.Value = cryptoPassword;

                        MySqlParameter paramid = new MySqlParameter("@id", MySqlDbType.VarChar, 45);
                        paramid.Value = id;

                        cmd.Parameters.Add(paramid);
                        cmd.Parameters.Add(paramPW);
                        cmd.ExecuteNonQuery();
                    }


                    var res = MessageBox.Show("새 비번이 바뀌었습니다.", "비번", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (res == DialogResult.Yes)
                    {
                        Hide();
                        LoginForm loginForm = new LoginForm();
                        loginForm.Show();
                    }
                }
            }
        }

        private void ChangePW()
        {
            TxtID.Focus();
            TxtID.PasswordChar = txtA.PasswordChar = '♥';
            CK = 1;
            TxtID.Text = txtA.Text = "";
            Txt.Text = "새 비번";
            PWA.Text = "새 비번 확인";
            label4.Visible = CboQ.Visible = CheckButton.Visible = false;
            Find.Text = "비번 바꾸기";
        }

        private void FindForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void TxtID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13 && CK == 0)
            {
                CheckButton_Click(sender, new EventArgs());
            }
        }

        private void txtA_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                Find_Click(sender, new EventArgs());
            }
        }
    }
}

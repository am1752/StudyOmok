using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class SignForm : Form
    {
        private TcpClient tcpClient;
        private NetworkStream stream;
        public SignForm()
        {
            InitializeComponent();
            stream = LoginForm.tcpClient.GetStream();
            tcpClient = LoginForm.tcpClient;
        }

        private void signButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(idTextBox.Text) && string.IsNullOrEmpty(passwordTextBox.Text))
            {
                MessageBox.Show("아이디 패스워드를 입력해주세요");
                return;
            }
            else if (string.IsNullOrEmpty(idTextBox.Text))
            {
                MessageBox.Show("아이디를 입력해주세요");
                return;
            }
            else if (string.IsNullOrEmpty(passwordTextBox.Text))
            {
                MessageBox.Show("패스워드를 입력해주세요");
                return;
            }

            byte[] buf = Encoding.ASCII.GetBytes("[Sign]" + idTextBox.Text + "," + passwordTextBox.Text);
            
            stream.Write(buf, 0, buf.Length);
            int bufBytes = stream.Read(buf, 0, buf.Length);
            
            string message = Encoding.ASCII.GetString(buf, 0, bufBytes);
            
            if (message == "succ")
            {
                MessageBox.Show("가입성공");
            }
            else if (message == "fail")
            {
                MessageBox.Show("이미 가입된 아이디 입니다.");
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            tcpClient.Close();
            stream.Close();
            Close();
        }

        private void idTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                signButton_Click(sender, new EventArgs());
            }
        }

        private void passwordTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                signButton_Click(sender, new EventArgs());
            }
        }
    }
}

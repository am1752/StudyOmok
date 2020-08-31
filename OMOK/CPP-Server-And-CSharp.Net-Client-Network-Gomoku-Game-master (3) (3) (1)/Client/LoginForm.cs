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
    public partial class LoginForm : Form
    {
        public static TcpClient tcpClient;// TCP 클라이언트
        public static NetworkStream stream;
        public LoginForm()
        {
            InitializeComponent();
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            tcpClient = new TcpClient();
            tcpClient.Connect("127.0.0.1", 9876);
            //tcpClient.Connect("0.tcp.ngrok.io", 14114);
            stream = tcpClient.GetStream();
            
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

            byte[] buf = new byte[256];
            buf = Encoding.ASCII.GetBytes("[Login]" + idTextBox.Text + "," + passwordTextBox.Text);
            stream.Write(buf, 0, buf.Length);
            int bufBytes = stream.Read(buf, 0, buf.Length);

            string message = Encoding.ASCII.GetString(buf, 0, bufBytes);
            if (message == "valid")
            {
                Hide();
                LobbyForm lobbyForm = new LobbyForm();
                lobbyForm.FormClosed += new FormClosedEventHandler(childForm_Closed);
                lobbyForm.Show();
            }
            else if(message=="already")
            {
                MessageBox.Show("이미 로그인중");
            }
            else
            {
                MessageBox.Show("아이디 또는 비번 안맞음");
            }
        }
        void childForm_Closed(object sender, FormClosedEventArgs e)
        {
            init();
            Show();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            init();
        }
        
        private void signButton_Click(object sender, EventArgs e)
        {
            tcpClient = new TcpClient();
            tcpClient.Connect("127.0.0.1", 9876);
            stream = tcpClient.GetStream();

            Hide();
            SignForm signForm = new SignForm();
            signForm.FormClosed += new FormClosedEventHandler(childForm_Closed);
            signForm.Show();
        }

        private void init()
        {
            
            //tcpClient = new TcpClient();
            //tcpClient.Connect("127.0.0.1", 9876);
            //stream = tcpClient.GetStream();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void idTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                loginButton_Click(sender, new EventArgs());
            }
        }

        private void passwordTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                loginButton_Click(sender, new EventArgs());
            }
        }

        private void LoginForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (tcpClient != null)
            {
                tcpClient.Close();
                stream.Close();
            }
        }
    }
}

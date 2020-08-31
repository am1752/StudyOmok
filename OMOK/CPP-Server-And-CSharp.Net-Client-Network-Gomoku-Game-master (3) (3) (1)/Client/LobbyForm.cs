using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class LobbyForm : Form
    {
        public Thread thread; // 통신을 위한 쓰레드
        private System.Windows.Forms.Timer timer;
        private TcpClient tcpClient;// TCP 클라이언트
        private NetworkStream stream;
        private int inviteRoomNum = -1;

        MultiPlayForm multiPlayForm;
        public LobbyForm()
        {
            InitializeComponent();
            init();
        }

        private void init()
        {
            inviteTimePrg.Visible = false;
            inviteButton.Visible = false;

            tcpClient = LoginForm.tcpClient;
            stream = LoginForm.stream;
            byte[] buf = Encoding.ASCII.GetBytes("[Lobby]Refresh");
            stream.Write(buf, 0, buf.Length);

            thread = new Thread(new ThreadStart(read));
            thread.IsBackground = true;
            thread.Start();
        }

        private void read()
        {
            while (true)
            {
                byte[] buf = new byte[1024];
                int bufBytes = stream.Read(buf, 0, buf.Length);
                string message = Encoding.UTF8.GetString(buf, 0, bufBytes);

                if (message.Contains("[People]"))
                {
                    string[] s = message.Split(']')[1].Split(',');

                    peopleListView.Invoke((MethodInvoker)delegate ()
                    {
                        peopleListView.Items.Clear();
                        foreach (var word in s)
                        {
                            ListViewItem item = new ListViewItem(word);
                            peopleListView.Items.Add(item);
                        }
                    });
                }
                else if (message.Contains("[Room]"))
                {

                    string[] s = message.Split(']')[1].Split(';');

                    roomListView.Invoke((MethodInvoker)delegate ()
                    {
                        roomListView.Items.Clear();
                        foreach (var item in s)
                        {
                            string[] temp = item.Split(',');
                            ListViewItem lvitem = new ListViewItem(temp);
                            roomListView.Items.Add(lvitem);
                        }
                    });
                }
                else if (message.Contains("[Chat]"))
                {
                    string s = message.Split(']')[1].Replace((char)0x02, ']');
                    for (int i = 0; i < s.Length; i++)
                    {
                        if (s[i] == ' ')
                        {
                            s = s.Insert(i, "] ");
                            break;
                        }
                    }

                    chatTextBox.Invoke((MethodInvoker)delegate ()
                    {
                        chatTextBox.Text += ("[" + s + Environment.NewLine);
                        chatTextBox.SelectionStart = chatTextBox.Text.Length;
                        chatTextBox.ScrollToCaret();
                    });
                }
                else if (message.Contains("[Create]"))
                {
                    string s = message.Split(']')[1];

                    Room.currentRoomNum = int.Parse(s);

                    BeginInvoke(new Action(() =>
                    {
                        if (thread.IsAlive)
                            thread.Abort();

                        if (timer!=null)
                            timer.Stop();
                        inviteButton.Visible = false;
                        inviteTimePrg.Visible = false;


                        Hide();
                        multiPlayForm = new MultiPlayForm();
                        multiPlayForm.FormClosed += new FormClosedEventHandler(childForm_Closed);
                        multiPlayForm.Show();
                    }));

                }
                else if (message.Contains("[Join]"))
                {
                    string[] s = message.Split(']')[1].Split(',');
                    if (s[0] == "succ")
                    {
                        if (s[1] == "Player") Status.player = true;
                        else if (s[1] == "Observer") Status.player = false;
                        Room.currentRoomNum = int.Parse(s[2]);
                        BeginInvoke(new Action(() =>
                        {
                            if (thread.IsAlive)
                                thread.Abort();
                            if (timer != null)
                                timer.Stop();
                            inviteButton.Visible = false;
                            inviteTimePrg.Visible = false;


                            Hide();
                            multiPlayForm = new MultiPlayForm();
                            multiPlayForm.FormClosed += new FormClosedEventHandler(childForm_Closed);
                            multiPlayForm.Show();
                        }));

                    }
                    else if (s[0] == "fail")
                    {
                        MessageBox.Show("접속실패");
                    }
                }
                else if (message.Contains("[Refresh]"))
                {
                    string s = message.Split(']')[1];
                    string[] room = s.Split('/')[0].Split(';');
                    string[] people = s.Split('/')[1].Split(',');

                    if (InvokeRequired == true)
                    {
                        roomListView.Invoke((MethodInvoker)delegate ()
                        {
                            peopleListView.Invoke((MethodInvoker)delegate ()
                            {
                                roomListView.Items.Clear();
                                peopleListView.Items.Clear();
                                if (!(room[0] == "empty"))
                                {
                                    foreach (var item in room)
                                    {
                                        string[] temp = item.Split(',');
                                        ListViewItem lvitem = new ListViewItem(temp);
                                        roomListView.Items.Add(lvitem);
                                    }
                                }
                                foreach (var word in people)
                                {
                                    ListViewItem item = new ListViewItem(word);
                                    peopleListView.Items.Add(item);
                                }
                            });
                        });
                    }
                    else
                    {
                        roomListView.Items.Clear();
                        peopleListView.Items.Clear();
                        if (!(room[0] == "empty"))
                        {
                            foreach (var item in room)
                            {
                                string[] temp = item.Split(',');
                                ListViewItem lvitem = new ListViewItem(temp);
                                roomListView.Items.Add(lvitem);
                            }
                        }
                        foreach (var word in people)
                        {
                            ListViewItem item = new ListViewItem(word);
                            peopleListView.Items.Add(item);
                        }
                    }

                }
                else if (message.Contains("[Invite]"))
                {
                    string s = message.Split(']')[1];
                    inviteRoomNum = int.Parse(s);
                    inviteButton.Invoke((MethodInvoker)delegate ()
                    {
                        inviteButton.Text = $"{s}번 방에서 초대가 왔습니다.";
                        inviteButton.Visible = true;

                        inviteTimePrg.Value = 10;
                        inviteTimePrg.Visible = true;

                        if (timer != null)
                        {
                            timer.Dispose();
                        }

                        timer = new System.Windows.Forms.Timer();
                        timer.Interval = 1000;
                        timer.Tick += new EventHandler(inviteTime);
                        timer.Start();
                    });
                }
            }
        }

        void childForm_Closed(object sender, FormClosedEventArgs e)
        {
            Show();
            thread = new Thread(new ThreadStart(read));
            thread.Start();
            byte[] buf = Encoding.ASCII.GetBytes($"[Room]Exit");
            stream.Write(buf, 0, buf.Length);
        }

        private void LobbyForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (thread.IsAlive)
                thread.Abort();

            stream.Close();
            tcpClient.Close();
        }

        private void chatInputTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) && !string.IsNullOrEmpty(chatInputTextBox.Text))
            {
                byte[] buf = Encoding.UTF8.GetBytes($"[Lobby]Chat{(char)0x01}{chatInputTextBox.Text.Replace(']', (char)0x02)}");
                stream.Write(buf, 0, buf.Length);
                chatInputTextBox.Clear();
            }
        }

        private void createRoomButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(roomTitleTextBox.Text))
            {
                MessageBox.Show("방제목 입력");
                return;
            }

            byte[] buf = Encoding.UTF8.GetBytes($"[Room]Create{(char)0x01}{roomTitleTextBox.Text}");
            stream.Write(buf, 0, buf.Length);
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            byte[] buf = Encoding.ASCII.GetBytes($"[Room]Refresh");
            stream.Write(buf, 0, buf.Length);
        }

        private void roomListView_DoubleClick(object sender, EventArgs e)
        {
            byte[] buf = Encoding.UTF8.GetBytes($"[Room]Join{(char)0x01}{roomListView.SelectedItems[0].Text}");
            stream.Write(buf, 0, buf.Length);
        }

        private void inviteButton_Click(object sender, EventArgs e)
        {
            inviteButton.Visible = false;
            byte[] buf = Encoding.UTF8.GetBytes($"[Room]Join{(char)0x01}{inviteRoomNum}");
            stream.Write(buf, 0, buf.Length);
        }
        private void inviteTime(object sender,EventArgs e)
        {
            inviteTimePrg.Value--;
            if (inviteTimePrg.Value <= 0)
            {
                inviteButton.Visible = false;
                inviteTimePrg.Visible = false;
                timer.Stop();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Client
{
    public partial class MultiPlayForm : Form
    {
        public Thread thread; // 통신을 위한 쓰레드
        private TcpClient tcpClient;// TCP 클라이언트
        private NetworkStream stream;

        private const int rectSize = 33; // 오목판의 셀 크기
        private const int edgeCount = 15; // 오목판의 선 개수

        private enum Horse { none = 0, BLACK, WHITE };
        private Horse[,] board;
        private Horse nowPlayer;
        private bool nowTurn;

        private bool playing;
        private bool entered;
        private bool threading;

        private bool judge(Horse Player) // 승리 판정 함수
        {
            for (int i = 0; i < edgeCount - 4; i++) // 가로
                for (int j = 0; j < edgeCount; j++)
                    if (board[i, j] == Player && board[i + 1, j] == Player && board[i + 2, j] == Player &&
                        board[i + 3, j] == Player && board[i + 4, j] == Player)
                        return true;
            for (int i = 0; i < edgeCount; i++) // 세로
                for (int j = 4; j < edgeCount; j++)
                    if (board[i, j] == Player && board[i, j - 1] == Player && board[i, j - 2] == Player &&
                        board[i, j - 3] == Player && board[i, j - 4] == Player)
                        return true;
            for (int i = 0; i < edgeCount - 4; i++) // Y = X 직선
                for (int j = 0; j < edgeCount - 4; j++)
                    if (board[i, j] == Player && board[i + 1, j + 1] == Player && board[i + 2, j + 2] == Player &&
                        board[i + 3, j + 3] == Player && board[i + 4, j + 4] == Player)
                        return true;
            for (int i = 4; i < edgeCount; i++) // Y = -X 직선
                for (int j = 0; j < edgeCount - 4; j++)
                    if (board[i, j] == Player && board[i - 1, j + 1] == Player && board[i - 2, j + 2] == Player &&
                        board[i - 3, j + 3] == Player && board[i - 4, j + 4] == Player)
                        return true;
            return false;
        }

        private void refresh()
        {
            this.boardPicture.Refresh();
            for (int i = 0; i < edgeCount; i++)
                for (int j = 0; j < edgeCount; j++)
                    board[i, j] = Horse.none;
        }
        public MultiPlayForm()
        {
            InitializeComponent();
            init();
        }

        private void init()
        {
            invitePanel.Visible = false;
            playing = false;
            entered = false;
            threading = false;
            board = new Horse[edgeCount, edgeCount];
            nowTurn = false;

            tcpClient = LoginForm.tcpClient;
            stream = LoginForm.stream;

            roomNumLabel.Text = $"{Room.currentRoomNum}번 방";

            Thread.Sleep(10);
            thread = new Thread(new ThreadStart(read));
            thread.Start();
            threading = true;

            if (Status.player == false)
            {
                readyButton.Visible = false;
                status.Text = "관전중....";
                byte[] buf = Encoding.ASCII.GetBytes("[Observer]");
                stream.Write(buf, 0, buf.Length);
            }
        }

        /* 서버로부터 메시지를 전달 받습니다. */
        private void read()
        {
            while (true)
            {
                byte[] buf = new byte[1024];
                int bufBytes = stream.Read(buf, 0, buf.Length);
                string message = Encoding.UTF8.GetString(buf, 0, bufBytes);
                if (Status.player == true)
                {
                    if (message.Contains("[Start]"))
                    {
                        entered = true;
                        status.Invoke((MethodInvoker)delegate ()
                        {
                            status.Text = "시작";
                        });
                    }
                    if (message.Contains("[Play]"))
                    {
                        refresh();
                        string horse = message.Split(']')[1];
                        if (horse.Contains("Black"))
                        {
                            this.status.Text = "당신의 차례입니다.";
                            nowTurn = true;
                            nowPlayer = Horse.BLACK;
                        }
                        else
                        {
                            this.status.Text = "상대방의 차례입니다.";
                            nowTurn = false;
                            nowPlayer = Horse.WHITE;
                        }
                        playing = true;
                    }
                    if (message.Contains("[Exit]"))
                    {
                        this.status.Text = "상대방이 나갔습니다.";
                        refresh();
                    }
                    /* 상대방이 돌을 둔 경우 (메시지: [Put]{X,Y}) */
                    if (message.Contains("[Put]"))
                    {
                        string position = message.Split(']')[1];
                        int x = Convert.ToInt32(position.Split(',')[0]);
                        int y = Convert.ToInt32(position.Split(',')[1]);
                        Horse enemyPlayer = Horse.none;
                        if (nowPlayer == Horse.BLACK)
                        {
                            enemyPlayer = Horse.WHITE;
                        }
                        else
                        {
                            enemyPlayer = Horse.BLACK;
                        }
                        if (board[x, y] != Horse.none) continue;
                        board[x, y] = enemyPlayer;
                        Graphics g = this.boardPicture.CreateGraphics();
                        if (enemyPlayer == Horse.BLACK)
                        {
                            SolidBrush brush = new SolidBrush(Color.Black);
                            g.FillEllipse(brush, x * rectSize, y * rectSize, rectSize, rectSize);
                        }
                        else
                        {
                            SolidBrush brush = new SolidBrush(Color.White);
                            g.FillEllipse(brush, x * rectSize, y * rectSize, rectSize, rectSize);
                        }
                        if (judge(enemyPlayer))
                        {
                            status.Text = "패배했습니다.";
                            playing = false;
                        }
                        else
                        {
                            status.Text = "당신이 둘 차례입니다.";
                        }
                        nowTurn = true;
                    }
                    
                }
                else
                {
                    if (message.Contains("[Put]"))
                    {
                        string s = message.Split(']')[1];

                        string[] position = s.Split(';');

                        Graphics g = this.boardPicture.CreateGraphics();

                        for (int i = 0; i < position.Length - 1; i++)
                        {
                            SolidBrush brush;
                            if (position[i].Split(',')[0] == "W")
                            {
                                brush = new SolidBrush(Color.White);
                            }
                            else
                            {
                                brush = new SolidBrush(Color.Black);
                            }

                            int x = Convert.ToInt32(position[i].Split(',')[1]);
                            int y = Convert.ToInt32(position[i].Split(',')[2]);

                            g.FillEllipse(brush, x * rectSize, y * rectSize, rectSize, rectSize);
                        }
                    }
                }
                if (message.Contains("[Chat]"))
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
                else if (message.Contains("[People]"))
                {
                    string[] s = message.Split(']')[1].Split(',');

                    inviteListView.Invoke((MethodInvoker)delegate ()
                    {
                        foreach (var word in s)
                        {
                            ListViewItem item = new ListViewItem(word);
                            inviteListView.Items.Add(item);
                        }
                    });
                }
                else if (message.Contains("[Invite]"))
                {
                    string s = message.Split(']')[1];
                    if (s == "fail")
                        MessageBox.Show("초대 실패");
                    
                }
            }
        }

        private void boardPicture_MouseDown(object sender, MouseEventArgs e)
        {
            if (Status.player == false)
            {
                //MessageBox.Show("관전중입니다.");
                return;
            }
            else if (!playing)
            {
                MessageBox.Show("게임을 실행해주세요.");
                return;
            }
            if (!nowTurn)
            {
                return;
            }
            Graphics g = this.boardPicture.CreateGraphics();
            int x = e.X / rectSize;
            int y = e.Y / rectSize;
            if (x < 0 || y < 0 || x >= edgeCount || y >= edgeCount)
            {
                MessageBox.Show("테두리를 벗어날 수 없습니다.");
                return;
            }
            if (board[x, y] != Horse.none) return;
            board[x, y] = nowPlayer;
            if (nowPlayer == Horse.BLACK)
            {
                SolidBrush brush = new SolidBrush(Color.Black);
                g.FillEllipse(brush, x * rectSize, y * rectSize, rectSize, rectSize);
            }
            else
            {
                SolidBrush brush = new SolidBrush(Color.White);
                g.FillEllipse(brush, x * rectSize, y * rectSize, rectSize, rectSize);
            }
            /* 놓은 바둑돌의 위치 보내기 */
            string message = "[Put]" + x + "," + y + "," + (nowPlayer == Horse.BLACK ? "B" : "W");
            byte[] buf = Encoding.ASCII.GetBytes(message);
            stream.Write(buf, 0, buf.Length);
            /* 판정 처리하기 */
            if (judge(nowPlayer))
            {
                status.Text = "승리했습니다.";
                playing = false;
                return;
            }
            else
            {
                status.Text = "상대방이 둘 차례입니다.";
            }
            /* 상대방의 차레로 설정하기 */
            nowTurn = false;
        }

        private void boardPicture_Paint(object sender, PaintEventArgs e)
        {
            Graphics gp = e.Graphics;
            Color lineColor = Color.Black; // 오목판의 선 색깔
            Pen p = new Pen(lineColor, 2);
            gp.DrawLine(p, rectSize / 2, rectSize / 2, rectSize / 2, rectSize * edgeCount - rectSize / 2); // 좌측
            gp.DrawLine(p, rectSize / 2, rectSize / 2, rectSize * edgeCount - rectSize / 2, rectSize / 2); // 상측
            gp.DrawLine(p, rectSize / 2, rectSize * edgeCount - rectSize / 2, rectSize * edgeCount - rectSize / 2, rectSize * edgeCount - rectSize / 2); // 하측
            gp.DrawLine(p, rectSize * edgeCount - rectSize / 2, rectSize / 2, rectSize * edgeCount - rectSize / 2, rectSize * edgeCount - rectSize / 2); // 우측
            p = new Pen(lineColor, 1);
            // 대각선 방향으로 이동하면서 십자가 모양의 선 그리기
            for (int i = rectSize + rectSize / 2; i < rectSize * edgeCount - rectSize / 2; i += rectSize)
            {
                gp.DrawLine(p, rectSize / 2, i, rectSize * edgeCount - rectSize / 2, i);
                gp.DrawLine(p, i, rectSize / 2, i, rectSize * edgeCount - rectSize / 2);
            }
        }

        private void MultiPlayForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(thread.IsAlive)
                thread.Abort();
        }

        private void readyButton_Click(object sender, EventArgs e)
        {
            status.Text = "Ready";
            byte[] buf = Encoding.ASCII.GetBytes($"[Room]Ready");
            stream.Write(buf, 0, buf.Length);
            readyButton.Enabled = false;
        }

        private void chatInputTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) && !string.IsNullOrEmpty(chatInputTextBox.Text))
            {
                byte[] buf = Encoding.UTF8.GetBytes($"[Room]Chat{(char)0x01}{chatInputTextBox.Text.Replace(']', (char)0x02)}");
                stream.Write(buf, 0, buf.Length);
                chatInputTextBox.Clear();
            }
        }

        private void inviteButton_Click(object sender, EventArgs e)
        {
            inviteListView.Items.Clear();
            byte[] buf = Encoding.ASCII.GetBytes("[Lobby]People");
            stream.Write(buf, 0, buf.Length);
            invitePanel.Visible = true;

            //InviteForm invite = new InviteForm();
            //DialogResult inviteDialog = invite.ShowDialog();
        }

        private void inviteCloseButton_Click(object sender, EventArgs e)
        {
            invitePanel.Visible = false;
        }

        private void sInviteButton_Click(object sender, EventArgs e)
        {
            if (inviteListView.SelectedItems.Count == 0) 
            {
                MessageBox.Show("유저를 선택해 주세요.");
            }
            else
            {
                byte[] buf = Encoding.UTF8.GetBytes($"[Invite]{inviteListView.SelectedItems[0].Text},{Room.currentRoomNum}");
                stream.Write(buf, 0, buf.Length);
            }
        }

        private void inviteListView_DoubleClick(object sender, EventArgs e)
        {
            byte[] buf = Encoding.UTF8.GetBytes($"[Invite]{inviteListView.SelectedItems[0].Text},{Room.currentRoomNum}");
            stream.Write(buf, 0, buf.Length);
        }
    }
}

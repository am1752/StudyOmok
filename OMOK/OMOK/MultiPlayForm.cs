using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace OMOK
{
    public partial class MultiPlayForm : Form
    {
        private const int rectSize = 33; //오목판 셀 크기
        private const int edgeCount = 15; //오목판의 선 개수

        private enum Horse { none = 0, BLACK, WHITE };
        private Horse[,] board = new Horse[edgeCount, edgeCount];
        private Horse nowPlayer = Horse.BLACK;
        private bool nowTurn;

        public Thread thread; // 통신을 위한 쓰레드
        private TcpClient tcpClient;// TCP 클라이언트
        private NetworkStream stream;

        private bool playing;
        private bool entered;
        public static bool threading;


        string RoomName = Commons.ROOMName;
        string result = "";
        int CK = 0;
        int Color_DO = 0;

        Thread threads;

        string strConnString = "Server=localhost;Port=3306;" +
            "Database=OMOK;Uid=root;Pwd=mysql_p@ssw0rd;";
        byte[] buf = Encoding.ASCII.GetBytes("[ID]" + Commons.UserId);


        public MultiPlayForm()
        {
            InitializeComponent();
            tcpClient = LoginForm.tcpClient;
            stream = LoginForm.stream;
            

            string message = "[MULTI]";
            byte[] buf2 = Encoding.ASCII.GetBytes(message + RoomName + "," + Commons.UserId);
            LoginForm.stream.Write(buf2, 0, buf2.Length);

            playing = false;
            entered = false;
            threading = false;
            board = new Horse[edgeCount, edgeCount];
            nowTurn = false;

            BoardPicture.Enabled = (Commons.Mousedown == 0) ? true : false;
            playbutton.Enabled = (Commons.Mousedown == 0) ? true : false;

            View();

            this.status.Text = "[" + RoomName + "]번 방에 접속했습니다.";

            IDst.Text = Commons.UserId + "님 안녕하세요.";

            if(Commons.Mousedown == 1)
            {
                byte[] buf = Encoding.ASCII.GetBytes("[Observer]");
                stream.Write(buf, 0, buf.Length);
            }


        }

        private void BoardPicture_MouseDown(object sender, MouseEventArgs e)
        {
            if (Commons.Mousedown == 1)
            {
                MessageBox.Show("권한이 없습니다.");
                return;
            }
            if (!playing)
            {
                MessageBox.Show("게임을 실행해주세요.");
                return;
            }
            if (!nowTurn)
            {
                return;
            }
            Graphics g = this.BoardPicture.CreateGraphics();
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
            string message = "[Put]" + RoomName + "," + x + "," + y;
            byte[] buf = Encoding.ASCII.GetBytes(message);
            LoginForm.stream.Write(buf, 0, buf.Length);
            /* 판정 처리하기 */
            if (judge(nowPlayer))
            {
                status.Text = "승리했습니다.";
                UpdateWin();
                playing = false;
                playbutton.Text = "재시작";
                playbutton.Enabled = true;
                return;
            }
            else
            {
                if (Commons.Mousedown == 0) status.Text = "상대방이 둘 차례입니다.";
                else this.status.Text = "관전 중 입니다.";
            }
            /* 상대방의 차레로 설정하기 */
            nowTurn = false;

        }

        private void BoardPicture_Paint(object sender, PaintEventArgs e)
        {
            Graphics gp = e.Graphics;
            Color lineColor = Color.Black; // 오목판 선 색깔

            Pen p = new Pen(lineColor, 2);

            gp.DrawLine(p, rectSize / 2, rectSize / 2, rectSize / 2, rectSize * edgeCount - rectSize / 2); //좌측
            gp.DrawLine(p, rectSize / 2, rectSize / 2, rectSize * edgeCount - rectSize / 2, rectSize / 2); //상측
            gp.DrawLine(p, rectSize / 2, rectSize * edgeCount - rectSize / 2, rectSize * edgeCount - rectSize / 2, rectSize * edgeCount - rectSize / 2); // 하측
            gp.DrawLine(p, rectSize * edgeCount - rectSize / 2, rectSize / 2, rectSize * edgeCount - rectSize / 2, rectSize * edgeCount - rectSize / 2); //우측
            p = new Pen(lineColor, 1);

            for (int i = rectSize / 2 + rectSize; i < rectSize * edgeCount - rectSize / 2; i += rectSize)
            {
                gp.DrawLine(p, rectSize / 2, i, rectSize * edgeCount - rectSize / 2, i);
                gp.DrawLine(p, i, rectSize / 2, i, rectSize * edgeCount - rectSize / 2);
            }
        }

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
            BoardPicture.Refresh();
            for (int i = 0; i < edgeCount; i++)
            {
                for (int j = 0; j < edgeCount; j++)
                {
                    board[i, j] = Horse.none;
                }
            }
            playbutton.Enabled = false;
        }

        private void playbutton_Click(object sender, EventArgs e)
        {
            
            if (!playing)
            {
                refresh();
                playing = true;
                string message = "[Play]";
                byte[] buf = Encoding.ASCII.GetBytes(message + RoomName);
                LoginForm.stream.Write(buf, 0, buf.Length);
                //playbutton.Text = "재시작";
                //status.Text = nowPlayer.ToString() + "플레이어의 차례입니다.";

                this.status.Text = "상대 플레이어의 준비를 기다립니다.";
                this.playbutton.Enabled = false;
            }

            else
            {
                refresh();
                status.Text = "게임이 재시작되었습니다.";
            }
        }

        private void View()
        {
            //LoginForm.init();
            ///LoginForm.tcpClient.Client
            //LoginForm.stream = LoginForm.tcpClient.GetStream();

            Thread.Sleep(10);
            threads = new Thread(new ThreadStart(read));
            threads.Start();
            threading = true;

            if(Commons.Mousedown == 1)
            {
                byte[] buf = Encoding.ASCII.GetBytes("[Observer]");
                stream.Write(buf, 0, buf.Length);
            }

            ///* 방 접속 진행하기 */
            string message = "[Enter]";
            string issee = Commons.Mousedown == 0 ? "0" : "1";
            byte[] buf1 = Encoding.ASCII.GetBytes(message + RoomName + "," + Commons.UserId+","+issee);
            LoginForm.stream.Write(buf1, 0, buf1.Length);
            //LoginForm.stream.Write(buf, 0, buf.Length);


            //this.EnterButton.Enabled = false;
            //this.playbutton.Enabled = true;
            //this.status.Text = "[" + this.roomTextBox.Text + "]번 방에 접속";
        }

        /* 서버로부터 메시지를 전달 받습니다. */
        private void read()
        {
            while (true)
            {
                byte[] buf = new byte[1024];
                int bufBytes = LoginForm.stream.Read(buf, 0, buf.Length);
                string message = Encoding.ASCII.GetString(buf, 0, bufBytes);
                /* 접속 성공 (메시지: [Enter]) */
                if (Commons.Mousedown == 0)
                {
                    if (message.Contains("[Enter]"))
                    {
                        status.Invoke((MethodInvoker)delegate ()
                        {
                            this.status.Text = "[" + RoomName + "]번 방에 접속했습니다.";
                            /* 게임 시작 처리 */
                            entered = true;
                        });
                    }
                    /* 방이 가득 찬 경우 (메시지: [Full]) */
                    if (message.Contains("[Full]") && Commons.Mousedown == 1)
                    {
                        this.status.Text = "관전 중입니다.";
                        closeNetwork();
                    }
                    /* 게임 시작 (메시지: [Play]{Horse}) */
                    if (message.Contains("[Play]"))
                    {
                        refresh();
                        string horse = message.Split(']')[1];
                        if (horse.Contains("Black"))
                        {
                            if (Commons.Mousedown == 0) this.status.Text = "당신의 차례입니다.";
                            else this.status.Text = "관전 중 입니다.";
                            nowTurn = true;
                            //Color_DO = 1;
                            nowPlayer = Horse.BLACK;
                        }
                        else
                        {
                            if (Commons.Mousedown == 0) this.status.Text = "상대방의 차례입니다.";
                            else this.status.Text = "관전 중 입니다.";
                            nowTurn = false;
                            //Color_DO = 0;
                            nowPlayer = Horse.WHITE;
                        }
                        playing = true;
                    }
                    /* 상대방이 나간 경우 (메시지: [Exit]) */
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
                        Graphics g = this.BoardPicture.CreateGraphics();
                        if (enemyPlayer == Horse.BLACK || (Color_DO == 0 && Commons.Mousedown == 1))
                        {
                            SolidBrush brush = new SolidBrush(Color.Black);
                            g.FillEllipse(brush, x * rectSize, y * rectSize, rectSize, rectSize);
                            Color_DO = 1;
                        }
                        else
                        {
                            SolidBrush brush = new SolidBrush(Color.White);
                            g.FillEllipse(brush, x * rectSize, y * rectSize, rectSize, rectSize);
                            Color_DO = 0;
                        }
                        if (judge(enemyPlayer))
                        {
                            status.Text = "패배했습니다.";
                            Updatelose();
                            playing = false;
                            playbutton.Text = "재시작";
                            playbutton.Enabled = true;
                        }
                        else
                        {
                            if (Commons.Mousedown == 0) this.status.Text = "당신의 차례입니다.";
                            else this.status.Text = "관전 중 입니다.";
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

                        Graphics g = this.BoardPicture.CreateGraphics();

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
            }
        }


        private void closeNetwork()
        {
            if (threading && thread.IsAlive) thread.Abort();
            if (entered) tcpClient.Close();
        }

        private void MultiPlayForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //closeNetwork();
        }


        private void Exitbutton_Click(object sender, EventArgs e)
        {
            SelectPerson();
            //string txt = "상대방이 나갔습니다.";
            updateroom();
            if (result == "2" && Commons.Mousedown == 0) UpdatePerson1();
            else if (result == "1" && Commons.Mousedown == 0) DeleteRoom();

            threads.Abort();
            Hide();
            ROOMFORM roomFORM = new ROOMFORM();
            //roomFORM.FormClosed += new FormClosedEventHandler(childForm_Closed);
            roomFORM.Show();
        }

        void UpdatePerson1()
        {
            string strQuery = " UPDATE omok.roomtbl " +
                               " SET " +
                               " person = 1 " +
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

                MySqlParameter paramname = new MySqlParameter("@Name", MySqlDbType.VarChar, 45);
                paramname.Value = RoomName; // 공백 넣는 경우가 아주 많기때문에

                cmd.Parameters.Add(paramname);
                cmd.ExecuteNonQuery();
                //cmd.ExecuteScalar

            }

        }

        private void SelectPerson()
        {
            string strQuery = " SELECT " +
                              " person " +
                              " FROM roomtbl " +
                              " WHERE Name = @Name; ";

            using (MySqlConnection conn = new MySqlConnection(strConnString))
            {

                conn.Open();
                //MetroMessageBox.Show(this, $"DB접속성공!!");
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = strQuery;

                MySqlParameter paramname = new MySqlParameter("@Name", MySqlDbType.VarChar, 45);
                paramname.Value = RoomName; // 공백 넣는 경우가 아주 많기때문에

                cmd.Parameters.Add(paramname);
                cmd.ExecuteNonQuery();
                result = cmd.ExecuteScalar().ToString();

            }

        }


        void DeleteRoom()
        {
            string strQuery = " DELETE FROM roomtbl " +
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

                MySqlParameter paramname = new MySqlParameter("@Name", MySqlDbType.VarChar, 45);
                paramname.Value = RoomName; // 공백 넣는 경우가 아주 많기때문에

                cmd.Parameters.Add(paramname);
                cmd.ExecuteNonQuery();

            }
        }

        private void UpdateWin()
        {
            string strQuery = "  UPDATE " +
                               " scoretbl " +
                               " set " +
                               " win = @win " +
                               " where id = @id; ";

            using (MySqlConnection conn = new MySqlConnection(strConnString))
            {

                conn.Open();
                //MetroMessageBox.Show(this, $"DB접속성공!!");
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = strQuery;

                MySqlParameter paramname = new MySqlParameter("@id", MySqlDbType.VarChar, 45);
                paramname.Value = Commons.UserId; // 공백 넣는 경우가 아주 많기때문에
                cmd.Parameters.Add(paramname);

                MySqlParameter paramwin = new MySqlParameter("@win", MySqlDbType.Int32);
                paramwin.Value = win()+1; // 공백 넣는 경우가 아주 많기때문에
                cmd.Parameters.Add(paramwin);

                cmd.ExecuteNonQuery();

            }


        }

        private int win()
        {
            string strQuery = " SELECT win " +
                              " FROM scoretbl " +
                              " WHERE id = @id; ";

            using (MySqlConnection conn = new MySqlConnection(strConnString))
            {

                conn.Open();
                //MetroMessageBox.Show(this, $"DB접속성공!!");
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = strQuery;

                MySqlParameter paramname = new MySqlParameter("@id", MySqlDbType.VarChar, 45);
                paramname.Value = Commons.UserId; // 공백 넣는 경우가 아주 많기때문에
                cmd.Parameters.Add(paramname);

                cmd.ExecuteNonQuery();
                string win1 = cmd.ExecuteScalar().ToString();

                return int.Parse(win1);
            }
        }

        private void Updatelose()
        {
            string strQuery = " UPDATE " +
                               " scoretbl " +
                               " set " +
                               " lose = @lose " +
                               " where id = @id; ";

            using (MySqlConnection conn = new MySqlConnection(strConnString))
            {

                conn.Open();
                //MetroMessageBox.Show(this, $"DB접속성공!!");
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = strQuery;

                MySqlParameter paramname = new MySqlParameter("@id", MySqlDbType.VarChar, 45);
                paramname.Value = Commons.UserId; // 공백 넣는 경우가 아주 많기때문에
                cmd.Parameters.Add(paramname);

                MySqlParameter paramlose = new MySqlParameter("@lose", MySqlDbType.Int32);
                paramlose.Value = lose() + 1; // 공백 넣는 경우가 아주 많기때문에
                cmd.Parameters.Add(paramlose);

                cmd.ExecuteNonQuery();

            }


        }

        private int lose()
        {
            string strQuery = " SELECT lose " +
                              " FROM scoretbl " +
                              " WHERE id = @id; ";

            using (MySqlConnection conn = new MySqlConnection(strConnString))
            {

                conn.Open();
                //MetroMessageBox.Show(this, $"DB접속성공!!");
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = strQuery;

                MySqlParameter paramname = new MySqlParameter("@id", MySqlDbType.VarChar, 45);
                paramname.Value = Commons.UserId; // 공백 넣는 경우가 아주 많기때문에
                cmd.Parameters.Add(paramname);

                cmd.ExecuteNonQuery();
                string lose1 = cmd.ExecuteScalar().ToString();

                return int.Parse(lose1);
            }
        }

       

        private void updateroom()
        {
            string strQuery = " UPDATE omok.member " +
                               " SET " +
                               " room = NULL " +
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
                //MySqlParameter paramroom = new MySqlParameter("@room", MySqlDbType.VarChar, 45);
                //paramroom.Value = Commons.ROOMName; // 공백 넣는 경우가 아주 많기때문에

                MySqlParameter paramid = new MySqlParameter("@id", MySqlDbType.VarChar, 45);
                paramid.Value = Commons.UserId;

                cmd.Parameters.Add(paramid);
                //cmd.Parameters.Add(paramroom);
                cmd.ExecuteNonQuery();
                //UpdateData();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OMOK
{
    public partial class MenuForm : Form
    {
        public MenuForm()
        {
            InitializeComponent();
        }

        private void singlePlayButton_Click(object sender, EventArgs e)
        {
            Hide();
            SingleplayForm singleplayForm = new SingleplayForm();
            singleplayForm.FormClosed += new FormClosedEventHandler(childForm_Closed);
            singleplayForm.Show();
        }

        void childForm_Closed(object sender, FormClosedEventArgs e)
        {
            Show();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void multiPlayButton_Click(object sender, EventArgs e)
        {
            Hide();
            LoginForm loginForm = new LoginForm();
            loginForm.FormClosed += new FormClosedEventHandler(childForm_Closed);
            loginForm.Show();
        }

        private void AlphaPlayButton_Click(object sender, EventArgs e)
        {
            Hide();
            AllPlayForm allPlayForm = new AllPlayForm();
            allPlayForm.FormClosed += new FormClosedEventHandler(childForm_Closed);
            allPlayForm.Show();
        }

        private void MenuForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            Hide();
            SearchForm searchForm = new SearchForm();
            searchForm.FormClosed += new FormClosedEventHandler(childForm_Closed);
            searchForm.Show();
        }
    }
}

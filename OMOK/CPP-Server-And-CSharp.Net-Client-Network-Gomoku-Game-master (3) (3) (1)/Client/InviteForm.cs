using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class InviteForm : Form
    {
        public static ListViewItem userId;
        public InviteForm()
        {
            InitializeComponent();
        }

        private void inviteButton_Click(object sender, EventArgs e)
        {

            if (inviteListView.SelectedItems.Count != 0) 
            {
                userId = inviteListView.SelectedItems[0];
                DialogResult = DialogResult.OK;
            }
        }
    }
}

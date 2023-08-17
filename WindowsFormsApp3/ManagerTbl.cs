using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class ManagerTbl : Form
    {
        public UsersTbl userName;
        public ManagerTbl(UsersTbl user)
        {
            InitializeComponent();
            userName = user;
        }

        private void ManagerTbl_Load(object sender, EventArgs e)
        {

        }
    }
}

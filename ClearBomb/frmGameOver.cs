using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClearBomb
{
    public partial class frmGameOver : Form
    {
        public frmGameOver()
        {
            InitializeComponent();
        }
        public frmMain fm = null;
        private void button1_Click(object sender, EventArgs e)
        {
            fm.NewGameStr = "输了退出";
            fm.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            fm.NewGameStr = "输了重新开始";
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            fm.NewGameStr = "输了再玩一局";
            this.Close();
        }
    }
}

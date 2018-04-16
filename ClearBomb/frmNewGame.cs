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
    public partial class frmNewGame : Form
    {
        public frmNewGame()
        {
            InitializeComponent();
        }
        public frmMain fm = new frmMain();
        private void button1_Click(object sender, EventArgs e)
        {
            fm.NewGameStr = "新游戏";
            Back();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            fm.NewGameStr = "重新";
            Back();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            fm.NewGameStr = "继续游戏";
            Back();
        }
        public void Back()
        {
            this.Close();
        }
    }
}

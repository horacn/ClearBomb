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
    public partial class frmWin : Form
    {
        public frmWin()
        {
            InitializeComponent();
        }
        public int time = 0;
        public frmMain fm;
        private void button2_Click(object sender, EventArgs e)
        {
            fm.NewGameStr = "赢了退出";
            fm.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            fm.NewGameStr = "赢了再玩一局";
            this.Close();
        }

        private void frmWin_Load(object sender, EventArgs e)
        {
            if (time > 60)
            {
                int tempMin = time / 60;
                int tempSen = time % 60;
                if (tempSen == 0)
                {
                    this.label1.Text += tempMin + "分钟";
                }
                else
                {
                    this.label1.Text += tempMin + "分零" + tempSen + "秒";
                }
            }
            else
            {
                this.label1.Text += time + "秒";
            }
        }
    }
}

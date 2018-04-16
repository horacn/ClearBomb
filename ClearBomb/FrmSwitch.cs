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
    public partial class FrmSwitch : Form
    {
        public FrmSwitch()
        {
            InitializeComponent();
        }
        private void FrmSwitch_Load(object sender, EventArgs e)
        {
            //读取难度系数
            string nanDuStr = FileTool.ReadFile("config\\SwitchNanDu.bomb");
            if (nanDuStr == string.Empty)
            {
                 this.rbtnEasy.Checked = true;
            }
            else
            {
                nanDu = int.Parse(nanDuStr);
                switch (nanDu)
                {
                    case 1:
                        this.rbtnEasy.Checked = true;
                        break;
                    case 2:
                        this.rbtnSoSo.Checked = true;
                        break;
                    case 3:
                        this.rbtnHigh.Checked = true;
                        break;
                    case 4:
                        this.rbtnUser.Checked = true;
                        break;
                }
            }
            string height = string.Empty;
            string weitght = string.Empty;
            string bomb = string.Empty;
            //从文件读取高度，宽度，雷数
            FileTool.ReadFile("config\\SwitchNums.bomb", ref height, ref weitght, ref bomb);
            if (height != string.Empty && weitght != string.Empty && bomb != string.Empty)
            {
                this.txtBomb.Text = bomb;
                this.txtHigh.Text = height;
                this.txtWidth.Text = weitght;
            }
            else
            {
                this.txtBomb.Text = "48";
                this.txtHigh.Text = "24";
                this.txtWidth.Text = "30";
            }
        }
        public frmMain fm;
        private int nanDu;//难度

        public void XuanZe()
        {
            if (this.rbtnEasy.Checked)
            {
                fm.HowRowsCols = 1;
                fm.Rows = 9;
                fm.Cols = 9;
                fm.Bombs = 10;
                nanDu = 1;
            }
            else if (this.rbtnSoSo.Checked)
            {
                fm.HowRowsCols = 2;
                fm.Rows = 16;
                fm.Cols = 16;
                fm.Bombs = 40;
                nanDu = 2;
            }
            else if (this.rbtnHigh.Checked)
            {
                fm.HowRowsCols = 3;
                fm.Rows = 16;
                fm.Cols = 30;
                fm.Bombs = 99;
                nanDu = 3;
            }
            else
            {
                fm.HowRowsCols = 4;
                fm.Rows = int.Parse(this.txtHigh.Text.Trim());
                fm.Cols = int.Parse(this.txtWidth.Text.Trim());
                fm.Bombs = int.Parse(this.txtBomb.Text.Trim());
                nanDu = 4;
            }
        }
        private void rbtnUser_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtnUser.Checked)
            {
                this.txtBomb.Enabled = true;
                this.txtHigh.Enabled = true;
                this.txtWidth.Enabled = true;
                this.lblHeight.ForeColor = Color.Black;
                this.lblWidth.ForeColor = Color.Black;
                this.lblBombs.ForeColor = Color.Black;
            }
            else
            {
                this.txtBomb.Enabled = false;
                this.txtHigh.Enabled = false;
                this.txtWidth.Enabled = false;
                this.lblHeight.ForeColor = Color.Gray;
                this.lblWidth.ForeColor = Color.Gray;
                this.lblBombs.ForeColor = Color.Gray;
            }
        }

        private void txtHigh_Leave(object sender, EventArgs e)
        {
            int high = int.Parse(this.txtHigh.Text.Trim());
            if (high < 9)
            {
                this.txtHigh.Text = "9";
                MessageBox.Show("输入的值太小", "数值超出范围", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.txtHigh.Focus();
                return;
            }
            if (high > 24)
            {
                this.txtHigh.Text = "18";
                MessageBox.Show("输入的值太大", "数值超出范围", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.txtHigh.Focus();
                return;
            }
        }

        private void txtWidth_Leave(object sender, EventArgs e)
        {
            int width = int.Parse(this.txtWidth.Text.Trim());
            if (width < 9)
            {
                this.txtWidth.Text = "9";
                MessageBox.Show("输入的值太小", "数值超出范围", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.txtWidth.Focus();
                return;
            }
            if (width > 30)
            {
                this.txtWidth.Text = "22";
                MessageBox.Show("输入的值太大", "数值超出范围", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.txtWidth.Focus();
                return;
            }
        }

        private void txtBomb_Leave(object sender, EventArgs e)
        {
            int bomb = int.Parse(this.txtBomb.Text.Trim());
            if (bomb < 10)
            {
                this.txtBomb.Text = "10";
                MessageBox.Show("输入的值太小", "数值超出范围", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.txtBomb.Focus();
                return;
            }
            if (bomb > 268)
            {
                this.txtBomb.Text = "147";
                MessageBox.Show("输入的值太大", "数值超出范围", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.txtBomb.Focus();
                return;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.rbtnUser.Checked)
            {
                if (this.txtHigh.Text.Trim().Length == 0)
                {
                    MessageBox.Show("请输入高度！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.txtHigh.Text = "9";
                    this.txtHigh.Focus();
                    return;
                }
                if (this.txtWidth.Text.Trim().Length == 0)
                {
                    MessageBox.Show("请输入宽度！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.txtWidth.Text = "9";
                    this.txtWidth.Focus();
                    return;
                }
                if (this.txtBomb.Text.Trim().Length == 0)
	            {
                    MessageBox.Show("请输入雷数！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.txtBomb.Text = "10";
                    this.txtBomb.Focus();
                    return;
	            }
                if (!CheckStringIsNum(this.txtHigh.Text.Trim()))
                {
                    MessageBox.Show("您只能在此处输入数字 ", "不能接受的字符", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.txtHigh.Clear();
                    this.txtHigh.Focus();
                    return;
                }
                if (!CheckStringIsNum(this.txtBomb.Text.Trim()))
                {
                    MessageBox.Show("您只能在此处输入数字 ", "不能接受的字符", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.txtBomb.Clear();
                    this.txtBomb.Focus();
                    return;
                }
                if (!CheckStringIsNum(this.txtWidth.Text.Trim()))
                {
                    MessageBox.Show("您只能在此处输入数字 ", "不能接受的字符", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.txtWidth.Clear();
                    this.txtWidth.Focus();
                    return;
                }
                if (double.Parse(this.txtBomb.Text.Trim()) > int.Parse(this.txtHigh.Text.Trim()) * int.Parse(this.txtWidth.Text.Trim()) * 0.3722222222222223)
                {
                    MessageBox.Show("根据当前宽和高，雷数超出范围!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.txtHigh.Text = "22";
                    this.txtWidth.Text = "24";
                    this.txtBomb.Text = "147";
                    return;
                }
                //写入高度，宽度，雷数
                FileTool.WriteFile("config\\SwitchNums.bomb", this.txtHigh.Text.Trim(), this.txtWidth.Text.Trim(), this.txtBomb.Text.Trim());
            }
            fm.IsNewGames = true;
            XuanZe();
            //写入难度系数
            FileTool.WriteFile("config\\SwitchNanDu.bomb", nanDu.ToString());
            this.Close();
        }

        //检查数字是否合法
        private bool CheckStringIsNum(string checkStr)
        {
            if (checkStr.Trim().Length == 0)
                return false;
            double isMoney = 0;
            try
            {
                isMoney = Convert.ToDouble(checkStr.Trim());
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            fm.IsNewGames = false;
            this.Close();
        }

        private void txtHigh_KeyDown(object sender, KeyEventArgs e)
        {
            if (sender == this.txtHigh && this.txtHigh.Text.Trim().Length == 0)
            {
                return;
            }
            else if (sender == this.txtWidth && this.txtWidth.Text.Trim().Length == 0)
            {
                return;
            }
            else if (sender == this.txtBomb && this.txtBomb.Text.Trim().Length == 0)
            {
                return;
            }
            if (e.KeyCode != Keys.D1 && e.KeyCode != Keys.D2 && e.KeyCode != Keys.D3
                && e.KeyCode != Keys.D4 && e.KeyCode != Keys.D5 && e.KeyCode != Keys.D6
                && e.KeyCode != Keys.D7 && e.KeyCode != Keys.D8 && e.KeyCode != Keys.D9 && e.KeyCode != Keys.D0
                && e.KeyCode != Keys.NumPad0 && e.KeyCode != Keys.NumPad1 && e.KeyCode != Keys.NumPad2
                && e.KeyCode != Keys.NumPad3 && e.KeyCode != Keys.NumPad4 && e.KeyCode != Keys.NumPad5
                && e.KeyCode != Keys.NumPad6 && e.KeyCode != Keys.NumPad7 && e.KeyCode != Keys.NumPad8 && e.KeyCode != Keys.NumPad9
                && e.KeyCode != Keys.Back && e.KeyCode != Keys.Left && e.KeyCode != Keys.Right && e.KeyCode != Keys.Down && e.KeyCode != Keys.Up
                && e.KeyCode != Keys.Delete && e.KeyCode != Keys.Alt && e.KeyCode != Keys.ControlKey && e.KeyCode != Keys.ShiftKey && e.KeyCode != Keys.Enter
                && e.KeyCode != Keys.Home && e.KeyCode != Keys.End && e.KeyCode != Keys.PageDown && e.KeyCode != Keys.PageUp
                && e.KeyCode != Keys.Insert && e.KeyCode != Keys.Tab && e.KeyCode != Keys.Escape && e.KeyCode != Keys.F1
                && e.KeyCode != Keys.PrintScreen && e.KeyCode != Keys.Pause && e.KeyCode != Keys.Menu && e.KeyCode != Keys.LMenu && e.KeyCode != Keys.RMenu && e.KeyCode != Keys.Control
                && e.KeyCode != Keys.F2 && e.KeyCode != Keys.F3 && e.KeyCode != Keys.F4 && e.KeyCode != Keys.F5 && e.KeyCode != Keys.F6
                && e.KeyCode != Keys.F7 && e.KeyCode != Keys.F8 && e.KeyCode != Keys.F9)
            {
                MessageBox.Show("您只能在此处输入数字 ", "不能接受的字符", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (sender == this.txtHigh)
                {
                    this.txtHigh.Clear();
                    this.txtHigh.Focus();
                }
                else if (sender == this.txtWidth)
                {
                    this.txtWidth.Clear();
                    this.txtWidth.Focus();
                }
                else
                {
                    this.txtBomb.Clear();
                    this.txtBomb.Focus();
                }
            }
        }
    }
}

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
	public partial class frmMain : Form
	{
		public frmMain()
		{
			InitializeComponent();
		}
        //UcCell集合
        public Dictionary<string, UcCell> cells = new Dictionary<string, UcCell>();
        //格子集合
        public Dictionary<string, Cell> cs = new Dictionary<string, Cell>();
        //初始化
		private void frmMain_Load(object sender, EventArgs e)
		{
            string rows = string.Empty;
            string clos = string.Empty;
            string bombs = string.Empty;
            //读取高度，宽度，雷数参数
            FileTool.ReadFile("config\\MainNums.bomb", ref rows, ref clos, ref bombs);
            if (rows != string.Empty && clos != string.Empty && bombs != string.Empty)
            {
                Rows = int.Parse(rows);
                Cols = int.Parse(clos);
                Bombs = int.Parse(bombs);
            }
            else
            {
                Rows = 16;
                Cols = 16;
                Bombs = 40;
            }
            //游戏开始时，先创建一个雷阵
            NewGame(Rows, Cols, Bombs);
		}
        //创建游戏表格
		public void NewGame(int row,int col,int bombs)
        {
            //播放开始游戏的声音
            this.Media.URL = "Music\\开始游戏.wav";
            //初始化前先清空集合
            cells.Clear();
            cs.Clear();
            this.panelCells.Controls.Clear();

            //随机创建一定数量的雷
            int count = 0;//已经创建出的雷的数量
            List<bool> isBombList = new List<bool>();
            //随机数
            Random r = new Random(DateTime.Now.Year * 1000000 + DateTime.Now.Month * 10000 + DateTime.Now.Day * 1000 + DateTime.Now.Millisecond);
            for (int i = 0; i < row * col; i++)
            {
                bool isBomb = Convert.ToBoolean(r.Next(0, 2));
                if (count < bombs && isBomb)
                {
                    isBombList.Add(isBomb);
                    count++;
                }
                else
                    isBombList.Add(false);
            }
            //格子的坐标
            int x  = 0,y = 0;
            //uc对象
			UcCell uc = null;
            //创建游戏界面
			for (int i = 0; i < row; i++)
			{
				for (int j = 0; j < col; j++)
				{
                    //创建CELL对象
                    Cell c = null;
                    //随机产生有雷或者没雷的对象

                    int num = r.Next(0, isBombList.Count);
                    //如果num是1创建一个雷，如果时2的话，创建一个不是雷
                    if (isBombList[num])
                    {
                        c = new Bomb(i, j);
                    }
                    else
                    {
                        c = new NoBomb(i, j);
                    }
                    isBombList.RemoveAt(num);
                    //每一次创建出一个格子控件对象，将格子放到panel中
					uc  = new UcCell ();
                    uc.btn.MouseDown += btn_MouseDown;
                    uc.cell = c;
                    uc.Location = new Point(x, y);//设置这个控件在panel中的坐标
                    this.panelCells.Controls.Add(uc);//将这个控件放置到panel中
                    //将下一个的x坐标+格子宽度
                    x +=uc.Width;
                    this.cells.Add(uc.cell.Key, uc);
                    this.cs.Add(c.Key, c);
				}
                //将y坐标+=格子的高度
				y+=uc.Height;
				x = 0;
			}
            //计算窗体此时应该时什么宽高
			this.Width = uc.Width*col+this.Width-this.panelCells.Width;
            this.Height = uc.Height * row + this.Height + this.panel1.Height - this.panelCells.Height;
            //初始化每一个格子周围的雷数
            foreach (Cell c in cs.Values)
            {
                if (c is Bomb)
                {
                    continue;
                }
                c.GetNearBombs(cs);
            }
            //开始计算雷数显示到窗体上
            this.timer2.Start();
        }
        //打开格子次数
        public int ClickCount = 0;

        //打开格子的事件
        void btn_MouseDown(object sender, MouseEventArgs e)
        {
            //按下的按钮
            Button btn = (Button)sender;
            //打开的格子对象
            UcCell uc = (UcCell)btn.Parent;
            //如果按下的是左键
            if (e.Button == MouseButtons.Left)
            {
                uc.cells = this.cells;
                //如果打开的是空格，就播放声音
                string url =  uc.Open();
                if (url!=string.Empty)
                {
                    this.Media.URL = url;
                }

                //如果点到雷，游戏结束
                if (uc.IsGameOver == true)
                {
                    //时间计时器停止
                    this.timer1.Stop();
                    this.time = 0;
                    //播放声音
                    this.Media.URL = "Music\\爆炸.WAV";
                    //把所有得雷显示出来
                    foreach (UcCell ucell in cells.Values)
                    {
                        if (ucell.cell is Bomb)
                        {
                            ucell.Open();
                        }
                    }
                    //弹出游戏结束窗体
                    frmGameOver fg = new frmGameOver();
                    fg.fm = this;
                    fg.ShowDialog();
                    //开始新游戏
                    if (this.NewGameStr == "输了重新开始")
                    {
                        this.Media.URL = "Music\\开始游戏.wav";
                        foreach (UcCell u in this.cells.Values)
                        {
                            u.BeginAgianGame();
                        }
                        this.ClickCount = 0;
                        this.timer2.Start();
                    }
                    else if (this.NewGameStr == "输了再玩一局")
                    {
                        NewGame(Rows, Cols, Bombs);
                        this.ClickCount = 0;
                    }
                    uc.IsGameOver = false;
                    this.lblTime.Text = "0";
                    return;
                }
                //打开空格所有周围不是雷的格子
                uc.ShowNearNoBombs(uc);
                //打开的非雷的数量
                int openNoBombs = 0;
                //计算所有打开的不是雷的格子数量
                foreach (UcCell IsOk in cells.Values)
                {
                    if (IsOk.cell is NoBomb)
                    {
                        if (IsOk.cell.IsOpen)
                        {
                            openNoBombs++;
                        }
                    }
                }
                //如果打开的所有不是雷的数量加上雷的数量等于格子总数，那么游戏胜利
                if (openNoBombs +this.Bombs == this.Rows*this.Cols)
                {
                    this.timer1.Stop();
                    this.time = 0;
                    //把所有雷显示出来
                    foreach (UcCell ucell in cells.Values)
                    {
                        if (ucell.cell is Bomb)
                        {
                            ucell.btn.Image = null;
                            ucell.Open();
                        }
                    }
                    frmWin fw = new frmWin();
                    fw.fm = this;
                    fw.time = Convert.ToInt32(this.lblTime.Text);
                    fw.ShowDialog();
                    if (this.NewGameStr == "赢了退出")
                    {
                        return;
                    }
                    //开始新游戏
                    NewGame(Rows, Cols, Bombs);
                    //把点击次数清零
                    this.ClickCount = 0; 
                    uc.IsGameOver = false;
                    this.lblTime.Text = "0";
                    return;
                }
                //把点击次数加一
                this.ClickCount++;
                //加时间，每秒加一
                if (this.ClickCount == 1)
                {
                    this.timer1.Start();
                }
            }
            //如果按下的是右键，那么就做标记
            else if (e.Button == MouseButtons.Right)
            {
                //插小旗子
                if (uc.btn.Image == null && uc.btn.Text == string.Empty)
                {
                    uc.btn.Image = this.imageList1.Images[0];
                    return;
                }
                //标问号
                if (uc.btn.Image != null && uc.btn.Text == string.Empty)
                {
                    uc.btn.Text = "?";
                    uc.btn.ForeColor = Color.White;
                    uc.btn.Font = new Font(uc.btn.Name, 12, FontStyle.Regular | FontStyle.Bold);
                    uc.btn.Image = null;
                    return;
                }
                //还原
                if (uc.btn.Text != string.Empty && uc.btn.Image == null)
               {
                   uc.btn.Text = string.Empty;
                   uc.btn.Image = null;
               }
            }
        }

        //从难度窗口返回的游戏难度
        public int HowRowsCols = 0;
        //游戏的行数、列数、雷数
        public int Cols = 0;
        public int Rows = 0;
        public int Bombs = 0;
        //是否是新游戏
        public bool IsNewGames = false;
        private void toolStripMenuItem12_Click(object sender, EventArgs e)
        {
            this.IsNewGames = false;
            FrmSwitch fs = new FrmSwitch();
            fs.fm = this;
            fs.ShowDialog();
            //判断是否确定了游戏
            if (IsNewGames == false)
            {
                return;
            }
            //按照难度分别创建不同的表格
            //中等
            if (HowRowsCols == 2)
            {
                NewGame(Rows, Cols, Bombs);
                this.ClickCount = 0;
                ClearTime();
            }
            //难
            else if (HowRowsCols == 3)
            {
                NewGame(Rows, Cols, Bombs);
                this.ClickCount = 0;
                ClearTime();
            }
            //简单
            else if (HowRowsCols == 1)
            {
                NewGame(Rows, Cols, Bombs);
                this.ClickCount = 0;
                ClearTime();
            }
            //自定义难度
            else if (HowRowsCols == 4)
            {
                NewGame(Rows, Cols,Bombs);
                this.ClickCount = 0;
                ClearTime();
            }
        }
        //清除时间
        public void ClearTime()
        {
            this.timer1.Stop();
            this.time = 0;
            this.lblTime.Text = "0";
        }
        public string NewGameStr = string.Empty;
        //新游戏
        private void toolStripMenuItem11_Click(object sender, EventArgs e)
        {
            if (this.ClickCount == 0)
            {
                ClearTime();
                NewGame(Rows, Cols, Bombs);
                this.ClickCount = 0;
                return;
            }
            frmNewGame fn = new frmNewGame();
            fn.fm = this;
            fn.ShowDialog();
            if (this.NewGameStr == "新游戏")
            {
                ClearTime();
                NewGame(Rows, Cols, Bombs);
                this.ClickCount = 0;
            }
            else if (this.NewGameStr == "重新")
            {
                this.Media.URL = "Music\\开始游戏.wav";
                this.ClickCount = 0;
                foreach (UcCell u in this.cells.Values)
                {
                    u.BeginAgianGame();
                }
                this.timer2.Start();
                ClearTime();
            }
        }
        //关闭程序
        private void toolStripMenuItem18_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        public int time = 0;
        //显示游戏时间
        private void timer1_Tick(object sender, EventArgs e)
        {
            time++;
            this.lblTime.Text = time.ToString();
        }
        //每隔0.1秒就就算一下剩余雷数
        private void timer2_Tick(object sender, EventArgs e)
        {
            int count = 0;
            foreach (UcCell u in this.cells.Values)
            {
                if (u.cell is Bomb)
                {
                    if (!u.cell.IsOpen)
                    {
                        count++;
                    }
                }
                //如果做标记就减一
                if (u.btn.Image!=null)
                    {
                        if (count >0)
                        {
                            count--;
                        }    
                    }
            }
            this.lblBombs.Text = count.ToString();
        }
        //关于游戏
        private void 游戏规则GToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAbout fa = new frmAbout();
            fa.ShowDialog();
        }

        private void 关于我们AToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAboutWe fa = new frmAboutWe();
            fa.ShowDialog();
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            //写入高度，宽度，雷数参数
            FileTool.WriteFile("config\\MainNums.bomb", this.Rows.ToString(), this.Cols.ToString(), this.Bombs.ToString());
        }
	}
}

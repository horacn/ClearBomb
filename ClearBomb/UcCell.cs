using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClearBomb
{
    public partial class UcCell : UserControl
    {
        public UcCell()
        {
            InitializeComponent();
        }
        public Cell cell;
        //格子集合
        public Dictionary<string, UcCell> cells;
        //是否打开过
        public bool IsOpen = false;

        public bool IsGameOver = false;
        public string Open()
        {
            if (this.btn.Image != null && this.IsGameOver == false)
            {
                return string.Empty;
            }
            this.btn.Visible = false;
            this.IsOpen = true;
            this.cell.IsOpen = true;
            //判断该控件中的格子到底时雷还不是雷
            if (cell is Bomb)
            {
                this.lbl.Image = this.imageList1.Images[0];
                IsGameOver = true;
                return string.Empty;
            }
            else
            {
               return ShowNoBombs();
            }
        }
        //如果打开的这个格子是空，则打开它周围的非雷格子
        public void ShowNearNoBombs(UcCell uc)
        {
            if (uc.btn.Image != null)
            {
                return;
            }
            if (uc.lbl.Text == string.Empty && uc .cell is NoBomb)
            {
                int cols = uc.cell.Col;//y
                int rows = uc.cell.Row; //x
                string LC = rows - 1 + "_" + (cols);//上
                string CL = rows + "_" + (cols - 1);//左
                string CR = rows + "_" + (cols + 1);//右
                string UM = rows + 1 + "_" + (cols);//下
                string LT = rows - 1 + "_" + (cols - 1);  //左上
                string LR = rows - 1 + "_" + (cols + 1);//右上
                string UL = rows + 1 + "_" + (cols - 1);//左下
                string UR = rows + 1 + "_" + (cols + 1);//右下
                //集合中存在则打开
                if (cells.ContainsKey(LC))
                {
                    //判断是否打开过，如未打开过，则一并打开
                    if (cells[LC].IsOpen == false)
                    {
                        cells[LC].Open();//上
                        uc = cells[LC];
                        ShowNearNoBombs(uc);//递归
                    }
                }
                if (cells.ContainsKey(CL))
                {
                    if (cells[CL].IsOpen == false)
                    {
                        cells[CL].Open();//左
                        uc = cells[CL];
                        ShowNearNoBombs(uc);
                    }
                }
                if (cells.ContainsKey(CR))
                {
                    if (cells[CR].IsOpen == false)
                    {
                        cells[CR].Open();//右
                        uc = cells[CR];
                        ShowNearNoBombs(uc);
                    }
                }
                if (cells.ContainsKey(UM))
                {
                    if (cells[UM].IsOpen == false)
                    {
                        cells[UM].Open();//下
                        uc = cells[UM];
                        ShowNearNoBombs(uc);
                    }
                }
                if (cells.ContainsKey(LT))
                {
                    if (cells[LT].IsOpen == false)
                    {
                        cells[LT].Open();//左上
                        uc = cells[LT];
                        ShowNearNoBombs(uc);
                    }
                }
                if (cells.ContainsKey(LR))
                {
                    if (cells[LR].IsOpen == false)
                    {
                        cells[LR].Open();//右上
                        uc = cells[LR];
                        ShowNearNoBombs(uc);
                    }
                }
                if (cells.ContainsKey(UL))
                {
                    if (cells[UL].IsOpen == false)
                    {
                        cells[UL].Open();//左下
                        uc = cells[UL];
                        ShowNearNoBombs(uc);
                    }
                }
                if (cells.ContainsKey(UR))
                {
                    if (cells[UR].IsOpen == false)
                    {
                        cells[UR].Open();//右下
                        uc = cells[UR];
                        ShowNearNoBombs(uc);
                    }
                }
            }
        }
        //打开非雷的格子时，改变其风格
        public string ShowNoBombs()
        {
            int nearBombs = (this.cell as NoBomb).NearBombs;
            this.lbl.Text = nearBombs.ToString();
            //改变字体风格
            this.lbl.Font = new Font(this.lbl.Name, 12, FontStyle.Regular | FontStyle.Bold);
            string url = string.Empty;
            switch (nearBombs)
            {
                case 0:
                    this.lbl.Text = string.Empty;
                    url =  "Music\\打开空格.wav";
                    break;
                case 1:
                    this.lbl.ForeColor = Color.Blue;
                    break;
                case 2:
                    this.lbl.ForeColor = Color.Green;
                    break;
                case 3:
                    this.lbl.ForeColor = Color.Red;
                    break;
                case 4:
                    this.lbl.ForeColor = Color.DarkBlue;
                    break;
                case 5:
                    this.lbl.ForeColor = Color.Firebrick;
                    break;
                case 6:
                    this.lbl.ForeColor = Color.SpringGreen;
                    break;
                case 7:
                    this.lbl.ForeColor = Color.DarkRed;
                    break;
                case 8:
                    this.lbl.ForeColor = Color.Crimson;
                    break;
            }
            return url;
        }
        public void BeginAgianGame()
        {
            this.btn.Image = null;
            this.btn.Text = string.Empty;
            if (this.IsOpen == true)
            {
                this.btn.Visible = true;
                this.IsOpen = false;
                this.cell.IsOpen = false;
            }
        }
        //public void ShowNearNoBombs()
        //{
        //    //如果打开的这个格子是空，则打开它周围的格子
        //    if (this.lbl.Text == string.Empty)
        //    {
        //        int cols = this.cell.Col;//y
        //        int rows = this.cell.Row; //x
        //        string LT = rows - 1 + "_" + (cols - 1);  //左上
        //        string LC = rows - 1 + "_" + (cols);//上
        //        string LR = rows - 1 + "_" + (cols + 1);//右上
        //        string CL = rows + "_" + (cols - 1);//左
        //        string CR = rows + "_" + (cols + 1);//右
        //        string UL = rows + 1 + "_" + (cols - 1);//左下
        //        string UM = rows + 1 + "_" + (cols);//下
        //        string UR = rows + 1 + "_" + (cols + 1);//右下

        //        //九种情况，分别显示

        //        //左上角
        //        if (cols == 0 && rows == 0)
        //        {
        //            cells[CR].Open();
        //            cells[UM].Open();
        //            cells[UR].Open();
        //        }
        //        //上边
        //        else if (rows == 0 && cols > 0 && cols < Cols - 1)
        //        {
        //            cells[CL].Open();
        //            cells[CR].Open();
        //            cells[UL].Open();
        //            cells[UM].Open();
        //            cells[UR].Open();
        //        }
        //        //右上角
        //        else if (rows == 0 && cols == Cols - 1)
        //        {
        //            cells[CL].Open();
        //            cells[UL].Open();
        //            cells[UM].Open();
        //        }
        //        //左边
        //        else if (cols == 0 && rows > 0 && rows < Rows - 1)
        //        {
        //            cells[LC].Open();//上
        //            cells[UM].Open();
        //            cells[LR].Open();//右上
        //            cells[UR].Open();//右下
        //            cells[CR].Open();//右
        //        }
        //        //左下角
        //        else if (cols == 0 && rows == Rows - 1)
        //        {
        //            cells[LC].Open();//上
        //            cells[CR].Open();//右
        //            cells[LR].Open();//右上
        //        }
        //        //下边
        //        else if (rows == Rows - 1 && cols > 0 && cols < Cols - 1)
        //        {
        //            cells[CL].Open();//左
        //            cells[CR].Open();//右
        //            cells[LT].Open(); //左上
        //            cells[LC].Open();//上
        //            cells[LR].Open();//右上
        //        }
        //        //右下角
        //        else if (rows == Rows - 1 && cols == Cols - 1)
        //        {
        //            cells[LC].Open();//上
        //            cells[LT].Open(); //左上
        //            cells[CL].Open();//左
        //        }
        //        //右边
        //        else if (cols == Cols - 1 && rows > 0 && rows < Rows - 1)
        //        {
        //            cells[LC].Open();//上
        //            cells[UM].Open();//下
        //            cells[UL].Open();//左下
        //            cells[LT].Open(); //左上
        //            cells[CL].Open();//左
        //        }
        //        //否则显示全部八个格子
        //        else
        //        {
        //            cells[LT].Open(); //左上
        //            cells[LC].Open();//上
        //            cells[LR].Open();//右上
        //            cells[CL].Open();//左
        //            cells[CR].Open();//右
        //            cells[UL].Open();//左下
        //            cells[UM].Open();//下
        //            cells[UR].Open();//右下
        //        }
        //    }
        //}
    }
}

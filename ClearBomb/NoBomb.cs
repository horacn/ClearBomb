using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClearBomb
{
	public class NoBomb:Cell
	{
		public int NearBombs { get; set; }

		public NoBomb(int row,int col)
		:base(row,col)
		{
		}

		public override bool Flag()
		{
			throw new NotImplementedException();
		}
        //获取周围雷数
        public override void GetNearBombs(Dictionary<string, Cell> cs)
		{
            int cols = this.Col;
            int rows = this.Row;
            int showBombs = 0;
            string LT = rows - 1 + "_" + (cols - 1);
            string LC = rows - 1 + "_" + (cols);
            string LR = rows - 1 + "_" + (cols + 1);
            string CL = rows + "_" + (cols - 1);
            string CR = rows + "_" + (cols + 1);
            string UL = rows + 1 + "_" + (cols - 1);
            string UM = rows + 1 + "_" + (cols);
            string UR = rows + 1 + "_" + (cols + 1);
            if (cs.ContainsKey(LT) && cs[LT] is Bomb)
            {
                showBombs++;
            }
            if (cs.ContainsKey(LC) && cs[LC] is Bomb)
            {
                showBombs++;
            }
            if (cs.ContainsKey(LR) && cs[LR] is Bomb)
            {
                showBombs++;
            }
            if (cs.ContainsKey(CL) && cs[CL] is Bomb)
            {
                showBombs++;
            }
            if (cs.ContainsKey(CR) && cs[CR] is Bomb)
            {
                showBombs++;
            }
            if (cs.ContainsKey(UL) && cs[UL] is Bomb)
            {
                showBombs++;
            }
            if (cs.ContainsKey(UM) && cs[UM] is Bomb)
            {
                showBombs++;
            }
            if (cs.ContainsKey(UR) && cs[UR] is Bomb)
            {
                showBombs++;
            }
            this.SetNearBombs(showBombs);
		}

		public override bool Open()
		{
			throw new NotImplementedException();
		}

        public void SetNearBombs(int bombs)
        {
            this.NearBombs = bombs;
        }
	}
}

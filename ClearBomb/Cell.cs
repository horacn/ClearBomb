using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClearBomb
{
   public class Cell
	{
		public int Col { get; set; }
		public int Row { get; set; }
		public string Key { get; set; }
		public bool IsFlag { get; set; }
		public bool IsOpen { get; set; }
		
		public Cell(int row,int col)
		{
			this.Col = col;
			this.Row = row;
			this.Key = row+"_"+col;
		}

		public virtual bool Flag()
		{
            return false;
		}

        public virtual void GetNearBombs(Dictionary<string, Cell> cs)
		{

		}

		public virtual bool Open()
		{
			return false;
		}
	}
}

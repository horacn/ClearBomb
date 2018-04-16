using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClearBomb
{
	
	public class Bomb:Cell
	{
		public Bomb(int row,int col):base(row,col)
		{
		
		}

		public override bool Flag()
		{
			throw new NotImplementedException();
		}

        public override void GetNearBombs(Dictionary<string, Cell> cs)
		{
			throw new NotImplementedException();
		}

		public override bool Open()
		{
			throw new NotImplementedException();
		}
	}
}

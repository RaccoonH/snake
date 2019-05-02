using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Windows.Controls;

namespace Snake
{
	public class Segment
	{
		public Image image;
		public double x, y;
	}

	public class WallSegment : Segment
	{
	}

	public class TailSegment : Segment
	{
	}

	public class Fruit : Segment
	{
		Random rand = new Random();
		Boolean placed = false;
		Boolean placed2 = true;
		public void replace(Game game)
		{
			while (placed != true)
			{
				placed2 = true;
				x = rand.Next(0, 14) * game.mx;
				y = rand.Next(0, 14) * game.my;
				if (game.countSegment >= 224)
				{
					break;
				}
				for (int i = 1; i < game.countSegment; i++)
				{
					if (Math.Abs(x - game.segments[i].x) < game.mx && Math.Abs(y - game.segments[i].y) < game.my)
					{
						placed2 = false;
					}
				}
				if (placed2)
				{
					placed = true;
				}
			}
			placed = false;
			placed2 = true;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Snake
{
	public class Player
	{
		public List<TailSegment> tail = new List<TailSegment>();
		public Image head = new Image();
		public int color = 0;
		public int speed = 5;
		public int tailCount = 0;
		public double xHead, yHead;
		public int way;
		public Boolean canMove;

		public void eat(Fruit fruit, Game game)
		{
			fruit.replace(game);
			tail.Add(new TailSegment());
			tail[tailCount].image = new Image();
			tail[tailCount].image.Stretch = Stretch.Fill;
			switch (this.color)
			{
				case 0:
					tail[tailCount].image.Source = BitmapFrame.Create(new Uri(@"pack://application:,,,/Resources/tail.jpg"));
					break;
				case 1:
					tail[tailCount].image.Source = BitmapFrame.Create(new Uri(@"pack://application:,,,/Resources/tail1.jpg"));
					break;
				case 2:
					tail[tailCount].image.Source = BitmapFrame.Create(new Uri(@"pack://application:,,,/Resources/tail2.jpg"));
					break;
				case 3:
					tail[tailCount].image.Source = BitmapFrame.Create(new Uri(@"pack://application:,,,/Resources/tail3.jpg"));
					break;
				case 4:
					tail[tailCount].image.Source = BitmapFrame.Create(new Uri(@"pack://application:,,,/Resources/tail4.jpg"));
					break;
			}
			
			tail[tailCount].image.Height = game.my * 0.9;
			tail[tailCount].image.Width = game.mx * 0.9;
			game.segments.Add(tail[tailCount]);
			game.countSegment++;
			tailCount++;
		}

		public void createSnakeTail(Game game)
		{
			tail.Add(new TailSegment());
			tail[tailCount].image = new Image();
			tail[tailCount].image.Stretch = Stretch.Fill;
			switch (this.color)
			{
				case 0:
					tail[tailCount].image.Source = BitmapFrame.Create(new Uri(@"pack://application:,,,/Resources/tail.jpg"));
					break;
				case 1:
					tail[tailCount].image.Source = BitmapFrame.Create(new Uri(@"pack://application:,,,/Resources/tail1.jpg"));
					break;
				case 2:
					tail[tailCount].image.Source = BitmapFrame.Create(new Uri(@"pack://application:,,,/Resources/tail2.jpg"));
					break;
				case 3:
					tail[tailCount].image.Source = BitmapFrame.Create(new Uri(@"pack://application:,,,/Resources/tail3.jpg"));
					break;
				case 4:
					tail[tailCount].image.Source = BitmapFrame.Create(new Uri(@"pack://application:,,,/Resources/tail4.jpg"));
					break;
			}

			tail[tailCount].image.Height = game.my * 0.9;
			tail[tailCount].image.Width = game.mx * 0.9;
			game.segments.Add(tail[tailCount]);
			game.countSegment++;
			tailCount++;
		}
	}
}

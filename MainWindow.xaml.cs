using System;
using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
//using System.Windows.Media;
using System.Windows.Media.Imaging;
//using System.Windows.Navigation;
//using System.Windows.Shapes;
using System.Windows.Threading;
//using System.Collections;

namespace Snake
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		DispatcherTimer timer;
		Game game;
		Boolean replay = false;
		Boolean isWallCreated = false;
		Boolean isHeadCreated = false;
		int interimScore;
		int interimCountOfLives;
		double size;
		Map map;
		Player player;
		public MainWindow()
		{
			InitializeComponent();
			game = new Game();
			map = new Map();
			player = new Player();
			/*size
			size = sizeSlider.Value;
			MainW.Width = 450 + ((450 / 100) * size);
			MainW.Height = 450 + ((450 / 100) * size);
			//m
			//scale = 15;
			game.mx = MainW.Width / 15;
			game.my = MainW.Height / 15;
			head.Height = game.my;
			head.Width = game.mx;
			//game.start();
			//initialize game.player
			//game.player = new game.player();
			//game.player.way = 1;
			//game.player.speed = 7;
			Canvas.SetTop(head, game.my * 7);
			Canvas.SetLeft(head, game.mx * 7);
			game.player.xHead = Canvas.GetLeft(head);
			game.player.yHead = Canvas.GetTop(head);
			//fruit
			/*f = new Fruit();
			f.image = new Image();
			f.x = 100;
			f.y = 100;
			f.image.Source = BitmapFrame.Create(new Uri(@"C:\Users\tudun\source\repos\Snake\Snake\Resources\fruit.jpg"));
			f.image.Height = game.my* 0.9;
			f.image.Width = game.mx* 0.9;
			canvas.Children.Add(game.f.image);
			Canvas.SetTop(game.f.image, game.f.x);
			Canvas.SetLeft(game.f.image, game.f.y);
			game.segments.Add(game.f);
			game.countSegment++;
			//map
			game.map = new Map();
			game.map.defaultMap(game.mx, game.my);*/
		}


		private void Walking(object sender, object e)
		{
			for (int j = 0; j < game.countSegment; j++)
			{
				if (Math.Abs(game.player.yHead - game.segments[j].y) < game.my*0.9 && Math.Abs(game.player.xHead - game.segments[j].x) < game.mx * 0.9)
				{
					if (j==0 && game.countSegment<225)
					{
						game.player.eat(game.f, game);
						canvas.Children.Add(game.player.tail[game.player.tailCount - 1].image);
						game.scoreInt++;
						game.score.Text = "Your score: " + game.scoreInt;
						break;
					}
					else
					{
						game.countOfLives--;
						canvas.Children.Clear();
						if (game.countOfLives == 0)
						{
							canvas.Visibility = Visibility.Hidden;
							GameOverMenu.Visibility = Visibility.Visible;
							game.Over();
							scoreEnd.Text = "Your Score: " + game.scoreInt;
							recordEnd.Text = "Record: " + game.recordInt;
							if (game.scoreInt > game.recordInt)
							{
								commentGameOver.Text = "Congratulations";
							}
							else
							{
								commentGameOver.Text = "Do you wanna play again?";
							}
							
							replay = false;
							timer.Stop();
						}
						else
						{
							canvas.Visibility = Visibility.Hidden;
							countOfLives.Text = "You have " + game.countOfLives + " more lives";
							oneDeathMenuScore.Text = "Your score: " + game.scoreInt;
							replay = true;
							interimScore = game.scoreInt;
							interimCountOfLives = game.countOfLives;
							oneDeathMenu.Visibility = Visibility.Visible;
							timer.Stop();
						}
						//game.player.eat(f, game);
						//canvas.Children.Add(game.player.tail[game.player.tailCount - 1].image);
					}
				}
			}
			if (game.player.tailCount > 0)
			{
				for (int i = game.player.tailCount-1; i > 0; i--)
				{
					game.player.tail[i].x = game.player.tail[i - 1].x;
					game.player.tail[i].y = game.player.tail[i - 1].y;
					Canvas.SetTop(game.player.tail[i].image, game.player.tail[i].y);
					Canvas.SetLeft(game.player.tail[i].image, game.player.tail[i].x);
				}
				game.player.tail[0].x = game.player.xHead;
				game.player.tail[0].y = game.player.yHead;
				Canvas.SetTop(game.player.tail[0].image, game.player.tail[0].y);
				Canvas.SetLeft(game.player.tail[0].image, game.player.tail[0].x);
			}
			
			switch (game.player.way)
			{
				case 0:
					if(game.player.yHead <= 0)
					{
						game.player.yHead = 14 * game.my;
					}
					else
					{
						game.player.yHead -= game.my;
					}
					break;
				case 1:
					if (game.player.xHead >= 14 * game.mx - 15)
					{
						game.player.xHead = 0;
					}
					else
					{
						game.player.xHead += game.mx;
					}
					break;
				case 2:
					if(game.player.yHead >= game.my * 14)
					{
						game.player.yHead = 0;
					}
					else
					{
						game.player.yHead += game.my;
					}
					
					break;
				case 3:
					if (game.player.xHead <= 0)
					{
						game.player.xHead = 14 * game.mx;
					}
					else
					{
						game.player.xHead -= game.mx;
					}
					break;
			}
			game.player.canMove = true;

			Canvas.SetTop(game.player.head, game.player.yHead);
			Canvas.SetLeft(game.player.head, game.player.xHead);

			Canvas.SetTop(game.f.image, game.f.y);
			Canvas.SetLeft(game.f.image, game.f.x);

		}

		private void playButton_click(object sender, EventArgs e)
		{
			game = new Game();
			game.start();

			if (replay)
			{
				game.countOfLives = interimCountOfLives;
				game.scoreInt = interimScore;
			}
			else
			{
				interimCountOfLives = game.countOfLives;
				game.scoreInt = 0;
			}

			oneDeathMenu.Visibility = Visibility.Hidden;
			GameOverMenu.Visibility = Visibility.Hidden;

			switch (colorSnake.SelectedIndex)
			{
				case 0:
					game.player.color = 0;
					break;
				case 1:
					game.player.color = 1;
					break;
				case 2:
					game.player.color = 2;
					break;
				case 3:
					game.player.color = 3;
					break;
				case 4:
					game.player.color = 4;
					break;
			}

			switch (colorMap.SelectedIndex)
			{
				case 0:
					//game.map.color = 0;
					canvas.Background = new SolidColorBrush(Colors.Gray);
					break;
				case 1:
					//game.map.color = 1;
					canvas.Background = new SolidColorBrush(Colors.DarkCyan);
					break;
				case 2:
					//game.map.color = 2;
					canvas.Background = new SolidColorBrush(Colors.DarkRed);
					break;
				case 3:
					//game.map.color = 3;
					canvas.Background = new SolidColorBrush(Colors.DarkGreen);
					break;
				case 4:
					//game.map.color = 4;
					canvas.Background = new SolidColorBrush(Colors.DarkMagenta);
					break;
			}

			size = sizeSlider.Value;
			MainW.Width = 465 + ((450 / 100) * size);
			MainW.Height = 490 + ((450 / 100) * size);

			game.mx = (MainW.Width-15) / 15 ;
			game.my = (MainW.Height-40) / 15;

			//map
			switch (levelMap.SelectedIndex)
			{
				case 0:
					game.map.defaultMap(game.mx, game.my);
					break;
				case 1:
					game.map.withoutWalls();
					break;
				case 2:
					//game.map = map;
					game.map.customMap(game.mx, game.my, map, player);
					break;
			}

			game.player.speed = (int)speedSlider.Value;

			game.player.head.Source = BitmapFrame.Create(new Uri(@"pack://application:,,,/Resources/head.jpg"));
			game.player.head.Height = game.my * 0.9;
			game.player.head.Width = game.mx * 0.9;

			canvas.Children.Add(game.player.head);
			Canvas.SetTop(game.player.head, game.my * game.map.playerRespawnY);
			Canvas.SetLeft(game.player.head, game.mx * game.map.playerRespawnX);
			game.player.xHead = Canvas.GetLeft(game.player.head);
			game.player.yHead = Canvas.GetTop(game.player.head);
			Canvas.SetZIndex(game.player.head, 4);

			game.record = new TextBlock();
			game.record.Text = "Record: " + game.recordInt;
			game.record.FontSize = 15;
			canvas.Children.Add(game.record);
			Canvas.SetRight(game.record, 0);
			Canvas.SetZIndex(game.record, 4);

			for(int i = 0; i < game.countOfLives; i++)
			{
				game.live[i] = new Image();
				game.live[i].Source = BitmapFrame.Create(new Uri(@"pack://application:,,,/Resources/life1.png"));
				game.live[i].Width = game.mx * 0.8;
				game.live[i].Height = game.my * 0.8;
				canvas.Children.Add(game.live[i]);
				Canvas.SetLeft(game.live[i], i*game.mx);
				Canvas.SetZIndex(game.live[i], 5);
			}

			game.score = new TextBlock();
			game.score.Text = "Your Score: " + game.scoreInt;
			game.score.FontSize = 15;
			canvas.Children.Add(game.score);
			Canvas.SetRight(game.score, 0);
			Canvas.SetTop(game.score, 20);
			Canvas.SetZIndex(game.score, 4);

			/*f = new Fruit();
			f.image = new Image();
			f.x = 100;
			f.y = 100;
			f.image.Source = BitmapFrame.Create(new Uri(@"C:\Users\tudun\source\repos\Snake\Snake\Resources\fruit.jpg"));
			f.image.Height = game.my* 0.9;
			f.image.Width = game.mx* 0.9;*/

			game.segments.Add(game.f);
			game.countSegment++;

			for (int i = 0; i < Convert.ToInt32(lenghtSlider.Value); i++)
			{
				game.player.createSnakeTail(game);
				canvas.Children.Add(game.player.tail[i].image);
			}

			/*for (int i = 0; i < 15; i++)
			{
				map.walls[i].image.Height = game.my * 0.9;
				map.walls[i].image.Width = game.mx * 0.9;
				map.walls[i].x = i * game.mx;
				map.walls[i].y = 0;
				Canvas.SetTop(map.walls[i].image, map.walls[i].y);
				Canvas.SetLeft(map.walls[i].image, map.walls[i].x);
			}*/

			for (int i = 0; i < game.map.countWalls; i++)
			{
				game.map.walls[i].image.Height = game.my * 0.9;
				game.map.walls[i].image.Width = game.mx * 0.9;
				game.map.walls[i].image.Source = BitmapFrame.Create(new Uri(@"pack://application:,,,/Resources/wall.jpg"));
				canvas.Children.Add(game.map.walls[i].image);
				Canvas.SetTop(game.map.walls[i].image, game.map.walls[i].y);
				Canvas.SetLeft(game.map.walls[i].image, game.map.walls[i].x);
				Canvas.SetZIndex(game.map.walls[i].image, 3);
				game.segments.Add(game.map.walls[i]);
				game.countSegment++;
			}

			game.f.image.Source = BitmapFrame.Create(new Uri(@"pack://application:,,,/Resources/friut.png"));
			game.f.image.Height = game.my * 0.9;
			game.f.image.Width = game.mx * 0.9;
			game.f.replace(game);

			Canvas.SetTop(game.f.image, game.f.y);
			Canvas.SetLeft(game.f.image, game.f.x);
			canvas.Children.Add(game.f.image);

			canvas.Visibility = Visibility.Visible;
			MainMenu.Visibility = Visibility.Hidden;

			timer = new DispatcherTimer(DispatcherPriority.Render);
			timer.Interval = new TimeSpan(0, 0, 0, 0, Convert.ToInt32(1000/speedSlider.Value));
			timer.Tick += Walking;
			timer.Start();
		}

		private void Tail_KeyDown(object sender, KeyEventArgs e)
		{
			if (canvas.Visibility == Visibility.Visible)
			{
				switch (e.Key)
				{
					case Key.D:
						if (game.player.way != 3 && game.player.canMove)
						{
							game.player.way = 1;
						}
						game.player.canMove = false;
						break;
					case Key.W:
						if (game.player.way != 2 && game.player.canMove)
						{
							game.player.way = 0;
						}
						game.player.canMove = false;
						break;
					case Key.S:
						if (game.player.way != 0 && game.player.canMove)
						{
							game.player.way = 2;
						}
						game.player.canMove = false;
						break;
					case Key.A:
						if (game.player.way != 1 && game.player.canMove)
						{
							game.player.way = 3;
						}
						game.player.canMove = false;
						break;
					case Key.Escape:
						timer.Stop();
						PauseMenu.Visibility = Visibility.Visible;
						break;
				}
			}
			if (createLevelCanvas.Visibility == Visibility.Visible && (isWallCreated||isHeadCreated))
			{
				switch (e.Key)
				{
					case Key.D:
						if ((isWallCreated && (map.walls[map.countWalls-1].x*game.mx + game.mx)<MainW.Width-20))
						{
							map.walls[map.countWalls-1].x += 1;
							Canvas.SetTop(map.walls[map.countWalls-1].image, map.walls[map.countWalls-1].y * game.my);
							Canvas.SetLeft(map.walls[map.countWalls-1].image, map.walls[map.countWalls-1].x * game.mx);
						}
						if (isHeadCreated && ((player.xHead * game.mx + game.mx) < MainW.Width - 15))
						{
							player.xHead += 1;
							Canvas.SetTop(player.head, player.yHead * game.my);
							Canvas.SetLeft(player.head, player.xHead * game.mx);
						}
						break;
					case Key.W:
						if (isWallCreated && ((map.walls[map.countWalls - 1].y * game.my - game.my) >= 0))
						{
							map.walls[map.countWalls - 1].y -= 1;
							Canvas.SetTop(map.walls[map.countWalls - 1].image, map.walls[map.countWalls - 1].y * game.my);
							Canvas.SetLeft(map.walls[map.countWalls - 1].image, map.walls[map.countWalls - 1].x * game.mx);
						}
						if (isHeadCreated && ((player.xHead * game.mx + game.mx) >= 0))
						{
							player.yHead -= 1;
							Canvas.SetTop(player.head, player.yHead * game.my);
							Canvas.SetLeft(player.head, player.xHead * game.mx);
						}
						break;
					case Key.S:
						if (isWallCreated && ((map.walls[map.countWalls - 1].y * game.my + game.my) < MainW.Height-45))
						{
							map.walls[map.countWalls - 1].y += 1;
							Canvas.SetTop(map.walls[map.countWalls - 1].image, map.walls[map.countWalls - 1].y * game.my);
							Canvas.SetLeft(map.walls[map.countWalls - 1].image, map.walls[map.countWalls - 1].x * game.mx);
						}
						if (isHeadCreated && ((player.xHead * game.mx + game.mx) <= MainW.Height))
						{
							player.yHead += 1;
							Canvas.SetTop(player.head, player.yHead * game.my);
							Canvas.SetLeft(player.head, player.xHead * game.mx);
						}
						break;
					case Key.A:
						if (isWallCreated&&((map.walls[map.countWalls - 1].x * game.mx - game.mx) >= 0))
						{
							map.walls[map.countWalls - 1].x -= 1;
							Canvas.SetTop(map.walls[map.countWalls - 1].image, map.walls[map.countWalls - 1].y * game.my);
							Canvas.SetLeft(map.walls[map.countWalls - 1].image, map.walls[map.countWalls - 1].x * game.mx);
						}
						if (isHeadCreated && ((player.xHead * game.mx + game.mx) >= 0))
						{
							player.xHead -= 1;
							Canvas.SetTop(player.head, player.yHead * game.my);
							Canvas.SetLeft(player.head, player.xHead * game.mx);
						}
						break;
				}
			}
		}

		private void SettingsButton_Click(object sender, RoutedEventArgs e)
		{
			Settings.Visibility = Visibility.Visible;
			MainMenu.Visibility = Visibility.Hidden;
		}

		private void ExitButtonSettings_Click(object sender, RoutedEventArgs e)
		{
			size = sizeSlider.Value;
			MainW.Width = 465 + ((450 / 100) * size);
			MainW.Height = 490 + ((450 / 100) * size);
			game.mx = MainW.Width / 15;
			game.my = MainW.Height / 15;

			Settings.Visibility = Visibility.Hidden;
			MainMenu.Visibility = Visibility.Visible;
		}

		private void ExitPauseButton_Click(object sender, RoutedEventArgs e)
		{
			canvas.Visibility = Visibility.Hidden;
			PauseMenu.Visibility = Visibility.Hidden;
			MainMenu.Visibility = Visibility.Visible;
			canvas.Children.Clear();
			//game.countOfLives = 3;
			replay = false;
			game.Over();
		}

		private void ExitButton_Click(object sender, EventArgs e)
		{
			Application.Current.Shutdown();
		}

		private void ContinueButton_Click(object sender, RoutedEventArgs e)
		{
			canvas.Visibility = Visibility.Visible;
			PauseMenu.Visibility = Visibility.Hidden;
			timer.Start();
		}

		private void ExitGameOver_Click(object sender, RoutedEventArgs e)
		{
			GameOverMenu.Visibility = Visibility.Hidden;
			MainMenu.Visibility = Visibility.Visible;
			oneDeathMenu.Visibility = Visibility.Hidden;
			replay = false;
		}

		private void CreateWall_Click(object sender, RoutedEventArgs e)
		{
			//map = new Map();
			isHeadCreated = false;
			isWallCreated = true;
			MainW.Width = 465 + ((450 / 100) * size);
			MainW.Height = 490 + ((450 / 100) * size);
			game.mx = (MainW.Width - 15) / 15;
			game.my = (MainW.Height - 40) / 15;

			map.walls.Add(new WallSegment());
			map.walls[map.countWalls].image = new Image();

			map.walls[map.countWalls].image.Height = game.my * 0.9;
			map.walls[map.countWalls].image.Width = game.mx * 0.9;
			map.walls[map.countWalls].image.Source = BitmapFrame.Create(new Uri(@"pack://application:,,,/Resources/wall.jpg"));
			createLevelCanvas.Children.Add(map.walls[map.countWalls].image);
			map.walls[map.countWalls].x = 1;
			map.walls[map.countWalls].y = 1;
			Canvas.SetTop(map.walls[map.countWalls].image, map.walls[map.countWalls].y*game.my);
			Canvas.SetLeft(map.walls[map.countWalls].image, map.walls[map.countWalls].x * game.mx);
			map.countWalls++;
		}

		private void ApplyCreateLevel_Click(object sender, RoutedEventArgs e)
		{
			createLevelCanvas.Visibility = Visibility.Hidden;
			Settings.Visibility = Visibility.Visible;
		}

		private void RespawnCreateLevel_Click(object sender, RoutedEventArgs e)
		{
			isHeadCreated = true;
			isWallCreated = false;
			size = sizeSlider.Value;
			MainW.Width = 465 + ((450 / 100) * size);
			MainW.Height = 490 + ((450 / 100) * size);
			game.mx = (MainW.Width-15) / 15;
			game.my = (MainW.Height-40) / 15;

			if(player.head != null)
			{
				createLevelCanvas.Children.Remove(player.head);
			}
			player.head = new Image();

			player.head.Height = game.my * 0.9;
			player.head.Width = game.mx * 0.9;
			player.head.Source = BitmapFrame.Create(new Uri(@"pack://application:,,,/Resources/head.jpg"));
			createLevelCanvas.Children.Add(player.head);

			player.xHead = 1;
			player.yHead = 1;
			Canvas.SetTop(player.head, player.yHead * game.my);
			Canvas.SetLeft(player.head, player.xHead * game.mx);
		}

		private void DelWall_Click(object sender, RoutedEventArgs e)
		{
			if (map.countWalls != 0)
			{
				createLevelCanvas.Children.Remove(map.walls[map.countWalls - 1].image);
				map.walls.Remove(map.walls[map.countWalls - 1]);
				map.countWalls--;
			}
		}

		private void ButtonCreateLevel_Click(object sender, RoutedEventArgs e)
		{
			map = new Map();
			createLevelCanvas.Children.RemoveRange(1,createLevelCanvas.Children.Count);
			Settings.Visibility = Visibility.Hidden;
			createLevelCanvas.Visibility = Visibility.Visible;
		}
	}
}

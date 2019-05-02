using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Snake
{
	public class Game
	{
		public Map map;
		public Player player;
		public Fruit f;
		public int scale;
		public int recordInt;
		public int scoreInt;
		public List<Segment> segments = new List<Segment>();
		public int countSegment = 0;
		public int countOfLives = 3;
		public double mx, my;
		public TextBlock score;
		public TextBlock record;
		public Boolean win = false;
		//FileStream file = new FileStream(@"C:\Users\tudun\source\repos\Snake\Snake\Resources\record.txt", FileMode.Open, FileAccess.Read);
		public Image[] live;
		public void start()
		{
			//StreamReader reader = new StreamReader(file);
			recordInt = Properties.Settings.Default.record;
			//reader.Close();
			live = new Image[countOfLives];
			player = new Player();
			map = new Map();
			f = new Fruit();
			scale = 15;
			player.way = 1;
			player.speed = 7;
			//f
			f.image = new Image();
			//f.x = 100;
			//f.y = 100;
		}
		
		public void Over()
		{
			if (scoreInt > recordInt)
			{
				Properties.Settings.Default.record = scoreInt;
				//FileStream file = new FileStream(@"C:\Users\tudun\source\repos\Snake\Snake\Resources\record.txt", FileMode.Truncate, FileAccess.Write);
				//StreamWriter writer = new StreamWriter(file);
				//writer.Write(scoreInt);
				//writer.Close();
			}
		}
	}

	public class Map
	{
		public List<WallSegment> walls = new List<WallSegment>();
		public int countWalls=0;
		public double playerRespawnX;
		public double playerRespawnY;
		public void defaultMap(double mx, double my)
		{
			playerRespawnX = 7;
			playerRespawnY = 7;
			for (int i = 0; i < 15; i++)
			{
				walls.Add(new WallSegment());
				walls[i].image = new Image();
				walls[i].x = i * mx;
				walls[i].y = 0;
			}
			for (int i = 15; i < 30; i++)
			{
				walls.Add(new WallSegment());
				walls[i].image = new Image();
				walls[i].x = 0;
				walls[i].y = my * (i - 15);
			}
			for (int i = 30; i < 45; i++)
			{
				walls.Add(new WallSegment());
				walls[i].image = new Image();
				walls[i].x = 14 * mx;
				walls[i].y = my * (i - 30);
			}
			for (int i = 45; i < 60; i++)
			{
				walls.Add(new WallSegment());
				walls[i].image = new Image();
				walls[i].x = my * (i - 45);
				walls[i].y = 14 * my;
			}
			countWalls = 60;
		}
		public void customMap(double mx, double my, Map map1, Player player)
		{
			playerRespawnX = player.xHead;
			playerRespawnY = player.yHead;
			for(int i = 0; i < map1.countWalls; i++)
			{
				walls.Add(new WallSegment());
				walls[i].image = new Image();
				walls[i].x = map1.walls[i].x*mx;
				walls[i].y = map1.walls[i].y*my;
			}
			countWalls = map1.countWalls;
		}

		public void withoutWalls()
		{
			playerRespawnX = 7;
			playerRespawnY = 7;
		}
	}
}

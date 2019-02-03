using System.Collections.Generic;

namespace Snake
{
	public enum Direction
	{
		Left,
		Right,
		Up,
		Down
	}

	public struct Point
	{
		public Point(int x, int y)
		{
			X = x;
			Y = y;
		}

		public int X { get; set; }
		public int Y { get; set; }
	}

	public class SnakeLogic
	{
		private readonly List<Point> _snake = new List<Point>();

		public bool Compute(int[][] snakeGrid, Direction snakeDirection)
		{
			//THESE ARE THE VALUES THE UI USES TO PAINT THE SNAKE, EMPTY SPACE OR AN APPLY
			//DON'T CHANGE
			const int emptyValue = 0;
			const int snakeValue = 1;
			const int appleValue = 2;
			///////////////////////////////////////////////////////////////////////////////



			//lets put an apple on the grid for no reason :)
			snakeGrid[5][5] = appleValue;


			//erase current snake from the grid
			for (int i = 0; i < _snake.Count; i++)
			{
				int x = _snake[i].X;
				int y = _snake[i].Y;


				//TAKE NOTE HOW y AND x are used in snakeGrid. I.e, [y][x] and not [x][y]
				snakeGrid[y][x] = emptyValue;
			}

			//move snake according to direction
			if (snakeDirection == Direction.Right)
			{
				for (int i = 0; i < _snake.Count; i++)
				{
					//x and y are set to the next square to the right
					int x = _snake[i].X + 1;
					int y = _snake[i].Y;
					
					//if the snake reaches the horizontal corner of the grid
					//wrap around it
					if (x >= snakeGrid.Length)
					{
						x = 0;
					}
					//......................................................
					
					_snake[i] = new Point()
					{
						X = x,
						Y = y
					};
				}
			}

			//paint snake back onto grid
			for (int i = 0; i < _snake.Count; i++)
			{
				int x = _snake[i].X;
				int y = _snake[i].Y;
				snakeGrid[y][x] = snakeValue;
			}


			//return true if you want the game to continue
			//return false if the user died
			return true;
		}


		
		public int GetScore()
		{
			//the method is called to get the score
			return 5;
		}

		public void Reset()
		{
			//this method will be called
			//when a new game starts
			_snake.Clear();
			_snake.Add(new Point(2, 0));
			_snake.Add(new Point(1, 0));
			_snake.Add(new Point(0, 0));
		}
	}
}
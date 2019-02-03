namespace Snake
{
	public enum Direction
	{
		Left,
		Right,
		Up,
		Down
	}

	public class SnakeLogic
	{

		public bool Compute(int[][] snakeGrid, Direction snakeDirection)
		{
			//THESE ARE THE VALUES THE UI USES TO PAINT THE SNAKE, EMPTY SPACE OR AN APPLE
			//DON'T CHANGE
			const int emptyValue = 0;
			const int snakeValue = 1;
			const int appleValue = 2;
			///////////////////////////////////////////////////////////////////////////////

			//return true if you want the game to continue
			//return false if the user died
			return true;
		}
		
		public int GetScore()
		{
			//the method is called to get the score
			return 0;
		}

		public void Reset()
		{
			//this method will be called
			//when a new game starts
		}
	}
}

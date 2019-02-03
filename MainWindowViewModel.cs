using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Snake
{
	public class MainWindowViewModel : INotifyPropertyChanged
	{
		private const int GridSize = 20;
		private int _score;
		private ObservableCollection<ObservableCollection<int>> _snakeGrid;
		private bool _isPlaying;
		private DispatcherTimer _timer;
		private bool _isPlayAgainCommandEnabled;
		private SnakeLogic _snakeLogic = new SnakeLogic();

		public MainWindowViewModel()
		{
			InitializeSnakeGrid(GridSize);
			_timer = new DispatcherTimer();
			_timer.Tick += TimerOnTick;
			_timer.Interval = TimeSpan.FromSeconds(0.33);
			_timer.Start();
			IsPlaying = false;

			EventManager.RegisterClassHandler(typeof(Window), UIElement.PreviewKeyDownEvent, new KeyEventHandler(OnKeyDown));
		}

		private void OnKeyDown(object sender, KeyEventArgs e)
		{
			switch (e.Key)
			{
				case Key.Left:
					if (Direction != Direction.Right)
						Direction = Direction.Left;
					break;
				case Key.Up:
					if (Direction != Direction.Down)
						Direction = Direction.Up;
					break;
				case Key.Right:
					if (Direction != Direction.Left)
						Direction = Direction.Right;
					break;
				case Key.Down:
					if (Direction != Direction.Up)
						Direction = Direction.Down;
					break;
			}
		}

		private void TimerOnTick(object sender, EventArgs eventArgs)
		{
			if (!IsPlaying) return;
			int[][] snakeGrid = new int[GridSize][];

			for (int i = 0; i < GridSize; i++)
			{
				snakeGrid[i] = new int[GridSize];
				for (int j = 0; j < GridSize; j++)
				{
					snakeGrid[i][j] = SnakeGrid[i][j];
				}
			}
			bool result = _snakeLogic.Compute(snakeGrid, Direction);

			for (int i = 0; i < GridSize; i++)
			{
				for (int j = 0; j < GridSize; j++)
				{
					SnakeGrid[j][i] = snakeGrid[j][i];
				}
			}

			Score = _snakeLogic.GetScore();
			IsPlaying = result;
		}

		private void InitializeSnakeGrid(int gridSize)
		{
			SnakeGrid = new ObservableCollection<ObservableCollection<int>>();
			for (var i = 0; i < gridSize; i++)
			{
				SnakeGrid.Add(new ObservableCollection<int>());
				var data = new int[GridSize];
				SnakeGrid[i] = new ObservableCollection<int>(data);
			}
		}

		protected bool Set<T>(ref T property, T value, [CallerMemberName] string propertyName = null)
		{
			if (Equals(property, value)) return false;
			property = value;
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
			return true;
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public bool IsPlaying
		{
			get { return _isPlaying; }
			set
			{
				Set(ref _isPlaying, value);
				IsPlayAgainCommandEnabled = !value;
			}
		}

		public int Score
		{
			get { return _score; }
			set { Set(ref _score, value); }
		}

		public Direction Direction { get; set; }

		public ObservableCollection<ObservableCollection<int>> SnakeGrid
		{
			get { return _snakeGrid; }
			set { Set(ref _snakeGrid, value); }
		}

		public ICommand PlayAgainCommand => new RelayCommand(() =>
		{
			Score = 0;
			InitializeSnakeGrid(GridSize);
			IsPlaying = true;
			Direction = Direction.Right;
			_snakeLogic.Reset();

		});

		public bool IsPlayAgainCommandEnabled
		{
			get { return _isPlayAgainCommandEnabled; }
			set { Set(ref _isPlayAgainCommandEnabled, value); }
		}
	}
}
using SummaSQLGame.Helpers;
using SummaSQLGame.Models;
using SummaSQLGame.ViewModels.Puzzles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace SummaSQLGame.ViewModels
{
    public class ChallengeViewModel : ObservableObject
    {
        #region fields
        private DispatcherTimer _timer;
        private TimeSpan _elapsedTime = TimeSpan.Zero;
        private TimeSpan _totalTime = TimeSpan.FromMinutes(5);
        private TimeSpan _remainingTime;
        private IPuzzle? _activePuzzle = null;
        private int _score = 0;
        private int _basePuzzleScore = 500;
        private Visibility _startButtonVisibility = Visibility.Visible;
        private SaveState _saveState;
        private MainViewModel _mainViewModel;
        private Random _rand;
        private List<Type> _puzzleTypes;
        #endregion

        #region properties
        public IPuzzle? ActivePuzzle { get { return _activePuzzle; } set { _activePuzzle = value; OnPropertyChanged(); } }
        public int Score { get { return _score; } set { _score = value; OnPropertyChanged(); } }
        public TimeSpan RemainingTime { get { return _remainingTime; } set { _remainingTime = value; OnPropertyChanged(); } }
        public Visibility StartButtonVisibility { get { return _startButtonVisibility; } set { _startButtonVisibility= value; OnPropertyChanged(); } }

        public event EventHandler<EventArgs> UpdateProgressEvent;
        public event EventHandler<EventArgs> SubmitScoreEvent;
        #endregion

        #region commands
        public ICommand StartCommand { get; }
        #endregion

        #region constructors
        public ChallengeViewModel(MainViewModel mainViewModel)
        {
            _rand = new Random();
            _puzzleTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).Where(
                type => typeof(IPuzzle).IsAssignableFrom(type) 
                && !type.IsInterface 
                && !type.IsAbstract 
                && type != typeof(ChallengeExplanationViewModel) 
                //&& type != typeof(AdventurerViewModel) 
                //&& type != typeof(BattleshipViewModel) 
                //&& type != typeof(ButtonViewModel) 
                && type != typeof(MazeViewModel) 
                && type != typeof(StudentViewModel)).ToList();
            _mainViewModel = mainViewModel;
            _remainingTime = _totalTime;
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += _timer_Tick;
            StartCommand = new RelayCommand(ExecuteStartChallenge);
            ActivePuzzle = new ChallengeExplanationViewModel();
        }

        #endregion

        #region methods
        private void ExecuteStartChallenge(object? obj)
        {
            _elapsedTime = TimeSpan.Zero;
            _timer.Start();
            StartButtonVisibility = Visibility.Collapsed;
            /* Set active viewmodel */
            SetNewPuzzle();
        }

        private void SetNewPuzzle()
        {
            Type randomPuzzleType = _puzzleTypes[_rand.Next(_puzzleTypes.Count)];
            IPuzzle puzzleViewModel = (IPuzzle)Activator.CreateInstance(randomPuzzleType);
            ActivePuzzle = puzzleViewModel;
            _mainViewModel.SaveState.UpdateEncountered(ActivePuzzle.PuzzleType);
            ActivePuzzle.PuzzleCompleted += HandlePuzzleCompletion;
            
        }

        private void _timer_Tick(object? sender, EventArgs e)
        {
            _elapsedTime += _timer.Interval;
            RemainingTime = _totalTime - _elapsedTime;
            if (_remainingTime.Minutes <= 0 && _remainingTime.Seconds <= 0)
            {
                ResetGame();
            }
        }

        private void ResetGame()
        {
            _timer.Stop();
            // score to leaderboard Submit the score
            Score = 0;
            _elapsedTime = TimeSpan.FromSeconds(0);
            RemainingTime = _totalTime;
            StartButtonVisibility = Visibility.Visible;
            ActivePuzzle = new ChallengeExplanationViewModel();
        }

        private void HandlePuzzleCompletion(object? sender, EventArgs e)
        {
            var puzzle = sender as IPuzzle;
            var score = _basePuzzleScore - (50 * puzzle.Attempts);
            Score += score > 50 ? score : 50;
            _mainViewModel.SaveState.UpdateCompleted(ActivePuzzle.PuzzleType);
            SetNewPuzzle();
        }
        #endregion
    }
}

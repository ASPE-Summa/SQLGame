using Microsoft.EntityFrameworkCore;
using SummaSQLGame.Databases;
using SummaSQLGame.Helpers;
using SummaSQLGame.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace SummaSQLGame.ViewModels.Puzzles
{
    public class ButtonViewModel : BasePuzzleViewModel
    {
        #region fields
        private ObservableCollection<Models.Button> _buttons = new();
        #endregion

        #region properties
        public override event EventHandler<EventArgs> PuzzleCompleted;
        public ObservableCollection<Models.Button> Buttons { get { return _buttons;  } set { _buttons = value; OnPropertyChanged(); } }
        #endregion

        #region constructor
        public ButtonViewModel() : base()
        {
            _puzzleType = Helpers.Puzzles.BUTTON;
            LoadButtons();
            ButtonClickedCommand = new RelayCommand(ExecuteButtonClicked);
        }
        #endregion

        #region commands
        public ICommand ButtonClickedCommand { get; }
        #endregion

        #region methods
        /**
         * Draws 9 buttons from the database of which 3 are armed and 6 are not.
         */
        public void LoadButtons()
        {
            using (AppDbContext context = new AppDbContext())
            {
                List<Models.Button> dbButtons =
                [
                    .. context.Buttons.Where(p => p.ButtonSafety.IsSafe == true)
                        .Include(p => p.ButtonSafety)
                        .AsEnumerable()
                        .OrderBy(p => Guid.NewGuid())
                        .Take(3)
,
                    .. context.Buttons.Where(p => p.ButtonSafety.IsSafe == false)
                        .Include(p => p.ButtonSafety)
                        .AsEnumerable()
                        .OrderBy(p => Guid.NewGuid())
                        .Take(6)
,
                ];

                Shuffler.Shuffle(dbButtons);
                dbButtons.ForEach(Buttons.Add);
            }
        }

        public void ExecuteButtonClicked(object? obj)
        {
            Models.Button buttonModel = obj as Models.Button;

            buttonModel.IsPressed = !buttonModel.IsPressed;
            CheckVictory();
        }

        private void CheckVictory()
        {
            int buttonsCorrect = Buttons.Count(b => b.IsPressed && b.ButtonSafety.IsSafe);
            int buttonsWrong = Buttons.Count(b => b.IsPressed && !b.ButtonSafety.IsSafe);

            if (buttonsCorrect == 3)
            {
                PuzzleCompleted.Invoke(this, EventArgs.Empty);
            }
            else if(buttonsWrong > 0)
            {
                foreach (Models.Button b in Buttons)
                {
                    b.IsPressed = false;
                }
                Attempts++;
            }
        }
        #endregion
    }
}
using Org.BouncyCastle.Tsp;
using SummaSQLGame.Databases;
using SummaSQLGame.Helpers;
using SummaSQLGame.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SummaSQLGame.ViewModels.Puzzles
{
    public class BattleshipViewModel : BasePuzzleViewModel
    {
        #region fields
        private BattleShip _solution;
        private string _lipsum = "lorem ipsum dolor sit amet consectetur adipiscing elit maecenas magna nisi ultricies non erat non accumsan placerat nunc vestibulum mauris mi vulputate vel lectus at aliquam mattis diam pellentesque ornare sapien a tempus tristique cras convallis nulla nibh ut condimentum justo porttitor vitae integer vitae metus ac sem finibus gravida nullam consequat finibus velit ut dignissim massa blandit vitae sed ut massa vel ligula rutrum ornare interdum et malesuada fames ac ante ipsum primis in faucibus phasellus nec eros sed turpis varius gravida vestibulum venenatis eleifend metus ac fermentum augue scelerisque et maecenas mi enim bibendum fringilla vulputate sit amet volutpat et elit donec posuere leo in justo porta iaculis morbi purus augue mollis sit amet lobortis nec rutrum eu nisl duis ornare commodo neque sed hendrerit nibh ultricies ac aenean pulvinar dapibus massa quis malesuada aliquam suscipit lacus sed ornare dignissim vestibulum eu ligula arcu pellentesque nibh mi feugiat id placerat et gravida vel diam vestibulum fermentum aliquam lectus a bibendum velit pretium et nunc consectetur mauris sed aliquet finibus nibh ipsum placerat ligula eget posuere enim turpis eget leo duis interdum aliquam dignissim duis dapibus leo at vestibulum ultrices nisi libero mollis erat a laoreet velit erat ut nisl pellentesque nisl nunc egestas sit amet porta sed condimentum et felis etiam ut eros sapien fusce elementum ex pharetra sem cursus vulputate morbi commodo metus sem quis varius purus vulputate eu mauris at ante quis felis pulvinar auctor proin id urna nec orci auctor ornare nam scelerisque risus convallis feugiat dapibus odio leo varius ex a pulvinar orci justo sed massa nunc pretium consequat fermentum praesent pharetra ante et ante euismod egestas class aptent taciti sociosqu ad litora torquent per conubia nostra per inceptos himenaeos nunc at sodales risus ut pharetra est vitae rutrum faucibus orci nibh sollicitudin ligula a sollicitudin libero leo a lacus donec viverra tortor et convallis aliquet nisi est eleifend nunc eu pulvinar arcu est nec ante aliquam lectus nulla suscipit consectetur pharetra non volutpat nec ligula maecenas maximus tortor quis sodales mattis donec elementum elementum turpis dignissim lacinia fusce accumsan eros et eros malesuada sit amet hendrerit lorem cursus suspendisse libero dui vulputate vulputate erat eget placerat varius neque curabitur quis erat lectus pellentesque et elit ullamcorper metus aliquam tincidunt morbi pulvinar nisl justo vitae ornare libero viverra sit amet vestibulum cursus purus non consectetur pellentesque nisl est rhoncus libero sodales fringilla quam augue fermentum ante fusce auctor elementum purus sit amet aliquam pellentesque quis interdum nisi";
        private string? _firstWord;
        private string? _secondWord;
        private string? _thirdWord;
        #endregion

        #region properties
        public string? FirstWord { get { return _firstWord; } set { _firstWord = value; OnPropertyChanged(); } }
        public string? SecondWord { get { return _secondWord; } set { _secondWord = value; OnPropertyChanged(); } }
        public string? ThirdWord { get { return _thirdWord; } set { _thirdWord = value; OnPropertyChanged(); } }
        public override event EventHandler<EventArgs>? PuzzleCompleted;
        #endregion

        #region commands
        public ICommand ButtonClickedCommand { get; }
        #endregion

        #region constructors
        public BattleshipViewModel() : base()
        {
            _puzzleType = Helpers.Puzzles.BATTLESHIP;
            ButtonClickedCommand = new RelayCommand(ExecuteButtonClicked);
            SelectRandomPuzzle();
        }
        #endregion

        #region methods
        private void SelectRandomPuzzle()
        {
            using (AppDbContext context = new AppDbContext())
            {
                _solution = context.BattleShips.AsEnumerable().OrderBy(p => Guid.NewGuid()).First();
            }
            ExtractWords();
        }

        private void ExtractWords()
        {
            string[] words = _solution.Description.Split(' ');

            words = words.Where(x => !_lipsum.Contains(x)).ToArray();
            FirstWord = words[0];
            SecondWord = words[1];
            ThirdWord = words[2];
        }
        private void ExecuteButtonClicked(object? sender)
        {
            Attempts++;
            var button = sender as System.Windows.Controls.Button;
            if(button?.Content.ToString() == _solution.Coordinates)
            {
                PuzzleCompleted.Invoke(this, EventArgs.Empty);
            }
        }
        #endregion
    }
}

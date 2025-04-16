using SummaSQLGame.ViewModels.Puzzles;
using System.IO.Abstractions.TestingHelpers;

using System.Windows;

namespace SummaSQLGame.ViewModels.Tests
{
    [TestClass]
    public class ChallengeViewModelTests
    {
        /** Temporarily Commented because we cannot continue this test without dependency injection **/
        // [TestMethod]
        // public void StartCommand_StartsChallenge_SetsViewModel()
        // {
        //     // Arrange
        //     var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData> {
        //         { MainViewModel.JSONPATH, new MockFileData("")}
        //     });
        //     var vm = new ChallengeViewModel(new MainViewModel(fileSystem));
        //
        //     // Act
        //     vm.StartCommand.Execute(null);
        //
        //     // Assert
        //     Assert.IsNotNull(vm.ActivePuzzle);
        //     Assert.AreEqual(Visibility.Collapsed,vm.StartButtonVisibility);
        //     Assert.IsInstanceOfType(vm.ActivePuzzle, typeof(BasePuzzleViewModel));
        // }
    }
}
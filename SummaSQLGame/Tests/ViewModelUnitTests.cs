using System.Data;
using Moq;
using SummaSQLGame.Models;
using SummaSQLGame.Services;
using SummaSQLGame.ViewModels;
using Xunit;

namespace SummaSQLGame.Tests
{
    public class MainViewModelTests
    {
        [Fact]
        public void LoadSaveState_LoadsStateFromService()
        {
            // Arrange
            var saveState = new SaveState("test");
            var saveStateServiceMock = new Mock<ISaveStateService>();
            saveStateServiceMock.Setup(s => s.Load()).Returns(saveState);
            var serviceProviderMock = new Mock<IServiceProvider>();
            var vm = new MainViewModel(saveStateServiceMock.Object, serviceProviderMock.Object);

            // Act
            var result = vm.SaveState;

            // Assert
            Assert.Equal(saveState, result);
        }
    }

    public class ChallengeViewModelTests
    {
        [Fact]
        public void SetNewPuzzle_UpdatesActivePuzzle()
        {
            // Arrange
            var mainContextMock = new Mock<IMainViewModelContext>();
            var vm = new ChallengeViewModel(mainContextMock.Object);
            // Act
            vm.StartCommand.Execute(null);
            // Assert
            Assert.NotNull(vm.ActivePuzzle);
        }
    }
}

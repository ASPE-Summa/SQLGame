using System.Data;
using TestableIO.System.IO.Abstractions;
using TestableIO.System.IO.Abstractions.Wrappers;
using SummaSQLGame.Models;
using SummaSQLGame.Services;
using SummaSQLGame.ViewModels;
using Xunit;
using System.IO.Abstractions.TestingHelpers;

namespace SummaSQLGame.Tests
{
    public class MainViewModelTests
    {
        [Fact]
        public void LoadSaveState_LoadsStateFromService()
        {
            // Arrange
            var fileSystem = new MockFileSystem();
            var saveState = new SaveState("test");
            var jsonPath = @"Assets/SaveState.json";
            var basePath = "/tmp/testapp/";
            var fullPath = Path.Combine(basePath, jsonPath);
            fileSystem.AddDirectory(Path.Combine(basePath, "Assets"));
            fileSystem.AddFile(fullPath, new MockFileData(Newtonsoft.Json.JsonConvert.SerializeObject(saveState)));
            var saveStateService = new SaveStateService(new FileSystemWrapper(fileSystem), basePath, jsonPath);
            var serviceProvider = new TestServiceProvider();
            var vm = new MainViewModel(saveStateService, serviceProvider);

            // Act
            var result = vm.SaveState;

            // Assert
            Assert.Equal(saveState.Name, result.Name);
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

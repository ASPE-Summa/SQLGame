using Moq;
using SummaSQLGame.Models;
using SummaSQLGame.Services;
using System.Data;

namespace SummaSQLGame.ViewModels.Tests
{
    [TestClass]
    public class MainViewModelTests
    {
        [TestMethod]
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
            Assert.AreEqual(saveState, result);
        }
    }

    [TestClass]
    public class ChallengeViewModelTests
    {
        [TestMethod]
        public void SetNewPuzzle_UpdatesActivePuzzle()
        {
            // Arrange
            var mainContextMock = new Mock<IMainViewModelContext>();
            var vm = new ChallengeViewModel(mainContextMock.Object);
            // Act
            vm.StartCommand.Execute(null);
            // Assert
            Assert.IsNotNull(vm.ActivePuzzle);
        }
    }

    [TestClass]
    public class SelectViewModelTests
    {
        [TestMethod]
        public void ExecuteAndValidateQuery_UsesQueryService()
        {
            // Arrange
            var queryServiceMock = new Mock<IQueryService>();
            var expectedTable = new DataTable();
            expectedTable.Columns.Add("naam");
            expectedTable.Rows.Add("Fikkie");
            queryServiceMock.Setup(q => q.ExecuteQuery(It.IsAny<string>())).Returns(expectedTable);
            var vm = new SummaSQLGame.ViewModels.Select.SelectViewModel(queryServiceMock.Object);
            vm.QueryText = "select naam from honden;";
            // Act
            vm.QueryCommand.Execute(null);
            // Assert
            Assert.IsTrue(vm.QueryResult.Rows.Count == 1 && vm.QueryResult.Rows[0][0].ToString() == "Fikkie", "QueryService was not used correctly or QueryResult is incorrect.");
        }
    }
}
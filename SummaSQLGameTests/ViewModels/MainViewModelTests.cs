using Moq;
using SummaSQLGame.Models;
using SummaSQLGame.Services;
using SummaSQLGame.ViewModels;
using System.Data;

namespace SummaSQLGame.ViewModels.Tests;

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

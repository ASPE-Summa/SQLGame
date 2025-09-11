using Moq;
using SummaSQLGame.Services;
using SummaSQLGame.ViewModels.Select;
using System.Data;

namespace SummaSQLGame.ViewModels.Tests;

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
        var vm = new SelectViewModel(queryServiceMock.Object);
        vm.QueryText = "select naam from honden;";
        // Act
        vm.QueryCommand.Execute(null);
        // Assert
        Assert.IsTrue(vm.QueryResult.Rows.Count == 1 && vm.QueryResult.Rows[0][0].ToString() == "Fikkie", "QueryService was not used correctly or QueryResult is incorrect.");
    }
}

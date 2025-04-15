using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SummaSQLGame.Databases;
using SummaSQLGame.ViewModels;
using SummaSQLGame.ViewModels.Select;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummaSQLGame.ViewModels.Tests
{
    [TestClass()]
    public class BaseExplanationViewModelTests
    {
        [TestMethod()]
        public void NextCommand_CanPass_GoesToNext()
        {
            // Arrange
            var viewModel = new SelectViewModel();
            var currentIndex = viewModel.ExplanationIndex;
            var expectedIndex = viewModel.ExplanationIndex + 1;
            viewModel.CurrentExplanation.CanPass = true;

            // Act
            viewModel.NextExplanationCommand.Execute(null);

            // Assert
            Assert.AreEqual(expectedIndex, viewModel.ExplanationIndex);
            Assert.AreEqual(viewModel.CurrentExplanation, viewModel.Explanations[expectedIndex]);
        }

        [TestMethod()]
        public void NextCommand_CannotPass_DoesNotGoToNext()
        {
            // Arrange
            var viewModel = new SelectViewModel();
            var currentIndex = viewModel.ExplanationIndex;
            var expectedIndex = currentIndex;
            viewModel.CurrentExplanation.CanPass = false;

            // Act
            viewModel.NextExplanationCommand.Execute(null);

            // Assert
            Assert.AreEqual(expectedIndex, viewModel.ExplanationIndex);
            Assert.AreEqual(viewModel.CurrentExplanation, viewModel.Explanations[expectedIndex]);
        }

        [TestMethod()]
        public void NextCommand_NoMoreExplanations_DoesNotGoToNext()
        {
            // Arrange
            var viewModel = new SelectViewModel();
            viewModel.ExplanationIndex = viewModel.Explanations.Count - 1;
            var currentIndex = viewModel.ExplanationIndex;
            var expectedIndex = currentIndex;
            var expectedExplanation = viewModel.CurrentExplanation;
            // Act
            viewModel.NextExplanationCommand.Execute(null);
            // Assert
            Assert.AreEqual(expectedIndex, viewModel.ExplanationIndex);
            Assert.AreEqual(expectedExplanation, viewModel.CurrentExplanation);
        }

        [TestMethod()]
        public void PreviousCommand_GoesToPrevious()
        {
            // Arrange
            var viewModel = new SelectViewModel();
            viewModel.CurrentExplanation = viewModel.Explanations[1];
            viewModel.ExplanationIndex = 1;
            var expectedIndex = 0;

            // Act
            viewModel.PreviousExplanationCommand.Execute(null);

            // Assert
            Assert.AreEqual(expectedIndex, viewModel.ExplanationIndex);
            Assert.AreEqual(viewModel.CurrentExplanation, viewModel.Explanations[0]);
        }

        [TestMethod()]
        public void PreviousCommand_AtFirstIndex_DoesNotGoToPrevious()
        {
            // Arrange
            var viewModel = new SelectViewModel();
            viewModel.CurrentExplanation = viewModel.Explanations[0];
            viewModel.ExplanationIndex = 0;
            var currentIndex = viewModel.ExplanationIndex;
            var expectedIndex = currentIndex;

            // Act
            viewModel.PreviousExplanationCommand.Execute(null);

            // Assert
            Assert.AreEqual(expectedIndex, viewModel.ExplanationIndex);
        }

        [TestMethod()]
        public void QueryCommand_WithCorrectQuery_SetsCanPass()
        {
            // Arrange
            var viewModel = new SelectViewModel();
            viewModel.CurrentExplanation = viewModel.Explanations[3];
            viewModel.QueryText = "select * from honden;";
            var expectedCanPass = true;

            var mockContext = new Mock<IAppDbContext>();
            var mockDataTable = new System.Data.DataTable();
            mockDataTable.Columns.Add("id", typeof(int));
            mockDataTable.Columns.Add("naam", typeof(string));
            mockDataTable.Columns.Add("ras", typeof(string));
            mockDataTable.Rows.Add(1, "Fido", "Labrador");
            mockContext.Setup(m => m.ExecuteQuery(It.IsAny<string>())).Returns(mockDataTable);
            viewModel.Context = mockContext.Object;

            // Act
            viewModel.QueryCommand.Execute(null);

            // Assert
            Assert.AreEqual(expectedCanPass, viewModel.CurrentExplanation.CanPass);
        }

        [TestMethod()]
        public void QueryCommand_WithWrongColumns_DoesNotSetCanPass()
        {
            var viewModel = new SelectViewModel();
            viewModel.CurrentExplanation = viewModel.Explanations[3];
            viewModel.QueryText = "select id from honden;";
            var expectedCanPass = false;

            var mockContext = new Mock<IAppDbContext>();

            var answerMockData = new System.Data.DataTable();
            answerMockData.Columns.Add("id", typeof(int));
            answerMockData.Columns.Add("naam", typeof(string));
            answerMockData.Columns.Add("ras", typeof(string));
            answerMockData.Rows.Add(1, "Fido", "Labrador");

            var queryMockData = new System.Data.DataTable();
            queryMockData.Columns.Add("id", typeof(int));
            queryMockData.Rows.Add(1);

            mockContext.SetupSequence(m => m.ExecuteQuery(It.IsAny<string>()))
                .Returns(answerMockData)
                .Returns(queryMockData);
            viewModel.Context = mockContext.Object;

            // Act
            viewModel.QueryCommand.Execute(null);

            // Assert
            Assert.AreEqual(expectedCanPass, viewModel.CurrentExplanation.CanPass);
        }

        [TestMethod()]
        public void QueryCommand_WithWrongRows_DoesNotSetCanPass()
        {
            var viewModel = new SelectViewModel();
            viewModel.CurrentExplanation = viewModel.Explanations[3];
            viewModel.QueryText = "select * from honden LIMIT 1;";
            var expectedCanPass = false;

            var mockContext = new Mock<IAppDbContext>();

            var answerMockData = new System.Data.DataTable();
            answerMockData.Columns.Add("id", typeof(int));
            answerMockData.Columns.Add("naam", typeof(string));
            answerMockData.Columns.Add("ras", typeof(string));
            answerMockData.Rows.Add(1, "Fido", "Labrador");
            answerMockData.Rows.Add(2, "Buddy", "Beagle");

            var queryMockData = new System.Data.DataTable();
            queryMockData.Columns.Add("id", typeof(int));
            queryMockData.Columns.Add("naam", typeof(string));
            queryMockData.Columns.Add("ras", typeof(string));
            queryMockData.Rows.Add(1, "Fido", "Labrador");

            mockContext.SetupSequence(m => m.ExecuteQuery(It.IsAny<string>()))
                .Returns(answerMockData)
                .Returns(queryMockData);
            viewModel.Context = mockContext.Object;

            // Act
            viewModel.QueryCommand.Execute(null);

            // Assert
            Assert.AreEqual(expectedCanPass, viewModel.CurrentExplanation.CanPass);
        }
    }
}
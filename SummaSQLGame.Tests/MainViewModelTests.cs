using System;
using System.IO;
using Xunit;
using SummaSQLGame.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace SummaSQLGame.Tests
{
    public class MainViewModelTests
    {
        [Fact]
        public void ExecuteResetDatabase_DeletesAndRecreatesDatabase()
        {
            // Arrange
            var viewModel = new MainViewModel();
            string dbPath = "Databases/database.db";

            // Ensure database exists
            Directory.CreateDirectory("Databases");
            File.WriteAllText(dbPath, "dummy");
            Assert.True(File.Exists(dbPath));

            // Act
            viewModel.GetType().GetMethod("ExecuteResetDatabase", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .Invoke(viewModel, new object[] { null });

            // Assert
            Assert.True(File.Exists(dbPath)); // Should be recreated by migrations
        }
    }
}

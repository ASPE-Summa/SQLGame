using SummaSQLGame.Models;

namespace SummaSQLGame.Services
{
    public interface IMainViewModelContext
    {
        SaveState SaveState { get; }
        void UpdateCompletion(string subject, int completionPercentage);
        void UpdateEncountered(string puzzleType);
        void UpdateCompleted(string puzzleType);
    }
}

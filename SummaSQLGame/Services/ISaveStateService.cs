using SummaSQLGame.Models;

namespace SummaSQLGame.Services
{
    public interface ISaveStateService
    {
        SaveState Load();
        void Save(SaveState state);
    }
}

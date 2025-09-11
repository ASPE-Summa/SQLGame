using System.Data;

namespace SummaSQLGame.Services
{
    public interface IQueryService
    {
        DataTable ExecuteQuery(string queryText);
    }
}

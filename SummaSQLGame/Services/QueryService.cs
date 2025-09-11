using System.Data;
using SummaSQLGame.Databases;

namespace SummaSQLGame.Services
{
    public class QueryService : IQueryService
    {
        public DataTable ExecuteQuery(string queryText)
        {
            using (var dbContext = new AppDbContext())
            {
                return dbContext.ExecuteQuery(queryText);
            }
        }
    }
}

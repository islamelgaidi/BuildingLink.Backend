using Driver.API.Domain.Interfaces;

namespace Driver.API.Infrastructure
{
    public class DbMigration : IDbMigration
    {
        private readonly IDbContext _dbContext;        
        public DbMigration(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Execute()
        {            
            //
            string createDriverTable = @"
                CREATE TABLE IF NOT EXISTS Driver (
                    Id TEXT PRIMARY KEY,
                    FirstName TEXT,
                    LastName TEXT,
                    PhoneNumber TEXT,
                    Email TEXT
                )";
            return _dbContext.Execute(createDriverTable) > -1;             
        }
    }
}

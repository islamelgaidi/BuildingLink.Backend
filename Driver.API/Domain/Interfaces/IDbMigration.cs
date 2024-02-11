namespace Driver.API.Domain.Interfaces
{
    public interface IDbMigration
    {
        bool Execute();
    }
}

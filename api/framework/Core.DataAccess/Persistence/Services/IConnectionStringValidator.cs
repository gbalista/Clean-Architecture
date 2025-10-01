namespace Core.DataAccess.Persistence.Services;
public interface IConnectionStringValidator
{
    bool TryValidate(string connectionString, string? dbProvider = null);
}

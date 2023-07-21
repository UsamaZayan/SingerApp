using System.Threading.Tasks;

namespace SingerApp.Data;

public interface ISingerAppDbSchemaMigrator
{
    Task MigrateAsync();
}

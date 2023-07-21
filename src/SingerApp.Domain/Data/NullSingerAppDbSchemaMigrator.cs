using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace SingerApp.Data;

/* This is used if database provider does't define
 * ISingerAppDbSchemaMigrator implementation.
 */
public class NullSingerAppDbSchemaMigrator : ISingerAppDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}

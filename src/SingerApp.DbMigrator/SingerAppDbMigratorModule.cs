using SingerApp.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace SingerApp.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(SingerAppEntityFrameworkCoreModule),
    typeof(SingerAppApplicationContractsModule)
    )]
public class SingerAppDbMigratorModule : AbpModule
{
}

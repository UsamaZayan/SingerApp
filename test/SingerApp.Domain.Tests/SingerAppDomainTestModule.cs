using SingerApp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace SingerApp;

[DependsOn(
    typeof(SingerAppEntityFrameworkCoreTestModule)
    )]
public class SingerAppDomainTestModule : AbpModule
{

}

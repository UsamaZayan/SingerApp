using Volo.Abp.Modularity;

namespace SingerApp;

[DependsOn(
    typeof(SingerAppApplicationModule),
    typeof(SingerAppDomainTestModule)
    )]
public class SingerAppApplicationTestModule : AbpModule
{

}

using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace SingerApp.Web;

[Dependency(ReplaceServices = true)]
public class SingerAppBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "SingerApp";
}

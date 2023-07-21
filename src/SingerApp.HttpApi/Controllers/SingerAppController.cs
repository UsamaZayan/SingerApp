using SingerApp.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace SingerApp.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class SingerAppController : AbpControllerBase
{
    protected SingerAppController()
    {
        LocalizationResource = typeof(SingerAppResource);
    }
}

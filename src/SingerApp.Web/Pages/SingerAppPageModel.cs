using SingerApp.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace SingerApp.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class SingerAppPageModel : AbpPageModel
{
    protected SingerAppPageModel()
    {
        LocalizationResourceType = typeof(SingerAppResource);
    }
}

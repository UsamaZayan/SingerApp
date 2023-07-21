using System;
using System.Collections.Generic;
using System.Text;
using SingerApp.Localization;
using Volo.Abp.Application.Services;

namespace SingerApp;

/* Inherit your application services from this class.
 */
public abstract class SingerAppAppService : ApplicationService
{
    protected SingerAppAppService()
    {
        LocalizationResource = typeof(SingerAppResource);
    }
}

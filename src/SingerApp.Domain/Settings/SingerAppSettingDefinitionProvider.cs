using Volo.Abp.Settings;

namespace SingerApp.Settings;

public class SingerAppSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(SingerAppSettings.MySetting1));
    }
}

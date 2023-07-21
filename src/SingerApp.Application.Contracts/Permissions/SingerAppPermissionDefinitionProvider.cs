using SingerApp.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace SingerApp.Permissions;

public class SingerAppPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(SingerAppPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(SingerAppPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<SingerAppResource>(name);
    }
}

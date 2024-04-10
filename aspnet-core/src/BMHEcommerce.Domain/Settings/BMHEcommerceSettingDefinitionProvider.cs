using Volo.Abp.Settings;

namespace BMHEcommerce.Settings;

public class BMHEcommerceSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(BMHEcommerceSettings.MySetting1));
    }
}

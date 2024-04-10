using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace BMHEcommerce;

[Dependency(ReplaceServices = true)]
public class BMHEcommerceBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "BMHEcommerce";
}

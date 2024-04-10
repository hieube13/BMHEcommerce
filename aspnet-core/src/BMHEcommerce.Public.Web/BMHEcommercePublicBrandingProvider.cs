using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace BMHEcommerce.Public.Web;

[Dependency(ReplaceServices = true)]
public class BMHEcommercePublicBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "Public";
}

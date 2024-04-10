using BMHEcommerce.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace BMHEcommerce.Public.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class BMHEcommercePublicController : AbpControllerBase
{
    protected BMHEcommercePublicController()
    {
        LocalizationResource = typeof(BMHEcommerceResource);
    }
}

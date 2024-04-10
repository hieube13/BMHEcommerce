using BMHEcommerce.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace BMHEcommerce.Admin.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class BMHEcommerceController : AbpControllerBase
{
    protected BMHEcommerceController()
    {
        LocalizationResource = typeof(BMHEcommerceResource);
    }
}

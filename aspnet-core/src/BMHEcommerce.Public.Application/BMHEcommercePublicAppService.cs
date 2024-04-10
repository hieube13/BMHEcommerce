using System;
using System.Collections.Generic;
using System.Text;
using BMHEcommerce.Localization;
using Volo.Abp.Application.Services;

namespace BMHEcommerce.Public;

/* Inherit your application services from this class.
 */
public abstract class BMHEcommercePublicAppService : ApplicationService
{
    protected BMHEcommercePublicAppService()
    {
        LocalizationResource = typeof(BMHEcommerceResource);
    }
}

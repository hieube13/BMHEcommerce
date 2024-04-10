using System;
using System.Collections.Generic;
using System.Text;
using BMHEcommerce.Localization;
using Volo.Abp.Application.Services;

namespace BMHEcommerce.Admin;

/* Inherit your application services from this class.
 */
public abstract class BMHEcommerceAdminAppService : ApplicationService
{
    protected BMHEcommerceAdminAppService()
    {
        LocalizationResource = typeof(BMHEcommerceResource);
    }
}

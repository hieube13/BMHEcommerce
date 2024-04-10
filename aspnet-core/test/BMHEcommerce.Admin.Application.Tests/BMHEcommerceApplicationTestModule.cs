using Volo.Abp.Modularity;
using BMHEcommerce;

namespace BMHEcommerce.Admin;

[DependsOn(
    typeof(BMHEcommerceAdminApplicationModule),
    typeof(BMHEcommerceDomainTestModule)
    )]
public class BMHEcommerceApplicationTestModule : AbpModule
{

}

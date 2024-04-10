using Volo.Abp.Modularity;

namespace BMHEcommerce.Public;

[DependsOn(
    typeof(BMHEcommercePublicApplicationModule),
    typeof(BMHEcommerceDomainTestModule)
    )]
public class BMHEcommercePublicApplicationTestModule : AbpModule
{

}

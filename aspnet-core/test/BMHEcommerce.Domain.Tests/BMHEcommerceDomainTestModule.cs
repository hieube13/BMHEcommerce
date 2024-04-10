using BMHEcommerce.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace BMHEcommerce;

[DependsOn(
    typeof(BMHEcommerceEntityFrameworkCoreTestModule)
    )]
public class BMHEcommerceDomainTestModule : AbpModule
{

}

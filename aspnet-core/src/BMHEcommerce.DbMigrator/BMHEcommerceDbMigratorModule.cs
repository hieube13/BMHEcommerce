using BMHEcommerce.Admin;
using BMHEcommerce.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace BMHEcommerce.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(BMHEcommerceEntityFrameworkCoreModule),
    typeof(BMHEcommerceApplicationContractsModule)
    )]
public class BMHEcommerceDbMigratorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
    }
}

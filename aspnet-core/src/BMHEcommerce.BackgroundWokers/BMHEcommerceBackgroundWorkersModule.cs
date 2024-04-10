using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BMHEcommerce.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp.Caching;
using Volo.Abp.Caching.StackExchangeRedis;
using Volo.Abp.Modularity;
using BMHEcommerce.BackgroundWokers;
using BMHEcommerce.BackgroundWokers.MailCampaigns;
using Microsoft.AspNetCore.DataProtection;

namespace BMHEcommerce.BackgroundWorkers
{
    [DependsOn(
      typeof(AbpAutofacModule),
      typeof(AbpBackgroundWorkersModule),
      typeof(BMHEcommerceEntityFrameworkCoreModule),
      typeof(AbpCachingStackExchangeRedisModule)
  )]
    public class BMHEcommerceBackgroundWorkersModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            var hostEnvironment = context.Services.GetSingletonInstance<IHostEnvironment>();

            context.Services.AddHostedService<BMHEcommerceBackgroundWorkersHostedService>();

            Configure<AbpDistributedCacheOptions>(options =>
            {
                options.KeyPrefix = "BMH:";
            });
            var dataProtectionBuilder = context.Services.AddDataProtection().SetApplicationName("BMH");
            if (!hostEnvironment.IsDevelopment())
            {
                var redis = ConnectionMultiplexer.Connect(configuration["Redis:Configuration"]);
                dataProtectionBuilder.PersistKeysToStackExchangeRedis(redis, "BMH-Protection-Keys");
            }

        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            context.AddBackgroundWorkerAsync<EmailMarketingWorker>();
        }
    }
}
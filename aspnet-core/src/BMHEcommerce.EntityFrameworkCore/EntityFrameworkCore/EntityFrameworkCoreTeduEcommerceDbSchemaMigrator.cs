using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BMHEcommerce.Data;
using Volo.Abp.DependencyInjection;

namespace BMHEcommerce.EntityFrameworkCore;

public class EntityFrameworkCoreBMHEcommerceDbSchemaMigrator
    : IBMHEcommerceDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreBMHEcommerceDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the BMHEcommerceDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<BMHEcommerceDbContext>()
            .Database
            .MigrateAsync();
    }
}

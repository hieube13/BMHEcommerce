using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace BMHEcommerce.Data;

/* This is used if database provider does't define
 * IBMHEcommerceDbSchemaMigrator implementation.
 */
public class NullBMHEcommerceDbSchemaMigrator : IBMHEcommerceDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}

using System.Threading.Tasks;

namespace BMHEcommerce.Data;

public interface IBMHEcommerceDbSchemaMigrator
{
    Task MigrateAsync();
}

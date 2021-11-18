using System.Threading.Tasks;

namespace Product_Task.Data
{
    public interface IProduct_TaskDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}

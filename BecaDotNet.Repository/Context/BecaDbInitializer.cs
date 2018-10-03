using System.Data.Entity;

namespace BecaDotNet.Repository.Context
{
    public class BecaDbInitializer : DropCreateDatabaseIfModelChanges<BecaContext>
    {
    }
}

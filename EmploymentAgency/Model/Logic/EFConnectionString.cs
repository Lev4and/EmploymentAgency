using System.Configuration;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;

namespace EmploymentAgency.Model.Logic
{
    public class EFConnectionString
    {
        public static string GetConnectionString(string databaseName, string serverAddress)
        {
            SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder();

            sqlConnectionStringBuilder.InitialCatalog = databaseName;
            sqlConnectionStringBuilder.DataSource = $@"{serverAddress}";
            sqlConnectionStringBuilder.IntegratedSecurity = true;

            sqlConnectionStringBuilder.MultipleActiveResultSets = true;
            sqlConnectionStringBuilder.TrustServerCertificate = false;

            EntityConnectionStringBuilder entityConnectionStringBuilder = new EntityConnectionStringBuilder();

            entityConnectionStringBuilder.Provider = "System.Data.SqlClient";
            entityConnectionStringBuilder.ProviderConnectionString = sqlConnectionStringBuilder.ConnectionString;

            entityConnectionStringBuilder.Metadata = "res://*";

            return entityConnectionStringBuilder.ConnectionString;
        }
    }
}

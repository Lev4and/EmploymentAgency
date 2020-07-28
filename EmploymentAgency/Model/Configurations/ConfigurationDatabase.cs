using EmploymentAgency.Model.Logic;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace EmploymentAgency.Model.Configurations
{
    [Serializable]
    public class ConfigurationDatabase
    {
        public int CommandTimeout { get; set; }

        public string ServerAddress { get; set; }

        public string DatabaseName { get; set; }

        public string ConnectionString { get; private set; }

        public ConfigurationDatabase()
        {
            CommandTimeout = 180;
            ServerAddress = @"DESKTOP-9CDGA5B\SQLEXPRESS";
            DatabaseName = "Employment agency";
            ConnectionString = EFConnectionString.GetConnectionString(DatabaseName, ServerAddress);
        }

        public ConfigurationDatabase(int commandTimeout, string serverAddress, string databaseName)
        {
            CommandTimeout = commandTimeout;
            ServerAddress = serverAddress;
            DatabaseName = databaseName;
            ConnectionString = EFConnectionString.GetConnectionString(DatabaseName, ServerAddress);
        }

        public void Save()
        {
            BinaryFormatter formatter = new BinaryFormatter();

            using(FileStream stream = new FileStream("ConfigurationDatabase.bin", FileMode.OpenOrCreate))
            {
                formatter.Serialize(stream, this);
            }
        }

        public static ConfigurationDatabase GetConfiguration()
        {
            if(File.Exists("ConfigurationDatabase.bin"))
            {
                BinaryFormatter formatter = new BinaryFormatter();

                using(FileStream stream = new FileStream("ConfigurationDatabase.bin", FileMode.Open))
                {
                    return formatter.Deserialize(stream) as ConfigurationDatabase;
                }
            }

            throw new FileNotFoundException("Файл конфигурации не был найден.");
        }
    }
}

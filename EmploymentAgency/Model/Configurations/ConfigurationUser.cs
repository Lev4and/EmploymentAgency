using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace EmploymentAgency.Model.Configurations
{
    [Serializable]
    public class ConfigurationUser
    {
        public int IdUser { get; set; }

        public string RoleName { get; set; }

        public ConfigurationUser(int idUser, string roleName)
        {
            IdUser = idUser;
            RoleName = roleName;
        }

        public void Save()
        {
            BinaryFormatter formatter = new BinaryFormatter();

            using(FileStream stream = new FileStream("ConfigurationUser.bin", FileMode.OpenOrCreate))
            {
                formatter.Serialize(stream, this);
            }
        }

        public static ConfigurationUser GetConfiguration()
        {
            if(File.Exists("ConfigurationUser.bin"))
            {
                BinaryFormatter formatter = new BinaryFormatter();

                using (FileStream stream = new FileStream("ConfigurationUser.bin", FileMode.Open))
                {
                    return formatter.Deserialize(stream) as ConfigurationUser;
                }
            }

            throw new FileNotFoundException("Файл конфигурации не был найден.");
        }
    }
}

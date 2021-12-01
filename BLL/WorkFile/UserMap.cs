using CsvHelper.Configuration;
using DAL.Models;

namespace BLL.WorkFile
{
    internal class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Map(obj => obj.Date).TypeConverterOption.Format("dd.MM.yyyy").Index(0);
            Map(obj => obj.Name).Index(1);
            Map(obj => obj.LastName).Index(2);
            Map(obj => obj.Patronymic).Index(3);
            Map(obj => obj.City).Index(4);
            Map(obj => obj.Country).Index(5);
        }
    }
}

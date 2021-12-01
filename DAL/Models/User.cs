using System;

namespace DAL.Models
{
    /// <summary>
    /// Class describing user's model
    /// </summary>
    public class User
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; } // the same as Surname

        public string Patronymic { get; set; }

        public string City { get; set; }

        public string Country { get; set; }
    }
}

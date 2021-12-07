using BLL.ModelsDTO;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IFilter
    {
        /// <summary>
        /// Method for retrieving data from database by multiple fields
        /// </summary>
        /// <param name="date">date</param>
        /// <param name="name">name</param>
        /// <param name="lastName">last name</param>
        /// <param name="patronymic">patronymic</param>
        /// <param name="city">city</param>
        /// <param name="country">country</param>
        /// <returns>filtered list of users</returns>
        public List<UserDTO> Filtration(DateTime? date = null,
                           string name = null,
                           string lastName = null,
                           string patronymic = null,
                           string city = null,
                           string country = null);
    }
}

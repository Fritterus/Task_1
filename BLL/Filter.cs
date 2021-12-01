using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace BLL
{
    /// <summary>
    /// Class for working with fetching data from database
    /// </summary>
    internal class Filter : IFilter
    {
        private readonly IRepository<User> _db;

        public Filter(IRepository<User> db)
        {
            _db = db;
        }

        /// <inheritdoc/>
        public List<User> Filtration(DateTime? date = null,
                           string name = null,
                           string lastName = null,
                           string patronymic = null,
                           string city = null,
                           string country = null)
        {
            Validation(date, name, lastName, patronymic, city, country);

            var users = _db.GetAll();

            if (date.HasValue)
            {
                users = users.Where(e => e.Date == date);
            }
            if (!string.IsNullOrEmpty(name))
            {
                users = users.Where(e => e.Name == name);
            }
            if (!string.IsNullOrEmpty(lastName))
            {
                users = users.Where(e => e.LastName == lastName);
            }
            if (!string.IsNullOrEmpty(patronymic))
            {
                users = users.Where(e => e.Patronymic == patronymic);
            }
            if (!string.IsNullOrEmpty(city))
            {
                users = users.Where(e => e.City == city);
            }
            if (!string.IsNullOrEmpty(country))
            {
                users = users.Where(e => e.Country == country);
            }

            return users.ToList();
        }

        /// <summary>
        /// Method for validating input parameters 
        /// </summary>
        /// <param name="date">date</param>
        /// <param name="name">name</param>
        /// <param name="lastName">last name</param>
        /// <param name="patronymic">patronymic</param>
        /// <param name="city">city</param>
        /// <param name="country">country</param>
        private void Validation(DateTime? date,
                                string name,
                                string lastName,
                                string patronymic,
                                string city,
                                string country)
        {
            var onlyLetterPattern = new Regex(@"(^[a-zA-Zа-яА-Я]+$)|(^$)");

            if (!onlyLetterPattern.IsMatch(name)
                || !onlyLetterPattern.IsMatch(lastName)
                || !onlyLetterPattern.IsMatch(patronymic)
                || !onlyLetterPattern.IsMatch(city)
                || !onlyLetterPattern.IsMatch(country))
            {
                throw new ArgumentException("Wrong input format.");
            }
        }
    }
}

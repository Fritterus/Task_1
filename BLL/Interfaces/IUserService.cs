using DAL.Models;
using System.Collections.Generic;

namespace BLL
{
    public interface IUserService
    {
        /// <summary>
        /// User count in database
        /// </summary>
        public int Count { get; }

        /// <summary>
        /// Method for getting a list of all users
        /// </summary>
        /// <returns>list of all users</returns>
        public List<User> GetAllUsers(int from, int count);
    }
}

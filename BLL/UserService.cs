using DAL.Interfaces;
using DAL.Models;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    internal class UserService : IUserService
    {
        private readonly IRepository<User> _db;

        public UserService(IRepository<User> db)
        {
            _db = db;
        }

        /// <inheritdoc/>
        public int Count => _db.GetAll().Count();

        /// <inheritdoc/>
        public List<User> GetAllUsers(int from, int count)
        {
            return _db.GetAll().Skip(from).Take(count).ToList();
        }
    }
}

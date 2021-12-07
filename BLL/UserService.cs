using AutoMapper;
using BLL.Interfaces;
using BLL.ModelsDTO;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL
{
    internal class UserService : IUserService
    {
        private readonly IRepository<User> _db;
        private readonly IMapper _mapper;

        public UserService(IRepository<User> db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        public int Count => _db.GetAll().Count();

        /// <inheritdoc/>
        public List<UserDTO> GetAllUsers(int from, int count)
        {
            var users = _db.GetAll().Skip(from).Take(count).ToList();
            return _mapper.Map<List<UserDTO>>(users);
        }
    }
}

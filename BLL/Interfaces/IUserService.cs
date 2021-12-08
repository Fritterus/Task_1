using BLL.ModelsDTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
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
        public Task<List<UserDTO>> GetAllUsersAsync(int from, int count);
    }
}

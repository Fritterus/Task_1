using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IRepository<T>
        where T : class
    {
        /// <summary>
        /// Method for getting all database records
        /// </summary>
        /// <returns>IQueryably collection of T type</returns>
        IQueryable<T> GetAll();

        /// <summary>
        /// Method for getting record from database by its Id
        /// </summary>
        /// <param name="id">record id</param>
        /// <returns></returns>
        Task<T> GetAsync(int id);

        /// <summary>
        /// Method for creating record in database
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        Task CreateAsync(T obj);

        /// <summary>
        /// Method for creating multiple records in database
        /// </summary>
        /// <param name="obj"></param>
        Task AddRange(IEnumerable<T> obj);

        /// <summary>
        /// Method for updating record in database
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        Task UpdateAsync(T obj);

        /// <summary>
        /// Method for deleting record from database by its Id
        /// </summary>
        /// <param name="id">record id</param>
        /// <returns></returns>
        Task DeleteAsync(int id);
    }
}
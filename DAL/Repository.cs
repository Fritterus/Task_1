using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL
{
    internal class Repository<T> : IRepository<T>
where T : class
    {
        private readonly UserContext _userContext;

        public Repository(UserContext context)
        {
            _userContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <inheritdoc/>
        public async Task AddRange(IEnumerable<T> obj)
        {
            await _userContext.AddRangeAsync(obj);
            await _userContext.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task CreateAsync(T item)
        {
            await _userContext.AddAsync(item);
            await _userContext.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(int id)
        {
            var item = await GetAsync(id);

            if (item == null)
            {
                return;
            }

            _userContext.Remove(item);
            await _userContext.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public IQueryable<T> GetAll()
        {
            IQueryable<T> collection = _userContext.Set<T>().AsNoTracking();
            return collection;
        }

        /// <inheritdoc/>
        public async Task<T> GetAsync(int id)
        {
            T item = await _userContext.FindAsync<T>(id);
            return item;
        }

        /// <inheritdoc/>
        public async Task UpdateAsync(T item)
        {
            _userContext.Update(item);
            await _userContext.SaveChangesAsync();
        }
    }
}
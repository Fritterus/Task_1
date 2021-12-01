using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DAL
{
    public static class ServiceExtention
    {
        public static void ConnectDAL(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<UserContext>
               (options => options.UseSqlServer(connectionString));

            services.AddScoped<IRepository<User>, Repository<User>>();
        }
    }
}

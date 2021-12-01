using BLL.Interfaces;
using BLL.WorkFile;
using DAL;
using Microsoft.Extensions.DependencyInjection;

namespace BLL
{
    public static class ServiceExtention
    {
        public static void ConnectBLL(this IServiceCollection services, string connectionString)
        {
            services.ConnectDAL(connectionString);
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IFilter, Filter>();
            services.AddScoped<IUserService, UserService>();
        }
    }
}

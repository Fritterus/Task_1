using AutoMapper;
using BLL.ModelsDTO;
using DAL.Models;

namespace BLL.Mappers
{
    internal class ServiceMapperProfile : Profile
    {
        public ServiceMapperProfile()
        {
            CreateMap<UserDTO, User>();
            CreateMap<User, UserDTO>();
        }

    }
}

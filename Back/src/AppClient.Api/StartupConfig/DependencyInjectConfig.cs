using AppClient.Domain.DTO;
using AppClient.Domain.Entities;
using AppClient.Repository.Implementations;
using AppClient.Repository.Interface;
using AppClient.Repository.Models;
using AppClient.Service.Service.Implementations;
using AppClient.Service.Service.Interfaces;
using AutoMapper;

namespace AppClient.Api.StartupConfig
{
    public static class DependencyInjectConfig
    {
        public static void AddDependencyInjectionConfig(this IServiceCollection services)
        {
            #region AutoMapper
            var config = new MapperConfiguration(configuration =>
            {
                //Client
                configuration.CreateMap<Client, ClientDto>().ReverseMap();
            });
            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);
            #endregion

            services.AddTransient<IClientRepository, ClientRepository>();
            services.AddTransient<IClientService, ClientService>();
        }
    }
}

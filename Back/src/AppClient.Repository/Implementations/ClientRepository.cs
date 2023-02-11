using AppClient.Domain.Entities;
using AppClient.Domain.Repository.Interface;
using AppClient.Repository.Implementations;

namespace AppClient.Repository.Implementations
{
    public class ClientRepository : RepositoryBase<Client>, IClientRepository
    {
        public ClientRepository(ApplicationDbContext contexto) : base(contexto)
        { }
    }
}

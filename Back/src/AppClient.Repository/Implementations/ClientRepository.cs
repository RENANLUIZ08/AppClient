using AppClient.Domain.Entities;
using AppClient.Repository.Interface;

namespace AppClient.Repository.Implementations
{
    public class ClientRepository : RepositoryBase<Client>, IClientRepository
    {
        public ClientRepository(ApplicationDbContext contexto) : base(contexto)
        { }
    }
}

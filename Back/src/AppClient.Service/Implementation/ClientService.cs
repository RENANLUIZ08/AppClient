using AppClient.Domain.DTO;
using AppClient.Domain.Entities;
using AppClient.Repository.Interface;
using AppClient.Repository.Models;
using AppClient.Service.Implementations;
using AppClient.Service.Service.Interfaces;
using AutoMapper;

namespace AppClient.Service.Service.Implementations
{
    public class ClientService : ServiceBase, IClientService
    {
        private readonly IClientRepository _repository;

        public ClientService(
            IClientRepository repository,
            IMapper mapper) : base(mapper)
        {
            _repository = repository;
        }

        public ClientDto Create(ClientDto dto)
        {
            var entity = _mapper.Map<Client>(dto);
            var result = _repository.InsertDb(entity);

            if (!_repository.SaveChanges())
            { throw new OperationCanceledException("Erro ao Cadastrar o cliente."); }

            return _mapper.Map<ClientDto>(result);
        }

        public ClientDto Update(int id, ClientDto dto)
        {
            var entity = _repository.GetById(id);
            if (entity == null)
            { throw new NullReferenceException("Cliente não localizado para atualização."); }

            _mapper.Map(dto, entity);
            _repository.UpdateDb(entity);

            if (!_repository.SaveChanges())
            { throw new OperationCanceledException("Erro ao Atualizar o cliente."); }

            return dto;
        }

        public void Delete(int id)
        {
            var getClient = _repository.GetById(id);
            if (getClient == null)
            { throw new NullReferenceException("Cliente não encontrado para exclusão."); }

            _repository.DeleteDb(getClient);
            if (!_repository.SaveChanges())
            { throw new OperationCanceledException("Erro ao Deletar o cliente."); }
        }

        public ClientDto GetById(int id)
        {
            var getClient = _repository.GetById(id);
            if (getClient == null)
            { throw new NullReferenceException("Cliente não encontrado."); }

            return _mapper.Map<ClientDto>(getClient);
        }

        public async Task<PageList<ClientDto>> GetAll(PageParams pageParams)
        {
            var getClients = await _repository.GetByWhere(pageParams);

            // if (getClients.Count == 0)
            // { throw new NullReferenceException("Nenhum cliente foi localizado."); }

            var getResult = _mapper.Map<PageList<ClientDto>>(getClients);
            getResult.CurrentPage = getClients.CurrentPage;
            getResult.TotalPages = getClients.TotalPages;
            getResult.PageSize = getClients.PageSize;
            getResult.TotalCount = getClients.TotalCount;

            return getResult;
        }
    }
}

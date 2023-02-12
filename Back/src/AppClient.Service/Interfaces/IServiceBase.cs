using AppClient.Repository.Models;

namespace AppClient.Service.Repository.Interfaces
{
    public interface IServiceBase<TDto> where TDto : class
    {
        TDto Create(TDto dto);
        TDto Update(int id, TDto dto);
        void Delete(int id);
        TDto GetById(int id);
        Task<PageList<TDto>> GetAll(PageParams pageParams);
    }
}

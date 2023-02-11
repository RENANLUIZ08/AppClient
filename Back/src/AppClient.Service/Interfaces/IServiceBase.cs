namespace AppClient.Service.Repository.Interfaces
{
    public interface IServiceBase<TDto> where TDto : class
    {
        TDto Create(TDto dto);
        TDto Update(TDto dto);
        void Delete(int id);
        TDto GetById(int id);
        List<TDto> GetAll();
    }
}

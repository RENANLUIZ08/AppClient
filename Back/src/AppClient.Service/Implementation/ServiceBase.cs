using AutoMapper;

namespace AppClient.Service.Implementations
{
    public abstract class ServiceBase
    {
        public readonly IMapper _mapper;

        protected ServiceBase(IMapper mapper)
        {
            _mapper = mapper;
        }

    }
}

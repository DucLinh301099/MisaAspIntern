
using MisaAsp.Models.ViewModel;
using MisaAsp.Repositories;
namespace MisaAsp.Services
{
    public interface IPagingFilterService
    {
    }
     public class PagingFilterService : IPagingFilterService
    {
        private readonly IPagingFilterRepository _pagingfilterRepository;
        public PagingFilterService(IPagingFilterRepository pagingfilterRepository)
        {
            _pagingfilterRepository = pagingfilterRepository;
        }


    }
}

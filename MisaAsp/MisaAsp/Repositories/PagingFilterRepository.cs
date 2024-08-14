using Dapper;
using MisaAsp.Models.ViewModel;
using MisaAsp.Repositories.Base;
using Newtonsoft.Json;
using System.Data;

namespace MisaAsp.Repositories
{
    public interface IPagingFilterRepository : IBaseRepository
    {
    }
     public class PagingFilterRepository : BaseRepository, IPagingFilterRepository
    {
        public PagingFilterRepository(IDbConnection connection) : base(connection) { }


    }
}

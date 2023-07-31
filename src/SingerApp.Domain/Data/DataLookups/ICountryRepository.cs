using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace SingerApp.Data.DataLookups
{
    public interface ICountryRepository : IRepository<Country, int>
    {
        Task<Country> FindByNameAsync(string name);
        Task<List<Country>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null
        );
    }
}

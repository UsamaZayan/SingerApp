using SingerApp.Singers.Dtos;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace SingerApp.Singers
{
    public interface ISingerAppService : IApplicationService 
    {
        Task<ListResultDto<CountryLookupDto>> GetCountryLookupAsync();
        Task AddTranslationsAsync(int id, SingerTranslationDto input);
        Task<ListResultDto<SingerTranslationDto>> GetSingerTranslationsAsync(int id);
        Task<PagedResultDto<SingerListDto>>GetListAsync(GetSingerInput input);
        Task CreateAsync(SingerCreareOrUpdateDto input);
        Task<SingerDto> GetAsync(int id);
        Task UpdateAsync(int id, SingerCreareOrUpdateDto input);
        Task DeleteAsync(int id);
    }
}

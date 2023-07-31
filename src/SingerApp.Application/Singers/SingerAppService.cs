using AutoMapper.Internal.Mappers;
using SingerApp.Data.DataLookups;
using SingerApp.DataFilters;
using SingerApp.Singers.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Data;
using Volo.Abp.Domain.Repositories;
using System.Linq.Dynamic.Core;

namespace SingerApp.Singers
{
    public class SingerAppService : SingerAppAppService, ISingerAppService
    {
        private readonly IRepository<Singer, int> _singerRepository;
        private readonly IRepository<SingerTranslation, int> _singerTranslationRepository;
        private readonly IRepository<Country, int> _countryRepository;
        private readonly IDataFilter _dataFilter;

        public SingerAppService(
            IRepository<Singer, int> singerRepository, 
            IRepository<Country, int> countryRepository, 
            IDataFilter dataFilter, 
            IRepository<SingerTranslation, int> singerTranslationRepository)
        {
            _singerRepository = singerRepository;
            _countryRepository = countryRepository;
            _dataFilter = dataFilter;
            _singerTranslationRepository = singerTranslationRepository;
        }

        public async Task CreateAsync(SingerCreareOrUpdateDto input)
        {
            await _singerRepository.InsertAsync(ObjectMapper.Map<SingerCreareOrUpdateDto, Singer>(input));
        }

        public async Task DeleteAsync(int id)
        {
            await _singerRepository.DeleteAsync(id);
        }

        public async Task<SingerDto> GetAsync(int id)
        {
            var queryable = await _singerRepository.WithDetailsAsync();

            var singer = await AsyncExecuter.FirstOrDefaultAsync(queryable, x => x.Id == id);

            return ObjectMapper.Map<Singer, SingerDto>(singer);
        }

        public async Task UpdateAsync(int id, SingerCreareOrUpdateDto input)
        {
            var singer = await _singerRepository.GetAsync(id);
            ObjectMapper.Map(input, singer);
        }

        public async Task AddTranslationsAsync(int id, SingerTranslationDto input)
        {
            var queryable = await _singerRepository.WithDetailsAsync();

            var singer = await AsyncExecuter.FirstOrDefaultAsync(queryable, x => x.Id == id);

            if (singer.Translations.Any(x => x.Language == input.Language)) 
            { 
                throw new UserFriendlyException($"Translation already available for {input.Language}"); 
            }

            singer.Translations.Add(new SingerTranslation{ 
                SingerId = singer.Id,
                Name = input.Name,
                Language = input.Language
            }); 

            await _singerRepository.UpdateAsync(singer);
        }

        public async Task<ListResultDto<SingerTranslationDto>> GetSingerTranslationsAsync(int id)
        {
            var queryable = await _singerTranslationRepository.WithDetailsAsync();

            var query = queryable.Where(x => x.SingerId == id);

            var queryResult = await AsyncExecuter.ToListAsync(query);

            return new ListResultDto<SingerTranslationDto>(
                ObjectMapper.Map<List<SingerTranslation>, List<SingerTranslationDto>>(queryResult));
        }

        public async Task<ListResultDto<CountryLookupDto>> GetCountryLookupAsync()
        {
            var countries = await _countryRepository.GetListAsync();
            
            return new ListResultDto<CountryLookupDto>(
                ObjectMapper.Map<List<Country>, List<CountryLookupDto>>(countries));
        }

        public async Task<PagedResultDto<SingerListDto>> GetListAsync(GetSingerInput input)
        {
            using (_dataFilter.Disable<IIsActive>()) {
                var queryable = await _singerRepository.WithDetailsAsync();
            

                var query = from singer in queryable 
                            join country in await _countryRepository.GetQueryableAsync() on singer.CountryId equals country.Id
                            select new{ singer, country };


                query = query
                    .WhereIf(!input.filter.IsNullOrWhiteSpace(), x => x.singer.Name.Contains(input.filter))
                    .WhereIf(input.CountryId.HasValue, x => x.country.Id == input.CountryId)
                    .OrderBy(NormalizeSorting(input.Sorting))
                    .Skip(input.SkipCount)
                    .Take(input.MaxResultCount);

                var queryResult = await AsyncExecuter.ToListAsync(query);

                var singerDtos = queryResult.Select(x => 
                { 
                    var singerDto = ObjectMapper.Map<Singer, SingerListDto>(x.singer);
                    singerDto.CountryName = x.country.Name; 
                    return singerDto; 
                }).ToList();

                var totalCount = await _singerRepository.GetCountAsync();

                return new PagedResultDto<SingerListDto>(totalCount, singerDtos);
            }
        }

        private static string NormalizeSorting(string sorting) 
        { 
            if (sorting.IsNullOrEmpty()) 
            { 
                return $"singer.{nameof(Singer.Name)}"; 
            } 
            if (sorting.Contains("countryName", StringComparison.OrdinalIgnoreCase)) 
            { 
                return sorting.Replace(
                    "countryName", 
                    "country.Name", 
                    StringComparison.OrdinalIgnoreCase); 
            } 
            return $"singer.{sorting}"; 
        }
    }
}

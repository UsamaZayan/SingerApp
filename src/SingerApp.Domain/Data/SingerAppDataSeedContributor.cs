using SingerApp.Data.DataLookups;
using SingerApp.Singers;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace SingerApp.Data
{
    public class SingerAppDataSeedContributor : IDataSeedContributor, ITransientDependency
    {

        private readonly IRepository<Singer, int> _singerRepository;
        private readonly IRepository<Country, int> _countryRepository;

        public SingerAppDataSeedContributor(
            IRepository<Singer, int> singerRepository, 
            IRepository<Country, int> countryRepository)
        {
            _singerRepository = singerRepository;
            _countryRepository = countryRepository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (await _singerRepository.CountAsync() > 0)
            {
                return;
            }

            var country1 = new Country
            {
                Name = "Palestine",
                Alpha2 = "PS",
                Alpha3 = "PSE",
                UnCode = 275,
                DialingCode = "970",
                NativeName = "فلسطين",
                Region = "Asia",
                SubRegion = "Western Asia",
                TimeZone = "UTC+02:00",
                Capital = "Jerusalem",
                Population = 4682467,
                Area = 25000,
                Flag = "Palestine.svg",
                DisplayOrder = 172
            };

            await _countryRepository.InsertAsync(country1);


            var singer1 = new Singer { 
                Country = country1,
                Translations = new[] { 
                    new SingerTranslation { 
                        Name = "Usama", 
                        Language = "en" 
                    }, 
                    new SingerTranslation { 
                        Name = "اسامه", 
                        Language = "ar" 
                    } 
                },
                IsActive = true
            };

            var singer2 = new Singer
            {
                Country = country1,
                Translations = new[] {
                    new SingerTranslation {
                        Name = "Mohammed",
                        Language = "en"
                    },
                    new SingerTranslation {
                        Name = "محمد",
                        Language = "ar"
                    }
                },
                IsActive = false
            };

            await _singerRepository.InsertManyAsync(new[] { singer1, singer2 });
        } 
    }
}

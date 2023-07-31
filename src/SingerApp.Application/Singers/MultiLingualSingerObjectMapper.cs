using AutoMapper.Internal.Mappers;
using SingerApp.Data.DataLookups;
using SingerApp.MultiLingualObjects;
using SingerApp.Singers.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Localization;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Settings;
using Volo.Abp.Threading;

namespace SingerApp.Singers
{
    public class MultiLingualSingerObjectMapper : IObjectMapper<Singer, SingerListDto>, ITransientDependency
    {
        private readonly MultiLingualObjectManager _multiLingualObjectManager;

        public MultiLingualSingerObjectMapper(MultiLingualObjectManager multiLingualObjectManager)
        {
            _multiLingualObjectManager = multiLingualObjectManager;
        }

        public SingerListDto Map(Singer source)
        {
            var translation = AsyncHelper.RunSync(() => 
                _multiLingualObjectManager.FindTranslationAsync<Singer, SingerTranslation>(source));

            return new SingerListDto
            {
                Id = source.Id,
                Name = translation?.Name ?? source.Name,
                CountryId = source.CountryId,
                IsActive = source.IsActive,
                StorageId = source.StorageId
            };
        }

        public SingerListDto Map(Singer source, SingerListDto destination)
        {
            return default;
        }
    }
}

using System;
using Volo.Abp.Application.Dtos;
using SingerApp.MultiLingualObjects;
using SingerApp.DataFilters;
using System.Collections.Generic;

namespace SingerApp.Singers.Dtos
{
    public class SingerDto : EntityDto<int>, IObjectTranslation, IIsActive
    {
        public string Name { get; set; }
        public int CountryId { get; set; }

        public string CountryName { get; set; }

        public bool IsActive { get; set; }

        public Guid? StorageId { get; set; }

        public string Language { get; set; }

        public List<SingerTranslationDto> Translations { get; set; }
    }
}

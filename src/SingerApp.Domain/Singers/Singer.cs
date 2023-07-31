using SingerApp.DataFilters;
using SingerApp.Data.DataLookups;
using SingerApp.MultiLingualObjects;
using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;

namespace SingerApp.Singers
{
    public class Singer : FullAuditedEntity<int>, IIsActive, IMultiLingualObject<SingerTranslation>
    {
        public string Name { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
        public ICollection<SingerTranslation> Translations { get; set; }
        public bool IsActive { get; set; }

        public Guid StorageId { get; set; }
    }
}

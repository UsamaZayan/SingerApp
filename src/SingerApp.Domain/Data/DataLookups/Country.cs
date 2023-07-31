using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace SingerApp.Data.DataLookups
{
    public class Country : Entity<int>
    {
        public string Name { get; set; }
        public string Alpha2 { get; set; }
        public string Alpha3 { get; set; }
        public int UnCode { get; set; }
        public string DialingCode { get; set; }
        public string NativeName { get; set; }
        public string Region { get; set; }
        public string SubRegion { get; set; }
        public string TimeZone { get; set; }
        public string Capital { get; set; }
        public int Population { get; set; }
        public int Area { get; set; }
        public string Flag { get; set; }
        public int DisplayOrder { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace SingerApp.Singers.Dtos
{
    public class CountryLookupDto : EntityDto<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}

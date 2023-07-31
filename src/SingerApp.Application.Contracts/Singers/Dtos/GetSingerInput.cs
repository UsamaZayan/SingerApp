using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace SingerApp.Singers.Dtos
{
    public class GetSingerInput : PagedAndSortedResultRequestDto
    {
        public string filter { get; set; }
        public int? CountryId { get; set; }
    }
}

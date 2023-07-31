using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SingerApp.Singers;
using SingerApp.Singers.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SingerApp.Web.Pages.Singers
{
    public class IndexModel : PageModel
    {
        private readonly ISingerAppService _singerAppService;

        public IndexModel(ISingerAppService singerAppService)
        {
            _singerAppService = singerAppService;
        }
        public IEnumerable<SelectListItem> Countries { get; set; }
        [BindProperty()]
        public GetSingerInput SearchInput { get; set; } = new GetSingerInput();
        public async Task OnGetAsync()
        {
            var countryLookup =
                await _singerAppService.GetCountryLookupAsync();
            Countries = countryLookup.Items
                .Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToArray();
        }
    }
}

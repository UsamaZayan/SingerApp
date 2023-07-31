using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SingerApp.Singers;
using SingerApp.Singers.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SingerApp.Web.Pages.Singers
{
    public class CreateSingerModalModel : SingerAppPageModel
    {
        [BindProperty]
        public SingerCreareOrUpdateViewModel Singer { get; set; }
        public SelectListItem[] Countries { get; set; }
        [BindProperty]
        public SingerTranslationViewModel TranslationViewModelEn { get; set; }
        [BindProperty]
        public SingerTranslationViewModel TranslationViewModelAr { get; set; }

        private readonly ISingerAppService _singerAppService;
        public CreateSingerModalModel(ISingerAppService singerAppService)
        {
            _singerAppService = singerAppService;

            TranslationViewModelEn = new SingerTranslationViewModel { Language = "en" };
            TranslationViewModelAr = new SingerTranslationViewModel { Language = "ar" };
        }
        public async Task OnGetAsync()
        {
            Singer = new SingerCreareOrUpdateViewModel();

            var countryLookup =
                await _singerAppService.GetCountryLookupAsync();
            Countries = countryLookup.Items
                .Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToArray();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            TranslationViewModelEn.Language = "en";
            TranslationViewModelAr.Language = "ar";

            Singer.Translations = new List<SingerTranslationViewModel>(
                new[] { TranslationViewModelEn, TranslationViewModelAr }
            );

            await _singerAppService.CreateAsync(
                ObjectMapper.Map<SingerCreareOrUpdateViewModel, SingerCreareOrUpdateDto>(Singer)
            );
            return NoContent();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using SingerApp.Singers;
using SingerApp.Singers.Dtos;
using System.Linq;
using System.Collections.Generic;

namespace SingerApp.Web.Pages.Singers
{
    public class EditSingerModalModel : SingerAppPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }
        [BindProperty]
        public SingerCreareOrUpdateViewModel Singer { get; set; }
        public SelectListItem[] Countries { get; set; }
        public List<SelectListItem> Languages { get; set; }
        [BindProperty]
        public SingerTranslationViewModel TranslationViewModelEn { get; set; }
        [BindProperty]
        public SingerTranslationViewModel TranslationViewModelAr { get; set; }

        private readonly ISingerAppService _singerAppService;
        public EditSingerModalModel(ISingerAppService productAppService)
        {
            _singerAppService = productAppService;
        }
        public async Task OnGetAsync()
        {
            var singerDto = await _singerAppService.GetAsync(Id);
            Singer = ObjectMapper.Map<SingerDto, SingerCreareOrUpdateViewModel>(singerDto);

            var countryLookup = await _singerAppService.GetCountryLookupAsync();

            Countries = countryLookup.Items
                .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
                .ToArray();


            string NameEn = "Defult";
            string NameAr = "Defult";

            foreach (var s in singerDto.Translations)
            {
                if (s.Language == "en")
                {
                    NameEn = s.Name;
                }
                if (s.Language == "ar")
                {
                    NameAr = s.Name;
                }
            }

            TranslationViewModelEn = new SingerTranslationViewModel { Name = NameEn };
            TranslationViewModelAr = new SingerTranslationViewModel { Name = NameAr };
        }

        public async Task<IActionResult> OnPostAsync()
        {
            TranslationViewModelEn.Language = "en";
            TranslationViewModelAr.Language = "ar";

            Singer.Translations = new List<SingerTranslationViewModel>(
                new[] { TranslationViewModelEn, TranslationViewModelAr }
            );

            await _singerAppService.UpdateAsync(Id,
                ObjectMapper.Map<SingerCreareOrUpdateViewModel, SingerCreareOrUpdateDto>(Singer)
            );

            return NoContent();
        }
    }
}

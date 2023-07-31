using AutoMapper;
using SingerApp.Singers.Dtos;
using System.ComponentModel.DataAnnotations;

namespace SingerApp.Web.Pages.Singers
{
    [AutoMap(typeof(SingerTranslationDto), ReverseMap = true)]
    public class SingerTranslationViewModel
    {
        [Required]
        public string Language { get; set; }

        [Required]
        public string Name { get; set; }

    }
}

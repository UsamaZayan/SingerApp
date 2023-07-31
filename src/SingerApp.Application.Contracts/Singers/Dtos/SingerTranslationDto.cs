using SingerApp.MultiLingualObjects;
using System.ComponentModel.DataAnnotations;

namespace SingerApp.Singers.Dtos
{
    public class SingerTranslationDto : IObjectTranslation
    {
        public SingerTranslationDto()
        {

        }
        public SingerTranslationDto(string name, string language)
        {
            Name = name;
            Language = language;
        }

        [Required]
        [StringLength(maximumLength: SingerConsts.MaxNameLength, MinimumLength = SingerConsts.MinNameLength)]
        public string Name { get; set; }
        [Required]

        public string Language { get; set; }
    }
}
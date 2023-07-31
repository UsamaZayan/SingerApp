using SingerApp.DataFilters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SingerApp.Singers.Dtos
{
    public class SingerCreareOrUpdateDto : IIsActive
    {
        [Required]
        [Display(Name = "FormName:Translations")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "FormName:Country")]
        public virtual int CountryId { get; set; }

        [Display(Name = "FormName:IsActive")]
        public virtual bool IsActive { get; set; }


        [Display(Name = "FormName:SingerPicture")]

        public virtual Guid? StorageId { get; set; }

        public ICollection<SingerTranslationDto> Translations { get; set; }

    }
}

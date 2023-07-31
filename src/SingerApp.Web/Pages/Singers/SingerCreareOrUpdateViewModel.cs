using System.ComponentModel.DataAnnotations;
using System;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using System.Collections.Generic;

namespace SingerApp.Web.Pages.Singers
{
    public class SingerCreareOrUpdateViewModel
    {
        [Required]
        [Display(Name = "FormName:DefaultName")]
        public string Name { get; set; }
        [SelectItems("Countries")]
        [Required]
        [Display(Name = "FormName:Country")]
        public virtual int CountryId { get; set; }

        [Display(Name = "FormName:IsActive")]
        public virtual bool IsActive { get; set; }


        [Display(Name = "FormName:SingerPicture")]

        public virtual Guid? StorageId { get; set; }
        [DynamicFormIgnore]
        public ICollection<SingerTranslationViewModel> Translations { get; set; }
    }
}

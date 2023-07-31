using AutoMapper;
using SingerApp.Singers.Dtos;
using SingerApp.Web.Pages.Singers;

namespace SingerApp.Web;

public class SingerAppWebAutoMapperProfile : Profile
{
    public SingerAppWebAutoMapperProfile()
    {
        //Define your AutoMapper configuration here for the Web project.
        CreateMap<SingerCreareOrUpdateViewModel, SingerCreareOrUpdateDto>();
        CreateMap<SingerDto, SingerCreareOrUpdateViewModel>();
    }
}

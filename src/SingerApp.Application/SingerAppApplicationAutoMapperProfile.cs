using AutoMapper;
using SingerApp.Data.DataLookups;
using SingerApp.Singers;
using SingerApp.Singers.Dtos;

namespace SingerApp;

public class SingerAppApplicationAutoMapperProfile : Profile
{
    public SingerAppApplicationAutoMapperProfile()
    {
        CreateMap<Singer, SingerDto>();
        CreateMap<Singer, SingerListDto>();
        CreateMap<SingerCreareOrUpdateDto, Singer>();
        CreateMap<SingerTranslation, SingerTranslationDto>();
        CreateMap<SingerTranslationDto, SingerTranslation>();
        CreateMap<Country, CountryLookupDto>();
    }
}

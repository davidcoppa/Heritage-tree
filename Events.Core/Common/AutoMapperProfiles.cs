using AutoMapper;
using Events.Core.DTOs;
using Events.Core.Model;
using EventsManager.Model;

namespace Events.Core.Common
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Event, EventCreateDTO>().ReverseMap();
            CreateMap<Event, EventCreateEditDTO>().ReverseMap();
            CreateMap<Event, PersonWithParents>().ReverseMap();


            CreateMap<Person, PersonCreateDTO>().ReverseMap();
            CreateMap<Person, PersonEditDTO>().ReverseMap();
            CreateMap<Person, PersonWithParents>().ReverseMap();

            //CreateMap<ParentPerson, ParentPersonCreateDTO>().ReverseMap();
            //CreateMap<ParentPerson, ParentPersonEditDTO>().ReverseMap();

            CreateMap<EventTypes, EventTypeCreateDTO>().ReverseMap();
            CreateMap<EventTypes, EventTypeEditDTO>().ReverseMap();


            CreateMap<CountryCreateDTO, Country>().ReverseMap();
            CreateMap<CountryEditDTO, Country>().ReverseMap();
            CreateMap<CountryReturnDTO, Country>().ReverseMap();
           
            CreateMap<StateReturnDTO, States>().ReverseMap();
            CreateMap<StateEditDTO, States>().ReverseMap();
            CreateMap<StateCreateDTO, States>().ReverseMap();
            
            CreateMap<CityReturnDTO, City>().ReverseMap();
            CreateMap<CityCreateDTO, City>().ReverseMap();
            CreateMap<CityEditDTO, City>().ReverseMap();



            CreateMap<MediaCreateDTO, Media>().ReverseMap();
            CreateMap<MediaDTO, Media>().ReverseMap();
            //CreateMap<List<MediaDTO> , List<Media>>().ReverseMap();
            CreateMap<FileDataCreateDTO, FileData>().ReverseMap();
            CreateMap<MediaTypeDTO, MediaType>().ReverseMap();
            CreateMap<TagsDTO, Tags>().ReverseMap();
            

        }


    }
}

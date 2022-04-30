using AutoMapper;
using Events.Core.DTOs;
using EventsManager.Model;

namespace Events.Core.Common
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Event, EventCreateDTO>().ReverseMap();
            CreateMap<Person, PersonCreateDTO>().ReverseMap();
            CreateMap<Person, PersonEditDTO>().ReverseMap();
            CreateMap<ParentPerson, ParentPersonCreateDTO>().ReverseMap();




        }


    }
}

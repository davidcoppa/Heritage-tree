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
            CreateMap<ParentPerson, PersonCreateDTO>().ReverseMap();
            CreateMap<ParentPerson, ParentPersonCreateDTO>().ReverseMap();




        }


    }
}

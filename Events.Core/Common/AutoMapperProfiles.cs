﻿using AutoMapper;
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

            CreateMap<ParentPerson, ParentPersonCreateDTO>().ReverseMap();
            CreateMap<ParentPerson, ParentPersonEditDTO>().ReverseMap();

            CreateMap<EventTypes, EventTypeCreateDTO>().ReverseMap();
            CreateMap<EventTypes, EventTypeEditDTO>().ReverseMap();


            CreateMap<CountryCreateDTO, Country>().ReverseMap();
            CreateMap<CountryEditDTO, Country>().ReverseMap();
          //  CreateMap<CountryReturnDTO, Country>().ReverseMap();
           

        }


    }
}

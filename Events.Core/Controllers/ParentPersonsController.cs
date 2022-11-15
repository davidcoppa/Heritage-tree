
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventsManager.Data;
using EventsManager.Model;
using Events.Core.DTOs;
using AutoMapper;
using Events.Core.Common.Validators;
using Events.Core.Common.Messages;
using EventsManager.Enums;

namespace Events.Core.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ParentPersonsController : Controller
    {
        private readonly EventsContext context;
        private readonly IMapper mapper;
        private readonly IDataValidator validator;
        private readonly IMessages messages;


        public ParentPersonsController(EventsContext context,
            IMapper mapper,
            IDataValidator validator,
            IMessages messages)
        {
            this.context = context;
            this.mapper = mapper;
            this.validator = validator;
            this.messages = messages;
        }


        [HttpGet("GetAllFilter")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllFilter(int idPerson)
        {
            if (idPerson == 0)
            {
                return NotFound();
            }

            var person = await context.Person.FirstOrDefaultAsync(m => m.Id == idPerson);
            if (person == null)
            {
                return NotFound();
            }

            List<Event> events = GetEventsPerson(idPerson);

            if (events == null)
            {
                return NotFound("NO event found on person");
            }


            PersonVal retValue = ProcessEventPerson(person, events, person.Id);
            retValue.TextClass = "emphasis";

            List<PersonVal> valuies = new List<PersonVal>();
            if (parents != null)
            {
                if (retValue.Id != parents.Id)
                {
                    //should be only 1 item in the list
                    foreach (Marriages item in parents.Marriages)
                    {
                        item.Children.Add(retValue);
                        item.spouse.IsMarriage = true;
                    }
                    valuies.Add(parents);
                }
                else
                {
                    //wrong!!!
                    //the parent node it's the same node as the son node
                }

            }
            else
            {
                valuies.Add(retValue);
            }

            return Ok(valuies);
        }
        PersonVal parentspouse, parents;// = new PersonVal();
        private PersonVal ProcessEventPerson(Person person, List<Event> events, int? mainId)
        {

            PersonVal retValue = new PersonVal
            {
                Id = person.Id,
                Name = person.FirstName ?? "No name",
                Order = person.Order ?? 1,
                Class = person.Sex.ToString(),
             //   IsMarriage = true
        };
            Marriages marriage = new Marriages();
            PersonVal children = new PersonVal();
            if (events.Where(x => x.EventType.Description.Contains("Nacimiento")
                                || x.EventType.Description.Contains("Casamiento")).Count() > 0)
            {
                retValue.Marriages = new List<Marriages>();
                marriage.Children = new List<PersonVal>();
            }

            foreach (var eventItem in events)
            {
                if (eventItem.EventType.Description.Contains("Casamiento"))
                {
                    if (eventItem.Person2 != null)
                    {
                        var spouse = new PersonVal();

                        if (retValue.Marriages.Count() == 0)
                        {
                            marriage = new Marriages();
                            spouse = CreateSpouse(eventItem.Person2, spouse);

                            //spouse.Id = eventItem.Person2.Id;
                            //spouse.Name = eventItem.Person2.FirstName ?? "No name";
                            //spouse.Order = eventItem.Person2.Order ?? 1;
                            //spouse.Class = eventItem.Person2.Sex.ToString();

                            marriage.spouse = spouse;
                            retValue.Marriages.Add(marriage);
                        }
                        else
                        {
                            foreach (var item in retValue.Marriages)
                            {
                                if (item.spouse.Id == eventItem.Person2.Id)
                                {
                                    marriage = new Marriages();
                                    marriage.spouse = item.spouse;
                                    //retValue.Marriages.Add(marriage);
                                }
                                else
                                {
                                    if (item.spouse.Id != 0)
                                    {

                                    }
                                    else
                                    {
                                        marriage = new Marriages();
                                        spouse = CreateSpouse(eventItem.Person2, spouse);

                                        //spouse.Id = eventItem.Person2.Id;
                                        //spouse.Name = eventItem.Person2.FirstName ?? "No name";
                                        //spouse.Order = eventItem.Person2.Order ?? 1;
                                        //spouse.Class = eventItem.Person2.Sex.ToString();

                                        marriage.spouse = spouse;

                                    }

                                }
                            }
                            retValue.Marriages.Add(marriage);
                        }

                    }

                }


                if (eventItem.EventType.Description.Contains("Nacimiento"))
                {
                    if (eventItem.Person1 != null)
                    {
                        if (eventItem.Person1.Id == person.Id)
                        {
                            if (mainId != null)
                            {
                                //TODO: get sibilings 


                                //his own birthday
                                //let's get the parents

                                parents = new PersonVal
                                {
                                    Id = person.Id,
                                    Name = person.FirstName ?? "No name",
                                    Order = person.Order ?? 1,
                                    Class = person.Sex.ToString(),

                                };
                                parentspouse = new PersonVal
                                {
                                    Id = person.Id,
                                    Name = person.FirstName ?? "No name",
                                    Order = person.Order ?? 1,
                                    Class = person.Sex.ToString(),

                                };
                                if (eventItem.Person2 != null)
                                {
                                    parents.Id = eventItem.Person2.Id;
                                    parents.Class = eventItem.Person2.Sex.ToString();
                                    parents.Name = eventItem.Person2.FirstName ?? "No name";
                                    parents.Order = eventItem.Person2.Order ?? 1;
                                }
                                else
                                {
                                    parents= CreateSpouse(eventItem.Person3, parentspouse);
                                }
                                //           if (eventItem.Person3 != null)
                                //      {
                                parentspouse = CreateSpouse(eventItem.Person3, parentspouse);
                                //     }

                                Marriages parentmarriage = new Marriages
                                {
                                    spouse = parentspouse,
                                    Children = new List<PersonVal>()
                                };
                                parents.Marriages = new List<Marriages>();
                                parents.Marriages.Add(parentmarriage);
                                // parentmarriage.Children = new List<PersonVal>();
                                // parentmarriage.Children.Add(retValue);
                            }
                        }
                        else
                        {

                            //it's a son
                            children = new PersonVal();

                            var spouse = new PersonVal();
                            bool dontJumpMarriage = true;

                            // es el otro padre/madre
                            if (eventItem.Person2 != null && eventItem.Person2.Id != person.Id)
                            {
                                foreach (var item in retValue.Marriages)
                                {
                                    if (item.spouse.Id == eventItem.Person2.Id)
                                    {
                                        dontJumpMarriage = false;
                                        marriage = new Marriages();
                                        marriage.spouse = item.spouse;
                                    }
                                }
                                if (dontJumpMarriage)
                                {
                                    spouse = CreateSpouse(eventItem.Person2, spouse);

                                    //spouse.Id = eventItem.Person2.Id;
                                    //spouse.Name = eventItem.Person2.FirstName ?? "No name";
                                    //spouse.Order = eventItem.Person2.Order ?? 1;
                                    //spouse.Class = eventItem.Person2.Sex.ToString();
                                }


                            }
                            else if (eventItem.Person3 != null && eventItem.Person3.Id != person.Id)
                            {
                                foreach (var item in retValue.Marriages)
                                {
                                    if (item.spouse.Id == eventItem.Person3.Id)
                                    {
                                        dontJumpMarriage = false;
                                        marriage = new Marriages();
                                        marriage.spouse = item.spouse;
                                    }
                                }
                                if (dontJumpMarriage)
                                {
                                    spouse = CreateSpouse(eventItem.Person3, spouse);
                                    //spouse.Id = eventItem.Person3.Id;
                                    //spouse.Name = eventItem.Person3.FirstName ?? "No name";
                                    //spouse.Order = eventItem.Person3.Order ?? 1;
                                    //spouse.Class = eventItem.Person3.Sex.ToString();
                                }
                            }
                            else
                            {
                                spouse = CreateSpouse(null, spouse);

                                //spouse.Name = "No name";
                                //spouse.Order = 1;
                                //spouse.Class = GetOtherSex(person.Sex);
                                marriage = new Marriages();
                                marriage.Children = new List<PersonVal>();
                            }
                            if (dontJumpMarriage)
                            {

                                marriage.spouse = spouse;
                            }

                            children.Id = eventItem.Person1.Id;
                            children.Name = eventItem.Person1.FirstName ?? "No name";
                            children.Order = eventItem.Person1.Order ?? 1;
                            children.Class = eventItem.Person1.Sex.ToString();

                            var eventSonPerson = GetEventsPerson(eventItem.Person1.Id);
                            if (eventSonPerson != null)
                            {


                                foreach (var item in eventSonPerson)
                                {
                                    if (item.Id != eventItem.Id)
                                    {
                                        children = ProcessEventPerson(eventItem.Person1, eventSonPerson, null);

                                    }
                                }
                            }

                            foreach (var item in retValue.Marriages)
                            {
                                if (item.spouse.Id == marriage.spouse.Id)
                                {
                                    if (item.Children == null)
                                    {
                                        item.Children = new List<PersonVal>();
                                    }
                                    item.Children.Add(children);
                                }
                            }
                            //if (addChildren)
                            //{
                            //    marriage.Children.Add(children);
                            //    retValue.Marriages.Add(marriage);
                            //}
                            if (dontJumpMarriage)
                            {
                                marriage.Children.Add(children);
                                retValue.Marriages.Add(marriage);
                            }
                        }

                    }
                }

            }

            return retValue;
        }
        private PersonVal CreateSpouse(Person? person, PersonVal spouse)
        {
            spouse.Id = (person == null) ? 0 : person.Id;
            spouse.Name = (person == null) ? "No name" : (person.FirstName ?? "No name");
            spouse.Order = (person == null) ? 1 : person.Order ?? 1;
            spouse.Class = (person == null) ? Gender.man.ToString() : person.Sex.ToString();
     //       spouse.IsMarriage = true;

            return spouse;
        }
        private List<Event> GetEventsPerson(int idPerson)
        {
            var ret = new List<Event>();
            ret = context.Event.Where(m =>
                                          //m.Description != "" && 
                                          m.Person1.Id.Equals(idPerson)
                                          || m.Person2.Id.Equals(idPerson)
                                          || m.Person3.Id.Equals(idPerson)
                                          )
                                          .Include(x => x.Person1)
                                          .Include(x => x.Person2)
                                          .Include(x => x.Person3)
                                          .Include(x => x.EventType)
                                          .ToList();

            return ret;
        }

        private string GetOtherSex(Gender sex)
        {
            var retval = Gender.man.ToString();
            foreach (int i in Enum.GetValues(typeof(Gender)))
            {
                if (i != (int)sex)
                {
                    Enum.TryParse(i.ToString(), out Gender value);
                    retval = value.ToString();
                }
            }
            return retval;
        }
    }
}

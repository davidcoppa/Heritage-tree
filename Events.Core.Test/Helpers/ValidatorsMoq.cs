using Events.Core.Common.Validators;
using EventsManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Core.Test.Helpers
{
    public class ValidatorsMoq : IDataValidator
    {

        public bool ValidateObject<Entity>(Entity myObject) where Entity : class
        {

            switch (myObject)
            {
                case Person p:
                    if (p.FirstName != null)
                    {
                        return false;
                    }
                    return true;
                case ParentPerson pp:
                    if (pp.Description != null)
                    {
                        return false;
                    }
                    return true;
                default:
                    return false;
            }
        }
    }
}

using Events.Core.Common.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Core.Test.Helpers
{
    internal class MessagesMoq : IMessages
    {
        public string BadRequestModelNullOrInvalid { get => "Model is null or not valid"; }
        public string BadRequestModelInvalid { get => "Model is not valid"; }
        public string EventTypeExistingDatabase { get => "An Event Type {0} already exist in the database"; }

        public string EventEmpty { get => "An event needs at least one person"; }
        public string EventNotFound { get => "We couldn't find the event"; }
        public string ParentPersonNotFound { get => "We couldn't find the parent person"; }

        public string PersonNotFound { get => "We couldn't find the person"; }

        public string EventTypeNotFound { get => "We couldn't find the Event Type"; }

        public string CountryEmpty { get => "We couldn't find the Country"; }
    }
}

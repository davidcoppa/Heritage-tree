namespace Events.Core.Common.Messages
{
    public class En_Messages: IMessages
    {
        public string BadRequestModelNullOrInvalid { get => "Model is null or not valid"; }
        public string BadRequestModelInvalid { get => "Model is not valid"; }
        public string EventTypeExistingDatabase { get => "An Event Type {0} already exist in the database"; }

        public string EventEmpty { get => "An event needs at least one person"; }
        public string PersonNotFound { get => "We couldn't find the person"; }
        public string EventNotFound{ get => "We couldn't find the event"; }
        public string EventTypeNotFound { get => "We couldn't find the event type"; }
        public string ParentPersonNotFound{ get => "We couldn't find the parent person"; }


        //


    }
}

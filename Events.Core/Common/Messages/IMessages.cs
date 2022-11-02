namespace Events.Core.Common.Messages
{
    public interface IMessages
    {
        public string BadRequestModelNullOrInvalid { get; }
        public string BadRequestModelInvalid { get; }

        public string EventTypeExistingDatabase { get; }
        public string EventEmpty { get; }
        public string EventNotFound { get; }
        public string EventTypeNotFound { get; }

        public string PersonNotFound { get; }
        public string ParentPersonNotFound { get; }

        public string CountryEmpty { get; }



    }
}

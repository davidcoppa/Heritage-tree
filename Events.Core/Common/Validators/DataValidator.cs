using EventsManager.Model;

namespace Events.Core.Common.Validators
{
    public class DataValidator: IDataValidator
    {
        public bool ValidateObject<Entity> (Entity myObject) where Entity : class
         {
            return myObject.GetType().GetProperties()
            .Where(p => p.GetValue(myObject) is string) // selecting only string props
            .All(p => string.IsNullOrWhiteSpace((p.GetValue(myObject) as string)));//check if all strings are null

        }

        
    }
}

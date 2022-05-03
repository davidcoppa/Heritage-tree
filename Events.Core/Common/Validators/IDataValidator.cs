using EventsManager.Model;

namespace Events.Core.Common.Validators
{
    public interface IDataValidator
    {
        bool ValidateObject<Entity>(Entity myObject) where Entity : class;
    }
}

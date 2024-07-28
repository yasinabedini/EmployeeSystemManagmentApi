using System.Domain.Common.Event;

namespace System.Domain.Common.Entities
{
    public interface IAggregateRoot
    {
        void ClearEvents();
        IEnumerable<IDomainEvent> GetEvents();
    }
}
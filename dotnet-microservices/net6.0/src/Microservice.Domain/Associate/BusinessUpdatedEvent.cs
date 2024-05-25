using GoodToCode.Shared.Domain;
using Microservice.Domain;

namespace Microservice.Domain
{
    public sealed class BusinessUpdatedEvent : IDomainEvent<IBusiness>
    {
        public IBusiness Item { get; }

        public BusinessUpdatedEvent(IBusiness item)
        {
            Item = item;
        }
    }
}

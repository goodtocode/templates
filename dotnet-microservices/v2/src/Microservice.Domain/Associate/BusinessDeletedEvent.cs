
using GoodToCode.Shared.Domain;
using Microservice.Domain;

namespace Microservice.Domain
{
    public sealed class BusinessDeletedEvent : IDomainEvent<IBusiness>
    {
        public IBusiness Item { get; }

        public BusinessDeletedEvent(IBusiness item)
        {
            Item = item;
        }
    }
}

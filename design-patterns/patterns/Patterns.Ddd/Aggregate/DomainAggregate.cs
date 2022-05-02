using System.Collections.Generic;

namespace GoodToCode.Shared.Patterns.Ddd
{
    public abstract class DomainAggregate<TAggregate> : IDomainAggregate<TAggregate> where TAggregate : IDomainAggregate<TAggregate>
    {
        private readonly List<IDomainEvent<TAggregate>> _domainEvents = new List<IDomainEvent<TAggregate>>();
        public IReadOnlyList<IDomainEvent<TAggregate>> DomainEvents => _domainEvents;

        protected DomainAggregate()
        {
        }

        protected void RaiseDomainEvent(IDomainEvent<TAggregate> domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is DomainAggregate<TAggregate> other))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (GetType() != other.GetType())
                return false;

            return false;
        }

        public static bool operator ==(DomainAggregate<TAggregate> a, DomainAggregate<TAggregate> b)
        {
            if (a is null && b is null)
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(DomainAggregate<TAggregate> a, DomainAggregate<TAggregate> b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (GetType().ToString()).GetHashCode();
        }
    }
}

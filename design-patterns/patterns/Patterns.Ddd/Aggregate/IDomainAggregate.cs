using System;
using System.Collections.Generic;

namespace GoodToCode.Shared.Patterns.Ddd
{
    public interface IDomainAggregate<TAggregate> : IDomainObject
    {
        IReadOnlyList<IDomainEvent<TAggregate>> DomainEvents { get; }
        void ClearDomainEvents();
    }
}
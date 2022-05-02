using System;
using System.Collections.Generic;

namespace GoodToCode.Templates.Patterns.Ddd
{
    public interface IDomainAggregate<TAggregate> : IDomainObject
    {
        IReadOnlyList<IDomainEvent<TAggregate>> DomainEvents { get; }
        void ClearDomainEvents();
    }
}
using System;
using System.Collections.Generic;

namespace GoodToCode.Templates.Patterns.Ddd
{
    public interface IDomainEntity<TModel> : IDomainObject, IEntity
    {
        void RaiseDomainEvent(IDomainEvent<TModel> domainEvent);
        void ClearDomainEvents();
    }
}
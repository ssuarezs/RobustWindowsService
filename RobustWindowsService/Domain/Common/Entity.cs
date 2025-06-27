using System;
using System.Collections.Generic;

namespace RobustWindowsService.Domain
{
    public abstract class Entity
    {
        private readonly List<IDomainEvent> _domainEvents = new List<IDomainEvent>();

        public Guid Id { get; protected set; }
        public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        public void ClearDomainEvents() => _domainEvents.Clear();
        protected void Raise(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);
    }
}

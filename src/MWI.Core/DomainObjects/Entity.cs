﻿using MWI.Core.Messages;

namespace MWI.Core.DomainObjects
{
    public abstract class Entity
    {
        public Guid Id { get; set; }

        private List<Event> _notifications = new();
        public IReadOnlyCollection<Event> Notificacoes => _notifications.AsReadOnly();

        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        public void AddEvent(Event eventMessage)
        {
            _notifications = _notifications ?? new List<Event>();
            _notifications.Add(eventMessage);
        }

        public void RemoveEvent(Event eventMessage)
        {
            _notifications?.Remove(eventMessage);
        }

        public void ClearEvents()
        {
            _notifications?.Clear();
        }

        public override bool Equals(object? obj)
        {
            var compareTo = obj as Entity;

            if (ReferenceEquals(this, compareTo)) return true;
            if (ReferenceEquals(null, compareTo)) return false;

            return Id.Equals(compareTo.Id);
        }

        public static bool operator ==(Entity a, Entity b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entity a, Entity b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (GetType().GetHashCode() * 907) + Id.GetHashCode();
        }

        public override string ToString()
        {
            return $"{GetType().Name} [Id={Id}]";
        }

        public virtual bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}

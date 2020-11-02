namespace ConferenceDude.Domain.Session
{
    using System;

    public readonly struct SessionIdentity : IEquatable<SessionIdentity>
    {
        public int Id { get; }

        public SessionIdentity(int id)
        {
            Id = id;
        }

        public static SessionIdentity NullIdentity => new SessionIdentity(0);

        public bool Equals(SessionIdentity other)
        {
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            return obj is SessionIdentity other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Id;
        }

        public static bool operator ==(SessionIdentity left, SessionIdentity right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(SessionIdentity left, SessionIdentity right)
        {
            return !left.Equals(right);
        }

        public static bool operator ==(int left, SessionIdentity right)
        {
            return left.Equals(right.Id);
        }

        public static bool operator ==(SessionIdentity left, int right)
        {
            return left.Id.Equals(right);
        }

        public static bool operator !=(int left, SessionIdentity right)
        {
            return !left.Equals(right.Id);
        }

        public static bool operator !=(SessionIdentity left, int right)
        {
            return !left.Id.Equals(right);
        }

        public static implicit operator int(SessionIdentity identity)
        {
            return identity.Id;
        }

        public static explicit operator SessionIdentity(int identity)
        {
            return new SessionIdentity(identity);
        }

        public override string ToString()
        {
            return Id.ToString();
        }
    }
}

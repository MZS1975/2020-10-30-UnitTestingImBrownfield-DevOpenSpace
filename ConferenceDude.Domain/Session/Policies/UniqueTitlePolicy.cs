namespace ConferenceDude.Domain.Session.Policies
{
    using System.Collections.Generic;
    using System.Linq;

    public class UniqueTitlePolicy : ISessionPolicy
    {
        public SessionPolicy ViolationCode { get; } = SessionPolicy.UniqueTitle;

        public bool Check(IEnumerable<Session> sessions, Session sessionToValidate)
        {
            return !sessions.Any(s => (s.Id != sessionToValidate.Id || sessionToValidate.Id == SessionIdentity.NullIdentity) && s.Title == sessionToValidate.Title);
        }
    }
}

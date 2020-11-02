namespace ConferenceDude.Domain.Session.Policies
{
    using System.Collections.Generic;

    public interface ISessionPolicyService
    {
        PolicyCheckResult<SessionPolicy> VerifySession(IEnumerable<Session> sessions, Session session);
    }
}

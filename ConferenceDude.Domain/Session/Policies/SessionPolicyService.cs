namespace ConferenceDude.Domain.Session.Policies
{
    using System.Collections.Generic;

    public class SessionPolicyService : ISessionPolicyService
    {
        private readonly SessionPolicyEvaluator _policyEvaluator;

        public SessionPolicyService()
        {
            _policyEvaluator = new SessionPolicyEvaluator();
            _policyEvaluator.AddPolicy(new UniqueTitlePolicy());
        }

        public PolicyCheckResult<SessionPolicy> VerifySession(IEnumerable<Session> sessions, Session session)
        {
            return _policyEvaluator.Evaluate(sessions, session);
        }
    }
}
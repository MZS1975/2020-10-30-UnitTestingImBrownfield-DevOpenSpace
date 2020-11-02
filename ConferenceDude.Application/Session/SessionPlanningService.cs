namespace ConferenceDude.Application.Session
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Domain;
    using Domain.Session;
    using Domain.Session.Policies;

    public class SessionPlanningService : ISessionPlanningService
    {
        private readonly ISessionRestClient _sessionClient;
        private readonly ISessionTransportConverter _sessionTransportConverter;
        private readonly ISessionPolicyService _sessionPolicyService;

        public SessionPlanningService(ISessionRestClient sessionClient, ISessionTransportConverter sessionTransportConverter, ISessionPolicyService sessionPolicyService)
        {
            _sessionClient = sessionClient;
            _sessionTransportConverter = sessionTransportConverter;
            _sessionPolicyService = sessionPolicyService;
        }

        public async Task<IEnumerable<Session>> ListSessionsAsync()
        {
            var sessions = await _sessionClient.ListAsync();
            return sessions.Select(s => _sessionTransportConverter.ToSession(s)!);
        }

        public async Task<Session?> GetSessionAsync(SessionIdentity id)
        {
            return _sessionTransportConverter.ToSession(await _sessionClient.GetAsync(id));
        }

        public async Task<PolicyCheckResult<SessionPolicy>> StoreSessionAsync(Session session)
        {
            var sessions = await ListSessionsAsync();

            var result = _sessionPolicyService.VerifySession(sessions, session);
            if (!result.IsValid)
            {
                return result;
            }

            if (session.IsNew)
            {
                await _sessionClient.CreateAsync(_sessionTransportConverter.FromSession(session));
            }
            else
            {
                await _sessionClient.UpdateAsync(_sessionTransportConverter.FromSession(session), session.Id);
            }

            return result;
        }

        public async Task RemoveSessionAsync(SessionIdentity id)
        {
            if (id != SessionIdentity.NullIdentity)
            {
                await _sessionClient.DeleteAsync(id);
            }
        }
    }
}

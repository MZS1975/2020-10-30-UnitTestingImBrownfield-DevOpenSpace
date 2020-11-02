namespace ConferenceDude.UI.Tests.Unit
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Application.Conference;
    using Application.Session;
    using Domain;
    using Domain.Session;
    using Domain.Session.Policies;

    public class ConferencePlanningServiceFake : IConferencePlanningService
    {
        private readonly SessionPlanningServiceFake _sessionPlanner;
        public List<Session> Sessions { get; }

        public bool ShouldThrowOnSave { get; private set; }

        public bool ShouldThrowOnDelete { get; private set; }


        public ConferencePlanningServiceFake()
        {
            _sessionPlanner = new SessionPlanningServiceFake(this);
            Sessions = new List<Session>();
        }

        public ISessionPlanningService SessionPlanner => _sessionPlanner;

        public void Setup(IEnumerable<Session> sessions, bool shouldThrowOnSave = false, bool shouldThrowOnDelete = false)
        {
            Sessions.Clear();
            Sessions.AddRange(sessions);

            ShouldThrowOnSave = shouldThrowOnSave;
            ShouldThrowOnDelete = shouldThrowOnDelete;
        }

        private class SessionPlanningServiceFake : ISessionPlanningService
        {
            private readonly ConferencePlanningServiceFake _owner;
            private readonly SessionPolicyService _sessionPolicyService;

            public SessionPlanningServiceFake(ConferencePlanningServiceFake owner)
            {
                _owner = owner;
                _sessionPolicyService = new SessionPolicyService();
            }

            public async Task<IEnumerable<Session>> ListSessionsAsync()
            {
                return await Task.FromResult(_owner.Sessions);
            }

            public async Task<Session?> GetSessionAsync(SessionIdentity id)
            {
                return await Task.FromResult(_owner.Sessions.FirstOrDefault(s => s.Id == id));
            }

            public async Task<PolicyCheckResult<SessionPolicy>> StoreSessionAsync(Session session)
            {
                if (_owner.ShouldThrowOnSave)
                {
                    throw new HttpRequestException();
                }

                var result = _sessionPolicyService.VerifySession(_owner.Sessions, session);
                if (result.IsValid)
                {
                    if (_owner.Sessions.FirstOrDefault(s => s.Id == session.Id) != null)
                    {
                        await RemoveSessionAsync(session.Id);
                    }

                    _owner.Sessions.Add(session);
                }

                return await Task.FromResult(result);
            }

            public Task RemoveSessionAsync(SessionIdentity id)
            {
                if (_owner.ShouldThrowOnDelete)
                {
                    throw new HttpRequestException();
                }

                _owner.Sessions.RemoveAll(s => s.Id == id);
                return Task.CompletedTask;
            }
        }
    }
}

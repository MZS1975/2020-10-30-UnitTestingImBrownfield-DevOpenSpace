namespace ConferenceDude.Application.Session
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Domain;
    using Domain.Session;
    using Domain.Session.Policies;

    public interface ISessionPlanningService
    {
        Task<IEnumerable<Session>> ListSessionsAsync();

        Task<Session?> GetSessionAsync(SessionIdentity id);

        Task<PolicyCheckResult<SessionPolicy>> StoreSessionAsync(Session session);

        Task RemoveSessionAsync(SessionIdentity id);
    }
}

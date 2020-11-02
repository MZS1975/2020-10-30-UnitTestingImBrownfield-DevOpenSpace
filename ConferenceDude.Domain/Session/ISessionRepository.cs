namespace ConferenceDude.Domain.Session
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ISessionRepository
    {
        Task<IEnumerable<Session>> ListAsync();

        Task<Session?> GetAsync(SessionIdentity identity);

        Task<Session> CreateAsync(Session entity);

        Task<Session> UpdateAsync(Session entity, SessionIdentity identity);

        Task DeleteAsync(SessionIdentity identity);
    }
}

namespace ConferenceDude.Storage.Sessions
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Domain.Session;
    using Microsoft.EntityFrameworkCore;

    public class SessionRepository : ISessionRepository
    {
        private readonly ConferenceContext _context;
        private readonly ISessionStorageConverter _sessionStorageConverter;

        public SessionRepository(ConferenceContext context, ISessionStorageConverter sessionStorageConverter)
        {
            _context = context;
            _sessionStorageConverter = sessionStorageConverter;
        }

        public async Task<IEnumerable<Session>> ListAsync()
        {
            return await _context.Sessions.Select(s => _sessionStorageConverter.FromDatabaseModel(s)).ToListAsync();
        }

        public async Task<Session?> GetAsync(SessionIdentity identity)
        {
            var dbSession = await _context.Sessions.FindAsync(identity.Id);
            return dbSession != null ? _sessionStorageConverter.FromDatabaseModel(dbSession) : null;
        }

        public async Task<Session> CreateAsync(Session session)
        {
            var addedSession = await _context.Sessions.AddAsync(_sessionStorageConverter.ToDatabaseModel(session));
            await _context.SaveChangesAsync();

            return _sessionStorageConverter.FromDatabaseModel(addedSession.Entity);
        }

        public async Task<Session> UpdateAsync(Session session, SessionIdentity identity)
        {
            _context.Entry(_sessionStorageConverter.ToDatabaseModel(session)).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await SessionExists(identity))
                {
                    throw;
                }
            }

            return (await GetAsync(identity))!;
        }

        public async Task DeleteAsync(SessionIdentity identity)
        {
            var session = await _context.Sessions.FindAsync(identity.Id);
            if (session != null)
            {
                _context.Sessions.Remove(session);
                await _context.SaveChangesAsync();
            }
        }

        private async Task<bool> SessionExists(SessionIdentity id)
        {
            return await _context.Sessions.AnyAsync(e => e.Id == id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConferenceDude.Domain.Sessions;
using ConferenceDude.Server.Database;
using Microsoft.EntityFrameworkCore;

namespace ConferenceDude.Server.DomainAdapter
{
    public class SessionRepository : ISessionRepository
    {
        private readonly Func<ConferenceContext> _conferenceContextBuilder;

        public SessionRepository(Func<ConferenceContext> conferenceContextBuilder)
        {
            _conferenceContextBuilder = conferenceContextBuilder;
        }

        public async Task<IReadOnlyCollection<Session>> GetAll()
        {
            using (var ctx = _conferenceContextBuilder())
            {
                var sessions = await ctx.Sessions
                    .Select(s => s.ToSession())
                    .ToListAsync()
                    .ConfigureAwait(false);
                return sessions;
            }
        }

        public async Task<Session> GetById(int id)
        {
            using (var ctx = _conferenceContextBuilder())
            {
                var session = await ctx.Sessions
                    .FindAsync(id)
                    .ConfigureAwait(false);
                return session.ToSession();
            }
        }

        public async Task<Session> GetByTitle(string title)
        {
            using (var ctx = _conferenceContextBuilder())
            {
                var session = await ctx.Sessions
                    .FirstOrDefaultAsync(s => s.Title == title)
                    .ConfigureAwait(false);

                return session?.ToSession();
            }
        }

        public async Task<int> Create(Session session)
        {
            using (var ctx = _conferenceContextBuilder())
            {
                var sessionEntity = session.ToSessionEntity();
                ctx.Sessions.Add(sessionEntity);
                await ctx.SaveChangesAsync().ConfigureAwait(false);

                return sessionEntity.Id;
            }
        }
    }
}
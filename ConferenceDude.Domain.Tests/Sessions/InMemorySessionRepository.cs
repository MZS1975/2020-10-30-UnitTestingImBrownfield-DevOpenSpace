using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using ConferenceDude.Domain.Sessions;

namespace ConferenceDude.Domain.Tests.Sessions
{
    [ExcludeFromCodeCoverage]
    internal class InMemorySessionRepository : ISessionRepository
    {
        public InMemorySessionRepository()
        {
            Sessions = new List<Session>();
        }

        public List<Session> Sessions { get; set; }

        public Task<IReadOnlyCollection<Session>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Session> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Session> GetByTitle(string title)
        {
            return Task.FromResult(Sessions.FirstOrDefault(s => s.Title == title));
        }

        public Task<int> Create(Session session)
        {
            var maxId = Sessions.Any() ? Sessions.Max(s => s.Id) : 0;
            session.Id = maxId + 1;
            Sessions.Add(session);

            return Task.FromResult(session.Id);
        }
    }
}
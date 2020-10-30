using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConferenceDude.Domain.Sessions
{
    public interface ISessionRepository
    {
        Task<IReadOnlyCollection<Session>> GetAll();
        Task<Session> GetById(int id);
    }
}
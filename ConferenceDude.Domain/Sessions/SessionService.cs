using System.Threading.Tasks;
using ConferenceDude.Domain.Shared;

namespace ConferenceDude.Domain.Sessions
{
    public class SessionService
    {
        private readonly ISessionRepository _sessionRepository;

        public SessionService(ISessionRepository sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }

        public async Task<(ValidationResult validationResult, int id)> Create(Session session)
        {
            var createdSessionId = 0;

            var validationResult = session.Validate();
            var existingSession = await _sessionRepository.GetByTitle(session.Title);
            if (existingSession != null)
            {
                validationResult.AddError(
                    nameof(Session.Title), 
                    "Eine Session mit diesem Titel ist bereits vorhanden.");
            }
            else
            {
                createdSessionId = await _sessionRepository.Create(session).ConfigureAwait(false);
            }

            return (validationResult, createdSessionId);
        }
    }
}
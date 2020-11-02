namespace ConferenceDude.Application.Session
{
    using Domain.Session;

    public class SessionTransportConverter : ISessionTransportConverter
    {
        public Session? ToSession(in SessionDto? sessionDto)
        {
            return sessionDto.HasValue
                       ? new Session(new SessionIdentity(sessionDto.Value.Id), sessionDto.Value.Title, sessionDto.Value.Abstract)
                       : null;
        }

        public SessionDto FromSession(Session session)
        {
            return new SessionDto(session.Id, session.Title, session.Abstract);
        }
    }
}

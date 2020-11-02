namespace ConferenceDude.Application.Session
{
    using Domain.Session;

    public interface ISessionTransportConverter
    {
        Session? ToSession(in SessionDto? sessionDto);

        SessionDto FromSession(Session session);
    }
}

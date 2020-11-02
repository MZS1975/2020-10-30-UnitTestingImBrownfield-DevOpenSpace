namespace ConferenceDude.Application.Session
{
    using Infrastructure;

    public interface ISessionRestClient : IGenericRestClient<SessionDto, int>
    {
    }
}

namespace ConferenceDude.Application.Session
{
    using System;
    using System.Net.Http;
    using Infrastructure;
    using Microsoft.Extensions.Configuration;

    public class SessionRestClient : GenericRestClient<SessionDto, int>, ISessionRestClient
    {
        public SessionRestClient(IConfiguration configuration, HttpClient? httpClient = null) : base(configuration, "sessions", httpClient)
        {
        }
    }
}

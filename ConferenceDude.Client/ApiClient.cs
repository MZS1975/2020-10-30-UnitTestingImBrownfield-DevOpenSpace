using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ConferenceDude.Domain.Sessions;

namespace ConferenceDude.Client
{
    internal class ApiClient
    {
        private readonly HttpClient _client;

        public ApiClient()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("http://localhost:51262");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
        }

        public async Task<List<SessionModel>> AllSessions()
        {
            var response = await _client.GetStringAsync("api/Sessions");
            if (!string.IsNullOrEmpty(response))
            {
                var sessions = JsonSerializer.Deserialize<List<SessionModel>>(response,
                    new JsonSerializerOptions {PropertyNameCaseInsensitive = true});
                return sessions;
            }

            return new List<SessionModel>();
        }

        public async Task<bool> Save(Session session)
        {
            if (session.IsNew())
            {
                return await CreateSession(session.ToSessionModel());
            }

            return await UpdateSession(session.ToSessionModel());
        }

        public async Task<bool> DeleteSession(int sessionId)
        {
            var response = await _client.DeleteAsync($"api/sessions/{sessionId}");
            return response.IsSuccessStatusCode;
        }

        private async Task<bool> CreateSession(SessionModel session)
        {
            var sessionAsJson = JsonSerializer.Serialize(session);
            var content = new StringContent(sessionAsJson, Encoding.UTF8, MediaTypeNames.Application.Json);

            var response = await _client.PostAsync("api/Sessions", content);
            return response.IsSuccessStatusCode;
        }

        private async Task<bool> UpdateSession(SessionModel session)
        {
            var sessionAsJson = JsonSerializer.Serialize(session);
            var content = new StringContent(sessionAsJson, Encoding.UTF8, MediaTypeNames.Application.Json);

            var response = await _client.PutAsync($"api/Sessions/{session.Id}", content);
            return response.IsSuccessStatusCode;
        }
    }
}
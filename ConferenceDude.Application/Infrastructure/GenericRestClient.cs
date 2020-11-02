namespace ConferenceDude.Application.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Net.Mime;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json;

    public class GenericRestClient<TEntity, TEntityKey> : IGenericRestClient<TEntity, TEntityKey>
        where TEntity : struct
        where TEntityKey : struct
    {
        private readonly string _path;
        private readonly HttpClient _client;

        public GenericRestClient(IConfiguration configuration, string path, HttpClient? httpClient = null)
        {
            _path = path;
            _client = httpClient ?? new HttpClient
            {
                BaseAddress = new Uri(configuration.GetValue<string>("serverUri"))
            };

            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
        }

        public async Task<IEnumerable<TEntity>> ListAsync()
        {
            string? response = await _client.GetStringAsync(_path);
            return string.IsNullOrEmpty(response)
                       ? new List<TEntity>()
                       : JsonConvert.DeserializeObject<List<TEntity>>(response);
        }

        public async Task<TEntity?> GetAsync(TEntityKey identity)
        {
            string? response = await _client.GetStringAsync($"{_path}/{identity}");
            if (string.IsNullOrEmpty(response))
            {
                return default;
            }

            var entity = JsonConvert.DeserializeObject<TEntity>(response);
            return entity;

        }

        public async Task CreateAsync(TEntity entity)
        {
            var sessionAsJson = JsonConvert.SerializeObject(entity);
            var content = new StringContent(sessionAsJson, Encoding.UTF8, MediaTypeNames.Application.Json);

            var response = await _client.PostAsync(_path, content);
            var responseContent = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateAsync(TEntity entity, TEntityKey identity)
        {
            var sessionAsJson = JsonConvert.SerializeObject(entity);
            var content = new StringContent(sessionAsJson, Encoding.UTF8, MediaTypeNames.Application.Json);

            var response = await _client.PutAsync($"{_path}/{identity}", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(TEntityKey identity)
        {
            var response = await _client.DeleteAsync($"{_path}/{identity}");
            response.EnsureSuccessStatusCode();
        }
    }
}

namespace ConferenceDude.Application.Session
{
    using Newtonsoft.Json;

    public readonly struct SessionDto
    {
        [JsonConstructor]
        public SessionDto(int id, string title, string @abstract)
        {
            Id = id;
            Title = title;
            Abstract = @abstract;
        }

        public int Id { get; }
        public string Title { get; }
        public string Abstract { get; }
    }
}

using System;

namespace ConferenceDude.Domain
{
    public class Session
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Abstract { get; set; }
        public bool IsNew => Id == 0;
    }
}

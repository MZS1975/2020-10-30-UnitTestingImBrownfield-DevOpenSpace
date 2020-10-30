using System.Runtime.InteropServices.ComTypes;

namespace ConferenceDude.Domain
{
    public class SessionValidator
    {
        public bool IsValid(Session session, out string msg)
        {
            if (string.IsNullOrEmpty(session.Title))
            {
                msg = "Es muss ein Titel angegeben werden.";
                return false;
            }

            if (string.IsNullOrEmpty(session.Abstract))
            {
                msg = "Es muss ein Abstrakt angegeben werden.";
                return false;
            }

            msg = string.Empty;
            return true;
        }
    }
}
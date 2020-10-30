using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConferenceDude.Domain.Sessions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ConferenceDude.Server.Database;

namespace ConferenceDude.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionsController : ControllerBase
    {
        private readonly ISessionRepository _sessionRepository;

        public SessionsController(ISessionRepository sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }

        // GET: api/Sessions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SessionDto>>> GetSessions()
        {
            var sessions = await _sessionRepository.GetAll().ConfigureAwait(false);
            var sessionDtos = sessions.Select(s => s.ToSessionDto()).ToList();
            return sessionDtos;
        }

        // GET: api/Sessions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SessionDto>> GetSession(int id)
        {
            var session = await _sessionRepository.GetById(id);

            if (session == null)
            {
                return NotFound();
            }

            return session.ToSessionDto();
        }

        // PUT: api/Sessions/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSession(int id, SessionEntity session)
        {
            if (id != session.Id)
            {
                return BadRequest();
            }

            var context = new ConferenceContext();
            context.Entry(session).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await SessionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Sessions
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<SessionEntity>> PostSession(SessionEntity session)
        {
            var context = new ConferenceContext();
            context.Sessions.Add(session);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetSession", new { id = session.Id }, session);
        }

        // DELETE: api/Sessions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SessionEntity>> DeleteSession(int id)
        {
            var context = new ConferenceContext();
            var session = await context.Sessions.FindAsync(id);
            if (session == null)
            {
                return NotFound();
            }

            context.Sessions.Remove(session);
            await context.SaveChangesAsync();

            return session;
        }

        private async Task<bool> SessionExists(int id)
        {
            var context = new ConferenceContext();
            return await context.Sessions.AnyAsync(e => e.Id == id);
        }
    }
}

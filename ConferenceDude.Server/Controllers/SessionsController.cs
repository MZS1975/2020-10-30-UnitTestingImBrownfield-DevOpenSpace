namespace ConferenceDude.Server.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Application.Session;
    using Domain.Session;
    using Microsoft.AspNetCore.Mvc;

    [Route("[controller]")]
    [ApiController]
    public class SessionsController : ControllerBase
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly ISessionTransportConverter _sessionTransportConverter;

        public SessionsController(ISessionRepository sessionRepository, ISessionTransportConverter sessionTransportConverter)
        {
            _sessionRepository = sessionRepository;
            _sessionTransportConverter = sessionTransportConverter;
        }

        // GET: sessions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SessionDto>>> GetSessions()
        {
            var sessions = await _sessionRepository.ListAsync();
            return Ok(sessions.Select(s => _sessionTransportConverter.FromSession(s)));
        }

        // GET: api/Sessions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SessionDto>> GetSession(int id)
        {
            var session = await _sessionRepository.GetAsync(new SessionIdentity(id));

            if (session == null)
            {
                return NotFound(id);
            }

            return Ok(_sessionTransportConverter.FromSession(session));
        }

        // PUT: api/Sessions/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSession([FromRoute]int id, [FromBody]SessionDto session)
        {
            if (id != session.Id)
            {
                return BadRequest();
            }

            await _sessionRepository.UpdateAsync(_sessionTransportConverter.ToSession(session)!, new SessionIdentity(id));

            return NoContent();
        }

        // POST: api/Sessions
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<SessionDto>> PostSession([FromBody] SessionDto session)
        {
            var created = await _sessionRepository.CreateAsync(_sessionTransportConverter.ToSession(session)!);

            return CreatedAtAction("GetSession", new { id = created.Id }, _sessionTransportConverter.FromSession(created));
        }

        // DELETE: api/Sessions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSession(int id)
        {
            await _sessionRepository.DeleteAsync(new SessionIdentity(id));
            return Ok();
        }
    }
}

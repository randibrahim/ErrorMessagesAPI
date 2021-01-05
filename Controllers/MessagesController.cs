using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorMessagesAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ErrorMessagesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageRepository _repo;

        public MessagesController(IMessageRepository repo)
        {
            _repo = repo;
        }

        // GET api/messages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Message>>> Get()
        {
            return new ObjectResult(await _repo.GetAllMessages());
        }

        // GET api/messages/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Message>> Get(long id)
        {
            var message = await _repo.GetMessage(id);

            if (message == null)
                return new NotFoundResult();

            return new ObjectResult(message);
        }

        // POST api/messages
        [HttpPost]
        public async Task<ActionResult<Message>> Post([FromBody] Message message)
        {
            message.Id = await _repo.GetNextId();
            await _repo.Create(message);
            return new OkObjectResult(message);
        }

        // PUT api/messages/1
        [HttpPut("{id}")]
        public async Task<ActionResult<Message>> Put(long id, [FromBody] Message message)
        {
            var todoFromDb = await _repo.GetMessage(id);

            if (todoFromDb == null)
                return new NotFoundResult();

            message.Id = todoFromDb.Id;
            message.InternalId = todoFromDb.InternalId;

            await _repo.Update(message);

            return new OkObjectResult(message);
        }

        // DELETE api/messages/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var post = await _repo.GetMessage(id);

            if (post == null)
                return new NotFoundResult();

            await _repo.Delete(id);

            return new OkResult();
        }
    }
}


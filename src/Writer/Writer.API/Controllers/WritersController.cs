using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Writer.API.Models;

namespace Writer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WritersController : ControllerBase
    {
        private readonly List<WriterModel> _writers;

        public WritersController()
        {
            _writers = new List<WriterModel>
            {
                new WriterModel { Id = 1, Name = "John Doe" },
                new WriterModel { Id = 2, Name = "Jane Doe" },
                new WriterModel { Id = 3, Name = "Jane Doe" },
                new WriterModel { Id = 4, Name = "Jane Doe" }
            };
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_writers);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var writer = _writers.FirstOrDefault(w => w.Id == id);
            if (writer == null)
            {
                return NotFound();
            }

            return Ok(writer);
        }

        [HttpPost]
        public IActionResult Post([FromBody] WriterModel writer)
        {
            _writers.Add(writer);
            return CreatedAtAction(nameof(Get), new { id = writer.Id }, writer);
        }
    }
}

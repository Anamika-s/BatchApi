using BatchApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryPatternDemo.Repo;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BatchApi.Controllers

{

    [Route("api/[controller]")]
    [ApiController]
    public class BatchController : ControllerBase
    {
        private BatchRepo _repo;
        public BatchController(BatchRepo repo)

        {
            _repo = repo;
        }
        // GET: api/<BatchController>
        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            if (_repo.GetBatches().Count == 0)
                return BadRequest();
            else

                return Ok(_repo.GetBatches().ToList());
        }

        // GET api/<BatchController>/5
        [HttpGet("{id}")]
        [Authorize]
        public IActionResult Get(int id)
        {
            if (_repo.GetBatch(id) == null)
                return NotFound();
            else
                return Ok(_repo.GetBatch(id));
        }

        // POST api/<BatchController>
        [HttpPost]
        [Authorize(Roles="Admin")]
        public IActionResult Post(Batch batch)
        {
            _repo.AddBatch(batch);
            return Created("added", batch);
        }

        // PUT api/<BatchController>/5
        [HttpPut("{id}")]
        [Authorize(Roles ="Admin")]
        public IActionResult Put(int id, Batch batch)
        {
            _repo.EditBatch(id, batch);
            return Ok();
        }

        // DELETE api/<BatchController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult Delete(int id)
        {
             _repo.DeleteBatch(id);
            return Ok();
        }
    }
}

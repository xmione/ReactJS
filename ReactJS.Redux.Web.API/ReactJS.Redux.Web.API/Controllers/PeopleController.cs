using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
#if CF
    using EF = ReactJS.Redux.CodeFirst;
    using EFModels = ReactJS.Redux.CodeFirst.Models;
#else
    using EF = ReactJS.Redux.DatabaseFirst;
    using EFModels = ReactJS.Redux.DatabaseFirst.Models;
#endif

using System;
using System.Threading.Tasks;
using ReactJS.Redux.Repositories;
using Microsoft.AspNetCore.Cors;

namespace ReactJS.Redux.Web.API.Controllers
{
    [Produces("application/json")]
    [Route("[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly IDataRepository<EFModels.Person> _repository;

        public PeopleController(IDataRepository<EFModels.Person> repository)
        {
            _repository = repository;
        }

        // GET: api/people
        [HttpGet]
        [EnableCors("AllowOrigin")]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                return Ok(await _repository.GetAll());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database. " + ex.Message);
            }
        }
        // GET: api/people/addmockdata
        
        [HttpGet("addmockdata")]
        public async Task<ActionResult> AddMockData()
        {
            try
            {
                return Ok(await _repository.AddMockData());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database. " + ex.Message);
            }
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EFModels.Person>> Get(int id)
        {
            try
            {
                return Ok(await _repository.Get(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database. " + ex.Message);
            }
        }

        // PUT: api/TodoItems/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, EFModels.Person item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            try
            {
                return Ok(await _repository.Add(item));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database. " + ex.Message);
            }
        }

        // POST: api/TodoItems
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<EFModels.Person>> PostTodoItem(EFModels.Person item)
        {
            try
            {
                return Ok(await _repository.Add(item));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database. " + ex.Message);
            }
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

            _repository.Delete(id);

        }
    }
}
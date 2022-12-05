using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TryingBackend.Models;
using TryingBackend.Repository;

namespace TryingBackend.Controllers
{
    [Produces("application/json")]
    [Route("api/persons")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly IPersonRepository _repo;

        public PeopleController(IPersonRepository repo)
        {
            _repo = repo;
        }


        /// <summary>
        /// GET the list of all the persons present in the API
        /// </summary>
        /// <returns>Returning lists of persons </returns>
        ///  
        /// <remarks>
        /// Sample request: GET api/Persons
        /// 
        /// </remarks>
        /// <response code="200">Success in the following operation </response>
        /// <response code="404">Not able to find and get the desired request</response> 
        /// <response code="500">Internal Server Error has been generated</response> 
        //GET api/Persons    
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<IEnumerable<Person>>> GetAll()
        {
            try
            {
                var getallpersons = await _repo.Getallpersons();
                if (getallpersons == null)
                {
                    return NotFound();
                }

                return Ok(getallpersons.OrderBy(getallpersons => getallpersons.FirstName).ToList());
            }
            catch (Exception)
            {

                return StatusCode(500, "Internal server error");
            }
        }


        /// <summary>
        /// GET the persons if present in the API with the given uuid
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>Returning the persons </returns>
        /// <remarks>
        /// 
        /// GET api/Person/{id}
        /// 
        /// Sample request ID: {uuid}
        /// 
        /// </remarks>
        /// <response code="200">Success in the following operation </response>
        /// <response code="400">A bad request is created</response> 
        /// <response code="404">Not able to find and get the desired id</response> 
        /// <response code="500">Internal Server Error has been generated</response> 
        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetPerson(Guid id)
        {
            try
            {
                var getpersonid = await _repo.Get(id);
                if (getpersonid == null)
                {
                    return NotFound();
                }
                return Ok(getpersonid);
            }
            catch (Exception)
            {

                return StatusCode(500, "Internal server error");
            }
        }



        /// <summary>
        /// POST the person in the given API
        /// </summary>
        /// <param name="personCreateDto">TO CREATE A PERSON</param>
        /// <returns>Person is posted </returns>
        /// <remarks>
        /// Sample request: POST api/Person
        /// 
        ///     {        
        ///       "firstName": "Mike",
        ///       "lastName": "Andrew",
        ///       "age": 45      
        ///     }
        /// 
        /// </remarks>
        /// <response code="201">Successfully created a new person</response>
        /// <response code="400">A bad request is created</response> 
        /// <response code="500">Internal Server Error has been generated</response> 
        //POST api/Person
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Person>> Addperson(Person person)
        {
            try
            {
                if (person == null)
                    return NoContent();
                if (ModelState.IsValid)
                {
                    Guid obj = Guid.NewGuid();
                    person.Id = obj;
                    await _repo.CreatePerson(person);
                    return CreatedAtAction("GetPerson", new { id = person.Id }, person);
                }
                else
                    return BadRequest();
            }
            catch (Exception)
            {

                return StatusCode(500, "Internal server error");
            }
        }



        /// <summary>
        /// PUT or edit the person present in the given API
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="personUpdateDto">TO UPDATE A PERSON</param>
        /// <returns>Person details has been edited </returns>
        /// <remarks>
        /// Sample request: POST api/Person/{id}
        ///  
        ///     {        
        ///       "firstName": "Mike",
        ///       "lastName": "Andrew",
        ///       "age": 45      
        ///     }
        /// 
        /// </remarks>
        /// <response code="204">Request has been succeeded with no content access</response> 
        /// <response code="400">A bad request is created with the invalid model</response> 
        /// <response code="404">Not able to find and get the desired id</response> 
        /// <response code="500">Internal Server Error has been generated</response> 
        //PUT api/Persons/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> UpdateOwner(Guid id, [FromBody] Person person)
        {
            try
            {
                if (person == null)
                {
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
                if (id != person.Id)
                {
                    return NotFound();
                }

                await _repo.UpdatePerson(person);
                _repo.SaveChanges();
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }


        /// <summary>
        /// DELETE the person if given in the API
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>Person is deleted </returns>
        /// <remarks>
        /// 
        /// DELETE api/Persons/{id}
        /// 
        /// Sample Request :{uuid}
        /// 
        /// </remarks>
        /// <response code="204">Request has been succeeded with no content access</response> 
        /// <response code="404">Not able to find and get the desired id</response> 
        /// <response code="500">Internal Server Error has been generated</response>
        //DELETE api/Persons/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public ActionResult DeletePerson(Guid id)
        {
            try
            {
                var personid = _repo.Get(id);
                if (personid == null)
                {
                    return NotFound();
                }
                _repo.DeletePerson(id);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }



    }
}

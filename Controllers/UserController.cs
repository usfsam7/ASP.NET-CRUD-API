using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using crud.Models;
using CRUD.Data;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }


        // GET ALL Users
        // GET api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {     
             return Ok(await _context.Users.ToListAsync());
        }



        // Get a specific user
        // GET: api/Users/id
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }
            return user;
        }




        // add a new user
        // POST api/Users
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            // check if the user provided data or not
            if (user == null)
            {
                return BadRequest();
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUsers), new { id = user.Id }, user);
        }


        // Update user data
        // PUT api/Users/{id}
        [HttpPut]
        public async Task<ActionResult<User>> PutUser(int id, User user)
        {

            var userInDb = await _context.Users.FindAsync(id);
            if (userInDb == null)
            {
                return NotFound();
            }

            // Update user data
            userInDb.Username = user.Username;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok(user);
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

        // Delete a specific user
         // 
    }
}

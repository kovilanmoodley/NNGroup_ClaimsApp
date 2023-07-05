using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NNGroup_FrontEnd.Server.DataAccess;
using ShareModels.Models;

namespace NNGroup_FrontEnd.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly IClaimDataAccess _context;
        public UserController(IClaimDataAccess context)
        {
            _context = context;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<User>> Get()
        {
            try
            {
                List<User> users = _context.GetAllUsers()!;

                if (users.Count() == 0)
                    return BadRequest();
                else
                    return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        
    }
}

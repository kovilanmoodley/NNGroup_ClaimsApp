using Microsoft.AspNetCore.Mvc;
using NNGroup_DataManager.DataAccess;
using ShareModels.Models;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NNGroup_DataManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubmitClaimController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IClaimDataAccess _context;
        public SubmitClaimController(IConfiguration config,IClaimDataAccess context)
        {
            _config = config;
            _context = context;
        }


        // POST api/<SubmitClaimController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Create(ClaimRequest claimRequest)
        {
            int newId = _context.MakeClaim(claimRequest);
            if (newId == -1)
                return BadRequest("Employee or Client List is empty");
            if (newId == -2)
                return NotFound("Client ID not found");
            var obj = new
            {
                ClaimID = newId
            };
            var json = JsonSerializer.Serialize(obj);
            return Ok(json);
        }
        

        
    }
}

﻿using Microsoft.AspNetCore.Mvc;
using NNGroup_DataManager.DataAccess;
using ShareModels.Models;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NNGroup_DataManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CancelClaimController : ControllerBase
    {
        public readonly IClaimDataAccess _context;
        public CancelClaimController(IClaimDataAccess context)
        {
            _context = context;
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<ServiceResult> CancelClaim([FromBody] ClaimStatusChangeRequest claimStatusChangeRequest)
        {
            string result = _context.CancelClaim(claimStatusChangeRequest.ClaimID, claimStatusChangeRequest.ID);
            ServiceResult serviceResult = new();

            if (result != "Ok")
            {
                serviceResult.ResultStatus = result;
                return BadRequest(result);
            }
            else
            {
                serviceResult.ResultStatus = "Claim has been cancelled";
                return Ok(serviceResult.ResultStatus);
            }

        }
    }
}

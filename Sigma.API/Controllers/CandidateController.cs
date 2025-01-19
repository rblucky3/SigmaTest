using Sigma.API.Model;
using Sigma.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Sigma.Service.Settings.Interfaces;

namespace Sigma.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly ICandidateService _candidateService;

        public CandidateController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdateCandidate([FromBody] Candidate candidate)
        {
            if (!ModelState.IsValid || candidate == null)
            {
                // Collect validation error messages
                var validationMessages = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                // Return a BadRequest with validation errors
                return BadRequest(new ApiResponse
                {
                    StatusCode = 400,
                    Message = "Validation failed.",
                    ValidationMessages = validationMessages
                });
            }

            var successResponse = new ApiResponse();

            var result=  await _candidateService.AddOrUpdateCandidateAsync(candidate);       
            if(result)
            {
                successResponse.StatusCode = 200;
                successResponse.Message = "Candidate  added/updated successfully!";
            }
            else
            {
                successResponse.StatusCode = 200;
                successResponse.Message = "Some Error Occurred!";
            }

            return Ok(successResponse);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCandidates()
        {
            try
            {
                var candidates = await _candidateService.GetAllCandidateAsync();

                if (candidates == null || !candidates.Any())
                {
                    return NotFound(new { message = "No candidates found." });
                }

                return Ok(candidates);
            }
            catch (Exception ex)
            {
                // Log the error
                return StatusCode(500, new { message = "An error occurred while fetching records.", details = ex.Message });
            }
        }
        [HttpGet("{email}")]
        public async Task<IActionResult> GetCandidateByEmail(string email)
        {
            var candidate = await _candidateService.GetCandidateByEmailAsync(email);
            if (candidate == null)
            {
                return NotFound(new { message = "Candidate not found" });
            }

            return Ok(candidate);
        }
    }
}

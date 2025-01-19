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

            await _candidateService.AddOrUpdateCandidateAsync(candidate);

            var successResponse = new ApiResponse
            {
                StatusCode = 200,
                Message = "Candidate  added/updated successfully!",

            };

            return Ok(successResponse);
        }
    }
}

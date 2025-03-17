using Microsoft.AspNetCore.Mvc;
using quorum.domain.Interfaces;

namespace quorum.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LegislatorController : ControllerBase
    {
        private readonly ILegislatorService _legislatorService;

        public LegislatorController(ILegislatorService legislatorService)
        {
            _legislatorService = legislatorService;
        }

        [HttpGet("{id}/bills-supported")]
        public async Task<IActionResult> GetSupportedBills(int id)
        {
            var supportedBills = await _legislatorService.GetSupportedBills(id);

            return Ok(new { LegislatorId = id, SupportedBills = supportedBills });
        }

        [HttpGet("{id}/bills-opposed")]
        public async Task<IActionResult> GetOpposedBills(int id)
        {
            var opposedBills = await _legislatorService.GetOpposedBills(id);

            return Ok(new { LegislatorId = id, OpposedBills = opposedBills });
        }
    }
}

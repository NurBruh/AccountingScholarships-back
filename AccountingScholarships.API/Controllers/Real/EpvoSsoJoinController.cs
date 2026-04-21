using AccountingScholarships.Application.Queries.EpvoSso.EpvoJoin;
using AccountingScholarships.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace AccountingScholarships.API.Controllers.Real
{
    [ApiController]
    [Route("api/epvo")]
    public class EpvoSsoJoinController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IStoredProcedureRepository _spRepo;

        public EpvoSsoJoinController(IMediator mediator, IStoredProcedureRepository spRepo)
        {
            _mediator = mediator;
            _spRepo = spRepo;
        }
        // --- Students SSO Details -------------------------------------

        /// <summary>
        /// Возвращает детальную информацию по студентам из STUDENT_SSO
        /// с расшифровкой формы обучения, языка, профессии, специализации, факультета.
        /// </summary>
        [HttpGet("students")]
        public async Task<IActionResult> GetStudentSsoDetails(CancellationToken ct)
        {
            var result = await _mediator.Send(new GetStudentSsoDetailsQuery(), ct);
            if (result is null)
                return NotFound();
            return Ok(result);
        }
    
        public class SyncBatchRequest
        {
            public List<string> IinS { get; set; } = new();
        }

        [HttpPost("sync-batch")]
        public async Task<IActionResult> SyncBatch([FromBody] SyncBatchRequest request, [FromServices] AccountingScholarships.Application.Interfaces.IComparisonRepository comparisonRepo, CancellationToken ct)
        {
            var currentUser = User.Identity?.Name ?? "System";
            var result = await comparisonRepo.SyncBatchAsync(request.IinS, currentUser, ct);
            return Ok(result);
        }
    }
}

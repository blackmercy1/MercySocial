using Microsoft.AspNetCore.Mvc;

namespace MercySocial.Presentation.Common.Controllers;

public interface IController<in TDto, in TId>
{
    Task<IActionResult> AddAsync([FromBody] TDto dto, CancellationToken cancellationToken);
    Task<IActionResult> UpdateById([FromRoute] TId tId, [FromBody] TDto dto, CancellationToken cancellationToken);
    Task<IActionResult> GetByIdAsync([FromRoute] TId tId, CancellationToken cancellationToken);
    Task<IActionResult> DeleteByIdAsync([FromRoute] TId tId, CancellationToken cancellationToken);
}
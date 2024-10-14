using Microsoft.AspNetCore.Mvc;

namespace MercySocial.Presentation.Common.Controllers;

public interface IController<in TDto, in TId>
{
    Task<IActionResult> AddAsync([FromBody] TDto dto);
    Task<IActionResult> UpdateById([FromRoute] TId tId, [FromBody] TDto dto);
    Task<IActionResult> GetByIdAsync([FromRoute] TId tId);
    Task<IActionResult> DeleteByIdAsync([FromRoute] TId tId);
}
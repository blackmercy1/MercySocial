using Microsoft.AspNetCore.Mvc;

namespace MercySocial.Presentation.Common.Controllers;

public interface IController<in TDto, in TId>
{
    Task<IActionResult> AddAsync([FromBody] TDto createDto);
    Task<IActionResult> UpdateById([FromBody] TDto createDto, TId id);
    Task<IActionResult> GetByIdAsync([FromQuery] TId tId);
    Task<IActionResult> DeleteAsync([FromBody] TDto readDto);
    Task<IActionResult> Update([FromBody] TDto createDto);
}
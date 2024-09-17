using Microsoft.AspNetCore.Mvc;

namespace MercySocial.Presentation.Common.Controllers;

public interface IController<in TCreateDto, in TReadDto, in TId>
{
    Task<IActionResult> AddAsync([FromBody] TCreateDto createDto);
    Task<IActionResult> UpdateById([FromBody] TReadDto createDto, TId id);
    Task<IActionResult> GetByIdAsync([FromQuery] TId id);
    Task<IActionResult> DeleteAsync([FromBody] TReadDto readDto);
    Task<IActionResult> Update([FromBody] TReadDto createDto);
}
using AutoMapper;
using ErrorOr;
using MercySocial.Application.Common.Service;
using MercySocial.Domain.common;
using Microsoft.AspNetCore.Mvc;

namespace MercySocial.Presentation.Common.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public abstract class EntityController<TModel, TDto, TId, TIdType> :
    ApiController,
    IController<TDto, TId>
    where TModel : Entity<TId> 
    where TId : AggregateRootId<TIdType>
    where TIdType : struct
{
    protected readonly IService<TModel, TId, TIdType> Service;
    protected readonly IMapper Mapper;

    public EntityController(
        IService<TModel, TId, TIdType> service,
        IMapper mapper)
    {
        Service = service;
        Mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] TDto dto)
    {
        return await ProcessRequestAsync(dto, async _ =>
        {
            var entity = Mapper.Map<TModel>(dto);
            var result = await Service.AddAsync(entity);
            return result;
        });
    }

    [HttpPut($"{{id:int}}")]
    public async Task<IActionResult> UpdateById([FromRoute] TId tId, [FromBody] TDto dto)
    {
        return await ProcessRequestAsync(dto, async _ =>
        {
            var entity = Mapper.Map<TModel>(dto);
            var result = await Service.UpdateByIdAsync(entity, tId);
            return result;
        });
    }

    [HttpGet($"{{id:int}}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] TId tId)
    {
        return await ProcessRequestAsync(tId, async _ =>
        {
            var result = await Service.GetByIdAsync(tId);
            return result;
        });
    }

    [HttpDelete($"{{id:int}}")]
    public async Task<IActionResult> DeleteByIdAsync(TId tId)
    {
        return await ProcessRequestAsync(tId, async _ =>
        {
            var result = await Service.DeleteByIdAsync(tId);
            return result;
        });
    }

    protected async Task<IActionResult> ProcessRequestAsync<TDtoOperation>(
        TDtoOperation dto,
        Func<TDtoOperation, Task<ErrorOr<TModel>>> operation)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await operation(dto);

        if (result.IsError)
            return BadRequest(new {result.Errors.FirstOrDefault().Description});

        return Ok(result.Value);
    }
}
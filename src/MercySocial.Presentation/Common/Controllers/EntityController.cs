using AutoMapper;
using ErrorOr;
using MercySocial.Application.Common.Service;
using MercySocial.Domain.Common;
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
    public async Task<IActionResult> AddAsync([FromBody] TDto dto, CancellationToken cancellationToken)
    {
        return await ProcessRequestAsync(dto, async _ =>
        {
            var entity = Mapper.Map<TModel>(dto);
            var result = await Service.AddAsync(entity, cancellationToken);
            return result;
        });
    }

    [HttpPut($"{{id:int}}")]
    public async Task<IActionResult> UpdateById(
        [FromRoute] TId tId,
        [FromBody] TDto dto,
        CancellationToken cancellationToken)
    {
        return await ProcessRequestAsync(dto, async _ =>
        {
            var entity = Mapper.Map<TModel>(dto);
            var result = await Service.UpdateByIdAsync(entity, tId, cancellationToken);
            return result;
        });
    }

    [HttpGet($"{{id:int}}")]
    public async Task<IActionResult> GetByIdAsync(
        [FromRoute] TId tId,
        CancellationToken cancellationToken)
    {
        return await ProcessRequestAsync(tId, async _ =>
        {
            var result = await Service.GetByIdAsync(tId, cancellationToken);
            return result;
        });
    }

    [HttpDelete($"{{id:int}}")]
    public async Task<IActionResult> DeleteByIdAsync(TId tId, CancellationToken cancellationToken)
    {
        return await ProcessRequestAsync(tId, async _ =>
        {
            var result = await Service.DeleteByIdAsync(tId, cancellationToken);
            return result;
        });
    }

    protected async Task<IActionResult> ProcessRequestAsync<TDtoOperation>(
        TDtoOperation dto,
        Func<TDtoOperation, Task<ErrorOr<TModel>>> operation)
    {
        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);

        var result = await operation(dto);

        return result.IsError ? Problem(result.Errors) : Ok(result.Value);
    }
}
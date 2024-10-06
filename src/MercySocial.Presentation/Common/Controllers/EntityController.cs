using AutoMapper;
using ErrorOr;
using MercySocial.Application.Common.Service;
using MercySocial.Domain.common;
using Microsoft.AspNetCore.Mvc;

namespace MercySocial.Presentation.Common.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public abstract class EntityController<TModel, TDto, TId, TIdType> :
    Controller,
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
    public async Task<IActionResult> AddAsync([FromBody] TDto createDto)
    {
        return await ProcessRequestAsync(createDto, async dto =>
        {
            var entity = Mapper.Map<TModel>(dto);
            var result = await Service.AddAsync(entity);
            return result;
        });
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] TDto createDto)
    {
        return await ProcessRequestAsync(createDto, async dto =>
        {
            var entity = Mapper.Map<TModel>(dto);
            var result = await Service.UpdateAsync(entity);
            return result;
        });
    }

    [HttpPut]
    public async Task<IActionResult> UpdateById([FromBody] TDto createDto, TId id)
    {
        return await ProcessRequestAsync(createDto, async dto =>
        {
            var entity = Mapper.Map<TModel>(dto);
            var result = await Service.UpdateByIdAsync(entity, id);
            return result;
        });
    }

    [HttpGet($"{{tId}}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] TId tId)
    {
        return await ProcessRequestAsync(tId, async id =>
        {
            var result = await Service.GetByIdAsync(id);
            return result;
        });
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAsync([FromBody] TDto readDto)
    {
        return await ProcessRequestAsync(readDto, async dto =>
        {
            var entity = Mapper.Map<TModel>(dto);
            var result = await Service.DeleteAsync(entity);
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
using AutoMapper;
using FluentResults;
using MercySocial.Application.Common.Service;
using MercySocial.Domain.common;
using Microsoft.AspNetCore.Mvc;

namespace MercySocial.Presentation.Common.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public abstract class EntityController<TModel, TCreateDto, TReadDto, TId> :
    Controller,
    IController<TCreateDto, TReadDto, TId>
    where TModel : Entity<TId> 
    where TId : notnull
{
    protected readonly IService<TModel, TId> Service;
    protected readonly IMapper Mapper;

    public EntityController(
        IService<TModel, TId> service,
        IMapper mapper)
    {
        Service = service;
        Mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] TCreateDto createDto)
    {
        return await ProcessRequestAsync(createDto, async dto =>
        {
            var entity = Mapper.Map<TModel>(dto);
            var result = await Service.AddAsync(entity);
            return result;
        });
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] TReadDto createDto)
    {
        return await ProcessRequestAsync(createDto, async dto =>
        {
            var entity = Mapper.Map<TModel>(dto);
            var result = await Service.UpdateAsync(entity);
            return result;
        });
    }

    [HttpPut]
    public async Task<IActionResult> UpdateById([FromBody] TReadDto createDto, TId id)
    {
        return await ProcessRequestAsync(createDto, async dto =>
        {
            var entity = Mapper.Map<TModel>(dto);
            var result = await Service.UpdateByIdAsync(entity, id);
            return result;
        });
    }

    [HttpGet]
    public async Task<IActionResult> GetByIdAsync([FromQuery] TId id)
    {
        return await ProcessRequestAsync(id, async id =>
        {
            var result = await Service.GetByIdAsync(id);
            return result;
        });
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAsync([FromBody] TReadDto readDto)
    {
        return await ProcessRequestAsync(readDto, async dto =>
        {
            var entity = Mapper.Map<TModel>(dto);
            var result = await Service.DeleteAsync(entity);
            return result;
        });
    }

    protected async Task<IActionResult> ProcessRequestAsync<TDto>(
        TDto dto,
        Func<TDto, Task<Result<TModel>>> operation)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await operation(dto);

        if (result.IsFailed)
            return BadRequest(new {result.Errors.FirstOrDefault()?.Message});

        return Ok(result.Value);
    }
}
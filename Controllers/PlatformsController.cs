using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepo _repository;
        private readonly IMapper _mapper;

        public PlatformsController(IPlatformRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
        {
            Console.WriteLine("-->Getting platforms");
            var platformItem = _repository.GetAllPlatforms();
            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platformItem));    
        }
        [HttpGet("{id}", Name="GetPlatformById")]
        public ActionResult<PlatformReadDto> GetPlatformById( int id)
        {
            var platform = _repository.GetPlatformById(id);
            if(platform!=null){
                return Ok(_mapper.Map<PlatformReadDto>(platform));
            } 
            return NotFound();
        }
        [HttpPost]
        public ActionResult<PlatformReadDto> CreatePlatform(PlatformCreateDto platform)
        {
            var platformModel = _mapper.Map<Platform>(platform);
            _repository.CreatePlatform(platformModel);
            _repository.SaveChanges();
            var platformReadDto = _mapper.Map<PlatformReadDto>(platformModel);

            return CreatedAtRoute(nameof(GetPlatformById), new {id=platformReadDto.Id},platformReadDto );
        }
    }           
}
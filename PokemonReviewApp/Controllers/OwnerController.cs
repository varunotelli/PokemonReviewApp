using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly IMapper _mapper;

        public OwnerController(IOwnerRepository ownerRepository, IMapper mapper)
        {
            _ownerRepository = ownerRepository;
            _mapper = mapper;
            
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IList<OwnerDto>))]
        public IActionResult GetOwners() 
        {
            var owners = _mapper.Map<OwnerDto>(_ownerRepository.GetOwners());
            if(!ModelState.IsValid) 
            { 
                return BadRequest(ModelState);
            }
            return Ok(owners);
        }

        [HttpGet("{ownerId}/pokemon")]
        [ProducesResponseType(200, Type = typeof(IList<PokemonDto>))]
        public IActionResult GetPokemonsByOwner(int ownerId)
        {

            if(!_ownerRepository.OwnerExist(ownerId)) return NotFound();
            var pokemon = _mapper.Map<PokemonDto>(_ownerRepository.GetPokemonByOwner(ownerId));
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            return Ok(pokemon);
        }



    }
}

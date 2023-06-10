﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;
        public CountryController(ICountryRepository countryRepository, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IList<CountryDto>))]
        public IActionResult GetCountries()
        {
            var countries = _mapper.Map<IList<CountryDto>>(_countryRepository.GetCountries());
            if(!ModelState.IsValid) { return BadRequest(ModelState); }
            return Ok(countries);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(CountryDto))]
        public IActionResult GetCountry(int id)
        {
            if(!_countryRepository.CountryExists(id)) { return NotFound(); }
            var country = _mapper.Map<CountryDto>(_countryRepository.GetCountry(id));
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            return Ok(country);
        }

        [HttpGet("owners/{ownerId}")]
        [ProducesResponseType(200, Type = typeof(CountryDto))]
        public IActionResult GetCountryByOwner(int ownerId)
        {
           
            var country = _mapper.Map<CountryDto>(_countryRepository.GetCountryByOwner(ownerId));
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            return Ok(country);
        }

    }
}

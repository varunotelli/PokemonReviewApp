﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;   
        }

        [HttpGet]
        [ProducesResponseType(200, Type=typeof(IList<CategoryDto>))]
        public IActionResult GetCategories()
        {
            var categories = _mapper.Map<IList<CategoryDto>>(_categoryRepository.GetCategories());
            if(!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            return Ok(categories);

        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(CategoryDto))]
        public IActionResult GetCategory(int id)
        {
            if(!_categoryRepository.CategoryExists(id))
            {
                return NotFound();
            }

            var category = _mapper.Map<CategoryDto>(_categoryRepository.GetCategory(id));


            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            
            return Ok(category);

        }

        [HttpGet("{id}/pokemon")]
        [ProducesResponseType(200, Type = typeof(IList<PokemonDto>))]
        public IActionResult GetPokemon(int id)
        {
            if (!_categoryRepository.CategoryExists(id))
            {
                return NotFound();
            }

            var pokemons = _mapper.Map<IList<PokemonDto>>(_categoryRepository.GetPokemonByCategory(id));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(pokemons);
        }


    }
}

﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Application.Interfaces;
using Server.Contracts.Abstractions.Shared;
using Server.Contracts.DTO.SubCategory;
using Server.Domain.Entities;

namespace Server.API.Controllers
{
    [ApiController]
    [Route("Api")]
    public class SubCategoryController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISubCategoryService _subCategoryService;

        public SubCategoryController(IMapper mapper, ISubCategoryService subCategoryService)
        {
            _mapper = mapper;
            _subCategoryService = subCategoryService;
        }

        [HttpGet("GetAllSubCategories")]
        [ProducesResponseType(200, Type = typeof(Result<object>))]
        public async Task<IActionResult> GetAllSubCategories()
        {
            var result = await _subCategoryService.GetSubs();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(result);
        }
        [HttpPost("CreateSubCategory")]
        [ProducesResponseType(204, Type = typeof(Result<object>))]
        [ProducesResponseType(400, Type = typeof(Result<object>))]
        public async Task<IActionResult> CreateSubCategory([FromBody] CreateSubCategoryDTO CategorySubCreate, string CategoryId)
        {
            if (CategorySubCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await _subCategoryService.GetByName(CategorySubCreate.SubName) != null)
            {
                ModelState.AddModelError("", "This sub category already exists");
                return StatusCode(422, ModelState);
            }

            Guid Id;

            try
            {
                Id = new Guid(CategoryId);
            }
            catch
            {
                return BadRequest("There is no category has this id.");
            }

            var result = await _subCategoryService.AddSubCategoryToCategory(_mapper.Map<SubCategory>(CategorySubCreate), Id);

            return Ok(result);
        }

        [HttpDelete("SubCategoryId/Delete")]
        [ProducesResponseType(400, Type = typeof(Result<object>))]
        [ProducesResponseType(204, Type = typeof(Result<object>))]
        [ProducesResponseType(404, Type = typeof(Result<object>))]
        public async Task<IActionResult> DeleteSubCategory(string ID)
        {
            Guid SubcategoryId;

            try
            {
                SubcategoryId = new(ID);
            }
            catch
            {
                return NotFound("This sub category is not exist!!!");
            }

            var result = await _subCategoryService.Delete(SubcategoryId);

            if (result.Data is null)
            {
                return StatusCode(400, result);
            }

            return Ok(result);
        }
    }
}
using AutoMapper;
using ECommerice.Api.DTOs;
using ECommerice.Api.Helpers;
using ECommerice.Api.Helpers.Extensions;
using ECommerice.Core.Entities;
using ECommerice.Core.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerice.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : BaseApiController
    {
        private readonly IGenericRepository<Category> _categoryRepo;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public CategoryController(
            IGenericRepository<Category> category,
            IMapper mapper,
            UserManager<AppUser> userManager)
        {
            _categoryRepo = category;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet("getAllCategories")]
        public async Task<ActionResult<List<Category>>> GetAllCategories()
        {
            var categories = await _categoryRepo.ListAllAsync();
            return Ok(categories);
        }

        [Authorize]
        [HttpPost("getCategorPaging")]
        public async Task<ActionResult<ApiResponseDTO>> GetCategorPaging(CommonSearchDTO commonSearch)
        {
            if(commonSearch.SearchText == null || commonSearch.SearchText =="")
            {
                var categories = await _categoryRepo.GetAllPaging();
                var count = await _categoryRepo.GetCount();

                return Ok(new
                {
                    Data = categories.Skip((commonSearch.PageNo - 1) * commonSearch.pageSize).Take(commonSearch.pageSize),
                    Count = count
                });
            }
            else
            {
                var categories = await _categoryRepo.GetWherePagig(c => c.Name.Contains(commonSearch.SearchText));

                return Ok(new
                {
                    Data = categories.Skip((commonSearch.PageNo - 1) * commonSearch.pageSize).Take(commonSearch.pageSize),
                    Count = categories.Count()
                });
            }
           
        }

        [Authorize]
        [HttpGet("getCategor/{id}")]
        public async Task<ActionResult<Category>> GetCategories(int id)
        {
            var category = await _categoryRepo.GetByIdAsync(id);
            return Ok(category);
        }

        [Authorize]
        [HttpPost("updateCategory")]
        public async Task<ActionResult<Category>> UpdateCategory(Category category)
        {
            var user = await _userManager.FindByEmailFromClaimPrincipleAsync(HttpContext.User);
            if (user.UserType == "user") return Unauthorized(new ApiResponse(401));

            _categoryRepo.Update(category);

            if (await _categoryRepo.SaveChanges())
                return Ok(category);

            return BadRequest();
        }

        [Authorize]
        [HttpPost("addCategory")]
        public async Task<ActionResult<Category>> AddCategory(Category category)
        {
            var user = await _userManager.FindByEmailFromClaimPrincipleAsync(HttpContext.User);
            if (user.UserType == "user") return Unauthorized(new ApiResponse(401));

            _categoryRepo.Add(category);

            if (await _categoryRepo.SaveChanges())
                return Ok(category);

            return BadRequest();
        }


        [Authorize]
        [HttpDelete("deleteCategor/{id}")]
        public async Task<ActionResult> DeleteCategor(int id)
        {
            var user = await _userManager.FindByEmailFromClaimPrincipleAsync(HttpContext.User);
            if (user.UserType == "user") return Unauthorized(new ApiResponse(401));

            var category = await _categoryRepo.GetByIdAsync(id);
            _categoryRepo.Delete(category);

            if(await _categoryRepo.SaveChanges())
                return Ok(true);
            
            return BadRequest(false);
        }
    }
}

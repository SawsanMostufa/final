using AutoMapper;
using ECommerice.Api.DTOs;
using ECommerice.Api.Helpers;
using ECommerice.Api.Helpers.Extensions;
using ECommerice.Core.Entities;
using ECommerice.Core.IRepository;
using ECommerice.Core.Specification;
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
    public class ProductController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<Category> _category;
        public readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public ProductController(IGenericRepository<Product> productRepo,
            IGenericRepository<Category> category,
            IMapper mapper,
            UserManager<AppUser> userManager)
        {
            _productRepo = productRepo;
            _category = category;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet("getProducts")]
        public async Task<ActionResult<Pagination<ProductToReturnDTO>>> GetProducts([FromQuery] ProductSpecParam productParams)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(productParams);
            var countSpec = new ProductsWithFiltersForCountSpecification(productParams);
            var totalItems = await _productRepo.CountAsync(countSpec);
            var products = await _productRepo.ListAsync(spec);
            var data = _mapper
                .Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDTO>>(products);

            return Ok(new Pagination<ProductToReturnDTO>
            {
                PageIndex = productParams.PageIndex,
                PageSize = productParams.PageSize,
                Count = totalItems,
                Data = data
            });
        }

        [HttpGet("getProduct/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDTO>> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);
            var product = await _productRepo.GetEntityWithSpec(spec);

            if (product == null) return NotFound(new ApiResponse(404));

            var productDto = _mapper.Map<Product, ProductToReturnDTO>(product);
            //var productSizeDto = _mapper.Map<IReadOnlyList<Size>, IReadOnlyList<ProductSizeDto>>(product.ProductSizes);
            //productDto.ProductSizes = productSizeDto.ToList();

            //var sizes = new List<ProductSizeDto>();
            //foreach (var item in productDto.ProductSizes)
            //{
           
            //    if (item.OrderItemId!=null)
            //        {
            //        sizes.Add(new ProductSizeDto()
            //        {
            //            Id = item.Id,
            //            OrderItemId = item.OrderItemId,
            //            Price = item.Price,
            //            ProductId = item.ProductId,
            //            Quantity = item.Quantity,
            //            Discount = item.Discount,
            //            value=item.value
            //        });
                 
            //    }
            //}
            //productDto.ProductSizes = sizes;
            return productDto;
        }

        [HttpGet("getLatestProducts")]
        public async Task<ActionResult<Pagination<ProductToReturnDTO>>> GetLatestProducts()
        {
            var products = await _productRepo.GetLatestData(3);

            return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDTO>>(products));
        }

        [HttpGet("checkProductQtyAva")]
        public async Task<ActionResult<Pagination<ProductToReturnDTO>>> CheckProductQtyAva(int id, int qtyReq,string value)
        {
            ProductToReturnDTO _product;
            var spec = new ProductsWithTypesAndBrandsSpecification(id);
        
                var product = await _productRepo.GetEntityWithSpec(spec);
            var productDto = _mapper.Map<Product, ProductToReturnDTO>(product);

            //var sizes = new List<ProductSizeDto>();
            //foreach (var item in productDto.ProductSizes)
            //{

            //    if (item.OrderItemId != null)
            //    {
            //        sizes.Add(new ProductSizeDto()
            //        {
            //            Id = item.Id,
            //            OrderItemId = item.OrderItemId,
            //            Price = item.Price,
            //            ProductId = item.ProductId,
            //            Quantity = item.Quantity,
            //            Di1scount = item.Discount,
            //            value = item.value
            //        });

            //    }
            //}
            //productDto.ProductSizes = sizes;
             _product = productDto;
           
            foreach (var item in _product.ProductSizes)
            {
                if (item.value == value)
                {
                    if (item.Quantity == 0)
                    {
                        return Ok(new
                        {
                            message = "Quantity not available in stock",
                            status = false
                        });
                    }

                    if (qtyReq > item.Quantity)
                    {
                        return Ok(new
                        {
                            message = "Quantity request greater than in stock",
                            status = false
                        });
                    }

                    return Ok(new
                    {
                        message = "Quantity available",
                        status = true
                    });

                }
                //return Ok(new
                //{
                //    message = "Value Size Not Found",
                //    status = false
                //});



            }
            return Ok(new
            {
                message = "product Sizes Not Found",
                status = false
            });
        }





        #region admin
        [Authorize]
        [HttpPost("addProduct")]
        public async Task<ActionResult<Category>> AddProduct(Product product)
        {
            var user = await _userManager.FindByEmailFromClaimPrincipleAsync(HttpContext.User);
            if (user.UserType == "user") return Unauthorized(new ApiResponse(401));

            product.CreatedDate = DateTime.Now;
            product.PictureUrl = product.PictureUrl == "" ? product.PictureUrl = "DefualtImage.png" : product.PictureUrl;
            _productRepo.Add(product);

            if (await _productRepo.SaveChanges())
                return Ok();

            return BadRequest();
        }

        [Authorize]
        [HttpPost("updateProduct")]
        public async Task<ActionResult<Category>> UpdateProduct(Product product)
        {
            var user = await _userManager.FindByEmailFromClaimPrincipleAsync(HttpContext.User);
            if (user.UserType == "user") return Unauthorized(new ApiResponse(401));

            _productRepo.Update(product);

            if (await _productRepo.SaveChanges())
                return Ok(product);

            return BadRequest();
        }


        [HttpPost("getProductsPaging")]
        public async Task<ActionResult<Pagination<ProductToReturnDTO>>> getProductsPaging(CommonSearchDTO commonSearch)
        {
            var user = await _userManager.FindByEmailFromClaimPrincipleAsync(HttpContext.User);
            if (user.UserType == "user") return Unauthorized(new ApiResponse(401));

            if (commonSearch.SearchText == null || commonSearch.SearchText == "")
            {
                var _products = await _productRepo.GetAllWithIncludes("Category", "ProductSizes");
                var products = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductToReturnDTO>>(_products)
                         .Skip((commonSearch.PageNo - 1) * commonSearch.pageSize).Take(commonSearch.pageSize);

                return Ok(new
                {
                    Data = products,
                    Count = _products.Count()
                });
            }
            else
            {
                var _products = await _productRepo.GetWhereWithInclude(p => p.Name.Contains(commonSearch.SearchText), "Category");
                var products = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductToReturnDTO>>(_products)
                         .Skip((commonSearch.PageNo - 1) * commonSearch.pageSize).Take(commonSearch.pageSize);

                return Ok(new
                {
                    Data = products,
                    Count = _products.Count()
                });
            }
        }

        [Authorize]
        [HttpDelete("deleteProduct/{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var user = await _userManager.FindByEmailFromClaimPrincipleAsync(HttpContext.User);
            if (user.UserType == "user") return Unauthorized(new ApiResponse(401));

            var product = await _productRepo.GetByIdAsync(id);
            _productRepo.Delete(product);

            if (await _productRepo.SaveChanges())
                return Ok(true);

            return BadRequest(false);
        }

        #endregion
    }
}

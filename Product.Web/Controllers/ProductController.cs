using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Product.Domain;
using Microsoft.Extensions.Logging;
using Product.ServiceInterface;
using Product.Web.Dto;
using Product.Web.Extensions;
using Microsoft.AspNetCore.Mvc.Routing;
using Newtonsoft.Json;

namespace Product.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;
        private readonly IUrlHelper _urlHelper;


        public ProductController(ILogger<ProductController> logger, IProductService productService,
            IUrlHelper urlHelper)
        {
            _logger = logger;
            _productService = productService;
            _urlHelper = urlHelper;
        }


        [HttpGet()]
        [Route("", Name = "ProductList")]
        public async Task<ActionResult<List<ProductDto>>> Get(
            string sort = "name",
            int page = 1, int pageSize = 5)
        {
            try
            {
                var result = await _productService.GetAllAsync(sort, page, pageSize);

                var totalCount = await _productService.CountAsync();
                var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

                var firstPageLink = page > 1 ? _urlHelper.Link("ProductList",
                   new
                   {
                       page = 1,
                       pageSize = pageSize,
                       sort = sort
                   }) : "";

                var prevLink = page > 1 ? _urlHelper.Link("ProductList",
                    new
                    {
                        page = page - 1,
                        pageSize = pageSize,
                        sort = sort
                    }) : "";
                var nextLink = page < totalPages ? _urlHelper.Link("ProductList",
                    new
                    {
                        page = page + 1,
                        pageSize = pageSize,
                        sort = sort
                    }) : "";

                var lastPageLink = page < totalPages ? _urlHelper.Link("ProductList",
                   new
                   {
                       page = totalPages,
                       pageSize = pageSize,
                       sort = sort
                   }) : "";

                var metadata = new
                {
                    currentPage = page,
                    pageSize = pageSize,
                    totalCount = totalCount,
                    totalPages = totalPages,
                    hasPrevious = page > 1,
                    hasNext = page < totalPages,
                    previousPageLink = prevLink,
                    nextPageLink = nextLink,
                    firstPageLink = firstPageLink,
                    lastPageLink = lastPageLink
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

                return Ok(result.Select(p => p.ToDto()));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> Get(int id)
        {
            try
            {
                var result = await _productService.GetByIdAsync(id);

                if (result == null)
                {
                    return NotFound();
                }

                var dto = result.ToDto();

                //    dto.Types = Enum.GetNames(typeof(ProductType)).ToList();

                return Ok(dto);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpPost]
        public async Task<ActionResult<ProductInfo>> Post(ProductInsertDto productInsertDto)
        {
            try
            {
                var product = productInsertDto.ToDomain();
                if (!_productService.Validate(product))
                {
                    return BadRequest();
                }
                await _productService.AddAsync(product);

                return CreatedAtAction(nameof(Post), new { id = product.Id }, product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProductInfo>> Put(int id, ProductDto productDto)
        {
            if (id != productDto.Id)
            {
                return BadRequest();
            }
            try
            {

                var found = await _productService.AnyByIdAsync(id);
                if (!found)
                {
                    return NotFound();
                }
                var p = await _productService.GetByIdAsync(id);
                var product = productDto.ToDomain(p);
                if (!_productService.Validate(product))
                {
                    return BadRequest();
                }
                await _productService.UpdateAsync(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _productService.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return NoContent();
        }
    }
}

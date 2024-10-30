using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Application.Common;
using Server.Application.Interfaces;
using Server.Contracts.Abstractions.Shared;
using Server.Contracts.DTO.Service;

namespace Server.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpGet("SearchServicePagination")]
        [ProducesResponseType(200, Type = typeof(ViewServiceDTO))]
        [ProducesResponseType(400, Type = typeof(Result<object>))]
        public async Task<IActionResult> SearchServicePagination([FromQuery] string ServiceName, int pageIndex = 1, int pageSize = 10)
        {

            var result = await _searchService.SearchServicePagination(ServiceName, pageIndex, pageSize);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(result);
        }

    }
}

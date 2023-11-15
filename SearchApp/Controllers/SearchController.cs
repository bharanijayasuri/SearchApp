using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SearchApp.Services.Interfaces;

namespace SearchApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;
        private readonly ILogger _logger;

        public SearchController(ISearchService searchService, ILogger<SearchController> logger)
        {
            _searchService = searchService;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Search([FromQuery] string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return BadRequest();

            return Ok(_searchService.Search(searchString.ToLower()));
        }
    }
}

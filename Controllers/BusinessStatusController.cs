using Microsoft.AspNetCore.Mvc;
using HtmlAgilityPack;
using biz_status_api.Utilities;


namespace biz_status_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BusinessStatusController : ControllerBase
    {

        private readonly IHtmlSearchService _searchService;
        public BusinessStatusController(IHtmlSearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpGet]
        public IActionResult Get(string searchText, string expectedAddress, string expectedName)
        {
            var encodedSearchText = System.Web.HttpUtility.UrlEncode(searchText);

            //TODO: Move this into an independent service
            var url = "http://maps.google.com/?q=" + encodedSearchText;
            var web = new HtmlWeb();
            var doc = web.Load(url).DocumentNode;

            _searchService.Initialize(doc.InnerHtml);

            if(_searchService.ExactContains("CLOSED"))
                return NotFound("Business may be closed.");

            if(!_searchService.FuzzyContains(expectedAddress + "\\"))
                return NotFound("Address may have changed.");

            if(!_searchService.FuzzyContains(expectedName + "\\"))
                return NotFound("Name may have changed.");

            return Ok("Business seems valid.");
        }
    }
}

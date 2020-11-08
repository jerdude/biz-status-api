using Microsoft.AspNetCore.Mvc;
using HtmlAgilityPack;
using biz_status_api.Utilities;


namespace biz_status_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BusinessStatusController : ControllerBase
    {


        public BusinessStatusController()
        {

        }

        [HttpGet]
        public IActionResult Get(string searchText, string expectedAddress, string expectedName)
        {
            var encodedSearchText = System.Web.HttpUtility.UrlEncode(searchText);

            //TODO: Move this into an independent service
            var url = "http://maps.google.com/?q=" + encodedSearchText;
            var web = new HtmlWeb();
            var doc = web.Load(url).DocumentNode;

            //TODO: Inject me
            var htmlSearch = new HtmlSearchService(doc.InnerHtml);

            if(htmlSearch.ExactContains("CLOSED"))
                return NotFound("Business may be closed.");

            if(!htmlSearch.FuzzyContains(expectedAddress + "\\"))
                return NotFound("Address may have changed.");

            if(!htmlSearch.FuzzyContains(expectedName + "\\"))
                return NotFound("Name may have changed.");

            return Ok("Business seems valid.");
        }
    }
}

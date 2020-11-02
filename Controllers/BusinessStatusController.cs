using Microsoft.AspNetCore.Mvc;
using HtmlAgilityPack;


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

            var url = "http://maps.google.com/?q=" + encodedSearchText;
            var web = new HtmlWeb();
            var doc = web.Load(url).DocumentNode;

            if(doc.InnerHtml.Contains("CLOSED"))
                return NotFound("Business may be closed.");

            if(!doc.InnerHtml.Contains(expectedAddress + "\\"))
                return NotFound("Address may have changed.");

            if(!doc.InnerHtml.Contains(expectedName + "\\"))
            return NotFound("Name may have changed.");

            return Ok("Business seems valid.");
        }
    }
}

using System.Collections.Generic;
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
        public IEnumerable<string> Get()
        {
            //Test code - remove me
            var url = "http://html-agility-pack.net/";
            var web = new HtmlWeb();
            var doc = web.Load(url);


            return new List<string>(){"Test","One","Two"};
        }
    }
}

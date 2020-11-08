 using Microsoft.AspNetCore.Mvc;
using HtmlAgilityPack;


namespace biz_status_api.Utilities
{
    public class HtmlSearchService
    {
        private string _scrubbedHtml;
        private string _rawHtml;

        public HtmlSearchService(string rawHtml)
        {
            _rawHtml = rawHtml;
            _scrubbedHtml = ScrubString(rawHtml);
        }

        //Allows for extra spaces, periods, different casing, etc.
       public bool FuzzyContains(string searchText)
       {
           return _scrubbedHtml.Contains(ScrubString(searchText));
       }

       
       public bool ExactContains(string searchText)
       {
           return _rawHtml.Contains(searchText);
       }

        //Scrubs text to make it more easily comparable
        private string ScrubString(string toScrub)
        {
            toScrub = toScrub.Replace(".", "");
            toScrub = toScrub.Replace(" ", "");
            toScrub = toScrub.ToUpper();

            return toScrub;
        }
    }
}

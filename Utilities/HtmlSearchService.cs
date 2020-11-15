 using Microsoft.AspNetCore.Mvc;
using HtmlAgilityPack;


namespace biz_status_api.Utilities
{
    public interface IHtmlSearchService
    {
        bool FuzzyContains(string searchText);
        bool ExactContains(string searchText);
        void Initialize(string rawHtml);
    }

    public class HtmlSearchService : IHtmlSearchService
    {
        private string _scrubbedHtml;
        private string _rawHtml;

        public HtmlSearchService()
        {

        }

        public void Initialize(string rawHtml)
        {
            _rawHtml = rawHtml;
            _scrubbedHtml = ScrubString(rawHtml);
        }

        //Allows for extra spaces, periods, different casing, etc.
       public bool FuzzyContains(string searchText)
       {
           //TODO: Add test
           return _scrubbedHtml.Contains(ScrubString(searchText));
       }

       
       public bool ExactContains(string searchText)
       {
           //TODO: Add test
           return _rawHtml.Contains(searchText);
       }

        //Scrubs text to make it more easily comparable
        private string ScrubString(string toScrub)
        {
            //TODO: Replace St with Street, Ct with Court, Pl with Place, Blvd with BOulevard, etc.

            toScrub = toScrub.Replace(".", "");
            toScrub = toScrub.Replace(" ", "");
            toScrub = toScrub.ToUpper();

            return toScrub;
        }
    }
}

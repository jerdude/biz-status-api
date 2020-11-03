# biz-status-api

A simple api endpoint used to verify the status of a business. Specifically, we'll be using this for an online small business directory to periodically verify that a business record is still valid. 

- The endpoint will accept a search term (typically "Business Name + Address" or "Business Name + City")
- We will then send a search request to Google
- We'll parse the dom results looking for a few things:
  - Did it return a google places result on the right? If not, flag this business as possibly invalid.
  - Check the business name. Does it match the business name we were expecting? If not, flag this business.
  - Check the address. Does it match the business name we were expecting? If not, flag this business.
  - Check for the "Permanantly Closed" block. If present, flag this business.
  
The intent of this endpoint isn't to scrape new data, it's to indicate to us that something has changed with this business, such as:

- The business has closed.
- The business has moved.
- The business has changed ownership or names.

It's worth noting that this should all be more easily doable using the Google Places Search API. We will be using this to ocassionally verify individual records in our small business directory. If used in bulk, this approach will almost certainly get you flagged as a bot by Google.

Running locally:
- Use "donet run" to serve endpoints localhost:5001
- Use "localhost:5001/swagger" to test available endpoints.

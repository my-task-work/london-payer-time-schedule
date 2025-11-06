✅ Notes & Tips:

You’ll need to add the HtmlAgilityPack NuGet package in your project for HTML parsing.
Inspect your target website (https://londonprayertime.co.uk/) and confirm the table structure / element names so your XPath or CSS selectors match correctly.
If the site offers a JSON or XML API endpoint (the one you found at London Unified Prayer Times) you should use that instead of HTML parsing — it’s more reliable. 
Ensure you handle network errors, changes in markup, and nulls gracefully.
Consider caching results if you’ll call frequently.

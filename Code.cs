using System;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;    // Install-Package HtmlAgilityPack via NuGet
using System.Linq;

namespace LondonPrayerTimes
{
    public class PrayerTimes
    {
        public string Fajr { get; set; }
        public string Sunrise { get; set; }
        public string Dhuhr { get; set; }
        public string Asr { get; set; }
        public string Maghrib { get; set; }
        public string Isha { get; set; }

        public override string ToString()
        {
            return $"Fajr: {Fajr}\nSunrise: {Sunrise}\nDhuhr: {Dhuhr}\nAsr: {Asr}\nMaghrib: {Maghrib}\nIsha: {Isha}";
        }
    }

    class Program
    {
        // Change this URL if needed:
        private const string Url = "https://londonprayertime.co.uk/";

        static async Task Main(string[] args)
        {
            try
            {
                PrayerTimes times = await FetchPrayerTimesAsync();
                Console.WriteLine("London Prayer Times:");
                Console.WriteLine(times);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static async Task<PrayerTimes> FetchPrayerTimesAsync()
        {
            using var httpClient = new HttpClient();
            string html = await httpClient.GetStringAsync(Url);

            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            // Note: the CSS/XPath selectors must be adapted to the actual page structure.

            // Example XPaths (you’ll need to inspect page to confirm):
            string xpathFajr     = "//td[contains(text(),'Fajr')]/following-sibling::td[1]";
            string xpathSunrise = "//td[contains(text(),'Sunrise')]/following-sibling::td[1]";
            string xpathDhuhr   = "//td[contains(text(),'Dhuhr')]/following-sibling::td[1]";
            string xpathAsr     = "//td[contains(text(),'Asr')]/following-sibling::td[1]";
            string xpathMaghrib = "//td[contains(text(),'Maghrib')]/following-sibling::td[1]";
            string xpathIsha    = "//td[contains(text(),'Isha')]/following-sibling::td[1]";

            string fajr     = doc.DocumentNode.SelectSingleNode(xpathFajr)?.InnerText.Trim();
            string sunrise  = doc.DocumentNode.SelectSingleNode(xpathSunrise)?.InnerText.Trim();
            string dhuhr    = doc.DocumentNode.SelectSingleNode(xpathDhuhr)?.InnerText.Trim();
            string asr      = doc.DocumentNode.SelectSingleNode(xpathAsr)?.InnerText.Trim();
            string maghrib  = doc.DocumentNode.SelectSingleNode(xpathMaghrib)?.InnerText.Trim();
            string isha     = doc.DocumentNode.SelectSingleNode(xpathIsha)?.InnerText.Trim();

            if (fajr == null || dhuhr == null)
                throw new Exception("Could not parse prayer times from the page.");

            return new PrayerTimes
            {
                Fajr    = fajr,
                Sunrise = sunrise,
                Dhuhr   = dhuhr,
                Asr     = asr,
                Maghrib = maghrib,
                Isha    = isha
            };
        }
    }
}

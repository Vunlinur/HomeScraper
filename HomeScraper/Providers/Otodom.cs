using HomeScraper.ProviderBase;
using HtmlAgilityPack;
using System.Text.RegularExpressions;

namespace HomeScraper.Providers {
    [Provider]
    class Otodom : IProvider {
        const string stripNonNumbers = @"[^,\d]";
        const string valueCellClass = ".//div[contains(@class, 'css-1wi2w6s')]";
        // TODO inherit these fields
        const int defaultRent = 800;
        const string providerName = "otodom";

        public HomeData GetHomeData(string link) {
            var web = new HtmlWeb();
            var doc = web.Load(link);

            var homeData = new HomeData {
                Provider = providerName,
                Link = link
            };

            var price = doc.DocumentNode.SelectSingleNode("//strong[@data-cy='adPageHeaderPrice']").InnerText;
            homeData.Price = int.Parse(Regex.Replace(price, stripNonNumbers, ""));

            homeData.Address = doc.DocumentNode.SelectSingleNode("//a[@aria-label='Adres']").InnerText;

            var rentAria = doc.DocumentNode.SelectSingleNode("//div[@aria-label='Czynsz']");
            var rent = rentAria.SelectSingleNode(valueCellClass)?.InnerText;
            if (rent is not null)
                homeData.Rent = int.Parse(Regex.Replace(rent, stripNonNumbers, ""));
            else
                homeData.Rent = defaultRent;

            var areaAria = doc.DocumentNode.SelectSingleNode("//div[@aria-label='Powierzchnia']");
            var area = areaAria.SelectSingleNode(valueCellClass).InnerText;
            homeData.Area = float.Parse(Regex.Replace(area, stripNonNumbers, ""));

            var roomsAria = doc.DocumentNode.SelectSingleNode("//div[@aria-label='Liczba pokoi']");
            var rooms = roomsAria.SelectSingleNode(valueCellClass).InnerText;
            homeData.Rooms = int.Parse(Regex.Replace(rooms, stripNonNumbers, ""));

            var landArea = doc.DocumentNode.SelectSingleNode("//div[@aria-label='Powierzchnia działki']")?.InnerText;
            if (landArea is not null)
                homeData.LandArea = float.Parse(Regex.Replace(landArea, stripNonNumbers, ""));

            return homeData;
        }
    }
}

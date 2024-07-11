using HtmlAgilityPack;
using System.Linq;
using System.Text.RegularExpressions;

namespace HomeScraper.Providers {
	[Provider]
	class Otodom : IProvider {
		const string stripNonNumbers = @"[^,\d]";
        const string valueCellClass = ".//div[contains(@class, 'css-1wi2w6s')]";
        // TODO inherit these fields
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

			var areaAria = doc.DocumentNode.SelectSingleNode("//div[@aria-label='Powierzchnia']");
			var area = areaAria.SelectSingleNode(valueCellClass).InnerText;
			homeData.Area = float.Parse(Regex.Replace(area, stripNonNumbers, ""));
			
			var landArea = doc.DocumentNode.SelectSingleNode("//div[@aria-label='Powierzchnia działki']")?.InnerText;
			if (landArea is not null)
				homeData.LandArea = float.Parse(Regex.Replace(landArea, stripNonNumbers, ""));


            var roomsAria = doc.DocumentNode.SelectSingleNode("//div[@aria-label='Liczba pokoi']");
            var rooms = roomsAria.SelectSingleNode(valueCellClass).InnerText;
			homeData.Rooms = int.Parse(Regex.Replace(rooms, stripNonNumbers, ""));

			return homeData;
		}
	}
}

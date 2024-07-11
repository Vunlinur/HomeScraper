using HtmlAgilityPack;
using System.Linq;
using System.Text.RegularExpressions;

namespace HomeScraper.Providers {
	[Provider]
	class Otodom : IProvider {
		const string valueCellClass = ".//div[@class='css-1wi2w6s']";
		const string stripNonNumbers = @"[^,\d]";
		// TODO inherit these fields
		const string providerName = "otodom";
		const string providerLink = "https://www.otodom.pl/";

		public HomeData GetHomeData(string link) {
			var web = new HtmlWeb();
			var doc = web.Load(link);

			var homeData = new HomeData();
			homeData.Provider = providerName;
			homeData.Link = link;

			var price = doc.DocumentNode.SelectSingleNode("//strong[@data-cy='adPageHeaderPrice']").InnerText;
			homeData.Price = int.Parse(Regex.Replace(price, stripNonNumbers, ""));

			homeData.Address = doc.DocumentNode.SelectSingleNode("//a[@aria-label='Adres']").InnerText;

			var area = doc.DocumentNode.SelectSingleNode("//*[@id=\"__next\"]/main/div[2]/div[2]/div[1]/div/div[1]/div[2]/div").InnerText;
			homeData.Area = float.Parse(Regex.Replace(area, stripNonNumbers, ""));
			
			var landArea = doc.DocumentNode.SelectSingleNode("//div[@aria-label='Powierzchnia działki']")?.InnerText;
			if (landArea is not null)
				homeData.LandArea = float.Parse(Regex.Replace(landArea, stripNonNumbers, ""));

			var rooms = doc.DocumentNode.SelectSingleNode("//*[@id=\"__next\"]/main/div[2]/div[2]/div[1]/div/div[3]/div[2]/div/a").InnerText;
			homeData.Rooms = int.Parse(Regex.Replace(rooms, stripNonNumbers, ""));

			return homeData;
		}
	}
}

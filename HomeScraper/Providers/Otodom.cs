using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HomeScraper.Providers {
	[Provider]
	class Otodom : IProvider {
		const string valueCellClass = ".//div[@class='css-1wi2w6s estckra5']";
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

			var detailTable = doc.DocumentNode.SelectSingleNode("//div[@data-testid='ad.top-information.table']");

			var area = detailTable.SelectSingleNode("//div[@aria-label='Powierzchnia']").SelectSingleNode(valueCellClass).InnerText;
			homeData.Area = float.Parse(Regex.Replace(area, stripNonNumbers, ""));
			
			var landArea = detailTable.SelectSingleNode("//div[@aria-label='Powierzchnia działki']").SelectSingleNode(valueCellClass).InnerText;
			homeData.LandArea = float.Parse(Regex.Replace(landArea, stripNonNumbers, ""));

			var rooms = detailTable.SelectSingleNode("//div[@aria-label='Liczba pokoi']").SelectSingleNode(valueCellClass).InnerText;
			homeData.Rooms = int.Parse(Regex.Replace(rooms, stripNonNumbers, ""));

			return homeData;
		}
	}
}

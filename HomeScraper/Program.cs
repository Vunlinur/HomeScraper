using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace HomeScraper {
	class Program {
		static void Main(string[] args) {
			var links = @"https://www.otodom.pl/pl/oferta/5-pokojowy-dom-120m2-ogrodek-bezposrednio-ID4i9iU
https://www.otodom.pl/pl/oferta/nawet-7-pokoi-w-wawrze-pompa-ciepla-poddasze-uz-ID4iwWt
https://www.otodom.pl/pl/oferta/osiedle-chabrowa-nowoczesny-dom-173m2-garaz-x2-ID4dLzM
https://www.otodom.pl/pl/oferta/segment-z-ogrodkiem-2-miejsca-postojowe-las-ID4dQeN
https://www.otodom.pl/pl/oferta/przytulny-dom-z-duzym-ogrodem-lomianki-kielpin-ID4iv0D
https://www.otodom.pl/pl/oferta/nowy-dom-5-pokoi-duzy-ogrod-do-odbioru-w-2022-ID4ijm9
https://www.otodom.pl/pl/oferta/komfortowy-dom-w-spokojnej-okolicy-rembertow-ID4iv07
https://www.otodom.pl/pl/oferta/osiedle-atrakcyjna-ii-piekne-domy-na-bialolece-3b-ID4g5LB
https://www.otodom.pl/pl/oferta/segment-195m2-4-sypialnie-3-lazienki-garaz-ID4gOIv
https://www.otodom.pl/pl/oferta/blizniak-w-sulejowku-gotowy-do-wprowadzenia-ID4i6D5
https://www.otodom.pl/pl/oferta/promocja-segment-bandurskiego-marki-po-odbiorach-ID4hWzG
https://www.otodom.pl/pl/oferta/domy-tarchomin-112-m2-3-domy-oddanie-12-2022-ID4iqPH
https://www.otodom.pl/pl/oferta/dom-5-pokoi-zielen-cisza-zalew-promocja-ID4i0uV".Split("\r\n");

			var providers = ProviderFactory.GetProviders();

			var homeData = new List<HomeData>();
			HomeData data;
			foreach (IProvider provider in providers) {
				// TODO match provider with link
				foreach (string link in links) {
					Console.WriteLine("Scraping " + link);
					try {
						data = provider.GetHomeData(link);
						homeData.Add(data);
					}
					catch (Exception e) {
						// TODO log
						Console.WriteLine(e);
					}
					// TODO thread pooling
					Thread.Sleep(2000);
				}
			}

			// TODO proper file output
			var fileWriter = new StreamWriter("HomeData.csv");
			foreach (HomeData hd in homeData)
				fileWriter.WriteLine(hd);
			fileWriter.Close();
		}
	}
}

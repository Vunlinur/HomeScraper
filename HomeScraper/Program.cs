﻿using HomeScraper.ProviderBase;
using HomeScraper.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace HomeScraper {
    class Program {
        static void Main(string[] args) {
            var links = @"
https://www.otodom.pl/pl/oferta/dwupokojowe-mieszkanie-48-mkw-metro-wawrzyszew-ID4rpdd
https://www.otodom.pl/pl/oferta/2-pokoje-blisko-metra-ID4p9LC#map
https://www.otodom.pl/pl/oferta/dwupokojowe-zoliborz-ul-elblaska-od-1-sierpnia-ID4giUf
https://www.otodom.pl/pl/oferta/2-pokoje-z-balkonem-ID4qTaf
https://www.otodom.pl/pl/oferta/2-pokoje-bemowo-po-remoncie-ID4qoUp
https://www.otodom.pl/pl/oferta/mieszkanie-ul-obroncow-tobruku-38-fort-bema-ID4lubB
https://www.otodom.pl/pl/oferta/2-pok-przy-parku-taras-garaz-w-cenie-ID4reCC
".Split("\r\n").Where(x => !string.IsNullOrEmpty(x));

            var providers = ProviderFactory.GetProviders();

            var homeData = HomeDataSerialization.GetHomeData();
            HomeData data;
            foreach (IProvider provider in providers) {
                // TODO match provider with link
                foreach (string link in links) {
                    if (homeData.Any(d => d.Link == link))
                        continue;

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

            HomeDataSerialization.Save(homeData);
            HomeDataSerializationMD.Save(homeData);
            HomeDataSerializationCSV.Save(homeData);
        }
    }
}

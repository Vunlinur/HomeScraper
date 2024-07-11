using System;
using System.Collections.Generic;
using System.Linq;

namespace HomeScraper {
	interface IProvider {
		HomeData GetHomeData(string link);
	}
}

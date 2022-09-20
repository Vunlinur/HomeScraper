using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeScraper {
	interface IProvider {
		HomeData GetHomeData(string link);
	}
}

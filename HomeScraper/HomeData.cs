using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeScraper {
	class HomeData {
		// TODO add garage, scrape date
		public int Price;

		public float Area;
		public int Rooms;
		public float? LandArea;

		public string Address;
		public float? Distance;

		public string Link;
		public string Provider;

		public override string ToString() {
			return string.Join("|", new[] {
				Price.ToString(),
				Area.ToString(),
				Rooms.ToString(),
				LandArea.ToString(),
				Address,
				Distance.ToString(),
				Link,
				Provider
			});
		}
	}
}

using System.Linq;

namespace HomeScraper.Serialization {
    static class HomeDataCSVExtensions {

        public static string[] CSVHeaders(this HomeData homeData) =>
            GetFieldTuples(homeData).Select(t => t.name).ToArray();

        public static string[] CSVRow(this HomeData homeData) =>
            GetFieldTuples(homeData).Select(t => t.value?.ToString()).ToArray();

        private static (string name, object value)[] GetFieldTuples(HomeData d) => [
            (nameof(HomeData.Provider), d.Provider),
            (nameof(HomeData.Link), d.Link),
            (nameof(HomeData.Rooms), d.Rooms),
            (nameof(HomeData.Area), d.Area),
            (nameof(HomeData.Price), d.Price),
            (nameof(HomeData.Rent), d.Rent),
            ("total", d.Rent + d.Price),
            ("price/area", (d.Rent + d.Price) / d.Area),
            (nameof(HomeData.LandArea), d.LandArea),
            (nameof(HomeData.Address), d.Address),
            (nameof(HomeData.Distance), d.Distance)
        ];
    }
}

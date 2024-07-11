using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HomeScraper.Serialization {
    static class HomeDataSerializationMD {
        const string filePath = "HomeData.md";
        const string Delimiter = "|";

        public static void Save(IEnumerable<HomeData> homeData) {
            var fileWriter = new StreamWriter(filePath);
            fileWriter.WriteLine($"|{string.Join(Delimiter, HomeDataTable.Header())}|");
            fileWriter.WriteLine($"|{string.Join(Delimiter, HomeDataTable.Header().Select(h => "---"))}|");
            foreach (HomeData hd in homeData)
                fileWriter.WriteLine($"|{string.Join(Delimiter, HomeDataTable.Row(hd))}|");
            fileWriter.Close();
        }
    }
}

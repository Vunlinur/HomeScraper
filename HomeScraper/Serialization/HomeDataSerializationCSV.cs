using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HomeScraper.Serialization {
    static class HomeDataSerializationCSV {
        const string filePath = "HomeData.csv";

        public static string Delimiter { get; set; } = "|";

        public static void Save(IEnumerable<HomeData> homeData) {
            var fileWriter = new StreamWriter(filePath);
            fileWriter.WriteLine(string.Join(Delimiter, HomeDataTable.Header()));
            foreach (HomeData hd in homeData)
                fileWriter.WriteLine(string.Join(Delimiter, HomeDataTable.Row(hd)));
            fileWriter.Close();
        }
    }
}

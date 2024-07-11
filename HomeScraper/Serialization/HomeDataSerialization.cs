using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace HomeScraper.Serialization {
    static class HomeDataSerialization {
        public static List<HomeData> GetHomeData() {
            if (File.Exists(serializationFilePath))
                return Load();
            else
                return [];
        }

        const string serializationFilePath = "homeData.xml";
        public static void Save(IEnumerable<HomeData> homeData) {
            using var stream = Serialize(homeData);
            using var fileStream = File.Create(serializationFilePath);
            stream.Seek(0, SeekOrigin.Begin);
            stream.CopyTo(fileStream);
        }

        public static List<HomeData> Load() {
            using var fileStream = new FileStream(serializationFilePath, FileMode.Open, FileAccess.Read);
            return Deserialize(fileStream);
        }

        public static MemoryStream Serialize(IEnumerable<HomeData> homes) {
            var serializer = new XmlSerializer(typeof(List<HomeData>));
            var stream = new MemoryStream();
            serializer.Serialize(stream, homes.ToList());
            stream.Position = 0;
            return stream;
        }

        public static List<HomeData> Deserialize(Stream xml) {
            var serializer = new XmlSerializer(typeof(List<HomeData>));
            xml.Position = 0;
            return (List<HomeData>)serializer.Deserialize(xml);
        }


    }
}

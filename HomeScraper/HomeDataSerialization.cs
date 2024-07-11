using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HomeScraper {
    static class HomeDataSerialization {
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

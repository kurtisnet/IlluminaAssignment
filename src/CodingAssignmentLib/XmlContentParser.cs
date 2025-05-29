using CodingAssignmentLib.Abstractions;
using System.Xml.Serialization;

namespace CodingAssignmentLib
{
    /// <summary>
    /// Handles the parsing of a given .xml file as a collection of <see cref="Data"/>.
    /// </summary>
    public class XmlContentParser : IContentParser
    {
        /// <summary>
        /// Parses the given content of the .xml file and returns it as a collection of <see cref="Data"/>.
        /// </summary>
        /// <param name="content"> The contents of the .xml file. </param>
        /// <returns> Parsed data from the .xml file as a collection of <see cref="Data"/>. </returns>
        public IEnumerable<Data> Parse(string content)
        {
            var xmlSerializer = new XmlSerializer(typeof(List<Data>), new XmlRootAttribute("Datas"));

            using (var stringReader = new StringReader(content))
            {
                return (List<Data>)xmlSerializer.Deserialize(stringReader);
            }
        }
    }
}
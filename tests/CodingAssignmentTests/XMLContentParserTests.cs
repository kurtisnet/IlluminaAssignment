using CodingAssignmentLib;
using CodingAssignmentLib.Abstractions;
using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingAssignmentTests
{
    public class XMLContentParserTests
    {
        private readonly FileUtility _fileUtility;

        private XmlContentParser _xmlContentParser = null!;

        public XMLContentParserTests()
        {
            _fileUtility = new FileUtility(
                new FileSystem(),
                Path.Combine(AppContext.BaseDirectory, Constants.TestFilesDirectoryName));
        }

        [SetUp]
        public void Setup()
        {
            _xmlContentParser = new XmlContentParser();
        }

        [Test]
        public void Parse_ReturnsData()
        {
            var content = _fileUtility.GetContent("TestXMLFile.xml");
            var expectedList = new List<Data>()
            {
                new ("a", "b"),
                new ("c", "d")
            };

            var dataList = _xmlContentParser.Parse(content).ToList();
            Assert.That(dataList.SequenceEqual(expectedList, new IDataValueComparer()));
        }
    }
}
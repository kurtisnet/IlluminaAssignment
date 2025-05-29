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
    public class JsonContentParserTests
    {
        private readonly FileUtility _fileUtility;

        private JsonContentParser _jsonContentParser = null!;

        public JsonContentParserTests()
        {
            _fileUtility = new FileUtility(
                new FileSystem(),
                Path.Combine(AppContext.BaseDirectory, Constants.TestFilesDirectoryName));
        }

        [SetUp]
        public void Setup()
        {
            _jsonContentParser = new JsonContentParser();
        }

        [Test]
        public void Parse_ReturnsData()
        {
            var content = _fileUtility.GetContent("TestJsonFile.json");
            var expectedList = new List<Data>()
            {
                new ("a", "b"),
                new ("c", "d")
            };

            var dataList = _jsonContentParser.Parse(content).ToList();
            Assert.That(dataList.SequenceEqual(expectedList, new IDataValueComparer()));
        }
    }
}
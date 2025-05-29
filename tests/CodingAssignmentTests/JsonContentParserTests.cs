using CodingAssignmentLib;
using CodingAssignmentLib.Abstractions;
using CodingAssignmentLib.FileParsers;
using System.IO.Abstractions;

namespace CodingAssignmentTests
{
    public class JsonContentParserTests
    {
        private readonly FileUtility _fileUtility;

        private JsonContentParser _jsonContentParser = null!;

        public JsonContentParserTests()
        {
            _fileUtility = new FileUtility(new FileSystem());
        }

        [SetUp]
        public void Setup()
        {
            _jsonContentParser = new JsonContentParser();
        }

        [Test]
        [TestCase("TestJsonFile.json")]
        public void Parse_ReturnsData(string testFileName)
        {
            var jsonTestFile = FolderUtility.FindFileInDirectory(
                Path.Combine(AppContext.BaseDirectory, Constants.TestFilesDirectoryName),
                testFileName);

            var content = _fileUtility.GetContent(jsonTestFile);
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
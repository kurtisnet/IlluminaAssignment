using CodingAssignmentLib;
using CodingAssignmentLib.Abstractions;
using CodingAssignmentLib.FileParsers;
using System.IO.Abstractions;
using System.Text.Json;

namespace CodingAssignmentTests
{
    /// <summary>
    /// Contains tests for <see cref="JsonContentParser"/>.
    /// </summary>
    public class JsonContentParserTests
    {
        /// <summary>
        /// The file utility instance.
        /// </summary>
        private readonly IFileUtility _fileUtility;

        /// <summary>
        /// The parser for .json files.
        /// </summary>
        private JsonContentParser _jsonContentParser = null!;

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonContentParserTests"/> class.
        /// </summary>
        public JsonContentParserTests()
        {
            _fileUtility = new FileUtility(new FileSystem());
        }

        /// <summary>
        /// Procedures to run at the beginning of each test.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            _jsonContentParser = new JsonContentParser();
        }

        /// <summary>
        /// Tests how empty inputs are handled.
        /// </summary>
        /// <param name="input"> The empty inputs to be tested. </param>
        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void HandleEmptyInput_ReturnsNull(string input)
        {
            var dataList = _jsonContentParser.Parse(input);
            Assert.That(dataList, Is.Null);
        }

        /// <summary>
        /// Tests a given .json file and checks if it is parsed correctly.
        /// </summary>
        /// <param name="testFileName"> Name of the test .json file to look for. </param>
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
                new ("aa", "bb"),
                new ("cc", "dd"),
                new ("ee", "ff")
            };

            var dataList = _jsonContentParser.Parse(content);

            Assert.That(dataList, Is.Not.Null);
            Assert.That(dataList.SequenceEqual(expectedList, new IDataValueComparer()));
        }

        /// <summary>
        /// Tests an invalid non .json file and checks if an appropriate json serializer exception is thrown.
        /// </summary>
        /// <param name="invalidTestFileName"> Name of the invalid test file to be supplied. </param>
        [Test]
        [TestCase("TestCsvFile.csv")]
        [TestCase("TestXMLFile.xml")]
        public void ParseInvalidFile_ThrowsException(string invalidTestFileName)
        {
            var invalidTestFile = FolderUtility.FindFileInDirectory(
                Path.Combine(AppContext.BaseDirectory, Constants.TestFilesDirectoryName),
                invalidTestFileName);
            var content = _fileUtility.GetContent(invalidTestFile);

            Assert.Throws<JsonException>(() => _jsonContentParser.Parse(content));
        }
    }
}
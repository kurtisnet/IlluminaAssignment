using CodingAssignmentLib;
using CodingAssignmentLib.Abstractions;
using CodingAssignmentLib.FileParsers;
using System.IO.Abstractions;

namespace CodingAssignmentTests
{
    /// <summary>
    /// Contains tests for <see cref="XmlContentParser"/>.
    /// </summary>
    public class XMLContentParserTests
    {
        /// <summary>
        /// The file utility instance.
        /// </summary>
        private readonly FileUtility _fileUtility;

        /// <summary>
        /// The parser for .csv files.
        /// </summary>
        private XmlContentParser _xmlContentParser = null!;

        /// <summary>
        /// Initializes a new instance of the <see cref="XMLContentParserTests"/> class.
        /// </summary>
        public XMLContentParserTests()
        {
            _fileUtility = new FileUtility(new FileSystem());
        }

        /// <summary>
        /// Procedures to run at the beginning of each test.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            _xmlContentParser = new XmlContentParser();
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
            var dataList = _xmlContentParser.Parse(input);
            Assert.That(dataList, Is.Null);
        }

        /// <summary>
        /// Tests a given .xml file and checks if it is parsed correctly.
        /// </summary>
        /// <param name="testFileName"> Name of the test .xml file to look for. </param>
        [Test]
        [TestCase("TestXMLFile.xml")]
        public void Parse_ReturnsData(string testFileName)
        {
            var xmlTestFile = FolderUtility.FindFileInDirectory(
               Path.Combine(AppContext.BaseDirectory, Constants.TestFilesDirectoryName),
               testFileName);

            var content = _fileUtility.GetContent(xmlTestFile);
            var expectedList = new List<Data>()
            {
                new ("aaa", "bbb"),
                new ("ccc", "ddd"),
                new ("eee", "fff"),
                new ("aaAA", "bbBB"),
                new ("a1aAA", "b1bBB")
            };

            var dataList = _xmlContentParser.Parse(content);

            Assert.That(dataList, Is.Not.Null);
            Assert.That(dataList.SequenceEqual(expectedList, new IDataValueComparer()));
        }

        /// <summary>
        /// Tests an invalid non .xml file and checks if an appropriate XML serializer exception is thrown.
        /// </summary>
        /// <param name="invalidTestFileName"> Name of the invalid test file to be supplied. </param>
        [Test]
        [TestCase("TestCsvFile.csv")]
        [TestCase("TestJsonFile.json")]
        public void ParseInvalidFile_ThrowsException(string invalidTestFileName)
        {
            var invalidTestFile = FolderUtility.FindFileInDirectory(
                Path.Combine(AppContext.BaseDirectory, Constants.TestFilesDirectoryName),
                invalidTestFileName);
            var content = _fileUtility.GetContent(invalidTestFile);

            Assert.Throws<InvalidOperationException>(() => _xmlContentParser.Parse(content));
        }
    }
}
using CodingAssignmentLib;
using System.IO.Abstractions;

namespace CodingAssignmentTests
{
    /// <summary>
    /// Contains tests for <see cref="FileParsingHandler"/>.
    /// </summary>
    public class FileParsingHandlerTests
    {
        /// <summary>
        /// Directory containing test files.
        /// </summary>
        private readonly string _testFilesDirectory;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileParsingHandlerTests"/> class.
        /// </summary>
        public FileParsingHandlerTests()
        {
            _testFilesDirectory = Path.Combine(AppContext.BaseDirectory, Constants.TestFilesDirectoryName);
        }

        /// <summary>
        /// Tests whether the given test file is parsed correctly.
        /// </summary>
        /// <param name="testFileName"> The name of the test file. </param>
        /// <param name="dataItemsCount"> Total number of data items expected from parsing the test file. </param>
        [Test]
        [TestCase("TestCsvFile.csv", 2)]
        [TestCase("TestJsonFile.json", 3)]
        [TestCase("TestXMLFile.xml", 5)]
        public void ParseFiles_ReturnDataCorrectly(string testFileName, int dataItemsCount)
        {
            var testFilePath = FolderUtility.FindFileInDirectory(_testFilesDirectory, testFileName);

            var fileParsingHandler = new FileParsingHandler(testFilePath, new FileUtility(new FileSystem()));
            var fileData = fileParsingHandler.GetDataFromFile();

            Assert.That(fileData, Is.Not.Null);
            Assert.That(fileData.Count(), Is.EqualTo(dataItemsCount));
        }

        /// <summary>
        /// Tests the behavior of the handler when an unsupported file is supplied.
        /// </summary>
        /// <param name="testFileName"> Name of the invalid test file. </param>
        [Test]
        [TestCase("UnsupportedFile.txt")]
        public void ParseUnsupportedFile_ThrowsException(string testFileName)
        {
            var testFilePath = FolderUtility.FindFileInDirectory(_testFilesDirectory, testFileName);

            var fileParsingHandler = new FileParsingHandler(testFilePath, new FileUtility(new FileSystem()));

            Assert.Throws<ArgumentException>(() => fileParsingHandler.GetDataFromFile());
        }
    }
}
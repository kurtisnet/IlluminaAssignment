using CodingAssignmentLib;
using CodingAssignmentLib.Abstractions;
using CodingAssignmentLib.FileParsers;
using System.IO.Abstractions;

namespace CodingAssignmentTests;

/// <summary>
/// Contains tests for the <see cref="CsvContentParser"/>
/// </summary>
public class CsvContentParserTests
{
    /// <summary>
    /// The file utility instance.
    /// </summary>
    private readonly IFileUtility _fileUtility;

    /// <summary>
    /// The parser for .csv files.
    /// </summary>
    private CsvContentParser _csvContentParser = null!;

    /// <summary>
    /// Initializes a new instance of the <see cref="CsvContentParserTests"/> class.
    /// </summary>
    public CsvContentParserTests()
    {
        _fileUtility = new FileUtility(new FileSystem());
    }

    /// <summary>
    /// Procedures to run at the beginning of each test.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        _csvContentParser = new CsvContentParser();
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
        var dataList = _csvContentParser.Parse(input);
        Assert.That(dataList, Is.Null);
    }

    /// <summary>
    /// Tests various comma separated values with different line endings to see if they are handled properly.
    /// </summary>
    /// <param name="input"> Comma separated values input with different line endings. </param>
    /// <param name="expectedItemsCount"> Total number of expected results from parsing the input. </param>
    [Test]
    [TestCase($"a,b\r\nc,d", 2)]
    [TestCase($"a,b\nc,d\ne,f", 3)]
    public void HandleLineEndings_HandlesCorrectly(string input, int expectedItemsCount)
    {
        var dataList = _csvContentParser.Parse(input);

        Assert.That(dataList, Is.Not.Null);
        Assert.That(dataList.Count, Is.EqualTo(expectedItemsCount));
    }

    /// <summary>
    /// Tests a given .csv string input and checks if it is parsed correctly.
    /// </summary>
    [Test]
    public void Parse_ReturnsData()
    {
        var content = "a,b" + Environment.NewLine + "c,d" + Environment.NewLine;
        var expectedList = new List<Data>()
        {
            new ("a", "b"),
            new ("c", "d")
        };

        var dataList = _csvContentParser.Parse(content);

        Assert.That(dataList, Is.Not.Null);
        Assert.That(dataList.SequenceEqual(expectedList, new IDataValueComparer()));
    }

    /// <summary>
    /// Tests a given .csv file and checks if it is parsed correctly.
    /// </summary>
    /// <param name="testFileName"> Name of the test .csv file to look for. </param>
    [Test]
    [TestCase("TestCsvFile.csv")]
    public void ParseFile_ReturnsData(string testFileName)
    {
        var csvTestFile = FolderUtility.FindFileInDirectory(
            Path.Combine(AppContext.BaseDirectory, Constants.TestFilesDirectoryName),
            testFileName);

        var content = _fileUtility.GetContent(csvTestFile);
        var expectedList = new List<Data>()
            {
                new ("a", "b"),
                new ("c", "d")
            };

        var dataList = _csvContentParser.Parse(content);

        Assert.That(dataList, Is.Not.Null);
        Assert.That(dataList.SequenceEqual(expectedList, new IDataValueComparer()));
    }
}
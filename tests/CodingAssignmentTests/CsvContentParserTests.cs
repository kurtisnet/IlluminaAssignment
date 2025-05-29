using CodingAssignmentLib.Abstractions;
using CodingAssignmentLib.FileParsers;

namespace CodingAssignmentTests;

public class CsvContentParserTests
{
    private CsvContentParser _sut = null!;

    [SetUp]
    public void Setup()
    {
        _sut = new CsvContentParser();
    }

    [Test]
    public void Parse_ReturnsData()
    {
        var content = "a,b" + Environment.NewLine + "c,d" + Environment.NewLine;
        var expectedList = new List<Data>()
        {
            new ("a", "b"),
            new ("c", "d")
        };

        var dataList = _sut.Parse(content).ToList();
        Assert.That(dataList.SequenceEqual(expectedList, new IDataValueComparer()));
    }

    [Test]
    [TestCase($"a,b\r\nc,d", 2)]
    [TestCase($"a,b\nc,d\ne,f", 3)]
    public void HandleLineEndings_HandlesCorrectly(string input, int expectedItemsCount)
    {
        var dataList = _sut.Parse(input);
        Assert.That(dataList.Count, Is.EqualTo(expectedItemsCount));
    }
}
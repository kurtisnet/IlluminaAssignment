using CodingAssignmentLib;

namespace CodingAssignmentTests
{
    /// <summary>
    /// Contains tests for the <see cref="KeywordFinder"/>.
    /// </summary>
    public class KeywordFinderTests
    {
        /// <summary>
        /// The full path to the directory containing test files.
        /// </summary>
        private readonly string _testFilesDirectory;

        /// <summary>
        /// The keyword finder instance.
        /// </summary>
        private KeywordFinder _keywordFinder;

        /// <summary>
        /// Initializes a new instance of the <see cref="KeywordFinderTests"/> class.
        /// </summary>
        public KeywordFinderTests()
        {
            _testFilesDirectory = Path.Combine(AppContext.BaseDirectory, Constants.TestFilesDirectoryName);
        }

        /// <summary>
        /// Procedures to run at the beginning of each test.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            _keywordFinder = new KeywordFinder(_testFilesDirectory);
        }

        /// <summary>
        /// Tests the results when invalid keywords are supplied.
        /// </summary>
        /// <param name="invalidKeyword"> The keyword that should not return any results. </param>
        [Test]
        [TestCase("")]
        [TestCase("test")]
        public void FindInvalidKeys_ReturnsZeroResults(string invalidKeyword)
        {
            var resultDict = _keywordFinder.FindFilesWithKeyword(invalidKeyword);

            Assert.That(resultDict, Is.Not.Null);
            Assert.That(resultDict.Count, Is.EqualTo(0));
        }

        /// <summary>
        /// Tests valid keywords to see if expected results are returned.
        /// </summary>
        /// <param name="keyword"> The keyword to be tested. </param>
        [Test]
        [TestCase("a", new string[] { "a", "aa", "aaa", "aaAA", "a1aAA" })]
        [TestCase("A", new string[] { "a", "aa", "aaa", "aaAA", "a1aAA" })]
        [TestCase("aa", new string[] { "aa", "aaa", "aaAA" })]
        [TestCase("aA", new string[] { "aa", "aaa", "aaAA" })]
        [TestCase("a1", new string[] { "a1aAA" })]
        [TestCase("A1", new string[] { "a1aAA" })]
        [TestCase("cc", new string[] { "cc", "ccc" })]
        public void FindValidKeys_FoundKeysSuccessfully(string keyword, string[] expectedKeys)
        {
            var resultDict = _keywordFinder.FindFilesWithKeyword(keyword);

            // Get a flat list of all the keys from the results.
            var allKeyResults = resultDict.SelectMany(kvp => kvp.Value).Select(d => d.Key);

            Assert.That(expectedKeys.All(r => allKeyResults.Contains(r)));
        }
    }
}
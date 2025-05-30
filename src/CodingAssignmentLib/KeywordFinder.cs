using CodingAssignmentLib.Abstractions;

namespace CodingAssignmentLib
{
    /// <summary>
    /// Handles finding a keyword across all the files in a given directory.
    /// </summary>
    public class KeywordFinder : KeywordFinderBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KeywordFinder"/> class.
        /// </summary>
        /// <param name="directoryPath"> The full path to the directory in which data files exist. </param>
        public KeywordFinder(string directoryPath) : base(directoryPath)
        {
        }

        /// <summary>
        /// Finds files containing a full or partial match of the given keyword and returns the result as a
        /// dictionary where the key is the file path and the value is the list of matches. The keyword
        /// search is also not case-sensitive.
        /// </summary>
        /// <param name="keyword"> The keyword to look for. </param>
        /// <returns> Dictionary where the key is the file path and the value is a list of matching
        /// <see cref="Data"/> for the given keyword. Returns an empty dictionary if no results are found.
        /// </returns>
        public override Dictionary<string, List<Data>> FindFilesWithKeyword(string keyword)
        {
            var filePaths = FindFilesInDirectory();
            var result = new Dictionary<string, List<Data>>();

            if (string.IsNullOrWhiteSpace(keyword))
            {
                return result;
            }

            foreach (var filePath in filePaths)
            {
                IEnumerable<Data>? dataList;

                try
                {
                    dataList = GetDataFromFile(filePath);
                }
                catch (Exception)
                {
                    // Skip the file if it failed to parse.
                    continue;
                }

                var matchingDatasets = TryGetMatchingDatasets(
                    dataList,
                    keyword,
                    StringComparison.OrdinalIgnoreCase);

                if (matchingDatasets != null)
                {
                    result[filePath] = matchingDatasets.ToList();
                }
            }

            return result;
        }
    }
}
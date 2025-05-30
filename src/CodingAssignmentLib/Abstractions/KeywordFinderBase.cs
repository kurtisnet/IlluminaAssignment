using System.IO.Abstractions;

namespace CodingAssignmentLib.Abstractions
{
    /// <summary>
    /// Base class handling the finding of data files in a given directory, parsing them and looking for a
    /// keyword that matches fully or partially.
    /// </summary>
    public abstract class KeywordFinderBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KeywordFinderBase"/> class.
        /// </summary>
        /// <param name="directoryPath"> The full path to the directory in which data files exist. </param>
        public KeywordFinderBase(string directoryPath)
        {
            DirectoryPath = directoryPath;
        }

        /// <summary>
        /// Gets or sets the full path to the directory in which data files exist.
        /// </summary>
        protected string DirectoryPath { get; set; }

        /// <summary>
        /// Finds files containing a full or partial match of the given keyword and returns the result as a
        /// dictionary where the key is the file path and the value is the list of matches. The keyword
        /// search is also not case-sensitive.
        /// </summary>
        /// <param name="keyword"> The keyword to look for. </param>
        /// <returns> Dictionary where the key is the file path and the value is a list of matching
        /// <see cref="Data"/> for the given keyword. </returns>
        public abstract Dictionary<string, List<Data>> FindFilesWithKeyword(string keyword);

        /// <summary>
        /// Finds and returns all files found in the given directory.
        /// </summary>
        /// <returns> Array of all file paths found in the given directory. </returns>
        protected virtual string[] FindFilesInDirectory()
        {
            return FolderUtility.GetAllFiles(this.DirectoryPath);
        }

        /// <summary>
        /// Parses the given file and returns its data as a collection of <see cref="Data"/>.
        /// </summary>
        /// <param name="filePath"> The full path to the file to be parsed. </param>
        /// <returns> Parsed file data as a collection of <see cref="Data"/>. </returns>
        protected virtual IEnumerable<Data>? GetDataFromFile(string filePath)
        {
            var fileParsingHandler = new FileParsingHandler(filePath, new FileUtility(new FileSystem()));
            return fileParsingHandler.GetDataFromFile();
        }

        /// <summary>
        /// Tries to find one or more matching data items from the given data list in which the key value
        /// fully or partially matches the given keyword.
        /// </summary>
        /// <param name="dataList"> The collection of <see cref="Data"/> from which the keys matching the
        /// keyword should be looked for. </param>
        /// <param name="keyword"> The keyword to be found. </param>
        /// <param name="stringComparison"> Controls if keyword searches should be case sensitive or not. </param>
        /// <returns> A collection of <see cref="Data"/> if at least one match is found. Null otherwise. </returns>
        protected virtual IEnumerable<Data>? TryGetMatchingDatasets(
            IEnumerable<Data>? dataList,
            string keyword,
            StringComparison stringComparison)
        {
            if (dataList == null)
            {
                return null;
            }

            var matchingDataSets = dataList.Where(i =>
                i.Key != null &&
                i.Key.StartsWith(keyword, stringComparison));

            return !matchingDataSets.Any() ? null : matchingDataSets;
        }
    }
}
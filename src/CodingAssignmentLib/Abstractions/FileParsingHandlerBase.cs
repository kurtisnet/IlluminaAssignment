namespace CodingAssignmentLib.Abstractions
{
    /// <summary>
    /// Base class handling all the major file parsing logic.
    /// </summary>
    public abstract class FileParsingHandlerBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileParsingHandlerBase"/> class.
        /// </summary>
        /// <param name="filePath"> The full path to the file to be parsed. </param>
        /// <param name="fileUtility"> The file utility for file IO operations. </param>
        public FileParsingHandlerBase(string filePath, IFileUtility fileUtility)
        {
            FilePath = filePath;
            FileUtility = fileUtility;
        }

        /// <summary>
        /// Gets or sets the full path to the file to be parsed.
        /// </summary>
        protected string FilePath { get; set; }

        /// <summary>
        /// Gets or sets the file utility for file IO operations.
        /// </summary>
        protected IFileUtility FileUtility { get; set; }

        /// <summary>
        /// Parses the given file and returns its data as a collection of <see cref="Data"/>.
        /// </summary>
        /// <returns> Collection of <see cref="Data"/> obtained from parsing the given file. </returns>
        public abstract IEnumerable<Data>? GetDataFromFile();
    }
}
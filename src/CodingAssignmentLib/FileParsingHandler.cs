using CodingAssignmentLib.Abstractions;
using CodingAssignmentLib.FileParsers;

namespace CodingAssignmentLib
{
    /// <summary>
    /// Class handling appropriate file parsing depending on the given file's extension.
    /// </summary>
    public class FileParsingHandler : FileParsingHandlerBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileParsingHandler"/> class.
        /// </summary>
        /// <param name="filePath"> The full path to the file to be parsed. </param>
        /// <param name="fileUtility"> The file utility for file IO operations. </param>
        public FileParsingHandler(string filePath, IFileUtility fileUtility) : base(filePath, fileUtility)
        {
        }

        /// <summary>
        /// Parses the given file and returns its data as a collection of <see cref="Data"/>.
        /// </summary>
        /// <param name="filePath"> Full path to the file to be parsed. </param>
        /// <returns> Collection of <see cref="Data"/> obtained from parsing the given file. </returns>
        /// <exception cref="ArgumentException"> Throws if an unsupported file is supplied. </exception>
        public override IEnumerable<Data>? GetDataFromFile()
        {
            IEnumerable<Data>? dataList;

            var fileContent = FileUtility.GetContent(FilePath);

            switch (FileUtility.GetExtension(FilePath))
            {
                case FileExtensions.Csv:
                    dataList = new CsvContentParser().Parse(fileContent);
                    break;

                case FileExtensions.Json:
                    dataList = new JsonContentParser().Parse(fileContent);
                    break;

                case FileExtensions.Xml:
                    dataList = new XmlContentParser().Parse(fileContent);
                    break;

                default:
                    throw new ArgumentException("The given file extension is not recognized.");
            }

            return dataList;
        }
    }
}
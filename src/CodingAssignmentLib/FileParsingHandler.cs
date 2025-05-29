using CodingAssignmentLib.Abstractions;
using CodingAssignmentLib.FileParsers;
using System.IO.Abstractions;

namespace CodingAssignmentLib
{
    /// <summary>
    /// Class handling appropriate file parsing depending on the given file's extension.
    /// </summary>
    public static class FileParsingHandler
    {
        /// <summary>
        /// Parses the given file and returns its data as a collection of <see cref="Data"/>.
        /// </summary>
        /// <param name="filePath"> Full path to the file to be parsed. </param>
        /// <returns> Collection of <see cref="Data"/> obtained from parsing the given file. </returns>
        /// <exception cref="ArgumentException"> Throws if an unsupported file is supplied. </exception>
        public static IEnumerable<Data>? GetDataFromFile(string filePath)
        {
            var fileUtility = new FileUtility(new FileSystem());
            IEnumerable<Data>? dataList;

            var fileContent = fileUtility.GetContent(filePath);

            switch (fileUtility.GetExtension(filePath))
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
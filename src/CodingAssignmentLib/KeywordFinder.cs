using CodingAssignmentLib.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingAssignmentLib
{
    /// <summary>
    /// Handles finding a keyword across all the files in a given directory.
    /// </summary>
    public static class KeywordFinder
    {
        /// <summary>
        /// Finds files containing a full or partial match of the given keyword and returns the result as a
        /// dictionary where the key is the file path and the value is the list of matches. The keyword
        /// search is also not case-sensitive.
        /// </summary>
        /// <param name="directory"> The path to the directory containing files to search. </param>
        /// <param name="keyword"> The keyword to look for. </param>
        /// <returns> Dictionary where the key is the file path and the value is a list of matching
        /// <see cref="Data"/> for the given keyword. </returns>
        public static Dictionary<string, List<Data>> FindFilesWithKeyword(
            string directory,
            string keyword)
        {
            var filePaths = FolderUtility.GetAllFiles(directory);
            var result = new Dictionary<string, List<Data>>();

            foreach (var filePath in filePaths)
            {
                IEnumerable<Data>? dataList;

                try
                {
                    dataList = FileParsingHandler.GetDataFromFile(filePath);
                }
                catch (Exception)
                {
                    // Skip the file if it failed to parse.
                    continue;
                }

                if (dataList != null)
                {
                    var matchingData = dataList.FirstOrDefault(i =>
                        i.Key != null &&
                        i.Key.StartsWith(keyword, StringComparison.OrdinalIgnoreCase));

                    if (matchingData != null)
                    {
                        if (result.TryGetValue(filePath, out var existingDataList))
                        {
                            existingDataList.Add(matchingData);
                        }
                        else
                        {
                            result[filePath] = new List<Data>() { matchingData };
                        }
                    }
                }
            }

            return result;
        }
    }
}
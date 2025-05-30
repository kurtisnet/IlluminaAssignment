namespace CodingAssignmentLib
{
    /// <summary>
    /// Handles folder related utilities.
    /// </summary>
    public static class FolderUtility
    {
        /// <summary>
        /// Gets all files within the given directory as well as the sub-directories recursively.
        /// </summary>
        /// <param name="folderPath"> The full path to the folder containing files. </param>
        /// <returns> Array of all the file paths in the given folder as well as sub folders. </returns>
        public static string[] GetAllFiles(string folderPath)
        {
            return Directory.GetFiles(folderPath, string.Empty, SearchOption.AllDirectories);
        }

        /// <summary>
        /// Finds and returns the full path to a given file name in the given data directory and its sub
        /// directories.
        /// </summary>
        /// <param name="folderPath"> The full path to the folder to find the file. </param>
        /// <param name="fileName"> Name of the file to be found. </param>
        /// <returns> The full path to the file if it is found inside the given folder (or its sub directory).
        /// Null otherwise. </returns>
        public static string? FindFileInDirectory(string folderPath, string fileName)
        {
            return GetAllFiles(folderPath).FirstOrDefault(f => Path.GetFileName(f) == fileName);
        }
    }
}
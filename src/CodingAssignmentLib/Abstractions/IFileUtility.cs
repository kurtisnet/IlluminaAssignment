namespace CodingAssignmentLib.Abstractions;

/// <summary>
/// Interface containing file and IO related utilities.
/// </summary>
public interface IFileUtility
{
    /// <summary>
    /// Extracts the extension from the given file name.
    /// </summary>
    /// <param name="fileName"> The file name from which the extension should be extracted. </param>
    /// <returns> The extension (including the . prefix) of the given file name. </returns>
    string GetExtension(string fileName);

    /// <summary>
    /// Reads and returns the raw string of the contents of the given file.
    /// </summary>
    /// <param name="filePath"> The full path to the file to be read. </param>
    /// <returns> Raw string of the given file. </returns>
    string GetContent(string filePath);
}
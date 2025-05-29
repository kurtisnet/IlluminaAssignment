using System.IO.Abstractions;
using CodingAssignmentLib.Abstractions;

namespace CodingAssignmentLib;

/// <summary>
/// Handles file system and IO related utilities.
/// </summary>
public class FileUtility : IFileUtility
{
    /// <summary>
    /// Abstraction of the file system.
    /// </summary>
    private readonly IFileSystem _fileSystem;

    /// <summary>
    /// Initializes a new instance of the <see cref="FileUtility"/> class.
    /// </summary>
    /// <param name="fileSystem"> The file system instance. </param>
    /// <param name="dataFolderPath"> The full path to the folder containing the data files. </param>
    public FileUtility(IFileSystem fileSystem)
    {
        _fileSystem = fileSystem;
    }

    /// <summary>
    /// Reads and returns the raw string of the contents of the given file.
    /// </summary>
    /// <param name="filePath"> The full path to the file to be read. </param>
    /// <returns> Raw string of the given file. </returns>
    public string GetContent(string filePath)
    {
        return _fileSystem.File.ReadAllText(filePath);
    }

    /// <summary>
    /// Extracts the extension from the given file name.
    /// </summary>
    /// <param name="fileName"> The file name from which the extension should be extracted. </param>
    /// <returns> The extension (including the . prefix) of the given file name. </returns>
    public string GetExtension(string fileName)
    {
        return _fileSystem.FileInfo.New(fileName).Extension;
    }
}
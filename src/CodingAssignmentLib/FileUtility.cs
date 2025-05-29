using System.IO.Abstractions;
using CodingAssignmentLib.Abstractions;

namespace CodingAssignmentLib;

/// <summary>
/// Handles file system and IO related utilities.
/// </summary>
public class FileUtility : IFileUtility
{
    /// <summary>
    /// The full path to the directory containing the data files.
    /// </summary>
    private readonly string _dataFolderPath;

    /// <summary>
    /// Abstraction of the file system.
    /// </summary>
    private readonly IFileSystem _fileSystem;

    /// <summary>
    /// Initializes a new instance of the <see cref="FileUtility"/> class.
    /// </summary>
    /// <param name="fileSystem"> The file system instance. </param>
    /// <param name="dataFolderPath"> The full path to the folder containing the data files. </param>
    public FileUtility(IFileSystem fileSystem, string dataFolderPath)
    {
        _fileSystem = fileSystem;
        _dataFolderPath = dataFolderPath;
    }

    /// <summary>
    /// Reads and returns the raw string of the contents of the given file.
    /// </summary>
    /// <param name="fileName"> The name of the file to be read. </param>
    /// <returns> Raw string of the given file. </returns>
    /// <exception cref="FileNotFoundException"> Throws if there are no files with the given file name found
    /// in the data directory. </exception>
    public string GetContent(string fileName)
    {
        var filePath = GetFilePathInDataDirectory(fileName);

        if (filePath == null)
        {
            throw new FileNotFoundException($"The file with the given name {fileName} is not found in the " +
                $"data directory, {_dataFolderPath}.");
        }

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

    /// <summary>
    /// Tries to find the file with the given name in the data directory and returns its full path. Returns
    /// null if it is not found.
    /// </summary>
    /// <param name="fileName"> The name of the file to look for in the data directory. </param>
    /// <returns> The full path to the file if it is found. Null otherwise. </returns>
    private string? GetFilePathInDataDirectory(string fileName)
    {
        return Directory.GetFiles(_dataFolderPath, string.Empty, SearchOption.AllDirectories)
            .FirstOrDefault(f => Path.GetFileName(f) == fileName);
    }
}
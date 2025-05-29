// See https://aka.ms/new-console-template for more information

using CodingAssignmentApp;
using CodingAssignmentLib;
using CodingAssignmentLib.Abstractions;

Console.WriteLine("Coding Assignment!");

do
{
    Console.WriteLine("\n---------------------------------------\n");
    Console.WriteLine("Choose an option from the following list:");
    Console.WriteLine("\t1 - Display");
    Console.WriteLine("\t2 - Search");
    Console.WriteLine("\t3 - Exit");

    switch (Console.ReadLine())
    {
        case "1":
            Display();
            break;

        case "2":
            Search();
            break;

        case "3":
            return;

        default:
            return;
    }
} while (true);

/// <summary>
/// Feature to display the contents of a given supported file.
/// </summary>
void Display()
{
    Console.WriteLine("Enter the name of the file with its extension to display its content:");

    var fileName = Console.ReadLine()!;

    // Find if the file exists with the given name.
    var filePath = FolderUtility.FindFileInDirectory(
       Path.Combine(AppContext.BaseDirectory, Constants.DataDirectoryName),
       fileName);

    if (filePath == null)
    {
        Console.WriteLine($"The file with the given name {fileName} is not found in the " +
           $"{Constants.DataDirectoryName} directory.");
        return;
    }

    IEnumerable<Data>? dataList;

    try
    {
        dataList = FileParsingHandler.GetDataFromFile(filePath);
    }
    catch (Exception ex)
    {
        Console.WriteLine("An unexpected error has been encountered while parsing the file. The following " +
            $"error details might be helpful.\n{ex.Message}");
        return;
    }

    if (dataList != null)
    {
        if (!dataList.Any())
        {
            Console.WriteLine($"\nThe file {fileName} is empty.");
            return;
        }

        Console.WriteLine("\nData:");

        foreach (var data in dataList)
        {
            Console.WriteLine($"Key:{data.Key} Value:{data.Value}");
        }
    }
}

/// <summary>
/// Feature to allow the user to search for a key value either partially or fully across files in the data
/// directory in a non case-sensitive manner.
/// </summary>
void Search()
{
    Console.WriteLine("Enter the key to search.");
    var keyword = Console.ReadLine()!;

    var matchingFilesDict = KeywordFinder.FindFilesWithKeyword(
        Path.Combine(AppContext.BaseDirectory, Constants.DataDirectoryName),
        keyword);

    if (matchingFilesDict.Count == 0)
    {
        foreach (var kvp in matchingFilesDict)
        {
            var trimmedFilePath = kvp.Key.Replace(AppContext.BaseDirectory, string.Empty);
            foreach (var matchingData in kvp.Value)
            {
                Console.WriteLine($"Key:{matchingData.Key} Value:{matchingData.Value} FileName:{trimmedFilePath}");
            }
        }
    }
    else
    {
        Console.WriteLine("The given keyword is not found in any of the files.");
    }
}
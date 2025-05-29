// See https://aka.ms/new-console-template for more information

using System.IO.Abstractions;
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
    var dataList = GetDataFromFile(fileName);

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

IEnumerable<Data>? GetDataFromFile(string fileName)
{
    var fileUtility = new FileUtility(
        new FileSystem(),
        Path.Combine(AppContext.BaseDirectory, Constants.DataDirectoryName));

    IEnumerable<Data>? dataList = null;

    try
    {
        var fileContent = fileUtility.GetContent(fileName);

        switch (fileUtility.GetExtension(fileName))
        {
            case Constants.Csv:
                dataList = new CsvContentParser().Parse(fileContent);
                break;

            case Constants.Json:
                dataList = new JsonContentParser().Parse(fileContent);
                break;

            case Constants.Xml:
                dataList = new XmlContentParser().Parse(fileContent);
                break;

            default:
                Console.WriteLine("This file type is not supported.");
                return null;
        }
    }
    catch (FileNotFoundException)
    {
        Console.WriteLine($"The file with the given name {fileName} is not found in the " +
            $"{Constants.DataDirectoryName} directory.");
    }

    return dataList;
}

/// <summary>
/// Feature to allow the user to search for a key value either partially or fully across files in the data
/// directory in a non case-sensitive manner.
/// </summary>
void Search()
{
    Console.WriteLine("Enter the key to search.");
}
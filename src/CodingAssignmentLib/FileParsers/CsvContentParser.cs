using CodingAssignmentLib.Abstractions;

namespace CodingAssignmentLib.FileParsers;

/// <summary>
/// Handles the parsing of a given .csv file as a collection of <see cref="Data"/>.
/// </summary>
public class CsvContentParser : IContentParser
{
    /// <summary>
    /// Parses the given content of the .csv file and returns it as a collection of <see cref="Data"/>.
    /// </summary>
    /// <param name="content"> The contents of the .csv file. </param>
    /// <returns> Parsed data from the .csv file as a collection of <see cref="Data"/>. </returns>
    public IEnumerable<Data>? Parse(string content)
    {
        if (string.IsNullOrWhiteSpace(content))
        {
            return null;
        }

        return content
            .ReplaceLineEndings()
            .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
            .Select(line =>
            {
                var items = line.Split(',', StringSplitOptions.RemoveEmptyEntries);
                return new Data(items[0], items[1]);
            });
    }
}
namespace CodingAssignmentLib.Abstractions;

/// <summary>
/// Interface representing content to be parsed.
/// </summary>
public interface IContentParser
{
    /// <summary>
    /// Parses the given content and returns it as a collection of <see cref="Data"/>.
    /// </summary>
    /// <param name="content"> The content to be parsed. </param>
    /// <returns> Given content parsed as a collection of <see cref="Data"/>. </returns>
    IEnumerable<Data>? Parse(string content);
}
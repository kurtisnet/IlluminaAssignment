namespace CodingAssignmentLib.Abstractions;

/// <summary>
/// Represents common data structure for all supported file types. Uses a class so that data serialization
/// is possible.
/// </summary>
public class Data
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Data"/> class.
    /// </summary>
    /// <param name="key"> The key related to the value. </param>
    /// <param name="value"> The data value. </param>
    public Data(string key, string value)
    {
        Key = key;
        Value = value;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Data"/> class. Required for de-serialization.
    /// </summary>
    public Data()
    {
    }

    /// <summary>
    /// Gets or sets the key related the value.
    /// </summary>
    public string? Key { get; set; }

    /// <summary>
    /// Gets or sets the data value.
    /// </summary>
    public string? Value { get; set; }
}
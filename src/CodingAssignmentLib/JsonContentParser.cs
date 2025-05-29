using CodingAssignmentLib.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CodingAssignmentLib
{
    // <summary>
    /// Handles the parsing of a given .json file as a collection of <see cref="Data"/>.
    /// </summary>
    public class JsonContentParser : IContentParser
    {
        /// <summary>
        /// Parses the given content of the .json file and returns it as a collection of <see cref="Data"/>.
        /// </summary>
        /// <param name="content"> The contents of the .json file. </param>
        /// <returns> Parsed data from the .json file as a collection of <see cref="Data"/>. </returns>
        public IEnumerable<Data> Parse(string content)
        {
            return JsonSerializer.Deserialize<List<Data>>(content);
        }
    }
}
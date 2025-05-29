using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingAssignmentLib.Abstractions
{
    /// <summary>
    /// Helps compare the key and value properties of the given <see cref="Data"/> objects.
    /// </summary>
    public class IDataValueComparer : IEqualityComparer<Data>
    {
        /// <summary>
        /// Checks if the given two <see cref="Data"/> objects have the same key and value data.
        /// </summary>
        /// <param name="firstObj"> The first data object to be checked. </param>
        /// <param name="secondObj"> The second data object to be checked. </param>
        /// <returns> True if the two given data objects have the same key value info. False otherwise. </returns>
        public bool Equals(Data? firstObj, Data? secondObj)
        {
            if (firstObj == null || secondObj == null)
            {
                return false;
            }

            return firstObj.Key == secondObj.Key && firstObj.Value == secondObj.Value;
        }

        public int GetHashCode([DisallowNull] Data obj)
        {
            return obj.GetHashCode();
        }
    }
}
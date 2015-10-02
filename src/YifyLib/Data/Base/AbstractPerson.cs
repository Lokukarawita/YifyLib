using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YifyLib.Data.Base
{
    /// <summary>
    /// Abstract Person in YTS
    /// </summary>
    public abstract class AbstractPerson
    {
        /// <summary>
        ///  Get or set name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        ///  Get or set small avatar image URL
        /// </summary>
        public string SmallImage { get; set; }
        /// <summary>
        /// Get or set medium avatar image URL
        /// </summary>
        public string MediumImage { get; set; }
        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return Name;
        }
    }
}

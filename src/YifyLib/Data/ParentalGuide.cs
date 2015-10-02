using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YifyLib.Data
{
    /// <summary>
    /// Represent parental guide for a movie
    /// </summary>
    public class ParentalGuide
    {
        /// <summary>
        /// Get or set the type
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// Get or set the guide text
        /// </summary>
        public string Text { get; set; }
        
        /// <summary>
        /// Get a string representation of this object
        /// </summary>
        /// <returns>Object in string</returns>
        public override string ToString()
        {
            return Type;
        }
    }
}

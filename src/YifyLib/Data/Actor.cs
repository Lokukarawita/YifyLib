using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YifyLib.Data.Base;

namespace YifyLib.Data
{
    /// <summary>
    /// Represent an actor
    /// </summary>
    public class Actor : AbstractPerson
    {
        /// <summary>
        /// Instantiate a default instance of Actor
        /// </summary>
        public Actor()
            : base()
        { }

        /// <summary>
        /// Get or set the name of the character in the movie
        /// </summary>
        public string CharacterName { get; set; }

        /// <summary>
        /// Get a string representation of this object
        /// </summary>
        /// <returns>Object in string</returns>
        public override string ToString()
        {
            return string.Format("{0} as {1}", Name, CharacterName);
        }
    }
}

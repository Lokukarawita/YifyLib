using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YifyLib.Data.Base;

namespace YifyLib.Data
{
    /// <summary>
    /// Represent a movie suggestion
    /// </summary>
    public class SuggestionMovie : AbstractMovie
    {
        /// <summary>
        /// Get or set the state of movie
        /// </summary>
        public string State { get; set; }
    }
}

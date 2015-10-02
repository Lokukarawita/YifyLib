using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YifyLib.Data.Base;

namespace YifyLib.Data
{
    /// <summary>
    /// Represent a search result
    /// </summary>
    public class ListMovie : AbstractYifyMovie
    {
        /// <summary>
        /// Get or set the state
        /// </summary>
        public string State { get; set; }
    }
}

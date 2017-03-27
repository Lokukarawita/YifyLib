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
        public SuggestionMovie()
        {
            Torrents = new List<Torrent>();

        }

        /// <summary>
        /// Get or set the state of movie
        /// </summary>
        public string State { get; set; }

        [Obsolete("Maybe removed in the future depending on the API change")]
        public List<Torrent> Torrents { get; set; }
    }
}

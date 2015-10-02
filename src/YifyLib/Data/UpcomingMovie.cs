using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YifyLib.Data
{
    /// <summary>
    /// Represent a upcoming movie in YTS
    /// </summary>
    public class UpcomingMovie
    {
        /// <summary>
        /// Get or set the title of the movie
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Get or set the movie year
        /// </summary>
        public int Year { get; set; }
        /// <summary>
        /// Get or set the IMDB code
        /// </summary>
        public string IMDBCode { get; set; }
        /// <summary>
        /// Get or set the medium cover image
        /// </summary>
        public MovieImage MediumCover { get; set; }
        /// <summary>
        /// Get or set the date added
        /// </summary>
        public DateTime DateAdded { get; set; }
        /// <summary>
        /// Get or set the date added in unix format
        /// </summary>
        public long DateAddedUnix { get; set; }
    }
}

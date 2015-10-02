using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YifyLib.Data.Base
{
    /// <summary>
    /// Abstract movie
    /// </summary>
    public abstract class AbstractMovie
    { 
        /// <summary>
        ///  Initializes a new instance of the AbstractMovie class.
        /// </summary>
        public AbstractMovie()
        {
            Genres = new List<string>();
            Images = new List<MovieImage>();
        }

        /// <summary>
        /// Get or set the movie id
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Get or set the YTS page URL for the movie
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// Get or set the IMDB code
        /// </summary>
        public string IMDBCode { get; set; }
        /// <summary>
        /// Get or set the movie title
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Get or set the long movie title.
        /// Movies from YTS include movie year inside the brackets
        /// </summary>
        public string TitleLong { get; set; }
        /// <summary>
        /// Get or set the movie year
        /// </summary>
        public int Year { get; set; }
        /// <summary>
        /// Get or set the rating
        /// </summary>
        public string Rating { get; set; }
        /// <summary>
        /// Get or set the runtime of the movie
        /// </summary>
        public int Runtime { get; set; }
        /// <summary>
        /// Get a list of genres for the movie
        /// </summary>
        public List<string> Genres { get; private set; }
        /// <summary>
        /// Get a list of images for the movie
        /// </summary>
        public List<MovieImage> Images { get; private set; }

        /// <summary>
        /// Get a string representation of this object
        /// </summary>
        /// <returns>Object in string</returns>
        public override string ToString()
        {
            return string.Format("ID: {0}, Title: {1}", ID, TitleLong);
        }
    }
}

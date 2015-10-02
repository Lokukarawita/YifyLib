using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YifyLib.Data.Base;

namespace YifyLib.Data
{
    /// <summary>
    /// Represent a YTS movie
    /// </summary>
    public class Movie : AbstractYifyMovie
    {
        /// <summary>
        /// Instantiate a default instance of movie
        /// </summary>
        public Movie()
        {
            Directors = new List<Person>();
            Actors = new List<Actor>();
        }

        /// <summary>
        /// Get or set the download count
        /// </summary>
        public int DownloadCount { get; set; }
        /// <summary>
        /// Get or set the like count
        /// </summary>
        public int LikeCount { get; set; }
        /// <summary>
        /// Get or set Rotten Tomato critics score
        /// </summary>
        public int RTCriticsScore { get; set; }
        /// <summary>
        /// Get or set Rotten Tomato critics score
        /// </summary>
        public string RTCriticsRating { get; set; }
        /// <summary>
        /// Get or set Rotten Tomato audience score
        /// </summary>
        public int RTAudienceScore { get; set; }
        /// <summary>
        /// Get or set Rotten Tomato audience rating
        /// </summary>
        public string RTAudienceRating { get; set; }
        /// <summary>
        /// Get or set YTS intro description
        /// </summary>
        public string DescriptionIntro { get; set; }
        /// <summary>
        /// Get or set YTS full description
        /// </summary>
        public string DescriptionFull { get; set; }
        /// <summary>
        /// Get or set YouTube trailer code
        /// </summary>
        public string YTTrailerCode { get; set; }
        /// <summary>
        /// Get a list of directors for the movie
        /// </summary>
        public List<Person> Directors { get; private set; }
        /// <summary>
        /// Get a list if actors in a movie
        /// </summary>
        public List<Actor> Actors { get; private set; }
    }
}

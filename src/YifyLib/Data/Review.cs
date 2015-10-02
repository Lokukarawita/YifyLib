using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YifyLib.Data
{
    /// <summary>
    /// Represent a review of a movie
    /// </summary>
    public class Review
    {
        /// <summary>
        /// Get or set the reviewing user
        /// </summary>
        public ReviewUser User { get; set; }
        /// <summary>
        /// Get or set the rating of the movie by the reviewing user
        /// </summary>
        public int UserRating { get; set; }
        /// <summary>
        /// Get or set the summery of review
        /// </summary>
        public string ReviewSummary { get; set; }
        /// <summary>
        /// Get or set the review
        /// </summary>
        public string ReviewText { get; set; }
        /// <summary>
        /// Get or set the date when the review is written
        /// </summary>
        public DateTime DateWritten { get; set; }
        /// <summary>
        /// Get or set the date when the review is unix format
        /// </summary>
        public long DateWrittenUnix { get; set; }
        
        /// <summary>
        /// Get a string representation of this object
        /// </summary>
        /// <returns>Object in string</returns>
        public override string ToString()
        {
            return string.Format("{0} by {1}", ReviewSummary, 
                (User != null ? User.ToString() : string.Empty)); 
        }
    }
}

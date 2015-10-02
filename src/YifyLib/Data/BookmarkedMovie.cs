using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YifyLib.Data.Base;

namespace YifyLib.Data
{
    /// <summary>
    /// Represent a bookmarked movie
    /// </summary>
    public class BookmarkedMovie : AbstractYifyMovie
    {
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
        /// Get or set the state
        /// </summary>
        public string State { get; set; }
    }
}

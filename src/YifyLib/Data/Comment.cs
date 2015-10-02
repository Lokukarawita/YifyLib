using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YifyLib.Data
{
    /// <summary>
    /// Represent a comment in YTS
    /// </summary>
    public class Comment
    {
        /// <summary>
        /// Get or set the commented user
        /// </summary>
        public CommentUser User { get; set; }
        /// <summary>
        /// Get or set the like count for the comment
        /// </summary>
        public int LikeCount { get; set; }
        /// <summary>
        /// Get or set the comment text
        /// </summary>
        public string CommentText { get; set; }
        /// <summary>
        /// Get or set the date when the comment was added
        /// </summary>
        public DateTime DateAdded { get; set; }
        /// <summary>
        /// Get or set the date when the comment was added in unix format
        /// </summary>
        public long DateAddedUnix { get; set; }

        /// <summary>
        /// Get a string representation of this object
        /// </summary>
        /// <returns>Object in string</returns>
        public override string ToString()
        {
            return string.Format("{0} by {1}", CommentText, User);
        }
    }
}

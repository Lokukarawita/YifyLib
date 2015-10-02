using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YifyLib.Data.Base;

namespace YifyLib.Data
{
    /// <summary>
    /// Represent a user in YTS
    /// </summary>
    public class User : AbstractYifyUser
    {
        /// <summary>
        /// Instantiate this default user object
        /// </summary>
        public User()
        {
            Downloads = new List<UserDownloadedMovie>();
        }

        /// <summary>
        /// Get or set a list of downloaded movies.
        /// </summary>
        public List<UserDownloadedMovie> Downloads { get; set; }
        /// <summary>
        /// Get or set the about text of the user
        /// </summary>
        public string AboutText { get; set; }
        /// <summary>
        /// Get or set the data joined
        /// </summary>
        public DateTime DateJoined { get; set; }
        /// <summary>
        /// Get or set the data joined in unix format
        /// </summary>
        public long DateJoinedUnix { get; set; }
        /// <summary>
        /// Get or set the data last seen
        /// </summary>
        public DateTime DateLastSeen { get; set; }
        /// <summary>
        /// Get or set the data last seen unix format
        /// </summary>
        public long DateLastSeenUnix { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YifyLib.Data.Base
{
    /// <summary>
    /// Abstract User of from YTS
    /// </summary>
    public abstract class AbstractYifyUser : AbstractUser
    {
        /// <summary>
        ///  Initializes a new instance of the AbstractImage class.
        /// </summary>
        public AbstractYifyUser()
        {
            Avatars = new List<UserImage>();
        }
        /// <summary>
        /// Get or set the user id
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Get or set the group
        /// </summary>
        public string Group { get; set; }
        // Get or set the User's profile path
        public string Url { get; set; }
        /// <summary>
        /// Get or set the user avatar
        /// </summary>
        public List<UserImage> Avatars { get; set; }
        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return string.Format("{0}({1})", Username, ID);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YifyLib.Data.Base
{
    /// <summary>
    /// Abstract User
    /// </summary>
    public class AbstractUser
    {
        /// <summary>
        ///  Initializes a new instance of the AbstractUser class.
        /// </summary>
        public AbstractUser()
        {
            
        }

        /// <summary>
        /// Get or set the username
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return Username;
        }
    }
}

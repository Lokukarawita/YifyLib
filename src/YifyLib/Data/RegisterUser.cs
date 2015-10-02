using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YifyLib.Data.Base;

namespace YifyLib.Data
{
    /// <summary>
    /// Represent a Registered user in YTS
    /// </summary>
    public class RegisterUser : AbstractUser
    {
        /// <summary>
        /// Get or set the user key
        /// </summary>
        public string UserKey { get; set; }
        /// <summary>
        /// Get or set the email
        /// </summary>
        public string Email { get; set; }
    }
}

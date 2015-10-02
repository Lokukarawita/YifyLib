using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using YifyLib.Data.Base;

namespace YifyLib.Data
{
    /// <summary>
    /// Represent a profile in YTS
    /// </summary>
    public class Profile : AbstractYifyUser
    {
        private IPAddress _ip;

        /// <summary>
        /// Get or set the last visited IP in string notation. 
        /// Ex 192.168.1.1
        /// </summary>
        public string IPString
        {
            get {
                if (_ip == null) return IPAddress.None.ToString();
                return _ip.ToString(); 
            }
            set {
                IPAddress ip;
                if (IPAddress.TryParse(value, out ip))
                    _ip = ip;
                else
                    _ip = IPAddress.None;
            }
        }
        /// <summary>
        /// Get or set the last visited IP
        /// </summary>
        public IPAddress IP
        {
            get { return _ip; }
            set { _ip = value; }
        }
        /// <summary>
        /// Get or set the Email address
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Get or set the user key
        /// </summary>
        public string UserKey { get; set; }
    }
}

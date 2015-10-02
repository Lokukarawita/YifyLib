using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YifyLib.Data
{
    /// <summary>
    /// Represent a torrent file in YTS
    /// </summary>
    public class Torrent
    {
        /// <summary>
        /// Get or set the URL for torrent file
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// Get or set the torrent hash code
        /// </summary>
        public string Hash { get; set; }
        /// <summary>
        /// Get or set the movie quality 
        /// </summary>
        public string Quality { get; set; }
        /// <summary>
        /// Get or set the current number of seeds
        /// </summary>
        public string Seeds { get; set; }
        /// <summary>
        /// Get or set the current number of peers
        /// </summary>
        public string Peers { get; set; }
        /// <summary>
        /// Get or set the size of torrent in string format
        /// Ex: 750 MB
        /// </summary>
        public string Size { get; set; }
        /// <summary>
        /// Get or set the size of torrent in bytes
        /// </summary>
        public long SizeBytes { get; set; }
        /// <summary>
        /// Get or set the date uploaded
        /// </summary>
        public DateTime DateUploaded { get; set; }
        /// <summary>
        /// Get or set the date uploaded in unix format
        /// </summary>
        public long DateUploadedUnix { get; set; }
        
        /// <summary>
        /// Get a string representation of this object
        /// </summary>
        /// <returns>Object in string</returns>
        public override string ToString()
        {
            return string.Format("Quality: {0}, Size: {1} Peers: {2}, Seeds: {3} ", Quality, Size, Peers, Seeds);
        }
    }
}

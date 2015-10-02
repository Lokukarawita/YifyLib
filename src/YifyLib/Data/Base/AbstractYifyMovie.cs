using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YifyLib.Data.Base
{
    /// <summary>
    /// Abstract Movie of from YTS
    /// </summary>
    public abstract class AbstractYifyMovie : AbstractMovie
    {
        /// <summary>
        ///  Initializes a new instance of the AbstractYifyMovie class.
        /// </summary>
        public AbstractYifyMovie()
        {
            Torrents = new List<Torrent>();
        }
        
        /// <summary>
        /// Get or set the language
        /// </summary>
        public string Language { get; set; }
        /// <summary>
        /// Get or set the MPA rating
        /// </summary>
        public string MPARating { get; set; }
        /// <summary>
        /// Get a list of torrents belonging to this movie
        /// </summary>
        public List<Torrent> Torrents { get; private set; }
        /// <summary>
        /// Get or set the date uploaded
        /// </summary>
        public DateTime DateUploaded { get; set; }
        /// <summary>
        /// Get or set the date uploaded in unix format
        /// </summary>
        public long DateUploadedUnix { get; set; }
    }
}

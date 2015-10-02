using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YifyLib.Data.Base;

namespace YifyLib.Data
{
    /// <summary>
    /// Represent an image of a movie
    /// </summary>
    public class MovieImage : AbstractImage
    {
        /// <summary>
        /// get or set the type of image
        /// </summary>
        public MovieImageType ImageType { get; set; }

        /// <summary>
        /// Get a string representation of this object
        /// </summary>
        /// <returns>Object in string</returns>
        public override string ToString()
        {
            return string.Format("Type: {0}, Size: {1}", ImageType, ImageSize);
        }
    }
}

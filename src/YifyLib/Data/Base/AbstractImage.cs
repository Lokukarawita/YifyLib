using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YifyLib.Data.Base
{
    /// <summary>
    /// Abstract Image from YTS
    /// </summary>
    public class AbstractImage
    {
        /// <summary>
        ///  Initializes a new instance of the AbstractImage class.
        /// </summary>
        public AbstractImage()
        { var c = new object(); }

        /// <summary>
        /// URL of the image
        /// </summary>
        public string Url { get; set; }
        
        /// <summary>
        /// Image Size
        /// </summary>
        public ImageSize ImageSize { get; set; }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return ImageSize.ToString() + " image";
        }
    }
}

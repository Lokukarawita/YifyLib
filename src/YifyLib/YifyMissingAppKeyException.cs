using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YifyLib
{
    /// <summary>
    /// Exception for Application Key required error
    /// </summary>
    [Serializable]
    public class YifyMissingAppKeyException : Exception
    {
        /// <summary>
        /// Instantiate a default MissingAppKeyException
        /// </summary>
        public YifyMissingAppKeyException() { }
        /// <summary>
        /// Instantiate a MissingAppKeyException with a given message
        /// </summary>
        /// <param name="message">Exception message</param>
        public YifyMissingAppKeyException(string message) : base(message) { }
        /// <summary>
        /// Instantiate a MissingAppKeyException with a given message and its causing exception.
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="inner">Parent Exception</param>
        public YifyMissingAppKeyException(string message, Exception inner) : base(message, inner) { }
    }
}

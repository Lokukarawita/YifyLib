using System;

namespace YifyLib
{
    /// <summary>
    /// Exception in YiFy API operations
    /// </summary>
    [Serializable]
    public class YifyException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the YiFyException class.
        /// 
        /// </summary>
        public YifyException()
        {
            this.YiFyMessage = string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the YiFyException class with a specified error message.
        /// 
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public YifyException(string message)
            : base(message)
        {
            this.YiFyMessage = string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the YiFyException class with a specified error message and YiFy site response error message.
        /// 
        /// </summary>
        /// <param name="message">The message that describes the error.</param><param name="yifyMessage">YiFy site response error message</param>
        public YifyException(string message, string yifyMessage)
            : base(message)
        {
            this.YiFyMessage = yifyMessage;
        }

        /// <summary>
        /// Initializes a new instance of the YiFyException class with a
        ///             specified error message and a reference to the inner exception that is the cause of this exception.
        /// 
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param><param name="innerException">The exception that
        ///             is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public YifyException(string message, Exception innerException)
            : base(message, innerException)
        {
            this.YiFyMessage = string.Empty;
        }
        
        /// <summary>
        /// Error message from YiFy torrent site
        /// </summary>
        public string YiFyMessage { get; private set; }
    }
}

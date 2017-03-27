using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YifyLib.Api.Response
{
    /// <summary>
    /// Error check interface for YTS response
    /// </summary>
    /// <typeparam name="T">Type of parsed response</typeparam>
    public interface IYifyErrorCheck<T>
    {
        bool IsYifyError(T doc);
        string GetStatus(T doc);
        string GetStatusMessage(T doc);
    }
}

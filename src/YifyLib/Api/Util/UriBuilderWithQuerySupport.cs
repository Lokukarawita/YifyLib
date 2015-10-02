using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;

namespace YifyLib.Api.Util
{
    /// <summary>
    /// Uri builder with easy query string modification support.
    /// </summary>
    internal class UriBuilderWithQuerySupport : System.UriBuilder
    {
        private NameValueCollection queryParam;

        public UriBuilderWithQuerySupport(string uri)
            : base(uri)
        {
            queryParam = new NameValueCollection();
            var parms = HttpUtility.ParseQueryString(new Uri(uri).Query);
            if (parms != null && parms.Count > 0)
                queryParam = parms;

        }
        public UriBuilderWithQuerySupport(Uri uri)
            : base(uri)
        {
            queryParam = new NameValueCollection();
            var parms = HttpUtility.ParseQueryString(uri.Query);
            if (parms != null && parms.Count > 0)
                queryParam = parms;
        }


        private void GenerateQueryString()
        {

          var qry=  string.Join("&", Enumerable.ToArray<string>(
                Enumerable.SelectMany<string, string, string>((IEnumerable<string>)queryParam.AllKeys, (Func<string, IEnumerable<string>>)(key => (IEnumerable<string>)queryParam.GetValues(key)), (Func<string, string, string>)((key, value) => string.Format("{0}={1}", (object)HttpUtility.UrlEncode(key), (object)HttpUtility.UrlEncode(value))))));
          base.Query = qry;
        }

        /// <summary>
        /// Get the query string
        /// </summary>
        public new string Query { get { return base.Query; } }

        /// <summary>
        /// Add query parameter
        /// </summary>
        /// <param name="name">Field Name</param>
        /// <param name="value">Value</param>
        /// <param name="ignoreIfEmpty">If set to true then value is added only if it is not null or an empty string.</param>
        public void AddQueryParameter(string name, string value, bool ignoreIfEmpty)
        {
            if (queryParam.AllKeys.Contains(name))
            {
                if (!string.IsNullOrEmpty(value)) 
                    queryParam[name] = value;
                else if(string.IsNullOrEmpty(value) && !ignoreIfEmpty)
                    queryParam[name] = value;
            }
            else
            {
                if (!string.IsNullOrEmpty(value))
                    queryParam.Add(name, value);
                else if (string.IsNullOrEmpty(value) && !ignoreIfEmpty)
                    queryParam.Add(name, value);
            }
            GenerateQueryString();
        }
        /// <summary>
        /// Remove all the query parameters
        /// </summary>
        public void RemoveQueryParameter()
        {
            queryParam.Clear();
            GenerateQueryString();
        }
        /// <summary>
        /// Remove a query parameter
        /// </summary>
        /// <param name="name">Field name of the parameter to remove</param>
        public void RemoveQueryParameter(string name)
        {
            if (queryParam.AllKeys.Contains(name))
            {
                queryParam.Remove(name);
            }
            GenerateQueryString();
        }
    }
}

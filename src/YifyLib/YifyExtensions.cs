/**
 * 
 * Since version 1.1.7
 * 
 * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace YifyLib
{
    /// <summary>
    /// Useful extension methods for YifyLib
    /// </summary>
    public static class YifyExtensions
    {
        /// <summary>
        /// Convert YifyLib.Data.Torrent to Magnet URI
        /// </summary>
        /// <param name="t">YifyLib.Data.Torrent object to convert</param>
        /// <returns>Bit Torrent based Magnet URI in string</returns>
        public static string ToMagnetUri(this Data.Torrent t)
        {
            return string.Format("magnet:?xt=urn:btih:{0}", t.Hash);
        }
        /// <summary>
        /// Convert YifyLib.Data.Torrent to Magnet URI with a given display name
        /// </summary>
        /// <param name="t">YifyLib.Data.Torrent object to convert</param>
        /// <param name="dn">Display name to use on the torrent client</param>
        /// <returns>Bit Torrent based Magnet URI in string</returns>
        public static string ToMagnetUri(this Data.Torrent t, string dn)
        {
            return string.Format("magnet:?xt=urn:btih:{0}&dn={1}", t.Hash, HttpUtility.UrlEncode(dn));
        }
        /// <summary>
        /// Convert YifyLib.Data.Torrent to Magnet URI with given display name and a set of trackers.
        /// </summary>
        /// <param name="t">YifyLib.Data.Torrent object to convert</param>
        /// <param name="trackers">List of tracker Uri(s)</param>
        /// <param name="dn">Display name to use on the torrent client</param>
        /// <returns>Bit Torrent based Magnet URI in string</returns>
        public static string ToMagnetUri(this Data.Torrent t, string dn, params Uri[] trackers)
        {
            string uri = t.ToMagnetUri(dn);
            if (trackers != null && trackers.Length > 0)
            {
                string trks = trackers.Select(i => string.Format("tr={0}", HttpUtility.UrlEncode(i.ToString()))).Aggregate((i, j) => i + "&" + j);
                uri += "&" + trks;
            }
            return uri;
        }
        /// <summary>
        /// Convert YifyLib.Data.Torrent to Magnet URI with given set of trackers
        /// </summary>
        /// <param name="t">YifyLib.Data.Torrent object to convert</param>
        /// <param name="trackers">List of tracker Uri(s)</param>
        /// <returns>Bit Torrent based Magnet URI in string</returns>
        public static string ToMagnetUri(this Data.Torrent t, params Uri[] trackers)
        {
            string uri = t.ToMagnetUri();
            if (trackers != null && trackers.Length > 0)
            {
                string trks = trackers.Select(i => string.Format("tr={0}", HttpUtility.UrlEncode(i.ToString()))).Aggregate((i, j) => i + "&" + j);
                uri += "&" + trks;
            }
            return uri;
        }
    }
}

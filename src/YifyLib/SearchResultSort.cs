using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YifyLib
{
    /// <summary>
    /// Field to sort the search result.
    /// </summary>
    public enum SearchResultSort
    {
        /// <summary>
        /// Sort By Title
        /// </summary>
        Title,
        /// <summary>
        /// Sort by Year
        /// </summary>
        Year, 
        /// <summary>
        /// Sort by Rating
        /// </summary>
        Rating, 
        /// <summary>
        /// Sort by Peers
        /// </summary>
        Peers, 
        /// <summary>
        /// Sort by Seeders
        /// </summary>
        Seeds, 
        /// <summary>
        /// Sort by Download Count
        /// </summary>
        DownloadedCount, 
        /// <summary>
        /// Sort by Like Count
        /// </summary>
        LikeCount, 
        /// <summary>
        /// Sort by Date Added
        /// </summary>
        DateAdded
    }

    /// <summary>
    /// Extensions for SearchResultSort enum.
    /// </summary>
    internal static class SearchResultSortExtentions
    {
        /// <summary>
        /// Convert SearchResultSort value to its URL parameter name
        /// </summary>
        /// <param name="sort">Value to convert</param>
        /// <returns>Converted name in <code>System.String</code></returns>
        public static string ToQueryParameterName(this SearchResultSort sort)
        {
            switch (sort)
            {
                case SearchResultSort.Title:
                    return "title";
                case SearchResultSort.Year:
                    return "year";
                case SearchResultSort.Rating:
                    return "rating";
                case SearchResultSort.Peers:
                    return "peers";
                case SearchResultSort.Seeds:
                    return "seeds";
                case SearchResultSort.DownloadedCount:
                    return "downloaded_count";
                case SearchResultSort.LikeCount:
                    return "like_count";
                case SearchResultSort.DateAdded:
                    return "date_added";
                default:
                    throw new ArgumentException("Cannot find the sort specified");
            }
        }
    }
}

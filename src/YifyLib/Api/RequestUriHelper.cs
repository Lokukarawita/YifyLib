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
using YifyLib.Api.Response;

namespace YifyLib.Api
{
    /// <summary>
    /// Represent requests made to YTS API.
    /// </summary>
    internal sealed class RequestUriHelper
    {
        public static readonly RequestUriHelper ListMovies = new RequestUriHelper("list_movies");
        public static readonly RequestUriHelper MovieDetails = new RequestUriHelper("movie_details");
        public static readonly RequestUriHelper MovieSuggestions = new RequestUriHelper("movie_suggestions");
        public static readonly RequestUriHelper MovieComments = new RequestUriHelper("movie_comments");
        public static readonly RequestUriHelper MovieReview = new RequestUriHelper("movie_reviews.xml");
        public static readonly RequestUriHelper MovieParentalGuide = new RequestUriHelper("movie_parental_guides");
        public static readonly RequestUriHelper UserDetails = new RequestUriHelper("user_details");
        public static readonly RequestUriHelper GetUserKey = new RequestUriHelper("user_get_key");
        public static readonly RequestUriHelper UserProfile = new RequestUriHelper("user_profile");
        public static readonly RequestUriHelper EditUserSettigns = new RequestUriHelper("user_edit_settings");
        public static readonly RequestUriHelper RegisterUser = new RequestUriHelper("user_register");
        public static readonly RequestUriHelper ForgotUserPassword = new RequestUriHelper("user_forgot_password");
        public static readonly RequestUriHelper ResetUserPassword = new RequestUriHelper("user_reset_password");
        public static readonly RequestUriHelper LikeMovie = new RequestUriHelper("like_movie");
        public static readonly RequestUriHelper AddMovieBookmark = new RequestUriHelper("add_movie_bookmark");
        public static readonly RequestUriHelper MovieBookmarks = new RequestUriHelper("get_movie_bookmarks");
        public static readonly RequestUriHelper DeleteMovieBookmark = new RequestUriHelper("delete_movie_bookmark");
        public static readonly RequestUriHelper MakeComment = new RequestUriHelper("make_comment");
        public static readonly RequestUriHelper LikeComment = new RequestUriHelper("like_comment");
        public static readonly RequestUriHelper ReportComment = new RequestUriHelper("report_comment");
        public static readonly RequestUriHelper DeleteComment = new RequestUriHelper("delete_comment");
        public static readonly RequestUriHelper MakeRequest = new RequestUriHelper("make_request");

        /// <summary>
        /// Create a Uri for a request
        /// </summary>
        /// <param name="baseUri">Base Uri</param>
        /// <param name="respType">Response type</param>
        /// <param name="rType">Request type</param>
        /// <returns></returns>
        public static Uri GetUri(Uri baseUri, ResponseType respType,  RequestUriHelper rType)
        {
            Uri u = u = new Uri(baseUri, string.Format("{0}.{1}", rType.RelativePath, respType.ToString().ToLower()));
            return u;
        }
        
        
        private RequestUriHelper(string relativePath)
        {
            RelativePath = relativePath;
        }
        
        /// <summary>
        /// Relative URI path
        /// </summary>
        public string RelativePath { get; set; }
    }
}

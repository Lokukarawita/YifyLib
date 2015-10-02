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

namespace YifyLib.Api
{
    /// <summary>
    /// Represent requests made to YTS API.
    /// </summary>
    internal sealed class RequestUriHelper
    {
        public static readonly RequestUriHelper ListMovies = new RequestUriHelper("list_movies.xml");
        public static readonly RequestUriHelper MovieDetails = new RequestUriHelper("movie_details.xml");
        public static readonly RequestUriHelper MovieSuggestions = new RequestUriHelper("movie_suggestions.xml");
        public static readonly RequestUriHelper MovieComments = new RequestUriHelper("movie_comments.xml");
        public static readonly RequestUriHelper MovieReview = new RequestUriHelper("movie_reviews.xml");
        public static readonly RequestUriHelper MovieParentalGuide = new RequestUriHelper("movie_parental_guides.xml");
        public static readonly RequestUriHelper UserDetails = new RequestUriHelper("user_details.xml");
        public static readonly RequestUriHelper GetUserKey = new RequestUriHelper("user_get_key.xml");
        public static readonly RequestUriHelper UserProfile = new RequestUriHelper("user_profile.xml");
        public static readonly RequestUriHelper EditUserSettigns = new RequestUriHelper("user_edit_settings.xml");
        public static readonly RequestUriHelper RegisterUser = new RequestUriHelper("user_register.xml");
        public static readonly RequestUriHelper ForgotUserPassword = new RequestUriHelper("user_forgot_password.xml");
        public static readonly RequestUriHelper ResetUserPassword = new RequestUriHelper("user_reset_password.xml");
        public static readonly RequestUriHelper LikeMovie = new RequestUriHelper("like_movie.xml");
        public static readonly RequestUriHelper AddMovieBookmark = new RequestUriHelper("add_movie_bookmark.xml");
        public static readonly RequestUriHelper MovieBookmarks = new RequestUriHelper("get_movie_bookmarks.xml");
        public static readonly RequestUriHelper DeleteMovieBookmark = new RequestUriHelper("delete_movie_bookmark.xml");
        public static readonly RequestUriHelper MakeComment = new RequestUriHelper("make_comment.xml");
        public static readonly RequestUriHelper LikeComment = new RequestUriHelper("like_comment.xml");
        public static readonly RequestUriHelper ReportComment = new RequestUriHelper("report_comment.xml");
        public static readonly RequestUriHelper DeleteComment = new RequestUriHelper("delete_comment.xml");
        public static readonly RequestUriHelper MakeRequest = new RequestUriHelper("make_request.xml");

        public static Uri GetUri(Uri baseUri, RequestUriHelper rType)
        {
            Uri u = u = new Uri(baseUri, rType.RelativePath);

            //if (rType == RequestUriHelper.ListMovies) {  }
            //else if (rType == RequestUriHelper.MovieDetails) { }
            //else if (rType == RequestUriHelper.MovieSuggestions) { }
            //else if (rType == RequestUriHelper.MovieComments) { }
            //else if (rType == RequestUriHelper.MovieReview) { }
            //else if (rType == RequestUriHelper.MovieParentalGuide) { }
            //else if (rType == RequestUriHelper.UserDetails) { }
            //else if (rType == RequestUriHelper.GetUserKey) { }
            //else if (rType == RequestUriHelper.UserProfile) { }
            //else if (rType == RequestUriHelper.EditUserSettigns) { }
            //else if (rType == RequestUriHelper.RegisterUser) { }
            //else if (rType == RequestUriHelper.ForgotUserPassword) { }
            //else if (rType == RequestUriHelper.ResetUserPassword) { }
            //else if (rType == RequestUriHelper.LikeMovie) { }
            //else if (rType == RequestUriHelper.AddMovieBookmark) { }
            //else if (rType == RequestUriHelper.MovieBookmarks) { }
            //else if (rType == RequestUriHelper.DeleteMovieBookmark) { }
            //else if (rType == RequestUriHelper.MakeComment) { }
            //else if (rType == RequestUriHelper.LikeComment) { }
            //else if (rType == RequestUriHelper.ReportComment) { }
            //else if (rType == RequestUriHelper.DeleteComment) { }
            //else if (rType == RequestUriHelper.MakeRequest) { }
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

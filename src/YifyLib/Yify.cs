using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YifyLib.Api;
using YifyLib.Data;

namespace YifyLib
{
    /// <summary>
    /// Provides access to YTS API v2
    /// </summary>
    public sealed class Yify
    {
        #region Private Members
        private YifyAPI _api;
        private string userKey = string.Empty;
        private string appKey = string.Empty;
        #endregion

        #region Constructors
        /// <summary>
        /// Instantiate a new Yify class object
        /// </summary>
        public Yify()
        {
            _api = new YifyAPI();
        }

        #region Since version 1.1.7
        /// <summary>
        /// Instantiate a new Yify class object with given YTS API base URI
        /// </summary>
        /// <param name="baseUri">Base URI for YTS API, 
        /// Base URI should be an absolute URI which conforms with Base URI + Relative Path criteria. 
        /// Ex: 
        /// <code>https://yts.to/v2/ + list_movies.xml</code>
        /// </param>
        public Yify(Uri baseUri)
        {
            if (baseUri != null) YifyAPI.Base_URI = baseUri;
        }
        /// <summary>
        /// Instantiate a new Yify class object with given YTS API application key
        /// </summary>
        /// <param name="applicationKey">YTS API Application Key</param>
        /// <exception cref="ArgumentException">If applicationKey parameter is empty or null</exception>
        public Yify(string applicationKey)
        {
            if (string.IsNullOrWhiteSpace(applicationKey))
                throw new ArgumentException("Application key parameter cannot be empty");

            this.appKey = applicationKey;
        }
        /// <summary>
        /// Instantiate a new Yify class object with given YTS API application key and its base baseUri
        /// </summary>
        /// <param name="applicationKey">YTS API Application Key</param>
        /// <param name="baseUri">Base URI for YTS API, 
        /// Base URI should be an absolute URI which conforms with Base URI + Relative Path criteria. 
        /// Ex: 
        /// <code>https://yts.to/v2/ + list_movies.xml</code>
        /// </param>
        public Yify(string applicationKey, Uri baseUri)
        {
            this.appKey = applicationKey;
            if (baseUri != null) YifyAPI.Base_URI = baseUri;
        }
        #endregion
        #endregion

        #region Public Methods
        /// <summary>
        /// Search or list movies in YTS
        /// </summary>
        /// <param name="queryTerm">Term to search</param>
        /// <param name="quality">Quality of the release Ex: 720p, 1080p, 3D</param>
        /// <param name="genre">Genre of the movie (See http://www.imdb.com/genre/ for full list)</param>
        /// <param name="minimumRating">Minimum rating of the movie. Ex: 0 to 9</param>
        /// <param name="limit">The limit of results per page. Ex: 20</param>
        /// <param name="page">Page number to view the movies Ex: Limit 15 and Page 2 will show you movies 15 to 30</param>
        /// <param name="sortBy">Sorts the results by chosen field</param>
        /// <param name="orderBy">Order of the results in Ascending or Descending order</param>
        /// <returns>Collection of ListMovie class objects</returns>
        /// <exception cref="YifyLib.YifyException">If any errors occurred YTS process</exception>
        public List<ListMovie> ListMovies(
            string queryTerm = "",
            string quality = "All",
            string genre = "All",
            uint minimumRating = 0,
            uint limit = 20,
            uint page = 1,
            SearchResultSort sortBy = SearchResultSort.DateAdded,
            SortOrder orderBy = SortOrder.Desc)
        {

            try
            {
                var uri = YifyAPI.GetListMovieURI(queryTerm, quality, genre, minimumRating, limit, page, sortBy, orderBy);
                var res = YifyAPI.SendGetRequest(uri);
                return ResponseParser.ParseListMovieResponse(res);
            }
            catch (Exception ex)
            {
                throw new YifyException("An error occurred. See inner exception for more details", ex);
            }
        }
        /// <summary>
        /// Get movie details for a particular movie
        /// </summary>
        /// <param name="movieID">YTS movie id</param>
        /// <param name="includeCast">If true cast information will be included</param>
        /// <param name="includeImages">If true movie images will be included</param>
        /// <returns>Movie object containing the movie result</returns>
        public Movie GetMovie(int movieID,
            bool includeCast = true,
            bool includeImages = true)
        {
            try
            {
                var uri = YifyAPI.GetMovieURI(movieID, includeCast, includeImages);
                var res = YifyAPI.SendGetRequest(uri);
                return ResponseParser.ParseGetMovieResponse(res);

            }
            catch (Exception ex)
            {
                throw new YifyException("An error occurred. See inner exception for more details", ex);
            }

        }
        /// <summary>
        /// Get suggestion fro particular movie
        /// </summary>
        /// <param name="movieID">YTS movie id</param>
        /// <returns>Collection of SuggestionMovie class objects</returns>
        /// <exception cref="YifyLib.YifyException">If any errors occurred YTS process</exception>
        public List<SuggestionMovie> GetMovieSuggestion(int movieID)
        {
            try
            {
                var uri = YifyAPI.GetMovieSuggestionsURI(movieID);
                var res = YifyAPI.SendGetRequest(uri);
                return ResponseParser.ParseGetMovieSuggestionResponse(res);
            }
            catch (Exception ex)
            {
                throw new YifyException("An error occurred. See inner exception for more details", ex);
            }

        }
        /// <summary>
        /// Get comments made by YTS users for a particular movie
        /// </summary>
        /// <param name="movieID">YTS movie id</param>
        /// <returns>Collection of Comment class objects</returns>
        /// <exception cref="YifyLib.YifyException">If any errors occurred YTS process</exception>
        public List<Comment> GetMovieComments(int movieID)
        {
            try
            {
                var uri = YifyAPI.GetMovieCommentsURI(movieID);
                var res = YifyAPI.SendGetRequest(uri);
                return ResponseParser.ParseGetMovieCommentsResponse(res);
            }
            catch (Exception ex)
            {
                throw new YifyException("An error occurred. See inner exception for more details", ex);
            }
        }
        /// <summary>
        /// Get movie reviews made for a particular movie
        /// </summary>
        /// <param name="movieID">YTS movie id</param>
        /// <returns>Collection of Review class objects</returns>
        /// <exception cref="YifyLib.YifyException">If any errors occurred YTS process</exception>
        public List<Review> GetMovieReviews(int movieID)
        {
            try
            {
                var uri = YifyAPI.GetMovieReviewURI(movieID);
                var res = YifyAPI.SendGetRequest(uri);
                return ResponseParser.ParseGetMovieReviewResponse(res);
            }
            catch (Exception ex)
            {
                throw new YifyException("An error occurred. See inner exception for more details", ex);
            }

        }
        /// <summary>
        /// Get parental guides for a movie
        /// </summary>
        /// <param name="movieID">YTS movie id</param>
        /// <returns>Collection of ParentalGuide class objects</returns>
        /// <exception cref="YifyLib.YifyException">If any errors occurred YTS process</exception>
        public List<ParentalGuide> GetMovieParentalGuide(int movieID)
        {
            try
            {
                var uri = YifyAPI.GetMovieParentalGuideURI(movieID);
                var res = YifyAPI.SendGetRequest(uri);
                return ResponseParser.ParseGetMovieParentalGuideResponse(res);
            }
            catch (Exception ex)
            {
                throw new YifyException("An error occurred. See inner exception for more details", ex);
            }

        }
        /// <summary>
        /// Get user details
        /// </summary>
        /// <param name="userID">YTS user id</param>
        /// <returns>User class object containing user details.</returns>
        /// <exception cref="YifyLib.YifyException">If any errors occurred YTS process</exception>
        public User GetUserDetails(int userID)
        {
            try
            {
                var uri = YifyAPI.GetUserDetailsURI(userID, true);
                var res = YifyAPI.SendGetRequest(uri);
                return ResponseParser.ParseGetUserDetailsResponse(res);
            }
            catch (Exception ex)
            {

                throw new YifyException("An error occurred. See inner exception for more details", ex);
            }
        }

        /// <summary>
        /// Login to the Yify torrent.
        /// </summary>
        /// <param name="username">Yify torrent username</param>
        /// <param name="password">Yify torrent password</param>
        /// <returns>String containing the userKey</returns>
        /// <exception cref="YifyLib.YifyMissingAppKeyException">If application key is missing</exception>
        /// <exception cref="YifyLib.YifyException">If any errors occurred YTS process</exception>
        public string Login(string username, string password)
        {
            this.CheckAppKey();
            try
            {
                var req = YifyAPI.GetUserKeyRequest(username, password, appKey, false);
                var res = YifyAPI.SendPostRequest(req);
                string ukey = ResponseParser.ParseGetUserKeyResponse(res);
                this.userKey = ukey;
                return ukey;
            }
            catch (Exception ex)
            {
                throw new YifyException("An error occurred. See inner exception for more details", ex);
            }

        }
        /// <summary>
        /// Get the logged in user's Yify profile
        /// </summary>
        /// <returns>User's profile</returns>
        /// <exception cref="YifyLib.YifyNotLoggedInException">If user has not logged in</exception>
        /// <exception cref="YifyLib.YifyException">If any errors occurred YTS process</exception>
        public Profile GetProfile()
        {
            this.CheckLoggedIn();
            var url = YifyAPI.GetUserProfileURI(this.userKey);
            string res = YifyAPI.SendGetRequest(url);
            return ResponseParser.ParseGetProfileRequest(res);
        }
        /// <summary>
        /// Edit user's profile
        /// </summary>
        /// <param name="aboutText">About text for profile</param>
        /// <param name="newPassword">New Password</param>
        /// <param name="avatarImagePath">Path to avatar image file</param>
        /// <returns>Updated profile of the current user</returns>
        /// <exception cref="YifyLib.YifyMissingAppKeyException">If application key is missing</exception>
        /// <exception cref="YifyLib.YifyNotLoggedInException">If user has not logged in</exception>
        /// <exception cref="FileNotFoundException">If the avatar image could not be found in the specified location</exception>
        /// <exception cref="YifyLib.YifyException">If any errors occurred YTS process</exception>
        public Profile EditUserSettings(string aboutText = "", string newPassword = "", string avatarImagePath = "")
        {
            this.CheckAppKey();
            this.CheckLoggedIn();
            try
            {
                var req = YifyAPI.GetEditUserSettingsRequest(this.userKey, this.appKey, aboutText, newPassword, avatarImagePath);
                string res = Task<string>.Factory.StartNew(() =>
                {
                    return YifyAPI.SendPostRequestAsync(req).Result;
                }).Result;
                return ResponseParser.ParseGetProfileRequest(res);
            }
            catch (FileNotFoundException) { throw; }
            catch (Exception ex)
            {
                throw new YifyException("An error occurred. See inner exception for more details", ex);
            }
        }
        /// <summary>
        /// Register a new user on Yify torrent
        /// </summary>
        /// <param name="userName">Username</param>
        /// <param name="password">Password</param>
        /// <param name="email">Email Address</param>
        /// <returns>Registered user information</returns>
        /// <exception cref="YifyLib.YifyMissingAppKeyException">If application key is missing</exception>
        /// <exception cref="ArgumentException">If Username, Password or Email is empty or null</exception>
        /// <exception cref="YifyLib.YifyException">If any errors occurred YTS process</exception>
        public RegisterUser RegisterUser(string userName, string password, string email)
        {
            this.CheckAppKey();
            if (!userName.HasValue() || !password.HasValue() || !email.HasValue())
            {
                throw new ArgumentException("Username, Password or Email cannot be empty.");
            }
            else
            {
                try
                {
                    var req = YifyAPI.GetRegisterUserRequest(this.appKey, userName, password, email);
                    var res = YifyAPI.SendPostRequest(req);
                    var rgu = ResponseParser.ParseRegisterUserResponse(res);
                    this.userKey = rgu.UserKey;
                    return rgu;
                }
                catch (Exception ex) { throw new YifyException("An error occurred. See inner exception for more details", ex); }

            }
        }
        /// <summary>
        /// Send a password recovery request to Yify torrent. If the email is valid Yify torrent will email a 
        /// password rest code which can be used on <code>ResetUserPassword</code> function.
        /// </summary>
        /// <param name="email">Email of a registered account</param>
        /// <returns>True if the request is successful</returns>
        /// <exception cref="YifyLib.YifyMissingAppKeyException">If application key is missing</exception>
        /// <exception cref="ArgumentException">If Email is empty or null</exception>
        /// <exception cref="YifyLib.YifyException">If any errors occurred YTS process</exception>
        public bool ForgotUserPassword(string email)
        {
            this.CheckAppKey();
            if (!email.HasValue())
            {
                throw new ArgumentException("Email cannot be empty.");
            }
            else
            {
                try
                {
                    var req = YifyAPI.GetForgotUserPasswordRequest(this.appKey, email);
                    var res = YifyAPI.SendPostRequest(req);

                    var xDoc = ResponseParser.LoadXDoc(res);
                    return true;
                }
                catch (Exception ex) { throw new YifyException("An error occurred. See inner exception for more details", ex); }
            }
        }
        /// <summary>
        /// Reset the user password
        /// </summary>
        /// <param name="resetCode">Reset code received through email</param>
        /// <param name="newPassword">New password</param>
        /// <returns>True if change is successful</returns>
        ///  <exception cref="YifyLib.YifyMissingAppKeyException">If application key is missing</exception>
        ///  <exception cref="ArgumentException">If Reset Code or New Password is empty or null</exception>
        ///  <exception cref="YifyLib.YifyException">If any errors occurred YTS process</exception>
        public bool ResetUserPassword(string resetCode, string newPassword)
        {
            this.CheckAppKey();
            if (!resetCode.HasValue() || !newPassword.HasValue())
            {
                throw new ArgumentException("Reset Code or New Password cannot be empty.");
            }
            else
            {
                try
                {
                    var req = YifyAPI.GetResetUserPasswordRequest(this.appKey, resetCode, newPassword);
                    var res = YifyAPI.SendPostRequest(req);
                    var xDoc = ResponseParser.LoadXDoc(res);
                    return true;
                }
                catch (Exception ex) { throw new YifyException("An error occurred. See inner exception for more details", ex); }
            }
        }
        /// <summary>
        /// Like movie
        /// </summary>
        /// <param name="movieID">Movie ID</param>
        /// <returns>True if successful</returns>
        ///  <exception cref="YifyLib.YifyMissingAppKeyException">If application key is missing</exception>
        ///  <exception cref="YifyLib.YifyNotLoggedInException">If user has not logged in</exception>
        ///  <exception cref="ArgumentException">If movie id is invalid</exception>
        ///  <exception cref="YifyLib.YifyException">If any errors occurred YTS process</exception>
        public bool LikeMovie(int movieID)
        {
            this.CheckAppKey();
            this.CheckLoggedIn();

            if (movieID < 0)
            {
                throw new ArgumentException("Movie id should be greater than 0.");
            }
            else
            {
                try
                {
                    var req = YifyAPI.GetLikeMovieReqeust(this.appKey, this.userKey, movieID);
                    var res = YifyAPI.SendPostRequest(req);
                    var xDoc = ResponseParser.LoadXDoc(res);
                    return true;
                }
                catch (Exception ex) { throw new YifyException("An error occurred. See inner exception for more details", ex); }
            }
        }
        /// <summary>
        /// Get a list of bookmarked movie for currently logged in user.
        /// </summary>
        /// <param name="withRtRatings">If true results will include Rotten tomato ratings</param>
        /// <returns>Collection of Bookmarked Movie</returns>
        /// <exception cref="YifyLib.YifyNotLoggedInException">If user has not logged in</exception>
        /// <exception cref="YifyLib.YifyException">If any errors occurred YTS process</exception>
        public List<BookmarkedMovie> BookmarkedMovies(bool withRtRatings)
        {
            this.CheckLoggedIn();
            try
            {

                var req = YifyAPI.GetMovieBookmarksURI(this.userKey, withRtRatings);
                var res = YifyAPI.SendGetRequest(req);
                return ResponseParser.ParseGetBookmarkedMoviesResponse(res);
            }
            catch (Exception ex) { throw new YifyException("An error occurred. See inner exception for more details", ex); }
        }
        /// <summary>
        /// Bookmark a movie
        /// </summary>
        /// <param name="movieID">Movie ID to be bookmarked</param>
        /// <returns>True if bookmark was successful</returns>
        /// <exception cref="YifyLib.YifyMissingAppKeyException">If application key is missing</exception>
        /// <exception cref="YifyLib.YifyNotLoggedInException">If user has not logged in</exception>
        /// <exception cref="ArgumentException">If movie id is invalid</exception>
        /// <exception cref="YifyLib.YifyException">If any errors occurred YTS process</exception>
        public bool AddMovieBookmark(int movieID)
        {
            this.CheckAppKey();
            this.CheckLoggedIn();

            if (movieID < 0)
            {
                throw new ArgumentException("Movie id should be greater than 0.");
            }
            else
            {
                try
                {
                    var req = YifyAPI.GetAddMovieBookmarkReqeust(this.appKey, this.userKey, movieID);
                    var res = YifyAPI.SendPostRequest(req);

                    var xDoc = ResponseParser.LoadXDoc(res);
                    return true;
                }
                catch (Exception ex) { throw new YifyException("An error occurred. See inner exception for more details", ex); }
            }
        }
        /// <summary>
        /// Delete a bookmark
        /// </summary>
        /// <param name="movieID">Movie ID to be removed from bookmarks</param>
        /// <returns>True if bookmark was successful</returns>
        /// <exception cref="YifyLib.YifyMissingAppKeyException">If application key is missing</exception>
        /// <exception cref="YifyLib.YifyNotLoggedInException">If user has not logged in</exception>
        /// <exception cref="ArgumentException">If movie id is invalid</exception>
        /// <exception cref="YifyLib.YifyException">If any errors occurred YTS process</exception>
        public bool DeleteMovieBookmark(int movieID)
        {
            this.CheckAppKey();
            this.CheckLoggedIn();

            if (movieID < 0)
            {
                throw new ArgumentException("Movie id should be greater than 0.");
            }
            else
            {
                try
                {
                    var req = YifyAPI.GetDeleteMovieBookmarkReqeust(this.appKey, this.userKey, movieID);
                    var res = YifyAPI.SendPostRequest(req);
                    var xDoc = ResponseParser.LoadXDoc(res);
                    return true;
                }
                catch (Exception ex) { throw new YifyException("An error occurred. See inner exception for more details", ex); }
            }
        }
        /// <summary>
        /// Comment on a movie
        /// </summary>
        /// <param name="movieID">Movie ID to comment on</param>
        /// <param name="commentText">Comment Text</param>
        /// <returns>ID of the newly created comment</returns>
        /// <exception cref="YifyLib.YifyMissingAppKeyException">If application key is missing</exception>
        /// <exception cref="YifyLib.YifyNotLoggedInException">If user has not logged in</exception>
        /// <exception cref="ArgumentException">If movie id or comment text is null or empty</exception>
        /// <exception cref="YifyLib.YifyException">If any errors occurred YTS process</exception>
        public int MakeComment(int movieID, string commentText)
        {
            this.CheckAppKey();
            this.CheckLoggedIn();
            if (movieID < 0)
            {
                throw new ArgumentException("Movie id should be greater than 0.");
            }
            else if (string.IsNullOrWhiteSpace(commentText))
            {
                throw new ArgumentException("Comment text cannot be null or empty.");
            }
            else
            {
                var req = YifyAPI.GetMakeCommentReqeust(this.appKey, this.userKey, movieID, commentText);
                var res = YifyAPI.SendPostRequest(req);
                int parsed = ResponseParser.ParseMakeCommentResponse(res);
                try
                {
                    var xDoc = ResponseParser.LoadXDoc(res);
                    return -1;
                }
                catch (Exception ex) { throw new YifyException("An error occurred. See inner exception for more details", ex); }
            }
        }
        /// <summary>
        /// Like a comment
        /// </summary>
        /// <param name="commentID">Comment ID to like</param>
        /// <returns>True if comment was liked</returns>
        /// <exception cref="YifyLib.YifyMissingAppKeyException">If application key is missing</exception>
        /// <exception cref="YifyLib.YifyNotLoggedInException">If user has not logged in</exception>
        /// <exception cref="ArgumentException">If comment id is invalid</exception>
        /// <exception cref="YifyLib.YifyException">If any errors occurred YTS process</exception>
        public bool LikeComment(int commentID)
        {
            this.CheckAppKey();
            this.CheckLoggedIn();

            if (commentID < 0)
            {
                throw new ArgumentException("Comment id should be greater than 0.");
            }
            else
            {

                try
                {
                    var req = YifyAPI.GetLikeCommentReqeust(this.appKey, this.userKey, commentID);
                    var res = YifyAPI.SendPostRequest(req);
                    var xDoc = ResponseParser.LoadXDoc(res);
                    return true;
                }
                catch (Exception ex) { throw new YifyException("An error occurred. See inner exception for more details", ex); }
            }
        }
        /// <summary>
        /// Report a comment which is not in conformance with YTS policy.
        /// </summary>
        /// <param name="commentID">Comment ID to like</param>
        /// <returns>True if comment was reported</returns>
        /// <exception cref="YifyLib.YifyMissingAppKeyException">If application key is missing</exception>
        /// <exception cref="YifyLib.YifyNotLoggedInException">If user has not logged in</exception>
        /// <exception cref="ArgumentException">If comment id is invalid</exception>
        /// <exception cref="YifyLib.YifyException">If any errors occurred YTS process</exception>
        public bool ReportComment(int commentID)
        {
            this.CheckAppKey();
            this.CheckLoggedIn();

            if (commentID < 0)
            {
                throw new ArgumentException("Comment id should be greater than 0.");
            }
            else
            {
                try
                {
                    var req = YifyAPI.GetReportCommentReqeust(this.appKey, this.userKey, commentID);
                    var res = YifyAPI.SendPostRequest(req);
                    var xDoc = ResponseParser.LoadXDoc(res);
                    return true;
                }
                catch (Exception ex) { throw new YifyException("An error occurred. See inner exception for more details", ex); }

            }
        }
        /// <summary>
        /// Delete a comment.
        /// </summary>
        /// <param name="commentID">Comment ID to like</param>
        /// <returns>True if comment was deleted</returns>
        /// <exception cref="YifyLib.YifyMissingAppKeyException">If application key is missing</exception>
        /// <exception cref="YifyLib.YifyNotLoggedInException">If user has not logged in</exception>
        /// <exception cref="ArgumentException">If comment id is invalid</exception>
        /// <exception cref="YifyLib.YifyException">If any errors occurred YTS process</exception>
        public bool DeleteComment(int commentID)
        {
            this.CheckAppKey();
            this.CheckLoggedIn();

            if (commentID < 0)
            {
                throw new ArgumentException("Comment id should be greater than 0.");
            }
            else
            {
                try
                {
                    var req = YifyAPI.GetDeleteCommentReqeust(this.appKey, this.userKey, commentID);
                    var res = YifyAPI.SendPostRequest(req);
                    var xDoc = ResponseParser.LoadXDoc(res);
                    return true;
                }
                catch (Exception ex) { throw new YifyException("An error occurred. See inner exception for more details", ex); }
            }
        }
        /// <summary>
        /// Make a request for a movie on YTS
        /// </summary>
        /// <param name="movieTitle">Title of the Movie</param>
        /// <param name="requestMessage">Message from user which can include a short description, IMDb links, 
        /// and 1080p/720p sources which YTS can encode from</param>
        /// <returns>True if request was made.</returns>
        /// <exception cref="YifyLib.YifyMissingAppKeyException">If application key is missing</exception>
        /// <exception cref="YifyLib.YifyNotLoggedInException">If user has not logged in</exception>
        /// <exception cref="ArgumentException">If movie id or request message is null or empty</exception>
        /// <exception cref="YifyLib.YifyException">If any errors occurred YTS process</exception>
        public bool MakeRequest(string movieTitle, string requestMessage)
        {
            this.CheckAppKey();
            this.CheckLoggedIn();

            if (string.IsNullOrWhiteSpace(movieTitle))
            {
                throw new ArgumentException("Movie title cannot be null or empty.");
            }
            else if (string.IsNullOrWhiteSpace(requestMessage))
            {
                throw new ArgumentException("Request message cannot be null or empty.");
            }
            else
            {
                try
                {
                    var req = YifyAPI.GetMakeRequestReqeust(this.appKey, this.userKey, movieTitle, requestMessage);
                    var res = YifyAPI.SendPostRequest(req);
                    var xDoc = ResponseParser.LoadXDoc(res);
                    return true;
                }
                catch (Exception ex) { throw new YifyException("An error occurred. See inner exception for more details", ex); }
            }
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Get or Set the Application key provided by YTS torrent.
        /// </summary>
        public string ApplicationKey
        {
            get { return appKey; }
            set { appKey = value; }
        }
        /// <summary>
        /// Get or set the currently logged in user's user key
        /// </summary>
        public string CurrentUserKey
        {
            get { return userKey; }
            set { userKey = value; }
        }
        /// <summary>
        /// Get whether a user is currently logged in.
        /// </summary>
        public bool IsLoggedIn
        {
            get { return !string.IsNullOrWhiteSpace(userKey); }
        }

        #region Since version 1.1.7
        public Uri CurrentBaseUri
        {
            get
            {
                Uri u = new Uri(YifyAPI.Base_URI.ToString(), UriKind.Absolute);
                return u;
            }
            set {
                if (value != null)
                    YifyAPI.Base_URI = value;
            }
        }
        #endregion

        #endregion
    }
}

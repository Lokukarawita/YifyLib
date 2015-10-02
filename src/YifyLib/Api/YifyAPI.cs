using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Xml;
using System.Xml.Linq;
using YifyLib;
using System.Collections.Specialized;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using YifyLib.Api.Util;

namespace YifyLib.Api
{
    internal class YifyAPI
    {
        public static readonly Uri Default_Base_URI = new Uri("https://yts.to/api/v2/", UriKind.Absolute);
        public static Uri Base_URI = Default_Base_URI;

        public static bool IsYifyError(XDocument doc)
        {
            return GetStatus(doc).ToLowerInvariant() != "ok".ToLowerInvariant();
        }
        public static string GetStatus(XDocument doc)
        {
            var v = doc.GetXElement("status").Value;
            return v;
        }
        public static string GetStatusMessage(XDocument doc)
        {
            var v = doc.GetXElement("status_message").Value;
            return v;
        }

        public static Uri GetListMovieURI(string queryTerm = "",
            string quality = "All",
            string genre = "All",
            uint minimumRating = 0,
            uint limit = 20,
            uint page = 1,
            SearchResultSort sortBy = SearchResultSort.DateAdded,
            SortOrder orderBy = SortOrder.Desc)
        {
            UriBuilderWithQuerySupport u = new UriBuilderWithQuerySupport(RequestUriHelper.ListMovies.ToRequestUri(Base_URI));
            u.AddQueryParameter("query_term", queryTerm.CheckString(""), true);
            u.AddQueryParameter("quality", quality.CheckString("All"), true);
            u.AddQueryParameter("genre", genre.CheckString("All"), true);
            u.AddQueryParameter("minimum_rating", minimumRating.CheckMax(9, 0).ToString(), true);
            u.AddQueryParameter("limit", limit.CheckMax(50, 20).ToString(), true);
            u.AddQueryParameter("page", queryTerm.CheckString(""), true);
            u.AddQueryParameter("sort_by", queryTerm.CheckString(""), true);
            u.AddQueryParameter("order_by", queryTerm.CheckString(""), true);

            return u.Uri;
        }
        public static Uri GetMovieURI(int movieID,
            bool includeCast = true,
            bool includeImages = true)
        {

            UriBuilderWithQuerySupport u = new UriBuilderWithQuerySupport(RequestUriHelper.MovieDetails.ToRequestUri(Base_URI));
            u.AddQueryParameter("movie_id", movieID.ToString(), true);
            u.AddQueryParameter("with_images", includeImages.ToString().ToLowerInvariant(), true);
            u.AddQueryParameter("with_cast", includeCast.ToString().ToLowerInvariant(), true);
            return u.Uri;
        }
        public static Uri GetMovieSuggestionsURI(int movieID)
        {

            UriBuilderWithQuerySupport u = new UriBuilderWithQuerySupport(RequestUriHelper.MovieSuggestions.ToRequestUri(Base_URI));
            u.AddQueryParameter("movie_id", movieID.ToString(), true);
            return u.Uri;
        }
        public static Uri GetMovieCommentsURI(int movieID)
        {
            UriBuilderWithQuerySupport u = new UriBuilderWithQuerySupport(RequestUriHelper.MovieComments.ToRequestUri(Base_URI));
            u.AddQueryParameter("movie_id", movieID.ToString(), true);
            return u.Uri;
        }
        public static Uri GetMovieReviewURI(int movieID)
        {
            UriBuilderWithQuerySupport u = new UriBuilderWithQuerySupport(RequestUriHelper.MovieReview.ToRequestUri(Base_URI));
            u.AddQueryParameter("movie_id", movieID.ToString(), true);
            return u.Uri;
        }
        public static Uri GetMovieParentalGuideURI(int movieID)
        {
            UriBuilderWithQuerySupport u = new UriBuilderWithQuerySupport(RequestUriHelper.UserDetails.ToRequestUri(Base_URI));
            u.AddQueryParameter("movie_id", movieID.ToString(), true);
            return u.Uri;
        }
        public static Uri GetUserDetailsURI(int userID, bool withDownloads)
        {
            UriBuilderWithQuerySupport u = new UriBuilderWithQuerySupport(RequestUriHelper.UserDetails.ToRequestUri(Base_URI));
            u.AddQueryParameter("user_id", userID.ToString(), true);
            u.AddQueryParameter("with_recently_downloaded", withDownloads.ToString().ToLowerInvariant(), true);
            return u.Uri;
        }
        public static Uri GetUserProfileURI(string userKey)
        {
            UriBuilderWithQuerySupport u = new UriBuilderWithQuerySupport(RequestUriHelper.UserProfile.ToRequestUri(Base_URI));
            u.AddQueryParameter("user_key", userKey.CheckString(""), true);
            return u.Uri;
        }
        public static Uri GetMovieBookmarksURI(string userKey, bool rtRatings)
        {
            UriBuilderWithQuerySupport u = new UriBuilderWithQuerySupport(RequestUriHelper.MovieBookmarks.ToRequestUri(Base_URI));
            u.AddQueryParameter("user_key", userKey.CheckString(""), true);
            u.AddQueryParameter("with_rt_ratings", rtRatings.ToString().ToLowerInvariant(), true);
            return u.Uri;
        }

        public static YifyPostRequest GetUserKeyRequest(string username, string password, string appKey, bool withRecentDownloads)
        {
            YifyPostRequest req = new YifyPostRequest();
            req.Uri = RequestUriHelper.GetUserKey.ToRequestUri(Base_URI);
            req.Data.Add("username", username);
            req.Data.Add("password", password);
            req.Data.Add("application_key", appKey);
            req.Data.Add("with_recently_downloaded", withRecentDownloads.ToString().ToLowerInvariant());
            return req;
        }
        public static YifyMultiPartRequest GetEditUserSettingsRequest(
            string userKey,
            string applicationKey,
            string aboutText = "",
            string newPassword = "",
            string avatarImage = "")
        {

            YifyMultiPartRequest request = new YifyMultiPartRequest();
            request.Uri = RequestUriHelper.EditUserSettigns.ToRequestUri(Base_URI);
            request.Data.Add("user_key", userKey);
            request.Data.Add("application_key", applicationKey);
            if (newPassword.HasValue()) request.Data.Add("new_password", newPassword);
            if (aboutText.HasValue()) request.Data.Add("about_text", aboutText);
            if (avatarImage.HasValue())
            {
                Image i = avatarImage.ToImage();

                if (i != null)
                {

                    YifyUploadFile file = new YifyUploadFile();
                    file.FieldName = "avatar_image";
                    file.Filename = Path.GetFileName(avatarImage);
                    file.MimeType = i.GetMimeType();
                    file.Data = i.GetBytes();
                    request.Files.Add(file);
                }
            }
            return request;
        }
        public static YifyPostRequest GetRegisterUserRequest(string appKey, string userName, string password, string email)
        {
            YifyPostRequest req = new YifyPostRequest();
            req.Uri = RequestUriHelper.RegisterUser.ToRequestUri(Base_URI);
            req.Data.Add("application_key", appKey);
            req.Data.Add("username", userName);
            req.Data.Add("password", password);
            req.Data.Add("email", email);

            return req;
        }
        public static YifyPostRequest GetForgotUserPasswordRequest(string appKey, string email) {
            YifyPostRequest req = new YifyPostRequest();
            req.Uri = RequestUriHelper.ForgotUserPassword.ToRequestUri(Base_URI);
            req.Data.Add("application_key", appKey);
            req.Data.Add("email", email);
            return req;
        }
        public static YifyPostRequest GetResetUserPasswordRequest(string appKey, string resetCode, string newPassword) 
        {
            YifyPostRequest req = new YifyPostRequest();
            req.Uri = RequestUriHelper.ResetUserPassword.ToRequestUri(Base_URI);
            req.Data.Add("application_key", appKey);
            req.Data.Add("reset_code", resetCode);
            req.Data.Add("new_password", newPassword);
            return req;

        }
        public static YifyPostRequest GetLikeMovieReqeust(string appKey, string userKey, int movieID)
        {
            YifyPostRequest req = new YifyPostRequest();
            req.Uri = RequestUriHelper.LikeMovie.ToRequestUri(Base_URI);
            req.Data.Add("application_key", appKey);
            req.Data.Add("user_key", userKey);
            req.Data.Add("movie_id", movieID.ToString());
            return req;
        }
        public static YifyPostRequest GetAddMovieBookmarkReqeust(string appKey, string userKey, int movieID)
        {
            YifyPostRequest req = new YifyPostRequest();
            req.Uri = RequestUriHelper.AddMovieBookmark.ToRequestUri(Base_URI);
            req.Data.Add("application_key", appKey);
            req.Data.Add("user_key", userKey);
            req.Data.Add("movie_id", movieID.ToString());
            return req;
        }
        public static YifyPostRequest GetDeleteMovieBookmarkReqeust(string appKey, string userKey, int movieID)
        {
            YifyPostRequest req = new YifyPostRequest();
            req.Uri = RequestUriHelper.DeleteMovieBookmark.ToRequestUri(Base_URI);
            req.Data.Add("application_key", appKey);
            req.Data.Add("user_key", userKey);
            req.Data.Add("movie_id", movieID.ToString());
            return req;
        }
        public static YifyPostRequest GetMakeCommentReqeust(string appKey, string userKey, int movieID, string commentText)
        {
            YifyPostRequest req = new YifyPostRequest();
            req.Uri = RequestUriHelper.MakeComment.ToRequestUri(Base_URI);
            req.Data.Add("application_key", appKey);
            req.Data.Add("user_key", userKey);
            req.Data.Add("movie_id", movieID.ToString());
            req.Data.Add("comment_text", commentText);
            return req;
        }
        public static YifyPostRequest GetLikeCommentReqeust(string appKey, string userKey, int commentID)
        {
            YifyPostRequest req = new YifyPostRequest();
            req.Uri = RequestUriHelper.LikeComment.ToRequestUri(Base_URI);
            req.Data.Add("application_key", appKey);
            req.Data.Add("user_key", userKey);
            req.Data.Add("comment_id", commentID.ToString());
            return req;
        }
        public static YifyPostRequest GetReportCommentReqeust(string appKey, string userKey, int commentID)
        {
            YifyPostRequest req = new YifyPostRequest();
            req.Uri = RequestUriHelper.ReportComment.ToRequestUri(Base_URI);
            req.Data.Add("application_key", appKey);
            req.Data.Add("user_key", userKey);
            req.Data.Add("comment_id", commentID.ToString());
            return req;
        }
        public static YifyPostRequest GetDeleteCommentReqeust(string appKey, string userKey, int commentID)
        {
            YifyPostRequest req = new YifyPostRequest();
            req.Uri = RequestUriHelper.DeleteComment.ToRequestUri(Base_URI);
            req.Data.Add("application_key", appKey);
            req.Data.Add("user_key", userKey);
            req.Data.Add("comment_id", commentID.ToString());
            return req;
        }
        public static YifyPostRequest GetMakeRequestReqeust(string appKey, string userKey, string movieTitle, string requestMessage)
        {
            YifyPostRequest req = new YifyPostRequest();
            req.Uri = RequestUriHelper.MakeRequest.ToRequestUri(Base_URI);
            req.Data.Add("application_key", appKey);
            req.Data.Add("user_key", userKey);
            req.Data.Add("movie_title", movieTitle);
            req.Data.Add("request_message", requestMessage);
            return req;
        }

        public static string SendGetRequest(Uri u)
        {
            string response = string.Empty;
            using (WebClient c = new WebClient())
            {
                c.Encoding = System.Text.Encoding.UTF8;
                response = c.DownloadString(u);
            }
            return response;
        }
        public static string SendPostRequest(YifyPostRequest req)
        {
            using (WebClient c = new WebClient())
            {
                c.Encoding = Encoding.UTF8;
                byte[] res = c.UploadValues(req.Uri, req.Data);
                if (res.Length == 0)
                {
                    return string.Empty;
                }
                else
                {
                    return Encoding.UTF8.GetString(res);
                }
            }
        }
        public static async Task<string> SendPostRequestAsync(YifyMultiPartRequest request)
        {
            HttpClient client = new HttpClient();
            MultipartFormDataContent content = new MultipartFormDataContent();

            foreach (var item in request.Data.Keys)
            {
                content.Add(new StringContent(request.Data[item.ToString()], Encoding.UTF8), item.ToString());
            }

            foreach (var item in request.Files)
            {
                var bcontent = new ByteArrayContent(item.Data, 0, item.Data.Length);
                bcontent.Headers.Add("Content-Disposition", "file; filename=\"" + item.Filename + "\"; name=" + item.FieldName);
                bcontent.Headers.Add("Content-Type", item.MimeType);
                bcontent.Headers.Add("Content-Transfer-Encoding", "binary");
                content.Add(bcontent);
            }

            HttpResponseMessage message = await client.PostAsync(request.Uri, content);
            byte[] data = await message.Content.ReadAsByteArrayAsync();
            return Encoding.UTF8.GetString(data);
        }

        public static Uri CreateGetRequest(Uri uri, params KeyValuePair<string, string> [] queryParameters)
        {
            UriBuilderWithQuerySupport urib = new UriBuilderWithQuerySupport(uri);
            foreach (var i in queryParameters)
                urib.AddQueryParameter(i.Key, i.Value, true);
            return urib.Uri;
        }
    }

    internal class YifyPostRequest
    {
        public YifyPostRequest()
        {
            Data = new NameValueCollection();

        }

        public Uri Uri { get; set; }
        public NameValueCollection Data { get; set; }

    }
    internal class YifyMultiPartRequest : YifyPostRequest
    {
        public YifyMultiPartRequest()
        {
            Files = new List<YifyUploadFile>();
        }
        public List<YifyUploadFile> Files { get; private set; }
    }
    internal class YifyUploadFile
    {
        public string FieldName { get; set; }
        public byte[] Data { get; set; }
        public string Filename { get; set; }
        public string MimeType { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YifyLib.Data;

namespace YifyLib.Api.Response
{
    /// <summary>
    /// Response parser base
    /// </summary>
    public abstract class ResponseParser
    {

        public abstract List<ListMovie> ParseListMovieResponse(string response);
        public abstract Movie ParseGetMovieResponse(string res);
        public abstract List<SuggestionMovie> ParseGetMovieSuggestionResponse(string response);
        public abstract List<Comment> ParseGetMovieCommentsResponse(string res);
        public abstract List<Review> ParseGetMovieReviewResponse(string res);
        public abstract List<ParentalGuide> ParseGetMovieParentalGuideResponse(string res);
        public abstract User ParseGetUserDetailsResponse(string res);
        public abstract string ParseGetUserKeyResponse(string res);
        public abstract Profile ParseGetProfileRequest(string res);
        public abstract RegisterUser ParseRegisterUserResponse(string res);
        public abstract List<BookmarkedMovie> ParseGetBookmarkedMoviesResponse(string res);
        public abstract int ParseMakeCommentResponse(string res);
        public abstract object ToResponse(string response);

        public abstract ResponseType SupportedResponseType { get; }
    }
}

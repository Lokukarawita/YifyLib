using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YifyLib.Data;
using System.Xml.Linq;
using YifyLib.Data.Base;
using System.Net;

namespace YifyLib.Api.Response
{
    /// <summary>
    /// XML Response parser
    /// </summary>
    [Obsolete("Discontinued till YTS API complete its functionality. Try YifyLib.Api.Resposne.JSONResponseParser instead", true)]
    public class XMLResponseParser : ResponseParser, IYifyErrorCheck<XDocument>
    {
        private static void FillAbstractUser(XElement element, AbstractUser user)
        {
            user.Username = element.GetXDecendentValue<string>("username");
        }
        private static void FillAbstractYifyUser(XElement element, AbstractYifyUser u)
        {
            FillAbstractUser(element, u);
            u.ID = element.GetXDecendentValue<int>("id");
            u.Group = element.GetXDecendentValue<string>("group");
            u.Url = element.GetXDecendentValue<string>("url");

            u.Avatars.Add(new UserImage()
            {
                ImageSize = ImageSize.Small,
                Url = element.GetXDecendentValue<string>("small_avatar_image"),
            });

            u.Avatars.Add(new UserImage()
            {
                ImageSize = ImageSize.Medium,
                Url = element.GetXDecendentValue<string>("medium_avatar_image")
            });
        }
        private static void FillUser(XElement element, User u)
        {
            FillAbstractYifyUser(element, u);
            u.AboutText = element.GetXDecendentValue<string>("about_text");
            u.DateJoined = element.GetXDecendentValue<DateTime>("date_joined");
            u.DateJoinedUnix = element.GetXDecendentValue<long>("date_joined_unix");
            u.DateLastSeen = element.GetXDecendentValue<DateTime>("date_last_seen");
            u.DateLastSeenUnix = element.GetXDecendentValue<long>("date_last_seen_unix");
        }
        private static void FillProfile(XElement element, Profile p)
        {
            FillAbstractYifyUser(element, p);
            p.Email = element.GetXDecendentValue<string>("email");
            p.UserKey = element.GetXDecendentValue<string>("user_key");
            p.IPString = element.GetXDecendentValue<string>("ip_address");
        }
        private static void FillCommentUser(XElement element, CommentUser user)
        {
            FillAbstractUser(element, user);
            user.ID = element.GetXDecendentValue<int>("user_id");
            user.Url = element.GetXDecendentValue<string>("user_profile_url");
            user.Group = element.GetXDecendentValue<string>("user_group");
        }
        private static void FillCommentUserImage(XElement element, CommentUser u)
        {
            UserImage i = new UserImage();
            i.ImageSize = ImageSize.Small;
            i.Url = element.GetXDecendentValue<string>("small_user_avatar_image");
            u.Avatars.Add(i);

            i = new UserImage();
            i.ImageSize = ImageSize.Medium;
            i.Url = element.GetXDecendentValue<string>("medium_user_avatar_image");
            u.Avatars.Add(i);
        }
        private static void FillReviewUser(XElement element, ReviewUser u)
        {
            FillAbstractUser(element, u);
            u.UserLocation = element.GetXDecendentValue<string>("user_location");
        }
        private static void FillAbstractPerson(XElement element, AbstractPerson person)
        {
            person.Name = element.GetXDecendentValue<string>("name");
            person.SmallImage = element.GetXDecendentValue<string>("small_image");
            person.MediumImage = element.GetXDecendentValue<string>("medium_image");
        }
        private static void FillDirectors(XElement element, Movie movie)
        {
            var xdirector = element.GetXElements("director");
            foreach (var elem in xdirector)
            {
                Person p = new Person();
                FillAbstractPerson(elem, p);
                movie.Directors.Add(p);
            }
        }
        private static void FillActors(XElement xcast, Movie movie)
        {
            var xdirector = xcast.GetXElements("actor");
            foreach (var elem in xdirector)
            {
                Actor p = new Actor();
                FillAbstractPerson(elem, p);
                p.CharacterName = elem.GetXDecendentValue<string>("character_name");
                movie.Actors.Add(p);
            }
        }
        private static void FillTorrent(XElement element, AbstractYifyMovie movie)
        {
            var xtorrents = element.Descendants("torrent");
            foreach (XElement elem in xtorrents)
            {
                movie.Torrents.Add(new Torrent()
                {
                    Url = elem.GetXDecendentValue<string>("url"),
                    Hash = elem.GetXDecendentValue<string>("hash"),
                    Quality = elem.GetXDecendentValue<string>("quality"),
                    Seeds = elem.GetXDecendentValue<string>("seeds"),
                    Peers = elem.GetXDecendentValue<string>("peers"),
                    Size = elem.GetXDecendentValue<string>("size"),
                    SizeBytes = elem.GetXDecendentValue<long>("size_bytes"),
                    DateUploaded = element.GetXDecendentValue<DateTime>("date_uploaded"),
                    DateUploadedUnix = element.GetXDecendentValue<long>("date_uploaded_unix")
                });
            }
        }
        private static void FillAbstractMovie(XElement element, AbstractMovie movie)
        {
            movie.ID = element.GetXDecendentValue<int>("id");
            movie.Url = element.GetXDecendentValue<string>("url");
            movie.IMDBCode = element.GetXDecendentValue<string>("imdb_code");
            movie.Title = element.GetXDecendentValue<string>("title");
            movie.TitleLong = element.GetXDecendentValue<string>("title_long");
            movie.Year = element.GetXDecendentValue<int>("year");
            movie.Rating = element.GetXDecendentValue<string>("rating");
            movie.Runtime = element.GetXDecendentValue<int>("runtime");
            var xgenres = element.Descendants("genres").First().Descendants("genre").ToList();
            foreach (XElement elem in xgenres)
            {
                movie.Genres.Add(elem.GetXElementValue<string>());
            }
        }
        private static void FillAbstractYifyMovie(XElement element, AbstractYifyMovie movie)
        {
            FillAbstractMovie(element, movie);

            movie.Language = element.GetXDecendentValue<string>("language");
            movie.MPARating = element.GetXDecendentValue<string>("mpa_rating");
            movie.DateUploaded = element.GetXDecendentValue<DateTime>("date_uploaded");
            movie.DateUploadedUnix = element.GetXDecendentValue<long>("date_uploaded_unix");

            var xtorrents = element.GetXElement("torrents");
            FillTorrent(xtorrents, movie);
        }
        private static void FillMovieImages(XElement ele, Movie m)
        {
            MovieImage i = new MovieImage();
            i.ImageSize = ImageSize.NotRelevent;
            i.ImageType = MovieImageType.Background;
            i.Url = ele.GetXDecendentValue<string>("background_image");
            m.Images.Add(i);


            var xElemList = ele.Descendants().ToList();

            var xFilteredImgList = xElemList.Where(ximg => ximg.Name.LocalName.Contains("small"));
            foreach (var item in xFilteredImgList)
            {
                i = new MovieImage()
                {
                    ImageSize = ImageSize.Small,
                    Url = item.GetXElementValue<string>()
                };

                if (item.Name.LocalName.Contains("cover"))
                    i.ImageType = MovieImageType.Cover;
                else if (item.Name.LocalName.Contains("screenshot"))
                    i.ImageType = MovieImageType.ScreenShot;

                m.Images.Add(i);
            }

            xFilteredImgList = xElemList.Where(ximg => ximg.Name.LocalName.Contains("medium"));
            foreach (var item in xFilteredImgList)
            {
                i = new MovieImage()
                {
                    ImageSize = ImageSize.Medium,
                    Url = item.GetXElementValue<string>()
                };

                if (item.Name.LocalName.Contains("cover"))
                    i.ImageType = MovieImageType.Cover;
                else if (item.Name.LocalName.Contains("screenshot"))
                    i.ImageType = MovieImageType.ScreenShot;

                m.Images.Add(i);
            }

            xFilteredImgList = xElemList.Where(ximg => ximg.Name.LocalName.Contains("large"));
            foreach (var item in xFilteredImgList)
            {
                i = new MovieImage()
                {
                    ImageSize = ImageSize.Large,
                    Url = item.GetXElementValue<string>()
                };

                if (item.Name.LocalName.Contains("cover"))
                    i.ImageType = MovieImageType.Cover;
                else if (item.Name.LocalName.Contains("screenshot"))
                    i.ImageType = MovieImageType.ScreenShot;

                m.Images.Add(i);
            }
        }

        public override List<ListMovie> ParseListMovieResponse(string response)
        {
            XDocument doc = (System.Xml.Linq.XDocument)ToResponse(response);
            var dataElement = doc.GetXElement("data");

            if (dataElement == null || dataElement.Descendants().Count() == 0)
                return new List<ListMovie>();

            var xmovies = dataElement.GetXElements("movie").ToList();

            List<ListMovie> movies = new List<ListMovie>();

            foreach (XElement xmovie in xmovies)
            {
                ListMovie movie = new ListMovie();
                FillAbstractYifyMovie(xmovie, movie);

                movie.State = xmovie.Descendants("state").First().GetXElementValue<string>();

                MovieImage image;
                image = new MovieImage()
                {
                    Url = xmovie.Descendants("small_cover_image").First().GetXElementValue<string>(),
                    ImageSize = ImageSize.Small,
                    ImageType = MovieImageType.Cover
                };
                movie.Images.Add(image);

                image = new MovieImage()
                {
                    Url = xmovie.Descendants("medium_cover_image").First().GetXElementValue<string>(),
                    ImageSize = ImageSize.Medium,
                    ImageType = MovieImageType.Cover
                };
                movie.Images.Add(image);

                movies.Add(movie);
            }

            return movies;
        }
        public override Movie ParseGetMovieResponse(string res)
        {
            XDocument doc = (System.Xml.Linq.XDocument)ToResponse(res);
            var xmovie = doc.GetXElement("data");

            Movie movie = new Movie();
            FillAbstractYifyMovie(xmovie, movie);

            movie.YTTrailerCode = xmovie.GetXDecendentValue<string>("yt_trailer_code");
            movie.DescriptionFull = xmovie.GetXDecendentValue<string>("description_full");
            movie.DescriptionIntro = xmovie.GetXDecendentValue<string>("description_intro");
            movie.DownloadCount = xmovie.GetXDecendentValue<int>("download_count");
            movie.LikeCount = xmovie.GetXDecendentValue<int>("like_count");
            movie.RTAudienceRating = xmovie.GetXDecendentValue<string>("rt_audience_rating");
            movie.RTAudienceScore = xmovie.GetXDecendentValue<int>("rt_audience_score");
            movie.RTCriticsRating = xmovie.GetXDecendentValue<string>("rt_critics_rating");
            movie.RTCriticsScore = xmovie.GetXDecendentValue<int>("rt_critics_score");

            var xdirectors = xmovie.GetXElement("directors");
            FillDirectors(xdirectors, movie);

            var xcast = xmovie.GetXElement("actors");
            FillActors(xcast, movie);

            var ximages = xmovie.GetXElement("images");
            FillMovieImages(ximages, movie);

            return movie;
        }
        public override List<SuggestionMovie> ParseGetMovieSuggestionResponse(string response)
        {
            XDocument doc = (System.Xml.Linq.XDocument)ToResponse(response);
            var xmovies = doc.GetXElement("data").GetXElements("movie_suggestion").ToList();
            List<SuggestionMovie> movies = new List<SuggestionMovie>();

            foreach (var item in xmovies)
            {
                SuggestionMovie movie = new SuggestionMovie();
                FillAbstractMovie(item, movie);

                movie.State = xmovies.Descendants("state").First().GetXElementValue<string>();

                MovieImage image;
                image = new MovieImage()
                {
                    Url = xmovies.Descendants("small_cover_image").First().GetXElementValue<string>(),
                    ImageSize = ImageSize.Small,
                    ImageType = MovieImageType.Cover
                };
                movie.Images.Add(image);

                image = new MovieImage()
                {
                    Url = xmovies.Descendants("medium_cover_image").First().GetXElementValue<string>(),
                    ImageSize = ImageSize.Medium,
                    ImageType = MovieImageType.Cover
                };
                movie.Images.Add(image);

                movies.Add(movie);

            }

            return movies;
        }
        public override List<Comment> ParseGetMovieCommentsResponse(string res)
        {
            XDocument doc = (System.Xml.Linq.XDocument)ToResponse(res);
            var xmovies = doc.GetXElement("data").GetXElements("comment").ToList();
            List<Comment> comments = new List<Comment>();

            foreach (var item in xmovies)
            {
                Comment c = new Comment();
                c.User = new CommentUser();

                FillCommentUser(item, c.User);
                FillCommentUserImage(item, c.User);

                c.LikeCount = item.GetXDecendentValue<int>("like_count", 0);
                c.CommentText = item.GetXDecendentValue<string>("comment_text");
                c.DateAdded = item.GetXDecendentValue<DateTime>("date_added");
                c.DateAddedUnix = item.GetXDecendentValue<long>("date_added_unix");

                comments.Add(c);
            }

            return comments;
        }
        public override List<Review> ParseGetMovieReviewResponse(string res)
        {
            XDocument doc = (System.Xml.Linq.XDocument)ToResponse(res);
            var xmovies = doc.GetXElement("data").GetXElements("review").ToList();
            List<Review> review = new List<Review>();

            foreach (var item in xmovies)
            {
                Review c = new Review()
                {
                    User = new ReviewUser(),
                    UserRating = item.GetXDecendentValue<int>("user_rating"),
                    ReviewSummary = item.GetXDecendentValue<string>("review_summary"),
                    ReviewText = item.GetXDecendentValue<string>("review_text"),
                    DateWritten = item.GetXDecendentValue<DateTime>("date_written"),
                    DateWrittenUnix = item.GetXDecendentValue<long>("date_written_unix"),
                };
                FillReviewUser(item, c.User);
                review.Add(c);
            }

            return review;
        }
        public override List<ParentalGuide> ParseGetMovieParentalGuideResponse(string res)
        {
            XDocument doc = (System.Xml.Linq.XDocument)ToResponse(res);
            var xmovies = doc.GetXElement("data").GetXElements("parental_guide").ToList();
            List<ParentalGuide> result = new List<ParentalGuide>();

            foreach (var item in xmovies)
            {
                ParentalGuide c = new ParentalGuide()
                {
                    Type = item.GetXDecendentValue<string>("type"),
                    Text = item.GetXDecendentValue<string>("parental_guide_text")
                };

                result.Add(c);
            }

            return result;
        }
        public override User ParseGetUserDetailsResponse(string res)
        {
            XDocument doc = (System.Xml.Linq.XDocument)ToResponse(res);
            var xmovies = doc.GetXElement("data");
            User result = new User();
            FillAbstractUser(xmovies, result);
            FillUser(xmovies, result);
            var xdlMovies = xmovies.GetXElements("item").ToList();
            foreach (var xdlMovie in xdlMovies)
            {
                UserDownloadedMovie dl = new UserDownloadedMovie();
                FillAbstractMovie(xdlMovie, dl);
                result.Downloads.Add(dl);

                MovieImage image;
                image = new MovieImage()
                {
                    Url = xdlMovie.Descendants("small_cover_image").First().GetXElementValue<string>(),
                    ImageSize = ImageSize.Small,
                    ImageType = MovieImageType.Cover
                };
                dl.Images.Add(image);

                image = new MovieImage()
                {
                    Url = xdlMovie.Descendants("medium_cover_image").First().GetXElementValue<string>(),
                    ImageSize = ImageSize.Medium,
                    ImageType = MovieImageType.Cover
                };
                dl.Images.Add(image);
            }
            return result;
        }
        public override string ParseGetUserKeyResponse(string res)
        {
            XDocument doc = (System.Xml.Linq.XDocument)ToResponse(res);
            var xnodes = doc.GetXElement("data");
            return xnodes.GetXDecendentValue<string>("user_key");
        }
        public override Profile ParseGetProfileRequest(string res)
        {
            XDocument doc = (System.Xml.Linq.XDocument)ToResponse(res);
            var xnodes = doc.GetXElement("data");
            Profile p = new Profile();
            FillProfile(xnodes, p);
            return p;
        }
        public override RegisterUser ParseRegisterUserResponse(string res)
        {
            XDocument doc = (System.Xml.Linq.XDocument)ToResponse(res);
            var xnodes = doc.GetXElement("data");
            RegisterUser p = new RegisterUser();
            FillAbstractUser(xnodes, p);
            p.UserKey = xnodes.GetXDecendentValue<string>("user_key");
            p.Email = xnodes.GetXDecendentValue<string>("email");
            return p;
        }
        public override List<BookmarkedMovie> ParseGetBookmarkedMoviesResponse(string res)
        {
            XDocument doc = (System.Xml.Linq.XDocument)ToResponse(res);
            var xmovies = doc.GetXElement("data").GetXElements("movie").ToList();
            List<BookmarkedMovie> result = new List<BookmarkedMovie>();

            foreach (var item in xmovies)
            {
                BookmarkedMovie bm = new BookmarkedMovie();
                FillAbstractYifyMovie(item, bm);

                MovieImage image;
                image = new MovieImage()
                {
                    Url = item.Descendants("small_cover_image").First().GetXElementValue<string>(),
                    ImageSize = ImageSize.Small,
                    ImageType = MovieImageType.Cover
                };
                bm.Images.Add(image);

                image = new MovieImage()
                {
                    Url = item.Descendants("medium_cover_image").First().GetXElementValue<string>(),
                    ImageSize = ImageSize.Medium,
                    ImageType = MovieImageType.Cover
                };
                bm.Images.Add(image);
                bm.State = item.GetXDecendentValue<string>("state");
                bm.RTAudienceRating = item.GetXDecendentValue<string>("rt_audience_rating");
                bm.RTAudienceScore = item.GetXDecendentValue<int>("rt_audience_score");
                bm.RTCriticsRating = item.GetXDecendentValue<string>("rt_critics_rating");
                bm.RTCriticsScore = item.GetXDecendentValue<int>("rt_critics_score");


                result.Add(bm);
            }

            return result;
        }
        public override int ParseMakeCommentResponse(string res)
        {
            XDocument doc = (System.Xml.Linq.XDocument)ToResponse(res);
            var commentID = doc.GetXElement("data").GetXDecendentValue<int>("comment_id");
            return commentID;
        }
        public override object ToResponse(string response)
        {
            if (string.IsNullOrEmpty(response))
            {
                throw new YifyException("Empty response received");
            }
            else
            {
                XDocument doc = response.ToXDoc();
                if (YifyAPI.IsYifyError(doc))
                {
                    throw new YifyException(YifyAPI.GetStatusMessage(doc));
                }
                else
                {
                    return doc;
                }
            }
        }

        public virtual bool IsYifyError(XDocument doc)
        {
            return GetStatus(doc).ToLowerInvariant() != "ok".ToLowerInvariant();
        }
        public virtual string GetStatus(XDocument doc)
        {
            var v = doc.GetXElement("status").Value;
            return v;
        }
        public virtual string GetStatusMessage(XDocument doc)
        {
            var v = doc.GetXElement("status_message").Value;
            return v;
        }

        public override ResponseType SupportedResponseType
        {
            get { return ResponseType.XML; }
        }
    }
}

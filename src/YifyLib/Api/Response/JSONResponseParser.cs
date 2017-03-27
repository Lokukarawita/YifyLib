using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YifyLib.Data;
using YifyLib.Data.Base;

namespace YifyLib.Api.Response
{
    public class JSONResponseParser : ResponseParser, IYifyErrorCheck<JObject>
    {

        private void FillAbstractMovie(JToken xmovie, AbstractMovie movie)
        {
            movie.ID = xmovie["id"].Value<int>();
            movie.Url = xmovie["url"].Value<string>();
            movie.IMDBCode = xmovie["imdb_code"].Value<string>();
            movie.Title = xmovie["title"].Value<string>();
            movie.TitleLong = xmovie["title_long"].Value<string>();
            movie.Year = xmovie["year"].Value<int>();
            movie.Rating = xmovie["rating"].Value<string>();
            movie.Runtime = xmovie["runtime"].Value<int>();
            var xgenres = xmovie["genres"].Values<string>();
            movie.Genres.AddRange(xgenres);
        }
        private void FillAbstractYifyMovie(JToken element, AbstractYifyMovie movie)
        {
            FillAbstractMovie(element, movie);

            movie.Language = element["language"].Value<string>();
            movie.MPARating = element["mpa_rating"].Value<string>();
            movie.DateUploaded = element["date_uploaded"].Value<DateTime>();
            movie.DateUploadedUnix = element["date_uploaded_unix"].Value<long>();

            var xtorrents = element["torrents"];
            FillTorrent(xtorrents, movie);
        }
        private void FillTorrent(JToken element, AbstractYifyMovie movie)
        {
            FillTorrent(element, movie.Torrents);
        }
        private void FillTorrent(JToken element, List<Torrent> torrents)
        {
            var xtorrents = element.ToList();
            foreach (JToken elem in xtorrents)
            {
                torrents.Add(new Torrent()
                {
                    Url = elem["url"].Value<string>(),
                    Hash = elem["hash"].Value<string>(),
                    Quality = elem["quality"].Value<string>(),
                    Seeds = elem["seeds"].Value<string>(),
                    Peers = elem["peers"].Value<string>(),
                    Size = elem["size"].Value<string>(),
                    SizeBytes = elem["size_bytes"].Value<long>(),
                    DateUploaded = elem["date_uploaded"].Value<DateTime>(),
                    DateUploadedUnix = elem["date_uploaded_unix"].Value<long>()
                });
            }

        }

        private static void FillAbstractPerson(JToken element, AbstractPerson person)
        {
            person.Name = element["name"].Value<string>();
            person.SmallImage = element["url_small_image"].Value<string>();
            //person.MediumImage = element["medium_image"].Value<string>();
        }
        private static void FillDirectors(JToken element, Movie movie)
        {
            if (element == null) return;

            var xdirector = element["director"].ToList();
            foreach (var elem in xdirector)
            {
                Person p = new Person();
                FillAbstractPerson(elem, p);
                movie.Directors.Add(p);
            }
        }
        private static void FillActors(JToken xcast, Movie movie)
        {
            var xdirector = xcast.ToList();
            foreach (var elem in xdirector)
            {
                Actor p = new Actor();
                FillAbstractPerson(elem, p);
                p.IMDBCode = elem["imdb_code"].Value<string>();
                p.CharacterName = elem["character_name"].Value<string>();
                movie.Actors.Add(p);
            }
        }

        private static void FillMovieImages(JToken ele, Movie m)
        {
            MovieImage mi = new MovieImage();

            var elems = ele.Where(i =>
                i.Type == JTokenType.Property &&
                i.ToObject<JProperty>().Name.Contains("large_screenshot")).ToList();
            foreach (var item in elems)
            {
                try
                {
                    mi = new MovieImage()
                                    {
                                        ImageSize = ImageSize.Large,
                                        ImageType = MovieImageType.ScreenShot,
                                        Url = item.First().Value<string>()
                                    };
                    m.Images.Add(mi);
                }
                catch (Exception) { }
            }

            elems = ele.Where(i =>
                i.Type == JTokenType.Property &&
                i.ToObject<JProperty>().Name.Contains("medium_screenshot")).ToList();
            foreach (var item in elems)
            {
                try
                {
                    mi = new MovieImage()
                                    {
                                        ImageSize = ImageSize.Medium,
                                        ImageType = MovieImageType.ScreenShot,
                                        Url = item.First().Value<string>()
                                    };
                    m.Images.Add(mi);
                }
                catch (Exception) { }
            }


            elems = ele.Where(i =>
                i.Type == JTokenType.Property &&
                i.ToObject<JProperty>().Name.Contains("cover")).ToList();

            foreach (var item in elems)
            {
                try
                {
                    mi = new MovieImage()
                                   {
                                       ImageType = MovieImageType.Cover,
                                       Url = item.First().Value<string>()
                                   };

                    if (item.ToObject<JProperty>().Name.Contains("large"))
                        mi.ImageSize = ImageSize.Large;
                    else if (item.ToObject<JProperty>().Name.Contains("small"))
                        mi.ImageSize = ImageSize.Small;
                    else
                        mi.ImageSize = ImageSize.Medium;

                    m.Images.Add(mi);
                }
                catch (Exception) { }

            }

            elems = ele.Where(i =>
                i.Type == JTokenType.Property &&
                i.ToObject<JProperty>().Name.Contains("background")).ToList();

            foreach (var item in elems)
            {
                try
                {
                    mi = new MovieImage()
                    {
                        ImageType = MovieImageType.Background,
                        Url = item.First().Value<string>()
                    };

                    if (item.ToObject<JProperty>().Name.Contains("large"))
                        mi.ImageSize = ImageSize.Large;
                    else if (item.ToObject<JProperty>().Name.Contains("small"))
                        mi.ImageSize = ImageSize.Small;
                    else
                        mi.ImageSize = ImageSize.Medium;

                    m.Images.Add(mi);
                }
                catch (Exception) { }
            }
        }
        private static void FillMovieImages(JToken ele, List<MovieImage> images)
        {
            MovieImage mi = new MovieImage();

            var elems = ele.Where(i =>
                i.Type == JTokenType.Property &&
                i.ToObject<JProperty>().Name.Contains("large_screenshot")).ToList();
            foreach (var item in elems)
            {
                try
                {
                    mi = new MovieImage()
                                    {
                                        ImageSize = ImageSize.Large,
                                        ImageType = MovieImageType.ScreenShot,
                                        Url = item.First().Value<string>()
                                    };
                    images.Add(mi);
                }
                catch (Exception) { }
            }

            elems = ele.Where(i =>
                i.Type == JTokenType.Property &&
                i.ToObject<JProperty>().Name.Contains("medium_screenshot")).ToList();
            foreach (var item in elems)
            {
                try
                {
                    mi = new MovieImage()
                                    {
                                        ImageSize = ImageSize.Medium,
                                        ImageType = MovieImageType.ScreenShot,
                                        Url = item.First().Value<string>()
                                    };
                    images.Add(mi);
                }
                catch (Exception) { }
            }


            elems = ele.Where(i =>
                i.Type == JTokenType.Property &&
                i.ToObject<JProperty>().Name.Contains("cover")).ToList();

            foreach (var item in elems)
            {
                try
                {
                    mi = new MovieImage()
                                   {
                                       ImageType = MovieImageType.Cover,
                                       Url = item.First().Value<string>()
                                   };

                    if (item.ToObject<JProperty>().Name.Contains("large"))
                        mi.ImageSize = ImageSize.Large;
                    else if (item.ToObject<JProperty>().Name.Contains("small"))
                        mi.ImageSize = ImageSize.Small;
                    else
                        mi.ImageSize = ImageSize.Medium;

                    images.Add(mi);
                }
                catch (Exception) { }

            }

            elems = ele.Where(i =>
                i.Type == JTokenType.Property &&
                i.ToObject<JProperty>().Name.Contains("background")).ToList();

            foreach (var item in elems)
            {
                try
                {
                    mi = new MovieImage()
                    {
                        ImageType = MovieImageType.Background,
                        Url = item.First().Value<string>()
                    };

                    if (item.ToObject<JProperty>().Name.Contains("large"))
                        mi.ImageSize = ImageSize.Large;
                    else if (item.ToObject<JProperty>().Name.Contains("small"))
                        mi.ImageSize = ImageSize.Small;
                    else
                        mi.ImageSize = ImageSize.Medium;

                    images.Add(mi);
                }
                catch (Exception) { }
            }
        }

        public override List<Data.ListMovie> ParseListMovieResponse(string response)
        {
            var jsonDoc = (JObject)ToResponse(response);

            var count = jsonDoc["data"]["movie_count"].Value<int>();
            
            List<JToken> moviesJtkn;
            if (count == 0)
                return new List<ListMovie>();
            else
                moviesJtkn = jsonDoc["data"]["movies"].ToList();

            List<ListMovie> movies = new List<ListMovie>();

            foreach (JToken xmovie in moviesJtkn)
            {
                ListMovie movie = new ListMovie();
                FillAbstractYifyMovie(xmovie, movie);

                movie.State = xmovie["state"].Value<string>();

                MovieImage image;
                try
                {
                    image = new MovieImage()
                                    {
                                        Url = xmovie["small_cover_image"].Value<string>(),
                                        ImageSize = ImageSize.Small,
                                        ImageType = MovieImageType.Cover
                                    };
                    movie.Images.Add(image);
                }
                catch (Exception) { }


                try
                {
                    image = new MovieImage()
                                    {
                                        Url = xmovie["medium_cover_image"].Value<string>(),
                                        ImageSize = ImageSize.Medium,
                                        ImageType = MovieImageType.Cover
                                    };
                    movie.Images.Add(image);
                }
                catch (Exception) { }

                try
                {
                    image = new MovieImage()
                        {
                            Url = xmovie["large_cover_image"].Value<string>(),
                            ImageSize = ImageSize.Large,
                            ImageType = MovieImageType.Cover
                        };
                    movie.Images.Add(image);

                }
                catch (Exception) { }

                try
                {
                    image = new MovieImage()
                                    {
                                        Url = xmovie["background_image"].Value<string>(),
                                        ImageSize = ImageSize.NotRelevent,
                                        ImageType = MovieImageType.Background
                                    };
                    movie.Images.Add(image);
                }
                catch (Exception) { }

                try
                {
                    image = new MovieImage()
                        {
                            Url = xmovie["background_image_original"].Value<string>(),
                            ImageSize = ImageSize.NotRelevent,
                            ImageType = MovieImageType.Background
                        };
                    movie.Images.Add(image);

                }
                catch (Exception) { }

                movies.Add(movie);
            }

            return movies;
        }
        public override Data.Movie ParseGetMovieResponse(string res)
        {
            JToken doc = (JToken)ToResponse(res);

            var xmovie = doc["data"]["movie"];
            if (xmovie["id"].Value<int>() == 0) return null;

            Movie movie = new Movie();
            FillAbstractYifyMovie(xmovie, movie);

            movie.YTTrailerCode = xmovie["yt_trailer_code"].Value<string>();
            movie.DescriptionFull = xmovie["description_full"].Value<string>();
            movie.DescriptionIntro = xmovie["description_intro"].Value<string>();
            movie.DownloadCount = xmovie["download_count"].Value<int>();
            movie.LikeCount = xmovie["like_count"].Value<int>();

            /*
             * These are not available anymore
             * 
            movie.RTAudienceRating = xmovie["rt_audience_rating"].Value<string>();
            movie.RTAudienceScore = xmovie["rt_audience_score"].Value<int>();
            movie.RTCriticsRating = xmovie["rt_critics_rating"].Value<string>();
            movie.RTCriticsScore = xmovie["rt_critics_score"].Value<int>();*/

            var xdirectors = xmovie["directors"];
            FillDirectors(xdirectors, movie);

            var xcast = xmovie["cast"];
            FillActors(xcast, movie);

            FillMovieImages(xmovie, movie);

            return movie;
        }
        public override List<Data.SuggestionMovie> ParseGetMovieSuggestionResponse(string response)
        {
            JToken doc = (JToken)ToResponse(response);
            var xmovies = doc["data"]["movies"].ToList();
            List<SuggestionMovie> movies = new List<SuggestionMovie>();

            foreach (var item in xmovies)
            {
                SuggestionMovie movie = new SuggestionMovie();
                FillAbstractMovie(item, movie);


                movie.State = item["state"].Value<string>();
                FillMovieImages(item, movie.Images);

                FillTorrent(item["torrents"], movie.Torrents);


                movies.Add(movie);

            }

            return movies;
        }

        public override List<Data.Comment> ParseGetMovieCommentsResponse(string res)
        {
            throw new NotImplementedException();
        }
        public override List<Data.Review> ParseGetMovieReviewResponse(string res)
        {
            throw new NotImplementedException();
        }
        public override List<Data.ParentalGuide> ParseGetMovieParentalGuideResponse(string res)
        {
            throw new NotImplementedException();
        }
        public override Data.User ParseGetUserDetailsResponse(string res)
        {
            throw new NotImplementedException();
        }
        public override string ParseGetUserKeyResponse(string res)
        {
            throw new NotImplementedException();
        }
        public override Data.Profile ParseGetProfileRequest(string res)
        {
            throw new NotImplementedException();
        }
        public override Data.RegisterUser ParseRegisterUserResponse(string res)
        {
            throw new NotImplementedException();
        }
        public override List<Data.BookmarkedMovie> ParseGetBookmarkedMoviesResponse(string res)
        {
            throw new NotImplementedException();
        }
        public override int ParseMakeCommentResponse(string res)
        {
            throw new NotImplementedException();
        }

        public override object ToResponse(string response)
        {
            var doc = JObject.Parse(response);
            if (IsYifyError(doc))
            {
                throw new YifyException(GetStatusMessage(doc));
            }
            else
            {
                return doc;
            }
        }

        public virtual bool IsYifyError(JObject doc)
        {
            return doc["status"].Value<string>() != "ok";
        }
        public virtual string GetStatus(JObject doc)
        {
            return doc["status"].Value<string>();
        }
        public virtual string GetStatusMessage(JObject doc)
        {
            return doc["status_message"].Value<string>();
        }

        public override ResponseType SupportedResponseType
        {
            get { return ResponseType.JSON; }
        }
    }
}

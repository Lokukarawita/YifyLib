##NOTE
_YifyLib is currently in Alpha phase and only capable of accessing Yify API v1 only. Yify API v1 is schedule to be deprecated by the Yify authors. Once the API gets deprecated this library will not be able to function correctly. See [Coming Soon](/README.md#coming-soon) section for future developments._

# YifyLib
**.NET library to access [Yify torrent](https://yts.re/) API.**

This provide access to basic functionalities of [Yify torrent API v1](https://yts.re/api) like,

* Searching for movies ([`ListMovies`] (/README.md#searching-for-movies-listmovies))
* Getting movie details ([`GetMovieDetails`] (/README.md#getting-movie-details-getmoviedetails))
* Listing upcoming movies (`GetUpcomingMovies`)
* Listing comments for a movie (`GetComments`)
* Getting user profile (`GetProfile`)
* Getting user information for a particular user (`GetUser`)
* Posting comments (`PostComment `)
* Listing the current movie requests (`RequestList `)
* Vote for request (`VoteRequest `)
* Requesting a movie  (`MakeRequest `)

Etc…

##Installing

You can install this library using either 

1. **[Package Manager Dialog] (https://docs.nuget.org/consume/Package-Manager-Dialog)**
2. **[Package Manager Console](http://docs.nuget.org/consume/package-manager-console)** 

####To install via Package Manager Dialog

1. Open the Package Manager Dialog.
2. Search for `YifyLib` in `Online` > `nuget.org` section.

#### To install via Package Manager Console

1. Open Package Manager Console.
2. Run the following command.

```
  Install-Package YifyLib
```

##Getting Started

From any type of .NET compatible project.

* Install `YifyLib`

And that's it!.

##Examples

####Namespaces
```
using YifyLib;
using YifyLib.Data;
using YifyLib.Data.Base;
```

####Intialization

```
Yify yify = new Yify();
```

####Searching for movies (`ListMovies`)

#####Basic Search
* Search for movies where movie tile contains the word `Bourne`.
```
List<ListMovieItem> searchResult = yify.ListMovies("Bourne");
```

#####Advanced Search

* Search for movies where movie tile contains the word `Bourne`, `720p` quality and with minimum rating of 5.
```
List<ListMovieItem> searchResult = yify.ListMovies(
  keywords:"Bourne",
  quality: "720p",
  rating:5);
```

######Sorting and Ordering search result

- To Sort the search results by a particular field use `ListMovieSort` enum.
- To Order the search result in ascending or descending order use `SortOrder` enum.

Search movies where movie tile contains the word `Bourne`, sorted by date and orderd in ascending order.
```
List<ListMovieItem> searchResult = yify.ListMovies(
  keywords:"Bourne",
  sort:ListMovieSort.date, 
  order:SortOrder.asc);
```

####Getting movie details (`GetMovieDetails`)

* Getting movie details for movie 123 without loading the comments.

```
Movie movie = yify.GetMovieDetails(123, false);
```

# Coming Soon
* ~~Provide v1 API in the library to access the Yify torrent data via v2 API~~
* Deprecate v1 code and implement support for v2
* ~~Support for Yify API v2~~
* Examples for rest of the functionality.

##NOTE
_Since deprecation of API v1 by Yts.re YifyLib (since v1.1) no longer support the v1 of Yts API.
See [Coming Soon](/README.md#coming-soon) section for future developments._

# YifyLib
**.NET library to access [Yify torrent](https://yts.re/) API.**

This provide access to basic functionalities of [Yify torrent API v1](https://yts.to/api) like,

* Searching for movies ([`ListMovies`] (/README.md#searching-for-movies-listmovies))
* Getting movie details ([`GetMovie`] (/README.md#getting-movie-details-getmovie))

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
```

####Intialization

```
Yify yify = new Yify();
```

####Searching for movies (`ListMovies`)

#####Basic Search
* Search for movies where movie tile contains the word `Bourne`.
```
List<ListMovie> searchResult = yify.ListMovies("Bourne");
```

#####Advanced Search

* Search for movies where movie tile contains the word `Bourne`, `720p` quality and with minimum rating of 5.
```
List<ListMovie> result = y.ListMovies("Bourne", 
  quality: "720p", 
  minimumRating: 5);
```

######Sorting and Ordering search result

- To Sort the search results by a particular field use `ListMovieSort` enum.
- To Order the search result in ascending or descending order use `SortOrder` enum.

Search movies where movie tile contains the word `Bourne`, sorted by added date and orderd in ascending order.
```
List<ListMovie> result = y.ListMovies("Bourne", 
  sortBy: SearchResultSort.DateAdded, 
  orderBy: YifyLib.SortOrder.Asc);
```

####Getting movie (`GetMovie`)

* Getting movie details for movie 100.

```
Movie m = y.GetMovie(100);
```

# Coming Soon
* Examples for rest of the functionality.

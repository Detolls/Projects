using Newtonsoft.Json.Linq;
using RestSharp;
using Saritasa.OMDbScrubber.Domain.OMDb.Entities;
using System;
using System.Threading.Tasks;

namespace Saritasa.OMDbScrubber.Domain.OMDb.Services
{
    /// <summary>
    /// Class that contains the logic for getting data from OMDb API.
    /// </summary>
    public class OMDbService
    {
        /// <summary>
        /// Base OMDb url.
        /// </summary>
        private const string OMDbUrl = "http://www.omdbapi.com/";

        /// <summary>
        /// Client.
        /// </summary>
        private readonly RestClient client;

        /// <summary>
        /// API Key.
        /// </summary>
        private readonly string apiKey;

        /// <summary>
        /// Constructor of the <seealso cref="OMDbService"/> class.
        /// </summary>
        public OMDbService(string apiKey)
        {
            this.apiKey = apiKey ?? throw new ArgumentNullException("API Key can't be null.");

            client = new RestClient(OMDbUrl);
            client.UseJson();
        }

        /// <summary>
        /// Method to get movie from JSON of OMDb API async.
        /// </summary>
        /// <param name="movieId">Movie Id.</param>
        /// <returns>JSON data.</returns>
        public async Task<Movie> GetMovieFromJsonAsync(string movieId)
        {
            JObject jMovie = await GetMovieJsonDataAsync(movieId);

            var movie = new Movie
            {
                ImdbId = movieId,
                Title = jMovie.SelectToken("Title").Value<string>(),
                Genre = jMovie.SelectToken("Genre").Value<string>(),
                ReleaseDate = DateTime.TryParse(jMovie.SelectToken("Released").Value<string>(), out _) == true
                    ? jMovie.SelectToken("Released").Value<DateTime>()
                    : (DateTime?)null,
                RuntimeMins = int.Parse(jMovie.SelectToken("Runtime").Value<string>().Split(' ')[0]),
                ImdbRating = jMovie.SelectToken("imdbRating").Value<string>() != "N/A"
                    ? jMovie.SelectToken("imdbRating").Value<decimal>()
                    : 0,
                CreateAt = DateTime.Now
            };

            return movie;
        }

        /// <summary>
        /// Method to get movie actors names from JSON of OMDb API async.
        /// </summary>
        /// <param name="movieId">Movie Id.</param>
        /// <returns>Movie actors names.</returns>
        public async Task<string[]> GetMovieActorsNamesFromJsonAsync(string movieId)
        {
            JObject jMovie = await GetMovieJsonDataAsync(movieId);

            return jMovie.SelectToken("Actors").Value<string>().Split(',', ';');
        }

        /// <summary>
        /// Method to get movie from OMDb API in JSON async.
        /// </summary>
        /// <param name="movieId">Movie Id.</param>
        /// <returns>Movie JSON data.</returns>
        private async Task<JObject> GetMovieJsonDataAsync(string movieId)
        {
            var request = new RestRequest();
            request.AddParameter("i", movieId);
            request.AddParameter("apikey", apiKey);

            var response = await client.ExecuteGetTaskAsync(request);

            return JObject.Parse(response.Content);
        }
    }
}

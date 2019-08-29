using Microsoft.AspNetCore.Http;
using Saritasa.OMDbScrubber.Domain.OMDb.Entities;
using System.Collections.Generic;

namespace Saritasa.OMDbScrubber.Web.Models
{
    /// <summary>
    /// Movie model.
    /// </summary>
    public class MovieViewModel
    {
        /// <summary>
        /// Movie.
        /// </summary>
        public Movie Movie { get; set; }

        /// <summary>
        /// Movie actors.
        /// </summary>
        public List<Actor> Actors { get; set; } = new List<Actor>();
    }
}

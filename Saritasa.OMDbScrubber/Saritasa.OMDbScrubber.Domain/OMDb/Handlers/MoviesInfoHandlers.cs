using Saritasa.OMDbScrubber.Domain.OMDb.Commands;
using Saritasa.OMDbScrubber.Domain.OMDb.Repositories;
using Saritasa.Tools.Messages.Abstractions.Commands;
using System;
using System.Threading.Tasks;

namespace Saritasa.OMDbScrubber.Domain.OMDb.Handlers
{
    /// <summary>
    /// Import IMDb movies info command handlers.
    /// </summary>
    [CommandHandlers]
    public class MoviesInfoHandlers
    {
        /// <summary>
        /// Handle command async.
        /// </summary>
        public async Task HandleImportImdbMoviesInfoAsync(ImportImdbMoviesInfoCommand command, IMoviesService service)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            await service.UploadAsync(command.ImdbMovieIds);
        }
    }
}

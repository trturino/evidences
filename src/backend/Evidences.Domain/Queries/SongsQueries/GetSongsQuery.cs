using System.Collections.Generic;
using AzureFromTheTrenches.Commanding.Abstractions;
using Evidences.Domain.Models;

namespace Evidences.Domain.Queries.SongsQueries
{
    public class GetSongsQuery : ICommand<IEnumerable<Song>>
    {
    }
}
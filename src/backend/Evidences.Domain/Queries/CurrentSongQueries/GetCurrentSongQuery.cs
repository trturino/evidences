using System;
using System.Collections.Generic;
using System.Text;
using AzureFromTheTrenches.Commanding.Abstractions;
using Evidences.Domain.Models;

namespace Evidences.Domain.Queries.CurrentSongQueries
{
    public class GetCurrentSongQuery : ICommand<CurrentSong>
    {
    }
}

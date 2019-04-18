using System;
using System.Threading.Tasks;
using Evidences.Models;

namespace Evidences.Services
{
    public interface ISignalRService : IDisposable
    {
        Task Connect();

        Task Disconnect();

        bool IsConnected { get; }

        event EventHandler<Song> OnSongAdded;

        event EventHandler<CurrentSong> OnSongFinished;

        event EventHandler<CurrentSong> OnSongStarted;

        event EventHandler<SongToRemove> OnRemoveSong;
    }
}
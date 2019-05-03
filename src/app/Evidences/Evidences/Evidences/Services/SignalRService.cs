using System;
using System.Threading.Tasks;
using Evidences.Models;
using Evidences.Repositories;
using Microsoft.AspNetCore.SignalR.Client;
using Evidences.Models.Requests;

namespace Evidences.Services
{
    public class SignalRService : ISignalRService
    {
        private readonly IUserService _userService;
        private readonly ISignalRRepository _signalRRepository;
        private HubConnection _hubConnection;

        public SignalRService(
                IUserService userService,
                ISignalRRepository signalRRepository
            )
        {
            _userService = userService;
            _signalRRepository = signalRRepository;
        }

        public bool IsConnected { get; private set; }

        public event EventHandler<Song> OnSongAdded;

        public event EventHandler<CurrentSong> OnSongFinished;

        public event EventHandler<CurrentSong> OnSongStarted;

        public event EventHandler<SongToRemove> OnRemoveSong;

        public async Task Connect()
        {
            await CreateConnection();

            await _hubConnection.StartAsync();

            IsConnected = true;
        }

        public async Task Disconnect()
        {
            await _hubConnection.StopAsync();
        }

        private async Task CreateConnection()
        {
            var user = _userService.Get();
            if (user == null)
            {
                throw new InvalidOperationException("User not authenticated");
            }

            var credentials = await _signalRRepository.GetAuthInfo(new AuthInfoRequest { UserId = user.Id });

            _hubConnection = new HubConnectionBuilder()
                .WithUrl(credentials.Url,
                    options => options.AccessTokenProvider = () => Task.FromResult(credentials.AccessToken))
                .Build();

            SetUpEvents();
        }

        private void SetUpEvents()
        {
            _hubConnection.On<CurrentSong>("startSongCommandNotification", currentSong =>
            {
                OnSongStarted?.Invoke(this, currentSong);
            });

            _hubConnection.On<CurrentSong>("finishSongCommandNotification", currentSong =>
            {
                OnSongFinished?.Invoke(this, currentSong);
            });

            _hubConnection.On<Song>("addSongCommandNotification", song =>
            {
                OnSongAdded?.Invoke(this, song);
            });

            _hubConnection.On<SongToRemove>("removeSongCommandNotification", song =>
            {
                OnRemoveSong?.Invoke(this, song);
            });
        }

        private bool disposedValue = false;

        protected virtual async void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    OnSongStarted = null;
                    OnSongFinished = null;
                    OnSongAdded = null;
                    OnRemoveSong = null;
                }

                await _hubConnection.DisposeAsync();

                _hubConnection = null;

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}
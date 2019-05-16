// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NowPlayingViewModel.cs" company="ArcTouch LLC">
//   Copyright 2019 ArcTouch LLC.
//   All rights reserved.
//
//   This file, its contents, concepts, methods, behavior, and operation
//   (collectively the "Software") are protected by trade secret, patent,
//   and copyright laws. The use of the Software is governed by a license
//   agreement. Disclosure of the Software to third parties, in any form,
//   in whole or in part, is expressly prohibited except as authorized by
//   the license agreement.
// </copyright>
// <summary>
//   Defines the NowPlayingViewModel type.
// </summary>
//  --------------------------------------------------------------------------------------------------------------------
using System;
using System.Threading.Tasks;
using Evidences.Services;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;
using Evidences.Models;
using System.Windows.Input;
using System.Diagnostics;

namespace Evidences.ViewModel
{
    public class NowPlayingViewModel : BaseViewModel
    {
        private CurrentSong _currentSong;

        public CurrentSong CurrentSong
        {
            get
            {
                return _currentSong;
            }
            set
            {
                this._currentSong = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(AddedBy));
            }
        }

        public NowPlayingViewModel(IStateService stateService,
            IUserService userService,
            ISignalRService signaRService,
            IReactionService reactionService,
            IScoreService scoreService,
            INavigationService navigationService)
            : base(stateService, userService, signaRService, navigationService)
        {
            CloseCommand = new DelegateCommand(async () => await CloseExecute());
            Star1Command = new DelegateCommand(async () => await StarExecute(1));
            Star2Command = new DelegateCommand(async () => await StarExecute(2));
            Star3Command = new DelegateCommand(async () => await StarExecute(3));
            Star4Command = new DelegateCommand(async () => await StarExecute(4));
            Star5Command = new DelegateCommand(async () => await StarExecute(5));

            ReactionTappedCommand = new DelegateCommand<string>(async (x) => await SendReaction(x));
            ReactionService = reactionService;
            ScoreService = scoreService;
            RegisterSignalREvents();
        }

        protected override void SignalRService_OnSongFinished(object sender, CurrentSong e)
        {
            base.SignalRService_OnSongFinished(sender, e);

            CloseExecute();
        }


        public ICommand CloseCommand { get; }

        public Task CloseExecute()
            => NavigationService.GoBackAsync();

        public string AddedBy => $"Added by {CurrentSong?.AddedByUserName}";

        public bool Star1 { get; set; }
        public bool Star2 { get; set; }
        public bool Star3 { get; set; }
        public bool Star4 { get; set; }
        public bool Star5 { get; set; }

        public ICommand Star1Command { get; }
        public ICommand Star2Command { get; }
        public ICommand Star3Command { get; }
        public ICommand Star4Command { get; }
        public ICommand Star5Command { get; }

        public ICommand ReactionTappedCommand { get; }
        public IReactionService ReactionService { get; }
        public IScoreService ScoreService { get; }

        public async Task SendReaction(string reaction)
        {
            ReactionService.SendReaction(reaction);
        }

        public async Task StarExecute(int rate)
        {
            try
            {
                Star1 = (rate >= 1);
                Star2 = (rate >= 2);
                Star3 = (rate >= 3);
                Star4 = (rate >= 4);
                Star5 = (rate >= 5);

                await ScoreService.Add(CurrentSong.SongId, rate);
            }
            catch (Exception ex)
            {
                Debug.Write(ex);
            }
        }

        public override void OnNavigatingTo(INavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);
                
            if (parameters.TryGetValue("song", out CurrentSong song))
            {
                this.CurrentSong = song;
            }
        }
    }
}


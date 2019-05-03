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

namespace Evidences.ViewModel
{
    public class NowPlayingViewModel : BaseViewModel
    {
        public CurrentSong CurrentSong { get; set; }

        public NowPlayingViewModel(IStateService stateService,
            IUserService userService,
            ISignalRService signaRService,
            INavigationService navigationService)
            : base(stateService, userService, signaRService, navigationService)
        {
            CloseCommand = new DelegateCommand(async () => await CloseExecute());
            Star5Command = new DelegateCommand(async () => await Star5Execute());
        }


        public ICommand CloseCommand { get; }

        public Task CloseExecute()
            => NavigationService.GoBackAsync();

        public string AddedBy => $"Added by PedroK";

        public bool Star1 { get; set; }
        public bool Star2 { get; set; }
        public bool Star3 { get; set; }
        public bool Star4 { get; set; }
        public bool Star5 { get; set; }

        public ICommand Star5Command { get; }

        public async Task Star5Execute()
        {
            Star1 = true;
            Star2 = true;
            Star3 = true;
            Star4 = true;
            Star5 = true;
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


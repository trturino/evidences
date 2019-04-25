// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LoginViewModel.cs" company="ArcTouch LLC">
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
//   Defines the LoginViewModel type.
// </summary>
//  --------------------------------------------------------------------------------------------------------------------
using System;
using Evidences.Services;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;

namespace Evidences.ViewModel
{
    public class OnboardingViewModel : BaseViewModel
    {
        public OnboardingViewModel(IStateService stateService,
            IUserService userService,
            ISignalRService signaRService,
            INavigationService navigationService)
            : base(stateService, userService, signaRService, navigationService)
        {
            CreateUser = new DelegateCommand(CreateUserExecute);
        }

        public string UserName { get; set; }
        public DelegateCommand CreateUser { get; }

        private async void CreateUserExecute()
        {
            try
            {
                if (CurentUser == null)
                    await UserService.Add(UserName);

                await SignalRService.Connect();
            }
            catch (System.Exception ex)
            {

            }
        }

    }
}


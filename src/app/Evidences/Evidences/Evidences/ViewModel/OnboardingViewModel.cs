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
using System.Diagnostics;
using System.Threading.Tasks;
using Evidences.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Forms;

namespace Evidences.ViewModel
{
    public class OnboardingViewModel : BaseViewModel
    {
        protected readonly IPageDialogService PageDialogService;

        public OnboardingViewModel(IStateService stateService,
            IUserService userService,
            ISignalRService signaRService,
            INavigationService navigationService,
            IPageDialogService pageDialogService)
            : base(stateService, userService, signaRService, navigationService)
        {
            PageDialogService = pageDialogService;
            LetsSing = new DelegateCommand(async () => await LetsSingExecute()).ObservesCanExecute(() => IsNotBusy);
        }

        public string UserName { get; set; }
        public DelegateCommand LetsSing { get; }

        private async Task LetsSingExecute()
        {
            try
            {
                await ExecuteBusyAction(async () =>
                {
                    if (string.IsNullOrWhiteSpace(UserName))
                    {
                        await PageDialogService.DisplayAlertAsync("Really?", "Maybe you forgot something!", "Ok");
                        return;
                    }

                    if (CurentUser == null)
                        await UserService.Add(UserName);

                    await SignalRService.Connect();
                    await NavigationService.NavigateAsync("../Home");

                });
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

    }
}


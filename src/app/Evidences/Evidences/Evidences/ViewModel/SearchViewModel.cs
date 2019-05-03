// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SearchViewModel.cs" company="ArcTouch LLC">
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
//   Defines the SearchViewModel type.
// </summary>
//  --------------------------------------------------------------------------------------------------------------------
using System.Windows.Input;
using Evidences.Services;
using Prism.Commands;
using Prism.Navigation;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.ObjectModel;
using Evidences.YouTube;
using System.Linq;
using Evidences.Models;

namespace Evidences.ViewModel
{
    public class SearchViewModel : BaseViewModel
    {
        protected IYoutubeSearchService YoutubeSearchService { get; }
        protected ISongService SongService { get; }

        public SearchViewModel(
            IStateService stateService,
            IUserService userService,
            ISignalRService signaRService,
            INavigationService navigationService,
            ISongService songService,
            IYoutubeSearchService youtubeSearchService) :
             base(stateService, userService, signaRService, navigationService)
        {
            YoutubeSearchService = youtubeSearchService;
            SongService = songService;

            SearchCommand = new DelegateCommand(async () => await SearchExecute())
                .ObservesCanExecute(() => IsNotBusy);

            Songs = new ObservableCollection<VideoInformation>();
            AddItemCommand = new DelegateCommand<VideoInformation>(async (o) => await AddItemExecute(o))
                .ObservesCanExecute(() => IsNotBusy);

            CloseCommand = new DelegateCommand(async () => await CloseExecute());
            ClearCommand = new DelegateCommand(async () => await ClearExecute());
        }

        public ObservableCollection<VideoInformation> Songs { get; }

        public string SearchQuery { get; set; }

        public ICommand SearchCommand { get; }

        public ICommand CloseCommand { get; }

        public ICommand ClearCommand { get; }

        public bool IsClearVisible
        {
            get
            {
                if (SearchQuery != null)
                {
                    return SearchQuery.Length > 0;
                }

                return false;
            }
        }

        public ICommand AddItemCommand { get; }

        public async Task SearchExecute()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(SearchQuery))
                {
                    await ClearExecute();
                    return;
                }

                await ExecuteBusyAction(async () =>
                {
                    Songs.Clear();
                    var songs = await YoutubeSearchService.SearchVideo(SearchQuery, 1);
                    foreach (var song in songs)
                        Songs.Add(song);
                });
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public async Task CloseExecute()
        {
            try
            {
                await NavigationService.GoBackAsync();
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public async Task ClearExecute()
        {
            SearchQuery = null;
            Songs.Clear();
        }

        public async Task AddItemExecute(VideoInformation o)
        {
            try
            {
                if (o == null)
                {
                    return;
                }

                await ExecuteBusyAction(async () =>
                {
                    var song = new Song(o, UserService.Get().Id);
                    await SongService.Add(song);

                    await CloseExecute();
                });
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.TryGetValue("query", out string query))
            {
                SearchQuery = query;
                SearchCommand?.Execute(null);
            }
        }
    }
}

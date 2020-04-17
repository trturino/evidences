using System.Collections.Generic;
using System.Threading.Tasks;
using Evidences.YouTube;

namespace Evidences.Services
{
    public class YoutubeSearchService : IYoutubeSearchService
    {
        private readonly VideoSearch _youtubeSearch;

        public YoutubeSearchService(VideoSearch youtubeSearch)
        {
            _youtubeSearch = youtubeSearch;
        }

        public Task<IEnumerable<VideoInformation>> SearchVideo(string queryString, int queryPages, int queryPagesOffset = 0)
        {
            return _youtubeSearch.SearchQueryAsync(queryString, queryPages, queryPagesOffset);
        }
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using Evidences.YouTube;

namespace Evidences.Services
{
    public interface IYoutubeSearchService
    {
        Task<IEnumerable<VideoInformation>> SearchVideo(string querystring, int querypages, int querypagesOffset = 0);
    }
}
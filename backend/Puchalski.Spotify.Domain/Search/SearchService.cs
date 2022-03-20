using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Puchalski.Spotify.Domain.Configuration;
using System.Net;
using Newtonsoft.Json;

namespace Puchalski.Spotify.Domain.Search {
    public class SearchService : SpotifyDomainServiceBase, ISearchService {

        private readonly IConfiguration _configuration;

        public SearchService(IConfiguration configuration) : base(configuration) {
            _configuration = configuration;
        }

        public async Task<List<SearchItem>> SearchAsync(SearchRequest request) {
            List<SearchItem> result = new List<SearchItem>();

            if (string.IsNullOrEmpty(request.Text))
                throw new ArgumentNullException("Text is null");

            if (request.Text.Length < 3)
                throw new ArgumentOutOfRangeException("Text is too short");

            using (WebClient wc = new WebClient()) {
                wc.Headers[HttpRequestHeader.ContentType] = "application/json";
                wc.Headers[HttpRequestHeader.Accept] = "application/json";
                wc.Headers[HttpRequestHeader.Authorization] = "Bearer " + _apiKey?.access_token;
                var body = await wc.DownloadStringTaskAsync($"https://api.spotify.com/v1/search?q={request.Text}&type={request.Type.ToString().ToLower()}&limit=5");
                if (body != null) {
                    dynamic returnBody = JsonConvert.DeserializeObject(body);
                    IEnumerable<dynamic> items = request.Type == SearchRequestTypeEnum.Track ? (IEnumerable<dynamic>?)(returnBody?.tracks?.items) : (IEnumerable<dynamic>?)(returnBody?.artists?.items);
                    if (items == null) {
                        throw new ArgumentNullException("returnBody is null");
                    } else {
                        result = items?.Select(q => new SearchItem { Id = q.id, Name = q.name }).ToList();
                    }
                }
                return result;
            }
        }
    }
}

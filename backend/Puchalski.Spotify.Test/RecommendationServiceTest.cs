using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using Puchalski.Spotify.Domain.Search;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Puchalski.Spotify.Api.core.unitTest {
    public class RecommendationServiceTest {


        private IRecommendationService recommendationService;
        private ISearchService searchService;

        [SetUp]
        public void Setup() {
            var json = "{\"RecommendationApiName\": \"Spotify\", \"RecommendationApiSpotify\": { \"client_id\": \"996d0037680544c987287a9b0470fdbb\", \"client_secret\": \"5a3c92099a324b8f9e45d77e919fec13\"}}";
            var configuration = new ConfigurationBuilder().AddJsonStream(new MemoryStream(Encoding.ASCII.GetBytes(json))).Build();
            recommendationService = new RecommendationService(configuration);
            searchService = new SearchService(configuration);
        }

        [Test]
        async public Task is_SearchAsync_return_values() {
            var request = new SearchRequest();
            request.Text = "super";
            request.Type = SearchRequestTypeEnum.Track;
            var x = await searchService.SearchAsync(request);
            Assert.IsTrue(x.Count > 0);
        }

        [Test]
        async public Task is_SearchAsync_throw_exception() {
            var request = new SearchRequest();
            request.Text = null;
            request.Type = SearchRequestTypeEnum.Track;
            try {
                var x = await searchService.SearchAsync(request);
                Assert.Fail();
            } catch {

            }
        }

        [Test]
        async public Task is_GetRecommendationAsync_return_values_10() {
            var request = new RecommendationRequest();
            request.GenresName = new List<string> { "classic" };
            request.Limit = 10;
            request.Tracks = new List<string> { "0c6xIDDpzE81m2q797ordA" };
            request.Artists = new List<string> { "4NHQUGzhtTLFvgF5SZesLK" };
            request.Market = "IT";

            var x = await recommendationService.GetRecommendationAsync(request);
            Assert.IsTrue(x.Count == 10);
        }

        [Test]
        async public Task is_GetRecommendationAsync_return_values_100() {
            var request = new RecommendationRequest();
            request.GenresName = new List<string> { "classic" };
            request.Limit = 100;
            request.Tracks = new List<string> { "0c6xIDDpzE81m2q797ordA" };
            request.Artists = new List<string> { "4NHQUGzhtTLFvgF5SZesLK" };
            request.Market = "IT";

            var x = await recommendationService.GetRecommendationAsync(request);
            Assert.IsTrue(x.Count == 100);
        }

        [Test]
        async public Task is_GetRecommendationAsync_throw_exception() {
            var request = new RecommendationRequest();
            try {
                var x = await recommendationService.GetRecommendationAsync(request);
                Assert.Fail();
            } catch {

            }
        }
    }
}
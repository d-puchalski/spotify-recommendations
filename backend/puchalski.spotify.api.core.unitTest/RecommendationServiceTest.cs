using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using puchalski.model;
using puchalski.service;
using puchalski.spotify.api.Controllers;
using puchalski.spotify.api.core.externalApi;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace puchalski.spotify.api.core.unitTest {
    public class RecommendationServiceTest {


        private IRecommendationService service;
        private Mock<ILogger<IRecommendationService>> loggerServiceMock;

        [SetUp]
        public void Setup() {
            loggerServiceMock = new Mock<ILogger<IRecommendationService>>();
            var json = "{\"RecommendationApiName\": \"Spotify\", \"RecommendationApiSpotify\": { \"client_id\": \"996d0037680544c987287a9b0470fdbb\", \"client_secret\": \"5a3c92099a324b8f9e45d77e919fec13\"}}";
            var configuration = new ConfigurationBuilder().AddJsonStream(new MemoryStream(Encoding.ASCII.GetBytes(json))).Build();
            service = new RecommendationService(loggerServiceMock.Object, configuration);
        }

        [Test]
        async public Task is_SearchAsync_return_values() {
            var request = new SearchRequest();
            request.Text = "super";
            request.Type = SearchRequestTypeEnum.track;
            var x = await service.SearchAsync(request);
            Assert.IsTrue(x.Tracks.Items.Count > 0);
        }

        [Test]
        async public Task is_SearchAsync_throw_exception() {
            var request = new SearchRequest();
            request.Text = null;
            request.Type = SearchRequestTypeEnum.track;
            try {
                var x = await service.SearchAsync(request);
                Assert.Fail();
            } catch {

            }
        }

        [Test]
        async public Task is_GetRecommendationAsync_return_values_10() {
            var request = new GetRecommendationRequest();
            request.GenresName = new List<string> { "classic" };
            request.Limit = 10;
            request.Tracks = new List<string> { "0c6xIDDpzE81m2q797ordA" };
            request.Artists = new List<string> { "4NHQUGzhtTLFvgF5SZesLK" };
            request.Market = "IT";

            var x = await service.GetRecommendationAsync(request);
            Assert.IsTrue(x.Tracks?.Count == 10);
        }

        [Test]
        async public Task is_GetRecommendationAsync_return_values_100() {
            var request = new GetRecommendationRequest();
            request.GenresName = new List<string> { "classic" };
            request.Limit = 100;
            request.Tracks = new List<string> { "0c6xIDDpzE81m2q797ordA" };
            request.Artists = new List<string> { "4NHQUGzhtTLFvgF5SZesLK" };
            request.Market = "IT";

            var x = await service.GetRecommendationAsync(request);
            Assert.IsTrue(x.Tracks?.Count == 100);
        }

        [Test]
        async public Task is_GetRecommendationAsync_throw_exception() {
            var request = new GetRecommendationRequest();
            try {
                var x = await service.GetRecommendationAsync(request);
                Assert.Fail();
            } catch {

            }
        }
    }
}
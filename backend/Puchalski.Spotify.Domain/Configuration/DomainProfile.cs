using AutoMapper;
using Puchalski.Spotify.Domain.Recommendation;
using Puchalski.Spotify.Domain.Search;
using ext = Puchalski.Spotify.ExternalApi.Models;

namespace Puchalski.Spotify.Application.Configuration {
    public class DomainProfile : Profile {
        public DomainProfile() {
            CreateMap<RecommendationRequest, ext.RecommendationRequest>();
            CreateMap<SearchRequest, ext.SearchRequest>();

            CreateMap<ext.RecommendationResponse, RecommendationItem>();
            CreateMap<ext.SearchResponse, SearchItem>();
        }
    }
}

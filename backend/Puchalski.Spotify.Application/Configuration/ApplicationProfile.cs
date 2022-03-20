using AutoMapper;
using Puchalski.Spotify.Application.GetSearch;
using Puchalski.Spotify.Application.PostRecommendation;
using Puchalski.Spotify.Domain.Recommendation;
using Puchalski.Spotify.Domain.Search;

namespace Puchalski.Spotify.Application.Configuration {
    public class ApplicationProfile : Profile {
        public ApplicationProfile() {
            CreateMap<RecommendationItem, RecommendationItemDto>();
            CreateMap<SearchItem, SearchItemDto>();
        }
    }
}

using AutoMapper;
using Puchalski.Spotify.Domain.Search;
using Puchalski.Spotify.Integration.GetSearch;
using Puchalski.Spotify.Integration.PostRecommendation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puchalski.Spotify.Integration.Configuration {
    public class AutoMapperProfile : Profile {
        public AutoMapperProfile() {
            CreateMap<RecommentationItem, RecommendationItemDto>();
            CreateMap<SearchItem, SearchItemDto>();
        }
    }
}

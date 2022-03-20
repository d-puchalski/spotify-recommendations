using System;
using System.Collections.Generic;
using System.Linq;
using MediatR;
using System.Text;
using System.Threading.Tasks;

namespace Puchalski.Spotify.Application.GetSearch {
    public class GetSearchQuery : IRequest<List<SearchItemDto>> {
        public GetSearchQuery(string text, TypeSearchEnum typeSearch) {
            Text = text;
            TypeSearch = typeSearch;
        }

        public string Text { get; set; }

        public TypeSearchEnum TypeSearch { get; set; }
    }
}

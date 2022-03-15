using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace puchalski.model {


    public partial class GetRecommendationResponse {
        public List<Tracks> Tracks { get; set; }
        public List<Seed> Seeds { get; set; }
    }

    public partial class Seed {
        public long? InitialPoolSize { get; set; }
        public long? AfterFilteringSize { get; set; }
        public long? AfterRelinkingSize { get; set; }
        public string Id { get; set; }
        public string Type { get; set; }
        public Uri Href { get; set; }
    }

    public partial class Album {
        public AlbumType? AlbumType { get; set; }
        public List<LinkedFrom> Artists { get; set; }
        public ExternalUrls ExternalUrls { get; set; }
        public Uri Href { get; set; }
        public string Id { get; set; }
        public List<Image> Images { get; set; }
        public string Name { get; set; }
        public DateTimeOffset? ReleaseDate { get; set; }
        public ReleaseDatePrecision? ReleaseDatePrecision { get; set; }
        public long? TotalTracks { get; set; }
        public AlbumTypeEnum? Type { get; set; }
        public string Uri { get; set; }
    }

    public partial class LinkedFrom {
        public ExternalUrls ExternalUrls { get; set; }
        public Uri Href { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public LinkedFromType? Type { get; set; }
        public string Uri { get; set; }
    }

    public partial class ExternalIds {
        public string Isrc { get; set; }
    }

    public enum AlbumType { Album, Single };

    public enum LinkedFromType { Artist, Track };

}


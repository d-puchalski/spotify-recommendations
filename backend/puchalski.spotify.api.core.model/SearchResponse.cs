namespace puchalski.model {

    public partial class SearchResponse {
        public Artists Artists { get; set; }

        public Tracks Tracks { get; set; }
    }
    public partial class Tracks {
        public Uri Href { get; set; }
        public List<Item> Items { get; set; }
        public long Limit { get; set; }
        public Uri Next { get; set; }
        public long Offset { get; set; }
        public object Previous { get; set; }
        public long Total { get; set; }
        public Album Album { get; set; }
        public List<LinkedFrom> Artists { get; set; }
        public long? DiscNumber { get; set; }
        public long? DurationMs { get; set; }
        public bool? Explicit { get; set; }
        public ExternalIds ExternalIds { get; set; }
        public ExternalUrls ExternalUrls { get; set; }
        public string Id { get; set; }
        public bool? IsLocal { get; set; }
        public bool? IsPlayable { get; set; }
        public string Name { get; set; }
        public long? Popularity { get; set; }
        public Uri PreviewUrl { get; set; }
        public long? TrackNumber { get; set; }
        public string? Type { get; set; }
        public string Uri { get; set; }
        public LinkedFrom LinkedFrom { get; set; }
    }
    public partial class Artists {
        public Uri Href { get; set; }
        public List<Item> Items { get; set; }
        public long Limit { get; set; }
        public Uri Next { get; set; }
        public long Offset { get; set; }
        public object Previous { get; set; }
        public long Total { get; set; }
    }

    public partial class Item {
        public ExternalUrls ExternalUrls { get; set; }
        public Followers Followers { get; set; }
        public List<string> Genres { get; set; }
        public Uri Href { get; set; }
        public string Id { get; set; }
        public List<Image> Images { get; set; }
        public string Name { get; set; }
        public long Popularity { get; set; }
        public string Type { get; set; }
        public string Uri { get; set; }
    }

    public partial class ExternalUrls {
        public Uri Spotify { get; set; }
    }

    public partial class Followers {
        public object Href { get; set; }
        public long Total { get; set; }
    }

    public partial class Image {
        public long Height { get; set; }
        public Uri Url { get; set; }
        public long Width { get; set; }
    }

    public partial class Welcome {
        public Tracks Tracks { get; set; }
    }




    public enum AlbumTypeEnum { Album, Compilation, Single };

    public enum ArtistType { Artist };

    public enum ReleaseDatePrecision { Day };

    public enum ItemType { Track };


}

namespace puchalski.api.core {
    public static class ApiException {
        public static readonly Exception GetSearchException = new Exception("GetSearchException");
        public static readonly Exception CreateAccessTokenException = new Exception("CreateAccessTokenException");
        public static readonly Exception GetRecommendationException = new Exception("GetRecommendationException");
        public static readonly Exception GetRecommendationRequestException = new Exception("GetRecommendationRequestException");


    }
}

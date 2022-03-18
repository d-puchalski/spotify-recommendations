
// @ts-ignore
const apiUrl = API;

export function GetRecommendationsApi(currentformStore, currentArtistStore, currentTrackStore): Promise<any> {
  return new Promise(async (resolve, reject) => {
    var dataRequest = {
      limit: Number(currentformStore.limit),
      market: currentformStore.market,
      genresName: currentformStore.genres,
      artists: currentArtistStore,
      tracks: currentTrackStore,
    };

    fetch(`${apiUrl}/GetRecommendation`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        "Accept": "application/json",
      },
      body: JSON.stringify(dataRequest),
    })
      .then((response) => response.json())
      .then((data) => resolve(data))
      .catch((error) => {
        reject(error);
      });
  });
};

export function SearchApi (dataRequest): Promise<any> {
  return new Promise(async (resolve, reject) => {
    fetch(`${apiUrl}/Search`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        "Accept": "application/json",
      },
      body: JSON.stringify(dataRequest),
    })
      .then((response) => response.json())
      .then((data) => resolve(data))
      .catch((error) => {
        reject(error);
      });
  });
};

import { recommendationStore, trackStore, artistStore  } from "../services/store.js";
const apiUrl = API;

export const GetRecommendationsApi = function (currentformStore, currentArtistStore, currentTrackStore) {
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
        Accept: "application/json",
      },
      body: JSON.stringify(dataRequest),
    })
      .then((response) => response.json())
      .then((data) => {
        recommendationStore.set(data);
        resolve(true);
      })
      .catch((error) => {
        reject(error);
      });
  });
};

export const SearchApi = function (dataRequest) {
  return new Promise(async (resolve, reject) => {
    fetch(`${apiUrl}/Search`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Accept: "application/json",
      },
      body: JSON.stringify(dataRequest),
    })
      .then((response) => response.json())
      .then((data) => {
        if (dataRequest.type == 1) {
          trackStore.set(data);
        } else if (dataRequest.type == 2) {
          artistStore.set(data);
        }
        resolve(true);
      })
      .catch((error) => {
        reject(error);
      });
  });
};

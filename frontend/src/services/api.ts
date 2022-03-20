import { RecommentationItemDto } from "../models/RecommentationItemDto";
import { RecommentationRequestDto } from "../models/RecommentationRequestDto";
import { SearchItemDto } from "../models/SearchItemDto";
import { SearchItemRequestDto } from "../models/SearchItemRequestDto";

// @ts-ignore
const apiUrl = API;

export async function GetRecommendationsApi(currentformStore, currentArtistStore, currentTrackStore): Promise<Array<RecommentationItemDto>> {
  return new Promise(async (resolve, reject) => {
    var dataRequest: RecommentationRequestDto = {
      limit: Number(currentformStore.limit),
      market: currentformStore.market,
      genresName: currentformStore.genres,
      artists: currentArtistStore,
      tracks: currentTrackStore,
    };

    fetch(`${apiUrl}/api/recommendation`, {
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

export async function SearchApi(dataRequest: SearchItemRequestDto): Promise<Array<SearchItemDto>> {
  return new Promise(async (resolve, reject) => {
    fetch(`${apiUrl}/api/search/${dataRequest.text}/${dataRequest.type}`, {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
        "Accept": "application/json",
      }
    })
      .then((response) => response.json())
      .then((data) => resolve(data))
      .catch((error) => {
        reject(error);
      });
  });
};

import { readable, writable } from "svelte/store";
export let formStore = writable({
  market: "",
  limit: "",
  genres: [],
});

export let recommendationStore = writable({});
export let artistSelectedStore = writable([]);
export let trackSelectedStore = writable([]);

export let trackStore = writable({
  tracks: {
    items: [
      {
        name: null,
        id: null,
        images: [
          {
            href: null,
          },
        ],
      },
    ],
  },
});
export let artistStore = writable({
  artists: {
    items: [
      {
        name: null,
        id: null,
        images: [
          {
            href: null,
          },
        ],
      },
    ],
  },
});

export let initStore = readable({
  market: [
    { id: "", text: "---" },
    { id: "IT", text: "Italy" },
    { id: "ES", text: "Spain" },
    { id: "FR", text: "France" },
  ],
  limit: [
    { id: "", text: "---" },
    { id: "10", text: "10" },
    { id: "15", text: "15" },
    { id: "20", text: "20" },
  ],
  genre: [
    { id: "Country", text: "Classical" },
    { id: "Country", text: "Country" },
    { id: "Electronic", text: "Electronic" },
    { id: "Hiphop", text: "Hip-hop" },
    { id: "Indierock", text: "Indie rock" },
    { id: "Jazz", text: "Jazz" },
    { id: "Kpop", text: "K-pop" },
    { id: "Metal", text: "Metal" },
    { id: "Oldies", text: "Oldies" },
    { id: "Pop", text: "Pop" },
    { id: "Rap", text: "Rap" },
    { id: "RB", text: "R&B" },
    { id: "Rock", text: "Rock" },
    { id: "Techno", text: "Techno" },
  ],
});
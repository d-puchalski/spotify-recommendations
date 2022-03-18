<script lang="ts">
  import * as animateScroll from "svelte-scrollto";
  import * as api from "../services/api";
  import { globalStore, initStore } from "../services/store.js";
  import { IGlobalStore } from "../interfaces/IGlobalStore";
  import Result from "./result.svelte";
  import { get } from "svelte/store";
  import { onDestroy } from "svelte";

  let selectedArtists: Array<string> = [];
  let selectedTracks: Array<string> = [];
  let _globalStore: IGlobalStore = null;
  let suggestions: Array<any> = [];

  $: isFindable = selectedArtists && selectedArtists.length > 0 && selectedTracks && selectedTracks.length > 0 && _globalStore.limit && _globalStore.genres && _globalStore.genres.length > 0 && _globalStore.market;

  async function findSuggestion(): Promise<void> {
    suggestions = (await api.GetRecommendationsApi(get(globalStore), selectedArtists, selectedTracks)).tracks;
    animateScroll.scrollToBottom();
  }

  async function selectArtist(event): Promise<void> {
    if (selectedArtists.length > 1 && event.currentTarget.checked) {
      event.preventDefault();
    }
    if (event.currentTarget.checked) {
      if (!selectedArtists.find((q) => q == event.currentTarget.value)) {
        selectedArtists = [...selectedArtists.slice(-1), event.currentTarget.value];
      }
    } else {
      selectedArtists = selectedTracks.filter((q) => q != event.currentTarget.value);
    }
  }

  async function selectTrack(event): Promise<void> {
    if (selectedTracks.length > 1 && event.currentTarget.checked) {
      event.preventDefault();
    }
    if (event.currentTarget.checked) {
      if (!selectedTracks.find((q) => q == event.currentTarget.value)) {
        selectedTracks = [...selectedTracks.slice(-1), event.currentTarget.value];
      }
    } else {
      selectedTracks = selectedTracks.filter((q) => q != event.currentTarget.value);
    }
  }

  async function searchArtist(event): Promise<void> {
    if (event.currentTarget.value.length > 2) {
      globalStore.update((x: IGlobalStore) => {
        api.SearchApi({ text: event.currentTarget.value, type: 2 }).then((result) => {
          x.artists = result.artists;
        });
        return x;
      });
    }
  }

  async function searchTrack(event): Promise<void> {
    if (event.currentTarget.value.length > 2) {
      globalStore.update((x: IGlobalStore) => {
        api.SearchApi({ text: event.currentTarget.value, type: 1 }).then((result) => {
          x.tracks = result.tracks;
        });
        return x;
      });
    }
  }

  async function genresClick(event): Promise<void> {
    globalStore.update((s: IGlobalStore) => {
      if (s.genres.length > 1 && event.currentTarget.checked) {
        event.preventDefault();
        return s;
      }
      if (event.currentTarget.checked) {
        if (!s.genres.find((q) => q == event.currentTarget.value)) {
          s.genres = [...s.genres, event.currentTarget.value];
        }
      } else {
        s.genres = s.genres.filter((q) => q != event.currentTarget.value);
      }
      return s;
    });
  }

  const u = globalStore.subscribe((x) => {
    _globalStore = x;
  });

  onDestroy(u);
</script>

<section class="py-5">
  <div class="container px-5">
    <div class="bg-light rounded-3 py-5 px-4 px-md-5 mb-5">
      <div class="text-center mb-5">
        <div class="feature bg-primary bg-gradient text-white rounded-3 mb-3"><i class="bi bi-spotify" /></div>
        <h1 class="fw-bolder">Spotify recommendations</h1>
      </div>
      <div class="row gx-5 justify-content-center">
        <div class="col-lg-8 col-xl-6">
          <div class="form-floating mb-3">
            <input class="form-control" id="artist" placeholder="Search artists" on:keyup={searchArtist} />
            <label for="artist">Artists *</label>
            {#if _globalStore?.artists?.items && _globalStore?.artists?.items.length > 0}
              <div class="row">
                {#each _globalStore?.artists?.items as x}
                  {#if x.id && x.name}
                    <div class="col-4">
                      <div class="form-check form-switch">
                        <input class="form-check-input" type="checkbox" id="{x.id}Check" value={x.id} on:click={selectArtist} />
                        <label class="form-check-label" for="{x.id}Check">{x.name}</label>
                      </div>
                    </div>
                  {/if}
                {/each}
              </div>
            {/if}
          </div>
          <div class="form-floating mb-3">
            <input class="form-control" id="track" placeholder="Search tracks" on:keyup={searchTrack} />
            <label for="artist">Tracks *</label>
            {#if _globalStore?.tracks?.items && _globalStore?.tracks?.items.length > 0}
              <div class="row">
                {#each _globalStore?.tracks?.items as x}
                  {#if x.id && x.name}
                    <div class="col-4">
                      <div class="form-check form-switch">
                        <input class="form-check-input" type="checkbox" id="{x.id}Check" value={x.id} on:click={selectTrack} />
                        <label class="form-check-label" for="{x.id}Check">{x.name}</label>
                      </div>
                    </div>
                  {/if}
                {/each}
              </div>
            {/if}
          </div>
          <div class="form-floating mb-3">
            <select class="form-select form-select-lg mb-3" id="market" aria-label=".form-select-lg example" bind:value={_globalStore.market}>
              {#each $initStore.market as x}
                <option value={x.id}>
                  {x.text}
                </option>
              {/each}
            </select>
            <label for="market">Market *</label>
          </div>

          <div class="form-floating mb-3">
            <div class="row">
              {#each $initStore.genre as x}
                <div class="col-4">
                  <div class="form-check form-switch">
                    <input class="form-check-input" type="checkbox" id="{x.text}Check" value={x.text} on:click={genresClick} />
                    <label class="form-check-label" for="{x.text}Check">{x.text}</label>
                  </div>
                </div>
              {/each}
            </div>
          </div>

          <div class="form-floating mb-3">
            <select class="form-select form-select-lg mb-3" id="limit" aria-label=".form-select-lg example" bind:value={_globalStore.limit}>
              {#each $initStore.limit as y}
                <option value={y.id}>
                  {y.text}
                </option>
              {/each}
            </select>
            <label for="limit">How many recommendations you like to see? *</label>
          </div>
          <div class="d-grid"><button class="btn btn-primary btn-lg" id="submitButton" disabled={!isFindable} on:click={findSuggestion}>Find</button></div>
        </div>
      </div>
    </div>
  </div>
</section>
<Result items={suggestions} />

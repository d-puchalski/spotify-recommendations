<script lang="ts">
  import * as animateScroll from "svelte-scrollto";
  import * as api from "../services/api";
  import { initStore } from "../services/store.js";
  import { GlobalStore } from "../models/GlobalStore";
  import Result from "./result.svelte";
  import { SearchItemDto } from "../models/SearchItemDto";
  import { RecommentationItemDto } from "../models/RecommentationItemDto";
  import { SearchItemRequestDto } from "../models/SearchItemRequestDto";
  import debounce from "lodash/debounce";

  let selectedArtists: Array<SearchItemDto> = [];
  let selectedTracks: Array<SearchItemDto> = [];
  let onChange: GlobalStore = new GlobalStore();
  let suggestions: Array<RecommentationItemDto> = [];
  $: isFindable = selectedArtists && selectedArtists.length > 0 && selectedTracks && selectedTracks.length > 0 && onChange.limit && onChange.genres && onChange.genres.length > 0 && onChange.market;

  async function findSuggestion(): Promise<void> {
    suggestions = await api.GetRecommendationsApi(onChange, selectedArtists, selectedTracks);
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

  const searchArtist = debounce(async function (event) {
    if (event.target.value && event.target.value.length > 2) {
      const request: SearchItemRequestDto = new SearchItemRequestDto(event.target.value, 2);
      onChange.artists = await api.SearchApi(request);
    }
  }, 300);

  const searchTrack = debounce(async (event) => {
    if (event.target.value && event.target.value.length > 2) {
      const request: SearchItemRequestDto = new SearchItemRequestDto(event.target.value, 1);
      onChange.tracks = await api.SearchApi(request);
    }
  }, 300);

  async function genresClick(event): Promise<void> {
    if (onChange.genres.length > 1 && event.currentTarget.checked) {
      event.preventDefault();
      return;
    }
    if (event.currentTarget.checked) {
      if (!onChange.genres.find((q) => q == event.currentTarget.value)) {
        onChange.genres = [...onChange.genres, event.currentTarget.value];
      }
    } else {
      onChange.genres = onChange.genres.filter((q) => q != event.currentTarget.value);
    }
  }
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
            {#if onChange?.artists && onChange?.artists.length > 0}
              <div class="row">
                {#each onChange?.artists as x}
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
            {#if onChange?.tracks && onChange?.tracks?.length > 0}
              <div class="row">
                {#each onChange?.tracks as x}
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
            <select class="form-select form-select-lg mb-3" id="market" aria-label=".form-select-lg example" bind:value={onChange.market}>
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
            <select class="form-select form-select-lg mb-3" id="limit" aria-label=".form-select-lg example" bind:value={onChange.limit}>
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

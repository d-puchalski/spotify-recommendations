<script>
  import { onDestroy, onMount } from "svelte";
  import * as animateScroll from "svelte-scrollto";
  import * as api from "../services/api";
  import { formStore, initStore, trackStore, artistStore, artistSelectedStore, trackSelectedStore } from "../services/store.js";
  import Result from "./result.svelte";

  let currentformStore;
  let currentArtistStore;
  let currentTrackStore;
  let isFindableProp = false;

  onMount(async () => {
    artistStore.set(null);
    trackStore.set(null);
  });

  async function genresClick(event) {
    formStore.update((s) => {
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
      console.log(s);
      return s;
    });
  }

  async function find() {
    await api.GetRecommendationsApi(currentformStore, currentArtistStore, currentTrackStore);
    animateScroll.scrollToBottom();
  }

  async function selectArtist(event) {
    artistSelectedStore.update((a) => {
      if (a.length > 1 && event.currentTarget.checked) {
        event.preventDefault();
        return s;
      }
      if (event.currentTarget.checked) {
        if (!a.find((q) => q == event.currentTarget.value)) {
          a = [...a.slice(-1), event.currentTarget.value];
        }
      } else {
        a = a.filter((q) => q != event.currentTarget.value);
      }
      return a;
    });
  }

  async function selectTrack(event) {
    trackSelectedStore.update((a) => {
      if (a.length > 1 && event.currentTarget.checked) {
        event.preventDefault();
        return s;
      }
      if (event.currentTarget.checked) {
        if (!a.find((q) => q == event.currentTarget.value)) {
          a = [...a.slice(-1), event.currentTarget.value];
        }
      } else {
        a = a.filter((q) => q != event.currentTarget.value);
      }
      return a;
    });
  }

  async function searchArtist(event) {
    artistStore.set(null);
    trackSelectedStore.set([]);
    if (event.currentTarget.value.length > 2) {
      await searchItems(event.currentTarget.value, 2);
    }
  }

  async function searchTrack(event) {
    trackStore.set(null);
    trackSelectedStore.set([]);
    if (event.currentTarget.value.length > 2) {
      await searchItems(event.currentTarget.value, 1);
    }
  }

  async function searchItems(q, type) {
    await api.SearchApi({
      text: q,
      type: type,
    });
  }

  function isFindable() {
    isFindableProp = currentformStore && currentformStore.market && currentformStore.limit && currentformStore.genres && currentArtistStore && currentTrackStore;
  }

  let currentformStoreObs = formStore.subscribe((s) => {
    currentformStore = s;
    isFindable();
  });
  let artistSelectedStoreObs = artistSelectedStore.subscribe((s) => {
    currentArtistStore = s;
    isFindable();
  });
  let trackSelectedStoreObs = trackSelectedStore.subscribe((s) => {
    currentTrackStore = s;
    isFindable();
  });

  onDestroy(currentformStoreObs);
  onDestroy(artistSelectedStoreObs);
  onDestroy(trackSelectedStoreObs);
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
            <input class="form-control" id="artist" placeholder="Search artists" on:keypress={searchArtist} />
            <label for="artist">Artists *</label>
            {#if $artistStore?.artists?.items}
              <div class="row">
                {#each $artistStore?.artists?.items as x}
                  <div class="col-4">
                    <div class="form-check form-switch">
                      <input class="form-check-input" type="checkbox" id="{x.id}Check" value={x.id} on:click={selectArtist} />
                      <label class="form-check-label" for="{x.id}Check">{x.name}</label>
                    </div>
                  </div>
                {/each}
              </div>
            {/if}
          </div>
          <div class="form-floating mb-3">
            <input class="form-control" id="track" placeholder="Search tracks" on:keypress={searchTrack} />
            <label for="artist">Tracks *</label>
            {#if $trackStore?.tracks?.items}
              <div class="row">
                {#each $trackStore?.tracks?.items as x}
                  <div class="col-4">
                    <div class="form-check form-switch">
                      <input class="form-check-input" type="checkbox" id="{x.id}Check" value={x.id} on:click={selectTrack} />
                      <label class="form-check-label" for="{x.id}Check">{x.name}</label>
                    </div>
                  </div>
                {/each}
              </div>
            {/if}
          </div>
          <div class="form-floating mb-3">
            <select class="form-select form-select-lg mb-3" id="market" aria-label=".form-select-lg example" bind:value={$formStore.market}>
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
            <select class="form-select form-select-lg mb-3" id="limit" aria-label=".form-select-lg example" bind:value={$formStore.limit}>
              {#each $initStore.limit as y}
                <option value={y.id}>
                  {y.text}
                </option>
              {/each}
            </select>
            <label for="limit">How many recommendations you like to see? *</label>
          </div>
          <div class="d-grid"><button class="btn btn-primary btn-lg" id="submitButton" disabled={!isFindableProp} on:click={find}>Find</button></div>
        </div>
      </div>
    </div>
  </div>
</section>
<Result />

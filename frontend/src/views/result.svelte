<script lang="ts">
  export let items: Array<any>;
</script>

{#if items && items.length > 0}
  <section>
    <div class="container px-5">
      <table class="table table-striped table-dark">
        <tbody>
          {#each items as x}
            <tr>
              {#if x.album?.images && x.album?.images.length > 0 && x.album?.images[0]}
                <td><img src={x.album.images[0].url} alt="" /></td>
              {/if}
              <td>{x.name}</td>
            </tr>
          {/each}
        </tbody>
      </table>
    </div>
  </section>
{/if}

<style>
  img {
    width: 100px;
  }
</style>

<script lang="ts">
import { RecommentationItemDto } from "../models/RecommentationItemDto";


  export let items: Array<RecommentationItemDto>;
</script>

{#if items && items.length > 0}
  <section>
    <div class="container px-5">
      <table class="table table-striped table-dark">
        <tbody>
          {#each items as x}
            <tr>
              {#if x.urlImage}
                <td><img src={x.urlImage} alt="" /></td>
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

<template>
  <AppTopNavBar />
  <v-main style="padding: 2em;">
    <v-container max-width="100%" height="100%">
      <v-row style="min-height: 100%;">
        <v-col cols="3">
          <AppSideMenu />
        </v-col>
        <v-col cols="9">
          <div class="bodyInfo">
            <router-view />
          </div>
        </v-col>
      </v-row>
    </v-container>
  </v-main>

  <AppFooter />
</template>

<script lang="ts" setup>
  //
</script>

<style lang="scss">
.bodyInfo {
  height: 100%;

  background-color: #FaFafa;
  box-shadow: 5px 5px rgba(0, 0, 0, 0.2);;

  border-radius: 0.7rem;

}
</style>

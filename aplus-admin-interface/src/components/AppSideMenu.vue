<template>
  <div class="menuItems">
    <v-btn class="menuBtn" variant="plain"  @click="handleHomeRoute">Home</v-btn>
    <v-btn class="menuBtn" variant="plain" @click="handleStudentRoute">Alunos</v-btn>
  </div>
</template>

<script setup lang="ts">
import router from "@/router";

function handleStudentRoute() {
  router.push("/alunos");
}

function handleHomeRoute() {
    router.push("/")
}
</script>

<style lang="scss" scoped>
.menuItems {
    display: flex;
    flex-direction: column;
    gap: 0.5em;
    padding: 1em;

    height: 100%;
    width: 50%;

    border-radius: 0.7rem;

    background: linear-gradient(180deg, rgba(1,195,203,255) 0%, rgba(1,176,182,255) 50%, rgba(2,156,162,255));
}
</style>

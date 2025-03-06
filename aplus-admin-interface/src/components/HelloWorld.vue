<template>
  <div class="textWrapper">
    <img
      ref="test"
      class="testImg red"
      src="../assets/adorno-plataforma_a.svg"
    />
    <h3>Bem vindo(a)!</h3>
    <h2>Este é o módulo acadêmico do Grupo +A!</h2>

    <img ref="test" class="testImg blue" src="../assets/adorno-artmed.svg" />
  </div>
</template>

<script setup lang="ts">
//
</script>

<style lang="scss" scoped>
.textWrapper {
  display: flex;
  justify-content: center;
  flex-direction: column;
  align-items: center;
  padding: 1em;
  gap: 1em;

  .testImg {
    width: 300px;
    height: 300px;

    padding: 1em;
  }

  .testImg.red {
    align-self: flex-end;
  }

  .testImg.blue {
    align-self: flex-start;
  }
}
</style>

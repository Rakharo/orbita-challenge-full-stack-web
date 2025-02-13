<template>
  <v-container>
    <v-data-table
      v-model:options="options"
      :headers="headers"
      :items="studentList"
      :items-per-page="5"
      class="elevation-1"
      @update:page="handleGetStudents"
    />
  </v-container>
</template>

<script setup lang="ts">
import { getAllStudents } from "@/services/studentService";

import type { iStudent } from "@/interfaces/studentInterface";

const studentList = ref<iStudent[]>([]);

const headers = ref([
  { title: "RA", key: "ra" },
  { title: "Nome", key: "name" },
  { title: "CPF", key: "cpf" },
  { title: "E-mail", key: "email" },
]);

const options = ref({
  page: 1, // Initial page
  itemsPerPage: 5,
});

async function handleGetStudents(page: number) {
  const response = await getAllStudents(page, options.value.itemsPerPage);
  if (response) {
    studentList.value = response;
  }
}

onMounted(() => {
  handleGetStudents(1);
});
</script>

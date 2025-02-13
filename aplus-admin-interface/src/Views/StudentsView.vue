<template>
  <div class="createBtn">
    <v-btn prepend-icon="mdi-account-plus" color="#cd233e">Adicionar aluno</v-btn>
  </div>
  <v-container max-width="100%">
    <v-data-table
      v-model:options="options"
      :headers="headers"
      :items="studentList"
      :items-per-page="5"
      :hover="true"
      class="elevation-1"
      @update:page="handleGetStudents"
    >
      <template #item.actions="{ item }">
        <v-tooltip text="Editar">
          <template v-slot:activator="{ props }">
            <v-btn v-bind="props" variant="text" icon="$edit" @click="editStudent(item)" color="primary"></v-btn>
          </template>
        </v-tooltip>
        <v-tooltip text="Remover">
          <template v-slot:activator="{ props }">
            <v-btn v-bind="props"  variant="text" icon="mdi-delete" @click="deleteStudent(item)" color="orange"></v-btn>
          </template>
        </v-tooltip>
      </template>
    </v-data-table>
  </v-container>
</template>

<script setup lang="ts">
import { getAllStudents } from "@/services/studentService";
import type { iStudent } from "@/interfaces/studentInterface";

const studentList = ref<iStudent[]>([]);

const headers = ref([
  { title: "RA", value: "ra", align: 'center', sortable: true },
  { title: "Nome", value: "name", align: 'center', sortable: true },
  { title: "CPF", value: "cpf", align: 'center', sortable: true },
  { title: "Ações", value: "actions", align: 'center', sortable: false }
]);

const options = ref({
  page: 1, // Initial page
  itemsPerPage: 5,
});

function editStudent(item: iStudent) {
    console.log(item)
}

function deleteStudent(item: iStudent) {
    console.log(item)
}

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

<style lang="scss">
.createBtn {
  display: flex;
  justify-content: end;
  padding: 1em;
}
.elevation-1 {
  border-radius: 0.7rem !important;
}

</style>

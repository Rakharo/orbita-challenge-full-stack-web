<template>
  <div class="createBtn">
    <v-btn
      prepend-icon="mdi-account-plus"
      color="#cd233e"
      text="Adicionar Aluno"
      @click="handleStudentModal"
    />
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
            <v-btn
              v-bind="props"
              variant="text"
              icon="$edit"
              @click="handleEditBtn(item)"
              color="primary"
            />
          </template>
        </v-tooltip>
        <v-tooltip text="Remover">
          <template v-slot:activator="{ props }">
            <v-btn
              v-bind="props"
              variant="text"
              icon="mdi-delete"
              @click="handleDeleteBtn(item)"
              color="orange"
            />
          </template>
        </v-tooltip>
      </template>
    </v-data-table>
  </v-container>

  <StudentFormModal
    :key="componentKey"
    :model-value="studentModal"
    :is-edit="isEdit"
    :student="selectedStudent"
    @close-modal="handleCloseModal"
  />
</template>

<script setup lang="ts">
import StudentFormModal from "@/components/Students/StudentFormModal.vue";
import { getAllStudents } from "@/services/studentService";
import type { iStudent } from "@/interfaces/studentInterface";

const componentKey = ref(0);

const studentList = ref<iStudent[]>([]);
const studentModal = ref<boolean>(false);
const isEdit = ref<boolean>(false);
const selectedStudent = ref<iStudent>({
  ra: undefined,
  name: "",
  email: "",
  cpf: "",
});

const headers = ref([
  { title: "RA", value: "ra", align: "center", sortable: true },
  { title: "Nome", value: "name", align: "center", sortable: true },
  { title: "CPF", value: "cpf", align: "center", sortable: true },
  { title: "Ações", value: "actions", align: "center", sortable: false },
]);

const options = ref({
  page: 1, // Initial page
  itemsPerPage: 5,
});

function handleStudentModal() {
  console.log("aaa");
  studentModal.value = true;
}

function handleCloseModal() {
  studentModal.value = false;
  selectedStudent.value = {
    ra: undefined,
    name: "",
    email: "",
    cpf: "",
  };
  componentKey.value++;

  if (isEdit.value === true) {
    isEdit.value = false;
  }
}

function handleEditBtn(item: iStudent) {
  isEdit.value = true;
  selectedStudent.value = item;
  studentModal.value = true;
  componentKey.value++;
  console.log(selectedStudent.value);
}

function handleDeleteBtn(item: iStudent) {
  selectedStudent.value = item;
  console.log(item);
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

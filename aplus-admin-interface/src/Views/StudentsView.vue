<template>
  <div class="createBtn">
    <v-chip prepend-icon="mdi-filter" variant="text" @click="handleShowFilter">
      Filtro
    </v-chip>
    <v-btn
      prepend-icon="mdi-account-plus"
      color="#cd233e"
      text="Adicionar Aluno"
      @click="handleStudentModal"
    />
  </div>
  <div v-if="showFilter" class="filterWrapper">
    <v-form>
      <v-row>
        <v-col>
          <v-text-field
            v-model="filter.ra"
            label="Registro acadêmico (RA)"
            @change="handleListFilter"
          />
        </v-col>
        <v-col>
          <v-text-field
            v-model="filter.name"
            label="Nome"
            @change="handleListFilter"
          />
        </v-col>
        <v-col>
          <v-text-field
            v-model="filter.cpf"
            label="CPF"
            @change="handleListFilter"
          />
        </v-col>
      </v-row>
    </v-form>
  </div>
  <v-container max-width="100%">
    <v-data-table
      :key="componentKey"
      v-model:options="options"
      class="elevation-1"
      :headers="headers"
      :items="filteredList"
      :items-per-page="options.itemsPerPage"
      :server-items-length="totalItems"
      :hover="true"
      :page="page"
      @update:options="handleTableOptions"
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
    @submit-form="handleCreateStudent"
  />

  <WarningModal
    :model-value="warningModal"
    :modal-text="`Deseja excluir o(a) aluno(a) ${selectedStudent.name}?`"
    @close-modal="warningModal = false"
    @submit="handleDeleteStudent"
  />
</template>

<script setup lang="ts">
import StudentFormModal from "@/components/Students/StudentFormModal.vue";
import WarningModal from "@/components/global/WarningModal.vue";
import {
  createNewStudent,
  deleteStudent,
  editStudent,
  getAllStudents,
} from "@/services/studentService";
import type { iStudent } from "@/interfaces/studentInterface";

const componentKey = ref(0);

const studentList = ref<iStudent[]>([]);
const filteredList = ref<iStudent[]>([]);
const studentModal = ref<boolean>(false);
const warningModal = ref<boolean>(false);
const showFilter = ref<boolean>(false);
const isEdit = ref<boolean>(false);
const page = ref(1);
const cache = new Map();
const totalItems = ref(0);
const filter = ref({
  name: "",
  ra: "",
  cpf: "",
});
const selectedStudent = ref<iStudent>({
  ra: "",
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
  itemsPerPage: 10,
});

function handleTableOptions(newOptions: any) {
  // Verifique se o valor da página mudou
  if (newOptions.page !== page.value) {
    page.value = newOptions.page;
    handleGetStudents(page.value);
  }
}

function handleStudentModal() {
  studentModal.value = true;
}

function handleCloseModal() {
  studentModal.value = false;
  selectedStudent.value = {
    ra: "",
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
  warningModal.value = true;
  console.log(item);
}

function handleShowFilter() {
  showFilter.value = !showFilter.value;
}

function handleListFilter() {
  filteredList.value = studentList.value.filter((item) => {
    if (filter.value.ra && !item.ra.includes(filter.value.ra)) {
      return false;
    }

    if (
      filter.value.name &&
      !item.name.toLowerCase().includes(filter.value.name.toLowerCase())
    ) {
      return false;
    }

    if (filter.value.cpf && !item.cpf.includes(filter.value.cpf)) {
      return false;
    }

    return true;
  });
}

async function handleGetStudents(newPage: number) {
  const response = await getAllStudents(newPage, options.value.itemsPerPage);
  if (response && response.students) {
    componentKey.value++;
    cache.set(newPage, response.students);
    studentList.value = response.students;
    filteredList.value = response.students;
    totalItems.value = response.totalCount; // Atualize o número total de itens da API
  }
  page.value = newPage; // Atualize a página
}

async function handleCreateStudent(student: iStudent) {
  if (isEdit.value === true) {
    handleEditStudent(student);
  } else {
    const response = await createNewStudent(student);
    if (response) {
      componentKey.value++;
      handleCloseModal();
      handleGetStudents(1);
    }
  }
}

async function handleEditStudent(student: iStudent) {
  console.log("edita");
  const response = await editStudent(student.ra, student);
  if (response) {
    componentKey.value++;
    studentModal.value = false;
    handleGetStudents(1);
    handleCloseModal();
  }
}

async function handleDeleteStudent() {
  const response = await deleteStudent(selectedStudent.value.ra);
  if (response) {
    handleGetStudents(1);
    componentKey.value++;
    warningModal.value = false;
  }
}

watch(totalItems, (newTotal) => {
  console.log("Total de itens atualizado para: ", newTotal);
  if (newTotal <= options.value.itemsPerPage) {
    options.value.page = 1; // Reseta para a primeira página se os itens forem menores que o número de itens por página
  }
});
watch(page, (newPage) => {
  console.log("Página atualizada para:", newPage);
  handleGetStudents(newPage);
});
watch(totalItems, () => {
  componentKey.value++;
});

onMounted(() => {
  handleGetStudents(1);
});
</script>

<style lang="scss">
.filterWrapper {
  padding: 1em;
}
.createBtn {
  display: flex;
  justify-content: space-between;
  padding: 1em;
}
.elevation-1 {
  border-radius: 0.7rem !important;
}
</style>

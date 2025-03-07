<template>
  <div class="createBtn">
    <v-chip
      prepend-icon="mdi-filter"
      variant="text"
      @click="handleShowFilter"
    >
      Filtro
    </v-chip>
    <v-btn
      prepend-icon="mdi-account-plus"
      color="#cd233e"
      text="Adicionar Aluno"
      @click="handleStudentModal"
    />
  </div>

  <div
    v-if="showFilter"
    class="filterWrapper"
  >
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
      :hover="true"
      hide-default-footer
    >
      <template #item.actions="{ item }">
        <v-tooltip text="Editar">
          <template v-slot:activator="{ props }">
            <v-btn
              v-bind="props"
              variant="text"
              icon="$edit"
              color="primary"
              @click="handleEditBtn(item)"
            />
          </template>
        </v-tooltip>
        <v-tooltip text="Remover">
          <template v-slot:activator="{ props }">
            <v-btn
              v-bind="props"
              variant="text"
              icon="mdi-delete"
              color="orange"
              @click="handleDeleteBtn(item)"
            />
          </template>
        </v-tooltip>
      </template>
    </v-data-table>

    <v-pagination
      v-model="page"
      :length="totalPages"
      @update:model-value="handleGetStudents"
    />
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
import { createNewStudent, deleteStudent, editStudent, getAllStudents } from "@/services/studentService";
import type { iStudent } from "@/interfaces/studentInterface";
import { ref, computed, onMounted, watch } from "vue";

const componentKey = ref(0);

const studentList = ref<iStudent[]>([]);
const filteredList = ref<iStudent[]>([]);
const studentModal = ref<boolean>(false);
const warningModal = ref<boolean>(false);
const showFilter = ref<boolean>(false);
const isEdit = ref<boolean>(false);
const totalItems = ref(0);
const page = ref(1);
const itemsPerPage = ref(10);
const filter = ref({ name: "", ra: "", cpf: "" });

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
  page: 1
});

// Computa o total de páginas baseado no número total de itens
const totalPages = computed(() => Math.ceil(totalItems.value / itemsPerPage.value));

function handleStudentModal() {
  studentModal.value = true;
}

function handleCloseModal() {
  studentModal.value = false;
  selectedStudent.value = { ra: "", name: "", email: "", cpf: "" };
  componentKey.value++;
  if (isEdit.value) isEdit.value = false;
}

function handleEditBtn(item: iStudent) {
  isEdit.value = true;
  selectedStudent.value = item;
  studentModal.value = true;
  componentKey.value++;
}

function handleDeleteBtn(item: iStudent) {
  selectedStudent.value = item;
  warningModal.value = true;
}

function handleShowFilter() {
  showFilter.value = !showFilter.value;
}

function handleListFilter() {
  filteredList.value = studentList.value.filter((item) => {
    if (filter.value.ra && !item.ra.includes(filter.value.ra)) return false;
    if (filter.value.name && !item.name.toLowerCase().includes(filter.value.name.toLowerCase())) return false;
    if (filter.value.cpf && !item.cpf.includes(filter.value.cpf)) return false;
    return true;
  });
}

async function handleGetStudents() {
  const response = await getAllStudents(page.value, itemsPerPage.value);
  if (response && response.students) {
    studentList.value = response.students;
    filteredList.value = response.students;
    totalItems.value = response.totalCount;
  }
}

async function handleCreateStudent(student: iStudent) {
  if (isEdit.value) {
    handleEditStudent(student);
  } else {
    const response = await createNewStudent(student);
    if (response) {
      handleCloseModal();
      handleGetStudents();
    }
  }
}

async function handleEditStudent(student: iStudent) {
  const response = await editStudent(student.ra, student);
  if (response) {
    handleGetStudents();
    handleCloseModal();
  }
}

async function handleDeleteStudent() {
  const response = await deleteStudent(selectedStudent.value.ra);
  if (response) {
    handleGetStudents();
    warningModal.value = false;
  }
}

watch(page, () => {
  handleGetStudents();
});

onMounted(() => {
  handleGetStudents();
});
</script>

<style lang="scss" scoped>
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

<template>
  <v-dialog v-model="props.modelValue" width="50%">
    <v-card
      class="studentModal"
      :title="props.isEdit ? 'Atualizar cadastro' : 'Cadastro de aluno'"
      :prepend-icon="props.isEdit ? 'mdi-account-settings' : 'mdi-account-plus'"
    >
      <v-form ref="form" class="studentForm">
        <v-text-field
          v-model="studentForm.ra"
          label="Registro acadêmico"
          :disabled="props.isEdit ? true : false"
        />
        <v-text-field v-model="studentForm.name" label="Nome do aluno" />
        <v-text-field v-model="studentForm.email" label="E-mail" />
        <v-text-field
          v-model="studentForm.cpf"
          label="CPF"
          :disabled="props.isEdit ? true : false"
        />
      </v-form>
      <template v-slot:actions>
        <div class="btnWrapper">
          <v-btn
            class="ms-auto"
            text="Cancelar"
            color="gray"
            variant="outlined"
            @click="handleCancel"
          />
          <v-btn
            class="ms-auto"
            :text="props.isEdit ? 'Atualizar' : 'Cadastrar'"
            color="#01aab0"
            variant="tonal"
            @click="handleSubmitForm"
          />
        </div>
      </template>
    </v-card>
  </v-dialog>
</template>

<script setup lang="ts">
import type { iStudent } from "@/interfaces/studentInterface";

interface InterfaceProps {
  modelValue: boolean;
  isEdit: boolean;
  student: iStudent;
}

const props = defineProps<InterfaceProps>();
const emit = defineEmits(["closeModal", "submitForm"]);

const form = ref();
const studentForm = ref<iStudent>(
  !props.student
    ? {
        ra: "",
        name: "",
        email: "",
        cpf: "",
      }
    : props.student
);
function handleCancel() {
  emit("closeModal");
}

function handleSubmitForm() {
  emit("submitForm", studentForm.value);
}
</script>

<style lang="scss">
.studentModal {
  padding: 1em !important;

  .studentForm {
    width: 80%;
    align-self: center;
  }

  .btnWrapper {
    display: flex;
    gap: 1em;
  }
}
</style>

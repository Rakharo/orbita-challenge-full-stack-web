<template>
  <v-dialog v-model="props.modelValue" width="50%">
    <v-card
      class="studentModal"
      :title="props.isEdit ? 'Atualizar cadastro' : 'Cadastro de aluno'"
      :prepend-icon="props.isEdit ? 'mdi-account-settings' : 'mdi-account-plus'"
    >
      <v-form ref="form" class="studentForm" @submit.prevent="handleSubmitForm">
        <v-text-field
          v-model="studentForm.ra"
          label="Registro acadêmico"
          :disabled="props.isEdit"
          :rules="[requiredRule]"
          type="number"
        />
        <v-text-field v-model="studentForm.name" label="Nome do aluno" :rules="[requiredRule]" />
        <v-text-field v-model="studentForm.email" label="E-mail" :rules="[requiredRule, emailRule]" />
        <v-text-field
          v-model="studentForm.cpf"
          label="CPF"
          :disabled="props.isEdit"
          :rules="[requiredRule, cpfRule]"
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
  props.student || { ra: "", name: "", email: "", cpf: "" }
);

const requiredRule = (value: string) => !!value || "Campo obrigatório";
const emailRule = (value: string) =>
  /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/.test(value) ||
  "E-mail inválido";
const cpfRule = (value: string) =>
  /^\d{3}\.\d{3}\.\d{3}-\d{2}$/.test(value) || "CPF inválido";

function handleCancel() {
  emit("closeModal");
}

async function handleSubmitForm() {
  const { valid } = await form.value.validate();
  if (valid) {
    emit("submitForm", studentForm.value);
  }
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

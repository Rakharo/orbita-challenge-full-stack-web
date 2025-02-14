<template>
  <v-dialog v-model="props.modelValue" width="auto">
    <v-card
      class="warningModal"
      title="Atenção!"
      prepend-icon="mdi-alert-circle"
    >
      <v-alert :text="props.modalText" />
      <template v-slot:actions>
        <div class="btnWrapper">
          <v-btn
            class="ms-auto"
            :text="props.cancelBtnText ? props.cancelBtnText : 'Cancelar'"
            color="gray"
            variant="outlined"
            @click="handleCancel"
          />
          <v-btn
            class="ms-auto"
            :text="props.confirmBtnText ? props.confirmBtnText : 'Confirmar'"
            color="#01aab0"
            variant="tonal"
            @click="handleSubmit"
          />
        </div>
      </template>
    </v-card>
  </v-dialog>
</template>

<script setup lang="ts">
interface InterfaceProps {
  modelValue: boolean;
  modalText: string;
  cancelBtnText?: string;
  confirmBtnText?: string;
}

const props = defineProps<InterfaceProps>();
const emit = defineEmits(["closeModal", "submit"]);

function handleCancel() {
  emit("closeModal");
}

function handleSubmit() {
  emit("submit");
}
</script>

<style lang="scss">
.btnWrapper {
  display: flex;
  gap: 1em;
}
</style>

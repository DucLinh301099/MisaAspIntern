<template>
  <div class="combobox-account-input-wrapper" :class="{ disabled: disabled }">
    <label for="combobox-label">
      {{ label }} <span class="required" v-if="isRequired">*</span>
    </label>
    <div class="input-container">
      <div
        class="input-with-button"
        :class="{
          focus: isInputFocused,
          'has-error': errors && errors.length > 0,
        }"
      >
        <MSInput
          class="base-input"
          :value="inputValue"
          :comboboxError="errors && errors.length > 0"
          :noBorder="true"
          :disabled="disabled"
          @input="handleOnInput"
          @focus="handleFocus"
          @blur="handleBlur"
          ref="inputComponent"
        />
        <button
          v-if="showButton && !disabled"
          @click="openCreateModal"
          class="add-button"
        >
          +
        </button>
        <multiselect
          :options="filteredOptions"
          :searchable="true"
          :close-on-select="true"
          placeholder=""
          class="multiselect"
          :disabled="disabled"
          @open="onExpandCombox"
          @close="showTable = false"
          @focusin="handleFocus"
          @focusout="handleBlur"
        />
      </div>
    </div>
    <transition name="dropdown">
      <div v-show="showTable" class="dropdown-table-wrapper">
        <table class="dropdown-table">
          <thead>
            <tr>
              <th v-for="(column, index) in columnConfig" :key="index">
                {{ column.columnName }}
              </th>
            </tr>
          </thead>
          <tbody>
            <tr
              v-for="(item, index) in filteredOptions"
              :key="index"
              @click="selectRow(item)"
            >
              <td v-for="(column, colIndex) in columnConfig" :key="colIndex">
                {{ item[column.fieldName] }}
              </td>
            </tr>
          </tbody>
        </table>
      </div>
    </transition>
    <Modal :visible="isCreateModalVisible" @close="closeCreateModal">
      <component
        :is="ComponentAdd"
        @submit="handleSubmitModal"
        @close="closeCreateModal"
        @afterCallSuccess="handleSubmitModal"
      />
    </Modal>
  </div>
</template>

<script>
import Multiselect from "vue-multiselect";
import "vue-multiselect/dist/vue-multiselect.css";
import Modal from "../Base/Modal.vue";
import { baseApi } from "../../api/baseApi";

import BaseSubmit from "../Base/BaseSubmit.vue";

export default {
  name: "MSCombobox",
  extends: BaseSubmit,
  components: {
    Multiselect,
    Modal,
  },
  props: {
    selectedRow: {
      type: Object,
      default: null,
    },
    label: {
      type: String,
      default: null,
    },
    field: {
      type: String,
      default: null,
    },
    selectedOption: {
      type: Object,
      default: null,
    },
    showButton: {
      type: Boolean,
      default: true,
    },
    isRequired: {
      type: Boolean,
      default: false,
    },
    config: {
      type: Object,
      required: true,
    },
    ComponentAdd: {
      type: Object,
      default: null,
    },
    value: {
      type: Object,
      default: null,
    },
    disabled: {
      type: Boolean,
      default: false,
    },
  },
  data() {
    return {
      inputValue: "",
      secondInputValue: "",
      showTable: false,
      internalSelectedOption: this.selectedOption,
      isInputFocused: false,
      isCreateModalVisible: false,
      optionsData: this.options != null ? this.options : [],
      formData: {},
      errors: null,
    };
  },
  watch: {
    selectedOption(newVal) {
      this.internalSelectedOption = newVal;
    },
    internalSelectedOption(newVal) {
      this.$emit("update:selectedOption", newVal);
    },
    errors(newVal) {
      let refList = this.$refs;
      if (refList["inputComponent"]) {
        refList["inputComponent"].setError(newVal);
      }
    },
  },
  computed: {
    endpoint() {
      return this.config.endpoint;
    },
    filteredOptions() {
      if (!Array.isArray(this.optionsData)) {
        return [];
      }
      if (typeof this.inputValue !== "string" || this.inputValue === "") {
        return this.optionsData;
      }
      let displayField = this.config.columnConfig?.find(
        (col) => col.isDisplay
      )?.fieldName;
      return this.optionsData.filter((option) =>
        option[displayField]
          ?.toLowerCase()
          .includes(this.inputValue.toLowerCase())
      );
    },
    columnConfig() {
      return this.config.columnConfig || [];
    },
  },
  methods: {
    async fetchData() {
      if (!this.config || !this.config.endpoint) {
        return;
      }
      try {
        baseApi.buildUrlRequest(this.config);
        const response = await baseApi.getAuthenApi(this.config.url);
        this.optionsData = this.extractData(response);
      } catch (error) {
        this.optionsData = [];
      }
    },
    extractData(response) {
      if (response.data) {
        if (Array.isArray(response.data)) {
          return response.data;
        } else if (Array.isArray(response.data.data)) {
          return response.data.data;
        }
      }
      return [];
    },
    async onExpandCombox() {
      await this.fetchData();
      this.showTable = true;
    },
    selectRow(item) {
      let displayFirstValue = this.config.columnConfig?.find(
        (col) => col.isDisplay
      )?.fieldName;
      if (displayFirstValue) {
        this.inputValue = item[displayFirstValue];
      }
      let displaySecondValue = this.config.columnConfig?.find(
        (col) => col.isDisplaySecond
      )?.fieldName;
      if (displaySecondValue) {
        this.secondInputValue = item[displaySecondValue];
      }
      this.$emit("update:selectedRow", item);
      this.showTable = false;
      this.errors = null;
      this.$refs.inputComponent.setError(null);
    },
    handleOnInput(event) {
      this.inputValue = event.target.value;
    },
    handleInputChange(value) {
      this.internalSelectedOption = value;
    },
    openCreateModal() {
      this.isCreateModalVisible = true;
    },
    closeCreateModal() {
      this.isCreateModalVisible = false;
    },
    handleSubmitModal(responseData) {
      if (responseData && responseData.isSuccess) {
        this.$emit("createSubmit", responseData);
        this.closeCreateModal();
        this.showAlert(responseData.message, () => {});
      }
    },
    handleFocus() {
      this.isInputFocused = true;
    },
    handleBlur() {
      this.isInputFocused = false;
    },
    setError(item) {
      this.errors = item;
    },
  },
};
</script>

<style scoped>
.combobox-account-input-wrapper {
  display: flex;
  flex-direction: column;
  margin-bottom: 8px;
  width: 45%;
}
.input-with-button.focus {
  border-color: green;
}
.input-with-button.has-error {
  border: 1px solid #f85050;
}
label {
  margin-bottom: 5px;
  font-weight: bold;
}
.combobox-account-input-wrapper.disabled .input-with-button {
  background-color: #d3d3d3;
  cursor: not-allowed;
}

.combobox-account-input-wrapper.disabled .base-input,
.combobox-account-input-wrapper.disabled .multiselect__input {
  background-color: #d3d3d3;
  cursor: not-allowed;
}

.combobox-account-input-wrapper.disabled .multiselect__tags {
  background-color: #d3d3d3;
  cursor: not-allowed;
}

.input-container {
  display: flex;
}

.input-with-button {
  display: flex;
  align-items: center;
  border: 1px solid #999;
  border-radius: 2px;
  overflow: hidden;
  flex-grow: 2;
  height: 24.8px;
  position: relative;
}

.focus {
  border-color: green;
}

.input-wrapper {
  display: flex;
  justify-content: center;
  align-items: center;
}

.base-input {
  width: 100%;
  height: 28px;
  border: none;
  box-sizing: border-box;
  outline: none;
  display: flex;
  align-items: center;
}

.add-button {
  display: flex;
  justify-content: center;
  align-items: center;
  width: 40px;
  height: 40px;
  max-width: 40px;
  border: none;
  background-color: white;
  cursor: pointer;
  font-size: 24px;
  color: green;
  margin-left: auto;
}

.multiselect {
  width: 33px;
  border: none;
  border-left: 1px solid #999;
  margin-left: auto;
}
.multiselect .multiselect__option--highlight {
  background-color: #b0b0b0; /* Màu xám đậm hơn */
}
.dropdown-table-wrapper {
  position: absolute;
  z-index: 400;
  background-color: white;
  width: 50%;
  margin-top: 65px;
  max-height: 130px;
  overflow-y: auto;
}

.dropdown-table {
  width: 100%;
  border-collapse: collapse;
  border: 1px solid #808080;
}

.dropdown-table th,
.dropdown-table td {
  padding: 8px;
  text-align: left;
}

.dropdown-table th {
  background-color: #f2f2f2;
  font-weight: bold;
}

.dropdown-table tr:hover {
  background-color: #68c75b;
}

.dropdown-enter-active,
.dropdown-leave-active {
  transition: opacity 0.25s ease-in-out;
}

.dropdown-enter,
.dropdown-leave-to {
  opacity: 0;
}
</style>

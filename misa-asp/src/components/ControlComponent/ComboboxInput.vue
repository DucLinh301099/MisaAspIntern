<template>
  <div class="combobox-account-input-wrapper">
    <label for="combobox-label">
      {{ label }} <span class="required" v-if="isRequired">*</span>
    </label>
    <div class="input-container">
      <div class="input-with-button" :class="{ focus: isInputFocused }">
        <BaseInput
          class="base-input"
          :type="type"
          :value="inputValue"
          @input="handleOnInput"
          @focus="handleFocus"
          @blur="handleBlur"
        />

        <button v-if="showButton" @click="openCreateModal" class="add-button">
          +
        </button>

        <multiselect
          :options="filteredOptions"
          :searchable="true"
          :close-on-select="true"
          placeholder=""
          class="multiselect"
          @open="onExpandCombox"
          @close="showTable = false"
          @focus="handleFocus"
          @blur="handleBlur"
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
        @submit="handleCreateSubmit"
        @close="closeCreateModal"
      />
    </Modal>
  </div>
</template>

<script>
import BaseInput from "../BaseComponent/BaseInputComponent.vue";
import Multiselect from "vue-multiselect";
import "vue-multiselect/dist/vue-multiselect.css";
import Modal from "../BaseComponent/Modal.vue";
import { apiClient } from "../../api/base";

export default {
  name: "ComboboxInput",
  components: {
    BaseInput,
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
    };
  },
  watch: {
    selectedOption(newVal) {
      this.internalSelectedOption = newVal;
    },
    internalSelectedOption(newVal) {
      this.$emit("update:selectedOption", newVal);
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
        this.buildUrlRequest(this.config);
        const response = await apiClient[this.config.method](this.config.url);
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
    },
    handleOnInput(event) {
      this.inputValue = event.target.value;
    },
    handleInputChange(value) {
      this.internalSelectedOption = value;
    },
    buildUrlRequest(config) {
      config.url = `${config.endpoint}`;
      if (config.params) {
        if (config.method.toLowerCase() == "get") {
          let urlParam = Object.entries(config.params)
            .map(([key, value]) => `${key}=${value}`)
            .join("&&");

          config.url = urlParam
            ? `${config.endpoint}?${urlParam}`
            : `${config.endpoint}`;
        } else {
          config.body = config.params;
        }
      }
    },
    openCreateModal() {
      this.isCreateModalVisible = true;
    },
    closeCreateModal() {
      this.isCreateModalVisible = false;
    },
    handleCreateSubmit(formData) {
      this.$emit("createSubmit", formData);
      this.closeCreateModal();
    },
    handleFocus() {
      this.isInputFocused = true;
    },
    handleBlur() {
      this.isInputFocused = false;
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

label {
  margin-bottom: 8px;
  font-weight: bold;
}

.input-container {
  display: flex;
  align-items: center;
}

.input-with-button {
  display: flex;
  align-items: center;
  border: 1px solid #999;
  border-radius: 2px;
  overflow: hidden;
  flex-grow: 2;
  height: 30px;
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
  height: 30px; /* Bạn có thể thay đổi giá trị này nếu cần */
  border: none;
  padding: 0 8px;
  box-sizing: border-box;
}

.base-input {
  border: none;
  padding: 8px;
  box-sizing: border-box;
  height: 30px;
  outline: none;
}

.add-button {
  display: flex;
  justify-content: center;
  align-items: center;
  width: 40px;
  height: 40px;
  border: none;
  background-color: white;
  cursor: pointer;
  font-size: 24px;
  color: green;
}

.multiselect {
  width: 40px;
  border: none;
  border-left: 1px solid #999;
}

.second-input {
  border: 1px solid #999;
  border-radius: 2px;
  padding: 8px;
  box-sizing: border-box;
  height: 33px;
  flex-grow: 1;
  width: 50%;
  margin-left: 15px;
}
.second-input:focus {
  border-color: 1px solid #68c75b;
}
.second-input-e {
  border-radius: 2px;
  padding: 8px;
  box-sizing: border-box;
  height: 37px;
  flex-grow: 1;
  width: 50%;
  margin-left: 15px;
}

.dropdown-table-wrapper {
  position: absolute;
  z-index: 500;
  background-color: white;
  width: 60%;
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

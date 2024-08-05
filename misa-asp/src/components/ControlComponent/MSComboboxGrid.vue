<template>
  <div class="combobox-grid">
    <div class="input-container">
      <div
        class="input-with-button"
        :class="{
          invalid: isInputFocused && !inputValue,
          valid: inputValue && isInputFocused,
          'has-error': errors && errors.length > 0,
        }"
      >
        <MSInput
          :value="inputValue"
          :noBorder="true"
          :field="field"
          @input="updateInputValue"
          @focus="handleFocus"
          @blur="handleBlur"
          :comboboxGridError="errors && errors.length > 0"
          class="base-input"
          :disabled="disabled"
          ref="inputComponent"
        />
        <div
          class="input-status-container"
          v-if="isInputFocused && !inputValue"
        >
          <span class="input-status">!</span>
        </div>
        <multiselect
          :style="{ visibility: isMultiselectVisible ? 'visible' : 'hidden' }"
          :options="filteredOptions"
          :searchable="true"
          :close-on-select="true"
          placeholder=""
          class="multiselect"
          @open="onExpandCombox"
          @close="showTable = false"
          :disabled="disabled"
        />
      </div>
    </div>

    <transition name="dropdown">
      <div v-show="showTable" class="dropdown-table-wrapper">
        <table class="dropdown-table">
          <thead>
            <tr>
              <th v-for="(column, index) in config.columnConfig" :key="index">
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
              <td
                v-for="(column, colIndex) in config.columnConfig"
                :key="colIndex"
              >
                {{ item[column.fieldName] }}
              </td>
            </tr>
          </tbody>
        </table>
      </div>
    </transition>
  </div>
</template>

<script>
import Multiselect from "vue-multiselect";
import "vue-multiselect/dist/vue-multiselect.css";
import { baseApi } from "../../api/baseApi";
import BaseSubmit from "../Base/BaseSubmit.vue";

export default {
  name: "MSComboboxGrid",
  components: {
    Multiselect,
  },
  extends: BaseSubmit,
  props: {
    selectedRow: {
      type: Object,
      default: null,
    },
    disabled: {
      type: Boolean,
      default: false,
    },
    value: {
      type: Object,
      default: null,
    },
    config: {
      type: Object,
      required: true,
    },
    field: {
      type: String,
      default: null,
    },
  },
  data() {
    return {
      inputValue: "",
      showTable: false,
      isInputFocused: false,
      isMultiselectVisible: false,
      optionsData: this.config.options != null ? this.config.options : [],
      optionSelected: false,
      errors: null,
    };
  },
  computed: {
    filteredOptions() {
      let columnConfig = this.config.columnConfig;
      if (this.inputValue === "") {
        return this.optionsData;
      }

      let displayField = columnConfig.find(
        (col) => col.isDisplayGrid
      )?.fieldName;

      return this.optionsData.filter((option) =>
        option[displayField]
          ?.toLowerCase()
          .includes(this.inputValue.toLowerCase())
      );
    },
  },
  watch: {
    errors(newVal) {
      let refList = this.$refs;
      if (refList["inputComponent"]) {
        refList["inputComponent"].setError(newVal);
      }
    },
  },
  methods: {
    async fetchData() {
      if (!this.config || !this.config.endpoint) {
        return;
      }
      try {
        const response = await baseApi.getAuthenApi(this.config.endpoint);
        if (response.data && Array.isArray(response.data)) {
          this.optionsData = response.data;
        } else if (
          response.data &&
          response.data.data &&
          Array.isArray(response.data.data)
        ) {
          this.optionsData = response.data.data;
        } else {
          this.optionsData = [];
        }
      } catch (error) {
        this.optionsData = [];
      }
    },

    async onExpandCombox() {
      await this.fetchData();
      this.showTable = true;
    },

    updateInputValue(event) {
      this.inputValue = event.target.value;
    },
    handleFocus() {
      this.isInputFocused = true;
      this.isMultiselectVisible = true;
      this.optionSelected = false;
    },
    handleBlur() {
      this.isInputFocused = false;
      setTimeout(() => {
        if (!this.optionSelected) {
          this.isMultiselectVisible = false;
        }
      }, 5000);
    },

    selectRow(item) {
      let columnConfig = this.config.columnConfig;
      let displayFirstValue = columnConfig?.find(
        (col) => col.isDisplayGrid
      )?.fieldName;
      if (displayFirstValue) {
        this.inputValue = item[displayFirstValue];
      }
      let displaySecondValue = columnConfig?.find(
        (col) => col.isDisplaySecondGrid
      )?.fieldName;
      if (displaySecondValue) {
        this.secondInputValue = item[displaySecondValue];
      }

      this.$emit("update:selectedRow", item);

      this.showTable = false;
      this.isMultiselectVisible = false;
      this.optionSelected = true;
      this.errors = null;
      this.$refs.inputComponent.setError(null);
    },

    setError(item) {
      this.errors = item;
      this.showError = !!item;
    },
  },
};
</script>

<style scoped>
.combobox-grid {
  display: flex;
  flex-direction: column;
  margin-bottom: 8px;
}

.input-container {
  display: flex;
  align-items: center;
  margin-top: 8px;
}

.input-with-button {
  display: flex;
  align-items: center;
  border: 1px solid #ccc;
  border-radius: 3px;
  overflow: hidden;
  flex-grow: 2;
  height: 28px;
  position: relative;
  box-sizing: border-box;
  width: 100%;
  background-color: #fff;
}

.base-input {
  border: none;
  box-sizing: border-box;
  height: 28px;
  width: calc(100% - 40px);
  box-sizing: border-box;
}

.base-input:focus {
  border: none;
  outline: none;
}

.invalid {
  border-color: #ff6666;
}

.valid {
  border-color: green;
}
.input-with-button.has-error {
  border: 1px solid #ff6666;
}
.input-status-container {
  position: absolute;
  right: 50px;
  top: 50%;
  transform: translateY(-50%);
}

.input-status {
  display: flex;
  color: #ff6666;
  font-weight: bold;
  background: white;
  border: 1px solid #ff6666;
  border-radius: 50%;
  width: 18px;
  height: 18px;
  display: flex;
  align-items: center;
  justify-content: center;
  font-size: 14px;
  cursor: pointer;
  position: relative;
}

.tooltip {
  visibility: hidden;
  background-color: #f44336;
  color: #fff;
  text-align: center;
  border-radius: 4px;
  padding: 5px;
  position: absolute;
  z-index: 1;
  bottom: 125%;
  left: 50%;
  margin-left: -60px;
  opacity: 0;
  transition: opacity 0.3s;
  width: 160px;
}

.input-status-container:hover .tooltip {
  visibility: visible;
  opacity: 1;
}

.multiselect {
  width: 40px;
  border: none;
}

.dropdown-table-wrapper {
  position: absolute;
  z-index: 700;
  background-color: white;
  margin-top: 40px;
  margin-left: 35px;
  transform: translateX(-40px);
  max-height: 200px;
  overflow-y: auto;
  width: auto;
  white-space: nowrap;
  overflow-x: hidden;
}

.dropdown-table {
  border-collapse: collapse;
  border: 1px solid #ddd;
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

.error-message {
  color: red;
  font-weight: bold;
  margin-top: 5px;
}
</style>

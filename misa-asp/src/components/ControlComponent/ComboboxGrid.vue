<template>
  <div class="combobox-grid">
    <div class="input-container">
      <div
        class="input-with-button"
        :class="{
          invalid: isInputFocused && !inputValue,
          valid: inputValue && isInputFocused,
        }"
      >
        <input
          v-model="inputValue"
          :value="inputValue"
          :type="type"
          @input="updateInputValue"
          @focus="handleFocus"
          @blur="handleBlur"
          class="base-input"
        />
        <div
          class="input-status-container"
          v-if="isInputFocused && !inputValue"
        >
          <span class="input-status">!</span>
          <span class="tooltip">Tài khoản không được để trống.</span>
        </div>
        <multiselect
          ref="multiselect"
          :style="{ visibility: isMultiselectVisible ? 'visible' : 'hidden' }"
          :value="selectedOption"
          @input="updateSelectedOption"
          :options="filteredOptions"
          :searchable="true"
          :close-on-select="true"
          placeholder=""
          class="multiselect"
          @open="onExpandCombox"
          @close="showTable = false"
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
import { apiClient } from "../../api/base";

export default {
  name: "ComboboxGrid",
  components: {
    Multiselect,
  },
  props: {
    selectedRow: {
      type: Object,
      default: null,
    },
    config: {
      type: Object,
      required: true,
    },
  },
  data() {
    return {
      inputValue: "",
      showTable: false,
      isInputFocused: false,
      isMultiselectVisible: false,
      optionsData: this.config.options != null ? this.config.options : [],
      optionSelected: false, // Thêm biến để kiểm tra xem có tùy chọn đã được chọn hay chưa
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
  methods: {
    async fetchData() {
      if (!this.config || !this.config.endpoint) {
        return;
      }
      try {
        const response = await apiClient[this.config.method](
          this.config.endpoint
        );
        if (
          response.data &&
          Array.isArray(response.data) // Giả định API trả về danh sách các objects
        ) {
          this.optionsData = response.data;
          console.log("optionsData:", this.optionsData);
        } else if (
          response.data &&
          response.data.data && // Giả định API trả về danh sách arrays
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
    handleFocus() {
      this.isInputFocused = true;
      this.isMultiselectVisible = true;
      this.optionSelected = false; // Reset khi input nhận được focus
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
      this.optionSelected = true; // Đánh dấu tùy chọn đã được chọn
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
  border-radius: 2px;
  overflow: hidden;
  flex-grow: 2;
  height: 30px;
  position: relative;
  box-sizing: border-box;
  width: 120px;
  background-color: #fff;
}

.base-input {
  border: none;
  padding: 8px;
  box-sizing: border-box;
  height: 30px;
  width: calc(100% - 40px);
  box-sizing: border-box;
}

.base-input:focus {
  border: none;
  outline: none;
}

.invalid {
  border-color: red;
}

.valid {
  border-color: green;
}

.input-status-container {
  position: absolute;
  right: 50px;
  top: 50%;
  transform: translateY(-50%);
}

.input-status {
  color: red;
  font-weight: bold;
  background: white;
  border: 1px solid red;
  border-radius: 50%;
  width: 20px;
  height: 20px;
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
  transform: translateX(-40px); /* Shift to the left */
  max-height: 200px; /* Set maximum height for the dropdown */
  overflow-y: auto; /* Enable vertical scrolling */
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
</style>

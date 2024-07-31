<template>
  <div class="accounting-component">
    <label for="accounting" class="accounting">{{ label }}</label>
    <table class="accounting-table">
      <thead class="thead">
        <tr>
          <th>#</th>
          <th v-for="(column, index) in columnConfig" :key="index">
            {{ column.columnName }}
          </th>
          <th></th>
        </tr>
      </thead>
      <tbody class="table-grid">
        <tr
          class="table-tbody"
          v-for="(row, rowIndex) in modelValue"
          :key="rowIndex"
        >
          <td>{{ rowIndex + 1 }}</td>
          <td
            class="td-grid"
            v-for="(column, colIndex) in columnConfig"
            :key="colIndex"
          >
            <div v-if="column.dataType === 'dropdown'">
              <MSComboboxGrid
                :value="row[column.fieldName]"
                :config="column.dropDownConfig"
                @input="changeValueInput(rowIndex, column)"
                @update:selectedRow="updateRowField(rowIndex, column, $event)"
              />
            </div>
            <div v-else>
              <input
                v-model="row[column.fieldName]"
                @input="changeValueInput(rowIndex, column)"
                :class="{
                  'right-align-input': true,
                  focus: isInputFocused(rowIndex, column.fieldName),
                }"
                @focus="handleFocus(rowIndex, column.fieldName)"
                @blur="handleBlur"
              />
            </div>
          </td>
          <td>
            <button @click="removeRow(rowIndex)" v-if="hasRemoveRow">🗑️</button>
          </td>
        </tr>
      </tbody>
    </table>
    <div class="accounting-footer">
      <button class="btn-left" @click="addRow">Thêm dòng</button>
      <button class="btn-right" @click="clearRows">Xóa hết dòng</button>
    </div>
  </div>
</template>

<script>
import MSComboboxGrid from "../ControlComponent/MSComboboxGrid.vue";
import MSInput from "../Base/MSInput.vue";

export default {
  name: "MSGrid",
  components: {
    MSComboboxGrid,
    MSInput,
  },
  props: {
    submitTotal: Function,
    changeInputRow: Function,
    modelValue: {
      type: Array,
      default: null,
    },
    hasRemoveRow: {
      type: Boolean,
      default: true,
    },
    isAutoAddNewRow: {
      type: Boolean,
      default: true,
    },
    configColumGrid: {
      type: Object,
      default: true,
    },
    label: {
      type: String,
      default: null,
    },
  },
  data() {
    return {
      columnConfig: this.configColumGrid.columnConfig,
      currentTotal: 0,
      focusedCell: { rowIndex: null, fieldName: null },
    };
  },
  mounted() {
    if (this.isAutoAddNewRow) {
      this.addRow();
    }
  },
  watch: {
    modelValue: {
      deep: true,
      handler() {
        this.updateCurrentTotal();
      },
    },
  },
  methods: {
    /**
     * Thêm một dòng mới vào bảng
     */
    addRow() {
      let model = this.configColumGrid.model;
      const newRow = new model();
      this.modelValue.push(newRow);
      this.updateCurrentTotal();
    },

    /**
     * Xóa một dòng trong bảng
     * @param {number} index - Chỉ số của dòng cần xóa
     */
    removeRow(index) {
      this.modelValue.splice(index, 1);
      this.updateCurrentTotal();
    },

    /**
     * Xóa tất cả các dòng trong bảng
     */
    clearRows() {
      this.modelValue = [];
      this.updateCurrentTotal();
    },

    /**
     * Cập nhật giá trị  khi chọn một giá trị từ combobox
     * @param {number} rowIndex - Chỉ số của dòng
     * @param {object} column - Cấu hình của cột
     * @param {object} selectedOption - Giá trị được chọn từ combobox
     */
    updateRowField(rowIndex, column, selectedOption) {
      let record = this.modelValue[rowIndex],
        fieldName = column.fieldName;
      record[fieldName] = selectedOption[fieldName];
      if (record) {
        this.$emit("selectedCombox", record, column, selectedOption);
      }
    },

    /**
     * Xử lý sự kiện khi một ô được focus
     * @param {number} rowIndex - Chỉ số của dòng
     * @param {string} fieldName - Tên trường của cột
     */
    handleFocus(rowIndex, fieldName) {
      this.focusedCell = { rowIndex, fieldName };
    },

    /**
     * Xử lý sự kiện khi một ô mất focus
     */
    handleBlur() {
      this.focusedCell = { rowIndex: null, fieldName: null };
    },

    /**
     * Kiểm tra xem ô có đang được focus hay không
     * @param {number} rowIndex - Chỉ số của dòng
     * @param {string} fieldName - Tên trường của cột
     * @returns {boolean} - Trạng thái focus của ô
     */
    isInputFocused(rowIndex, fieldName) {
      return (
        this.focusedCell.rowIndex === rowIndex &&
        this.focusedCell.fieldName === fieldName
      );
    },

    /**
     * Thay đổi giá trị của ô trong bảng
     * @param {number} rowIndex - Chỉ số của dòng
     * @param {object} column - Cấu hình của cột
     */
    changeValueInput(rowIndex, column) {
      let record = this.modelValue[rowIndex];
      if (record && column && column.dataType) {
        let fieldName = column.fieldName;
        switch (column.dataType) {
          case "currency":
            let value = record[fieldName].replace(/[^\d]/g, "");
            value = value.replace(/^0+/, "") || "0";
            record[fieldName] = value.replace(/\B(?=(\d{3})+(?!\d))/g, ".");
            break;
        }
      }

      if (record) {
        this.$emit("changeValueInput", record, column);
      }
      this.updateCurrentTotal();
    },

    /**
     * Cập nhật tổng giá trị hiện tại của các dòng trong bảng
     */
    updateCurrentTotal() {
      this.currentTotal = this.modelValue.reduce(
        (sum, row) =>
          sum + Number(row.amount.toString().replace(/\./g, "") || 0),
        0
      );
      this.$emit("updateTotalAmount", this.currentTotal);
    },
  },
};
</script>

<style scoped>
/* (Existing styles remain unchanged) */
.accounting-component {
  padding-left: 20px;
  padding-right: 20px;
}
.table-tbody {
  background-color: #e5f3ff;
}

.td-grid {
  height: 40px;
}
.thead {
  background-color: #f4f5f8;
}
.right-align-input.focus {
  border-color: green;
}

.btn-left {
  margin-right: 15px;
  border: 0.5px solid #999;
  width: 150px;
  background-color: #fff;
  font-weight: bold;
}

.btn-right {
  border: 0.5px solid #999;
  width: 150px;
  background-color: #fff;
  font-weight: bold;
}

.accounting {
  margin-bottom: 16px;
  font-weight: bold;
  color: #0075c0;
  font-weight: 700;
  text-decoration: underline;
}

.accounting-table {
  width: 100%;
  border-collapse: collapse;
  margin-bottom: 16px;
  margin-top: 8px;
  font-size: 15px;
}

.accounting-table th {
  border: 1px solid #ccc;
  padding: 8px;
  text-align: left;
  font-size: 14;
}

.accounting-table td {
  border: 1px solid #ccc;
  padding: 0px 10px 0px 10px;
  text-align: left;
}

.accounting-table th {
  font-weight: bold;
}

.accounting-table input,
.accounting-table select {
  width: 100%;
  border: 1px solid #ccc;
  padding: 4px;
  box-sizing: border-box;
  height: 28px;
  border-radius: 3px;
  outline: none;
}

.dropdown-select-a {
  appearance: none;
  background-color: #fff;
  border: 1px solid #ccc;
  border-radius: 3px;
  padding: 2px;
  height: 30px;
}

.dropdown-select option {
  padding: 4px;
}

.accounting-footer {
  display: flex;
  justify-content: flex-start;
}

button {
  padding: 6px 10px;
  border: none;
  border-radius: 2.5px;
  cursor: pointer;
  background-color: #e5f3ff;
}
button:hover {
  background-color: #f4f5f8;
}
</style>

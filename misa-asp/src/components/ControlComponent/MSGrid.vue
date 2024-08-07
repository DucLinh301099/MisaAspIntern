<template>
  <div class="accounting-component">
    <label for="accounting" class="accounting">{{ label }}</label>
    <table class="accounting-table">
      <thead class="thead">
        <tr>
          <th class="th-index">#</th>
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
            :class="getColumnClass(column.columnName)"
            v-for="(column, colIndex) in columnConfig"
            :key="colIndex"
          >
            <div v-if="column.dataType === 'dropdown'">
              <MSComboboxGrid
                v-if="!disabled"
                :value="row[column.fieldName]"
                :config="column.dropDownConfig"
                @input="changeValueInput(rowIndex, column)"
                @update:selectedRow="updateRowField(rowIndex, column, $event)"
                :ref="`[${rowIndex}].${column.fieldName}`"
              />
              <span v-else>{{ row[column.fieldName] }}</span>
            </div>
            <div v-else>
              <input
                v-if="!disabled"
                v-model="row[column.fieldName]"
                @input="changeValueInput(rowIndex, column)"
                :class="{
                  'right-align-input': true,
                  focus: isInputFocused(rowIndex, column.fieldName),
                  'ten-doi-tuong-input': column.columnName === 'Tên đối tượng',
                }"
                @focus="handleFocus(rowIndex, column.fieldName)"
                @blur="handleBlur"
                ref="inputComponent"
              />
              <span v-else>{{ row[column.fieldName] }}</span>
            </div>
          </td>
          <td class="button-style">
            <button
              @click="removeRow(rowIndex)"
              :disabled="disabled"
              v-if="hasRemoveRow"
            >
              🗑️
            </button>
          </td>
        </tr>
      </tbody>
    </table>
    <div class="accounting-footer">
      <button :disabled="disabled" class="btn-left" @click="addRow">
        Thêm dòng
      </button>
      <button :disabled="disabled" class="btn-right" @click="clearRows">
        Xóa hết dòng
      </button>
    </div>
  </div>
</template>

<script>
import MSComboboxGrid from "../ControlComponent/MSComboboxGrid.vue";
import BaseSubmit from "../Base/BaseSubmit.vue";

export default {
  name: "MSGrid",
  extends: BaseSubmit,
  components: {
    MSComboboxGrid,
  },
  props: {
    submitTotal: Function,
    changeInputRow: Function,
    modelValue: {
      type: Array,
      default: null,
    },
    disabled: {
      type: Boolean,
      default: false,
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
    field: {
      type: String,
      default: null,
    },
  },
  data() {
    return {
      columnConfig: this.configColumGrid.columnConfig,
      currentTotal: 0,
      focusedCell: { rowIndex: null, fieldName: null },
      errors: null,
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
    errors(newVal) {
      let refList = this.$refs;
      if (refList["inputComponent"]) {
        refList["inputComponent"].setError(newVal);
      }
    },
  },
  methods: {
    addRow() {
      let model = this.configColumGrid.model;
      const newRow = new model();
      this.modelValue.push(newRow);
      this.updateCurrentTotal();
    },
    removeRow(index) {
      this.modelValue.splice(index, 1);
      this.updateCurrentTotal();
    },
    clearRows() {
      this.modelValue = [];
      this.updateCurrentTotal();
    },
    updateRowField(rowIndex, column, selectedOption) {
      let record = this.modelValue[rowIndex],
        fieldName = column.fieldName;
      record[fieldName] = selectedOption[fieldName];
      if (record) {
        this.$emit("selectedCombox", record, column, selectedOption);
      }
    },
    handleFocus(rowIndex, fieldName) {
      this.focusedCell = { rowIndex, fieldName };
    },
    handleBlur() {
      this.focusedCell = { rowIndex: null, fieldName: null };
    },
    isInputFocused(rowIndex, fieldName) {
      return (
        this.focusedCell.rowIndex === rowIndex &&
        this.focusedCell.fieldName === fieldName
      );
    },
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
    updateCurrentTotal() {
      this.currentTotal = this.modelValue.reduce(
        (sum, row) =>
          sum + Number(row.amount.toString().replace(/\./g, "") || 0),
        0
      );
      this.$emit("updateTotalAmount", this.currentTotal);
    },
    setError(item) {
      this.errors.push(item);
    },
    getColumnClass(columnName) {
      if (
        columnName === "TK Nợ" ||
        columnName === "TK Có" ||
        columnName === "Đối tượng"
      ) {
        return "medium-column";
      } else if (columnName === "Số tiền") {
        return "narrow-column";
      }
      return "";
    },
  },
};
</script>
<style scoped>
.accounting-component {
  padding-left: 20px;
  padding-right: 20px;
}
.table-tbody {
  background-color: #e5f3ff;
}
.ten-doi-tuong-input {
  border: none !important;
  background-color: transparent;
  color: #000; /* Match the text color */
  cursor: default; /* Change cursor to default */
  pointer-events: none; /* Prevent user interactions */
}
.td-grid {
  height: 40px;
}
.td-grid.medium-column {
  width: 170px; /* Adjust as necessary */
}
.td-grid.narrow-column {
  width: 170px; /* Adjust as necessary */
}
@media screen and (max-width: 320px) {
  .td-grid.medium-column {
    width: 60px; /* Adjust as necessary */
  }
  .td-grid.narrow-column {
    width: 60px; /* Adjust as necessary */
  }
}
.button-style {
  width: 38px;
}
.th-index {
  width: 28px;
  align-items: center;
}
.thead {
  background-color: #f4f5f8;
}
.right-align-input.focus {
  border-color: green;
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
  font-size: 12.5px;
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
  height: 27px;
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
  padding: 5px 8px;
  border: none;
  border-radius: 2.5px;
  cursor: pointer;
  background-color: #e5f3ff;
  font-size: 11.5px;
}
.btn-left {
  margin-right: 10px;
  border: 0.5px solid #999;
  width: 120px;
  background-color: #fff;
  font-weight: bold;
}

.btn-right {
  border: 0.5px solid #999;
  width: 120px;
  background-color: #fff;
  font-weight: bold;
}
button:hover {
  background-color: #f4f5f8;
}
</style>

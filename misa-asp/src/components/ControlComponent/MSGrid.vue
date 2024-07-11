<template>
  <div class="accounting-component">
    <label for="accounting" class="accounting">Hạch toán</label>
    <table class="accounting-table">
      <thead>
        <tr>
          <th>#</th>
          <th v-for="(column, index) in columnConfig" :key="index">
            {{ column.columnName }}
          </th>
          <th></th>
        </tr>
      </thead>
      <tbody class="table-grid">
        <tr v-for="(row, rowIndex) in modelValue" :key="rowIndex">
          <td>{{ rowIndex + 1 }}</td>
          <td
            class="td-grid"
            v-for="(column, colIndex) in columnConfig"
            :key="colIndex"
          >
            <div v-if="column.dataType === 'dropdown'">
              <ComboboxGrid
                v-model="row[column.fieldName]"
                :config="column.dropDownConfig"
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
import ComboboxGrid from "../ControlComponent/ComboboxGrid.vue";

export default {
  name: "MSGrid",
  components: {
    ComboboxGrid,
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
    addRow() {
      let model = this.configColumGrid.model;
      const newRow = new model();
      this.modelValue.push(newRow);
    },
    removeRow(index) {
      this.modelValue.splice(index, 1);
    },
    clearRows() {
      this.modelValue = [];
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
    },
    updateCurrentTotal() {
      this.currentTotal = this.modelValue.reduce(
        (sum, row) =>
          sum + Number(row.amount.toString().replace(/\./g, "") || 0),
        0
      );
    },
  },
};
</script>

<style scoped>
.accounting-component {
  padding-left: 20px;
  padding-right: 20px;
}

.td-grid {
  height: 40px;
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
}

.accounting-table th {
  border: 1px solid #ccc;
  padding: 8px;
  text-align: left;
}

.accounting-table td {
  border: 1px solid #ccc;
  padding: 0px 10px 0px 10px;
  text-align: left;
}

.accounting-table th {
  background-color: #f5f5f5;
  font-weight: bold;
}

.accounting-table input,
.accounting-table select {
  width: 100%;
  border: 1px solid #ccc;
  padding: 4px;
  box-sizing: border-box;
  height: 30px;
  border-radius: 2px;
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
  padding: 8px 16px;
  border: none;
  border-radius: 2.5px;
  cursor: pointer;
  background-color: #fff;
}

button:hover {
  background-color: #ddd;
}
</style>

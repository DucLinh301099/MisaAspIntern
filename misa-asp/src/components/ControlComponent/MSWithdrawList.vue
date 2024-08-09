<template>
  <div class="withdraw-list-component">
    <div class="withdraw-list-wrapper">
      <table class="withdraw-list-table">
        <thead class="thead">
          <tr>
            <th class="th-index">#</th>
            <th v-for="(column, index) in columnConfig" :key="index">
              {{ column.columnName }}
            </th>
          </tr>
        </thead>
        <tbody class="table-grid">
          <tr
            class="table-tbody"
            v-for="(row, rowIndex) in filteredOptionsData"
            :key="rowIndex"
          >
            <td>{{ rowIndex + 1 }}</td>
            <td
              :class="[
                'td-grid',
                getColumnClass(column.columnName),
                column.columnName === 'Số chứng từ' ? 'td-chung-tu' : '',
              ]"
              v-for="(column, colIndex) in columnConfig"
              :key="colIndex"
            >
              <span
                v-if="
                  column.columnName !== 'Số chứng từ' &&
                  column.columnName !== 'Chức năng'
                "
                >{{ row[column.fieldName] }}</span
              >
              <a v-else-if="column.columnName === 'Số chứng từ'" href="#">{{
                row[column.fieldName]
              }}</a>
              <div v-else class="actions-container">
                <div class="flex justify-end">
                  <div class="ms-dropdown">
                    <button
                      class="ms-button ms-radius-false ms-dropdown-type-feature ms-dropdown-padding-custom-feature"
                    >
                      <div class="ms-button--text flex">
                        <div class="con-ms-tooltip">
                          <div class="tooltip-content">Xem</div>
                        </div>
                      </div>
                    </button>
                    <div class="dropdown">
                      <button
                        class="ms-button dropbtn ms-padding-is-single-false-size-default ms-dropdown-type-feature"
                        @click="toggleDropdown(rowIndex)"
                      >
                        <div class="ms-button--text flex">
                          <div class="mi mi-16 mi-arrow-up--blue">&nbsp;</div>
                        </div>
                      </button>
                      <div
                        v-if="dropdownVisible === rowIndex"
                        class="dropdown-content"
                      >
                        <a href="#" @click="editRow(row)">Sửa</a>
                        <a href="#" @click="deleteRow(row)">Xóa</a>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
    <div class="total-amount">
      <span class="span-amount"
        >Tổng tiền:<strong>{{ formattedTotalAmount }}</strong></span
      >
    </div>

    <div class="total-records">
      Tổng số:
      <strong class="bold-number">{{ filteredOptionsData.length }}</strong> bản
      ghi
    </div>
  </div>
</template>

<script>
import { withdrawList } from "../../api/withdrawlist";
import withdrawListConfig from "../../config/WithdrawListConfig";
import BaseForm from "../Base/BaseForm.vue";
import { baseApi } from "../../api/baseApi";

export default {
  name: "MSWithdrawList",
  extends: BaseForm,
  props: {
    searchQuery: {
      type: String,
      default: "",
    },
  },
  data() {
    return {
      columnConfig: withdrawListConfig.columnConfig,
      optionsData: [],
      dropdownVisible: null,
    };
  },
  computed: {
    filteredOptionsData() {
      if (!this.searchQuery) {
        return this.optionsData;
      }
      const query = this.searchQuery.toLowerCase();
      return this.optionsData.filter((item) => {
        return Object.values(item).some((val) =>
          String(val).toLowerCase().includes(query)
        );
      });
    },
    totalAmount() {
      return this.filteredOptionsData.reduce((sum, item) => {
        const amount = parseInt(item.totalAmount.replace(/[^\d]/g, ""), 10);
        return sum + (amount || 0);
      }, 0);
    },
    formattedTotalAmount() {
      return this.totalAmount.toLocaleString("vi-VN");
    },
  },
  mounted() {
    this.getWithdrawList();
  },
  methods: {
    async getWithdrawList() {
      if (!withdrawListConfig.endpoint) {
        return;
      }
      try {
        const response = await baseApi.getAuthenApi(
          withdrawListConfig.endpoint
        );
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
    getColumnClass(columnName) {
      if (columnName === "Số tiền" || columnName === "Số tài khoản NH") {
        return "narrow-column";
      }
      return "";
    },
    toggleDropdown(index) {
      this.dropdownVisible = this.dropdownVisible === index ? null : index;
    },
    editRow(row) {
      // Xử lý khi nhấn tùy chọn "Sửa"
    },
    async deleteRow(row) {
      this.showConfirm("Bạn có chắc chắn muốn xóa bản ghi này?", async () => {
        try {
          await withdrawList.deletePaymentById(row.id);

          this.optionsData = this.optionsData.filter(
            (item) => item.id !== row.id
          );
          this.showAlert("Xóa thành công!");
        } catch (error) {
          this.showAlert("Xóa thất bại. Vui lòng thử lại.");
        }
      });
    },
  },
};
</script>

<style scoped>
.withdraw-list-component {
  padding-left: 20px;
  padding-right: 20px;
  font-family: AvertaStdCY, Helvetica, Arial, sans-serif;
}
.bold-number {
  font-weight: 800;
}
.span-amount {
  font-size: 14px;
  text-align: center;
  padding-right: 150px;
}
.total-amount {
  background-color: #e5f3ff;
}
.total-records {
  font-size: 13px;
  color: #333;
  margin-top: 10px;
  text-align: left;
  padding-left: 10px;
  font-family: AvertaStdCY, Helvetica, Arial, sans-serif;
}
/* Thêm lớp này để bao quanh bảng */
.withdraw-list-wrapper {
  max-height: 490px; /* Bạn có thể điều chỉnh chiều cao tối đa */
  overflow-y: auto;
  border-bottom: 2px solid #ccc; /* Thêm border bên dưới bảng */
  box-shadow: 0 2px 2px -1px rgba(0, 0, 0, 0.473);
}

.table-tbody {
  background-color: #fff;
}

table {
  width: 100%;
  table-layout: fixed;
}

.thead {
  background-color: #f4f5f8;
  white-space: nowrap;
  height: 40px;
  position: sticky; /* Thêm thuộc tính này để thead luôn dính ở đầu */
  top: 0; /* Đặt thead ở đầu của bảng khi scroll */
  z-index: 2; /* Đảm bảo thead luôn nằm trên các hàng khác khi cuộn */
  box-shadow: 0 2px 2px -1px rgba(0, 0, 0, 0.4);
  border-top: 1px solid #ccc !important;
  border-bottom: 1px solid #ccc;
}
/* Style for the total amount row */

.total-label {
  text-align: left;
  padding-left: 10px;
}

.withdraw-list-table th {
  border-left: 1px solid #ccc;
  border-right: 1px solid #ccc;
  padding: 8px;
  text-align: center;
  font-size: 14;
  font-weight: bold;
}

.td-grid {
  height: 40px;
  text-align: center;
  white-space: nowrap;
}

.td-grid.narrow-column {
  width: 170px;
}

@media screen and (max-width: 320px) {
  .td-grid.narrow-column {
    width: 60px;
  }
}

.th-index {
  width: 28px;
  align-items: center;
  text-align: center;
}

.withdraw-list {
  margin-bottom: 16px;
  font-weight: bold;
  color: #0075c0;
  font-weight: 700;
  text-decoration: underline;
}

.withdraw-list-table {
  width: 100%;
  border-collapse: collapse;

  font-size: 12.5px;
}

.withdraw-list-table td {
  border: 1px solid #ccc;
  padding: 0px 10px 0px 10px;
  text-align: center;
}

.withdraw-list-table tr:hover {
  background-color: #e5f3ff;
}

.withdraw-list-table .td-chung-tu {
  color: inherit;
}

.withdraw-list-table .td-chung-tu a {
  color: #0075c0;
  text-decoration: none;
}

.withdraw-list-table .td-chung-tu a:hover {
  color: #0075c0;
  text-decoration: underline;
}

span {
  width: 100%;
  display: block;
  padding: 4px;
  box-sizing: border-box;
  height: 27px;
  border-radius: 3px;
  outline: none;
}

.actions-container {
  display: flex;
  justify-content: center;
  align-items: center;
  white-space: nowrap;
}

.actions-container button {
  padding: 5px 10px;
}

.dropdown {
  position: relative;
  display: inline-block;
}

.dropbtn {
  padding: 5px 10px;
}

.dropdown-content {
  display: none;
  position: absolute;
  background-color: #fff;
  min-width: 100px;
  box-shadow: 0px 8px 16px 0px rgba(0, 0, 0, 0.2);
  z-index: 1;
  right: 0;
  top: 100%;
  border-radius: 4px;
}

.dropdown-content a {
  color: black;
  padding: 12px 16px;
  text-decoration: none;
  display: block;
}

.dropdown-content a:hover {
  background-color: #f3f5fa;
  color: #13b45c;
}

.dropdown:hover .dropdown-content {
  display: block;
}

.justify-end {
  justify-content: flex-end !important;
}

.flex {
  display: flex !important;
}

.ms-dropdown-padding-custom-feature {
  padding: 6px 0 6px 16px !important;
}

.ms-dropdown-type-feature {
  color: #0075c0;
}

.ms-radius-false {
  border-radius: 3px 0 0 3px;
}

.ms-button {
  transition: all 0.2s ease;
  border: 0;
  cursor: pointer;
  position: relative;
  overflow: hidden;
  box-sizing: border-box;
  background: 0 0;
  overflow: visible;
}

.ms-button .ms-button--text {
  font-weight: 600;
  position: relative;
  color: inherit;
  display: inline-block;
  transition: all 0.25s ease;
  white-space: nowrap;
  font-size: 13px;
  line-height: 13px;
}

.ms-padding-is-single-false-size-default {
  width: 36px;
  padding-bottom: 5px;
}

.mi.mi-arrow-up--blue {
  background-position: -896px -359px;
}

.mi-16 {
  width: 16px;
  height: 16px;
  min-width: 16px;
  min-height: 16px;
}

.mi {
  background: url(https://actaspcdng1.misacdn.net/assets/Sprites-11d892c3.svg)
    no-repeat;
  cursor: pointer;
}
</style>

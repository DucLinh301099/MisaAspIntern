<template>
  <div class="withdraw-list-component">
    <div class="withdraw-list-wrapper">
      <div class="table-container">
        <!-- Bọc table trong một div mới -->
        <table class="withdraw-list-table">
          <thead class="thead">
            <tr>
              <th class="th-index">#</th>
              <th
                v-for="(column, index) in columnConfig"
                :key="index"
                @dblclick="sortRecordsByDate(column.fieldName)"
              >
                {{ column.columnName }}
              </th>
            </tr>
          </thead>
          <tbody class="table-grid">
            <tr
              class="table-tbody"
              v-for="(row, rowIndex) in filteredOptionsData"
              :key="rowIndex"
              @dblclick="viewRow(row)"
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
                <a
                  v-else-if="column.columnName === 'Số chứng từ'"
                  @click="viewRow(row)"
                  href="#"
                  >{{ row[column.fieldName] }}</a
                >
                <div v-else class="actions-container">
                  <div class="flex justify-end">
                    <div class="ms-dropdown">
                      <button
                        class="ms-button ms-radius-false ms-dropdown-type-feature ms-dropdown-padding-custom-feature"
                        @click="viewRow(row)"
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
      <!-- Kết thúc div table-container -->
    </div>
    <div class="total-amount">
      <span class="span-amount"
        >Tổng tiền:<strong>{{ formattedTotalAmount }}</strong></span
      >
    </div>
    <!-- <div class="pagination">
      <div class="total-records">
        Tổng số:
        <strong class="bold-number">{{ filteredOptionsData.length }}</strong>
        bản ghi
      </div>
      <div class="page-controls">
        <select v-model="itemsPerPage" @change="updatePage">
          <option
            v-for="option in itemsPerPageOptions"
            :key="option"
            :value="option"
            class="option-dropdown"
          >
            {{ option }} bản ghi trên 1 trang
          </option>
        </select>
        <button :disabled="currentPage === 1" @click="goToPreviousPage">
          Trước
        </button>
        <input
          type="number"
          v-model.number="currentPageInput"
          @change="goToPage(currentPageInput)"
          min="1"
          :max="totalPages"
        />
       
        <button :disabled="currentPage === totalPages" @click="goToNextPage">
          Sau
        </button>
      </div>
    </div> -->
    <MSPagination
      :total-records="filteredOptionsData.length"
      :items-per-page="itemsPerPage"
      :current-page="currentPage"
      :total-pages="totalPages"
      :items-per-page-options="itemsPerPageOptions"
      @update:itemsPerPage="updatePage"
      @update:currentPage="goToPage"
      @previous-page="goToPreviousPage"
      @next-page="goToNextPage"
    />
  </div>
</template>

<script>
import { withdrawList } from "../../api/withdrawlist";
import withdrawListConfig from "../../config/WithdrawListConfig";
import BaseSubmit from "../Base/BaseSubmit.vue";
import { baseApi } from "../../api/baseApi";
import MSPagination from "../Base/MSPagination.vue"

export default {
  name: "MSWithdrawList",
  extends: BaseSubmit,
  components:{
    MSPagination,
  },
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
      sortAscending: true,
      itemsPerPage: 10, // Số bản ghi trên mỗi trang mặc định
      currentPage: 1,
      currentPageInput: 1,  
      itemsPerPageOptions: [10, 20, 30, 50, 100, 200],
    };
  },
  computed: {
    filteredOptionsData() {
      let sortedData = [...this.optionsData];

      // Sắp xếp dữ liệu theo thứ tự giảm dần của thời gian tạo (mới nhất trước)
      sortedData.sort((a, b) => {
        const dateA = new Date(a.createdAt);
        const dateB = new Date(b.createdAt);

        return dateB - dateA; // Đảo ngược thứ tự để bản ghi mới nhất đứng đầu
      });

      // Nếu không có searchQuery, trả về dữ liệu đã sắp xếp
      if (!this.searchQuery) {
        return sortedData;
      }

      // Lọc dữ liệu theo searchQuery và trả về kết quả đã sắp xếp
      const query = this.searchQuery.toLowerCase();
      return sortedData.filter((item) => {
        return Object.values(item).some((val) =>
          String(val).toLowerCase().includes(query)
        );
      });
    },
    totalPages() {
      return Math.ceil(this.filteredOptionsData.length / this.itemsPerPage);
    },
    paginatedData() {
      const start = (this.currentPage - 1) * this.itemsPerPage;
      const end = start + this.itemsPerPage;
      return this.filteredOptionsData.slice(start, end);
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
    addNewRecord(newRecord) {
      this.optionsData.push(newRecord);
      this.$forceUpdate(); // Buộc cập nhật giao diện
    },
    sortRecordsByDate(fieldName) {
      this.sortAscending = !this.sortAscending; // Đảo ngược thứ tự sắp xếp

      this.optionsData.sort((a, b) => {
        const valueA = a[fieldName];
        const valueB = b[fieldName];

        if (fieldName === "createdAt") {
          // Nếu là cột thời gian tạo, chuyển đổi giá trị thành đối tượng Date
          return this.sortAscending
            ? new Date(valueA) - new Date(valueB)
            : new Date(valueB) - new Date(valueA);
        }

        if (fieldName === "totalAmount") {
          // Nếu là cột Số tiền, chuyển đổi giá trị thành số nguyên để sắp xếp
          const amountA = parseInt(valueA.replace(/[^\d]/g, ""), 10) || 0;
          const amountB = parseInt(valueB.replace(/[^\d]/g, ""), 10) || 0;

          return this.sortAscending ? amountA - amountB : amountB - amountA;
        }

        // So sánh các giá trị khác
        return this.sortAscending
          ? valueA > valueB
            ? 1
            : -1
          : valueA < valueB
          ? 1
          : -1;
      });
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
      this.$router.push({
        name: "payment",
        params: { id: row.id },
        query: { mode: "edit" },
      });
    },
    viewRow(row) {
      this.$router.push({
        name: "payment",
        params: { id: row.id },
        query: { mode: "view" },
      });
    },
    updatePage(itemsPerPage) {
      this.itemsPerPage = itemsPerPage;
      this.currentPage = 1;
    },
    goToPreviousPage() {
      if (this.currentPage > 1) {
        this.currentPage -= 1;
      }
    },
    goToNextPage() {
      if (this.currentPage < this.totalPages) {
        this.currentPage += 1;
      }
    },
    goToPage(page) {
      if (page >= 1 && page <= this.totalPages) {
        this.currentPage = page;
      }
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
.amout {
  padding-left: 5px;
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
  border-bottom: 2px solid #ccc; /* Thêm border bên dưới bảng */
  box-shadow: 0 2px 2px -1px rgba(0, 0, 0, 0.473);
}

.table-container {
  max-height: 490px; /* Chiều cao tối đa của container */
  overflow-y: auto; /* Tạo thanh cuộn dọc */
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
  top: 0;
  z-index: 1;
  position: sticky;
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

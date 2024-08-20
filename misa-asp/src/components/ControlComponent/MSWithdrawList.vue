<template>
  <div class="withdraw-list-component">
    <div class="withdraw-list-wrapper">
      <div class="table-container">
        <table class="withdraw-list-table">
          <thead class="thead">
            <tr>
              <th class="th-index">
                <input type="checkbox" @change="toggleSelectAll" />
              </th>
              <th
                v-for="(column, index) in columnConfig"
                :key="index"
                @dblclick="sortRecords(column.fieldName)"
                :class="[
                  getColumnClass(column.columnName),
                  column.columnName === 'Diễn giải' ? 'description-column' : '',
                  column.columnName === 'Chức năng' ? 'sticky-column' : '',
                ]"
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
              <td>
                <input type="checkbox" v-model="selectedRows" :value="row" />
              </td>
              <td
                :class="[
                  'td-grid',
                  getColumnClass(column.columnName),
                  column.columnName === 'Số chứng từ' ? 'td-chung-tu' : '',
                  column.columnName === 'Chức năng' ? 'sticky-column' : '',
                ]"
                v-for="(column, colIndex) in columnConfig"
                :key="colIndex"
              >
                <span
                  v-if="
                    column.columnName !== 'Số chứng từ' &&
                    column.columnName !== 'Chức năng'
                  "
                  >{{ row[column.fieldName] }}
                </span>

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
    </div>
    <div class="total-amount">
      <span class="span-amount"
        >Tổng:<strong>{{ formattedTotalAmount }}</strong></span
      >
    </div>

    <MSPagination
      :total-records="totalRecords"
      :items-per-page="itemsPerPage"
      :current-page="currentPage"
      :total-pages="totalPages"
      :items-per-page-options="itemsPerPageOptions"
      @update:itemsPerPage="updatePage"
      @update:currentPage="goToPage"
      @updatePageData="updatePageData"
      @previous-page="goToPreviousPage"
      @next-page="goToNextPage"
    />
  </div>
</template>

<script>
import MSPagination from "../Base/MSPagination.vue";
import BaseSubmit from "../Base/BaseSubmit.vue";
import { withdrawList } from "../../api/withdrawlist";
import withdrawListConfig from "../../config/WithdrawListConfig";
import { baseApi } from "../../api/baseApi";
import Api from "../../api/apiConst";

export default {
  name: "MSWithdrawList",
  extends: BaseSubmit,
  components: {
    MSPagination,
  },
  props: {
    searchQuery: {
      type: String,
      default: "",
    },
    pageData: {
      type: Object,
      required: true,
    },
  },
  data() {
    return {
      columnConfig: withdrawListConfig.columnConfig,
      optionsData: [],
      selectedRows: [], // Lưu trữ các dòng được chọn
      selectAll: false, // Trạng thái của checkbox chọn tất cả

      totalAmount: 0,
      sort: [],
      dropdownVisible: null,
      sortAscending: true,
      totalRecords: null,
      dataLength: 0,
      itemsPerPage: 10,
      currentPage: 1,
      currentPageInput: 1,
      itemsPerPageOptions: [10, 20, 30, 50],
    };
  },
  computed: {
    filteredOptionsData() {
      let sortedData = [...this.optionsData];

      // Nếu không có truy vấn tìm kiếm, trả về dữ liệu gốc
      if (!this.searchQuery) {
        return sortedData;
      }

      const query = this.searchQuery.toLowerCase();

      // Lọc dữ liệu dựa trên truy vấn tìm kiếm
      return sortedData.filter((item) => {
        return Object.values(item).some((val) =>
          String(val).toLowerCase().includes(query)
        );
      });
    },
    totalPages() {
      return Math.ceil(this.dataLength / this.itemsPerPage);
    },

    totalRecords() {
      return this.dataLength;
    },

    formattedTotalAmount() {
      return this.totalAmount.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");
    },
  },
  mounted() {
    this.$emit("updatePageData", {
      currentPage: this.currentPage,
      itemsPerPage: this.itemsPerPage,
    });
  },
  watch: {
    pageData: {
      handler() {
        this.currentPage = this.pageData.currentPage;
        this.getPagingWithdrawList(); // Gọi lại API mỗi khi pageData thay đổi
      },
      deep: true,
    },
  },
  methods: {
    toggleSelectAll() {
      this.selectAll = !this.selectAll;
      if (this.selectAll) {
        this.selectedRows = [...this.optionsData]; // Chọn tất cả các dòng
      } else {
        this.selectedRows = []; // Bỏ chọn tất cả các dòng
      }
    },
    addNewRecord(newRecord) {
      this.optionsData.push(newRecord);
      this.$forceUpdate();
    },

    getColumnClass(columnName) {
      if (columnName === "Số tiền") {
        return "amount-column";
      }
      if (columnName === "Ngày hạch toán" || columnName === "Ngày chứng từ") {
        return "date-column";
      }
      if (columnName === "Diễn giải") {
        return "description-column";
      }
      return "left-align-column";
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

    /**
     * hàm update số trang bản ghi trong 1 trang
     * @param itemsPerPage
     */
    updatePage(itemsPerPage) {
      this.itemsPerPage = itemsPerPage;
      this.currentPage = 1;
      this.$emit("updatePageData", {
        currentPage: this.currentPage,
        itemsPerPage: this.itemsPerPage,
      });
    },

    /**
     * hàm chuyển về trang trước
     */
    goToPreviousPage() {
      if (this.currentPage > 1) {
        this.currentPage -= 1;
        this.$emit("updatePageData", {
          currentPage: this.currentPage,
          itemsPerPage: this.itemsPerPage,
        });
      }
    },
    /**
     * hàm chuyển đến trang sau
     */
    goToNextPage() {
      if (this.currentPage < this.totalPages) {
        this.currentPage += 1;
        this.$emit("updatePageData", {
          currentPage: this.currentPage,
          itemsPerPage: this.itemsPerPage,
        });
      }
    },
    /**
     * hàm nhập số trang vào input
     * để di chuyển đến trang đó
     * @param page
     */
    goToPage(page) {
      if (page >= 1 && page <= this.totalPages) {
        this.currentPage = page;
        this.$emit("updatePageData", {
          currentPage: this.currentPage,
          itemsPerPage: this.itemsPerPage,
        });
      }
    },

    updatePageData(pageData) {
      this.$emit("updatePageData", pageData);
    },

    /**
     * function sắp xếp các bản ghi theo yêu cầu
     * @param fieldName
     */
    sortRecords(fieldName) {
      // Kiểm tra xem fieldName có tồn tại trong sort không
      const existingSortIndex = this.sort.findIndex(
        (sortItem) => sortItem.property === fieldName
      );

      if (existingSortIndex !== -1) {
        // Nếu đã tồn tại, đảo ngược giá trị của `desc`
        this.sort[existingSortIndex].desc = !this.sort[existingSortIndex].desc;
      } else {
        // Nếu chưa tồn tại, thêm một đối tượng sort mới vào mảng
        this.sort.push({
          property: fieldName,
          desc: true, // Mặc định sắp xếp giảm dần
        });
      }
      this.$emit("sort-updated", this.sort);
    },

    /**
     * api xóa 1 bản ghi
     * @param row
     */
    async deleteRow(row) {
      this.showConfirm("Bạn có chắc chắn muốn xóa bản ghi này?", async () => {
        try {
          await withdrawList.deletePaymentById(row.id);

          this.optionsData = this.optionsData.filter(
            (item) => item.id !== row.id
          );

          this.getPagingWithdrawList();
          this.showAlert("Xóa thành công!");
        } catch (error) {
          this.showAlert("Xóa thất bại. Vui lòng thử lại.");
        }
      });
    },
    /**
     * api hiển thị tất cả các withdraw payment
     */
    async getPagingWithdrawList() {
      if (!Api.getPagingFilter.url) {
        return;
      }
      try {
        const response = await baseApi.postApi(
          Api.getPagingFilter.url,
          this.pageData
        );
        if (response.pageData && Array.isArray(response.pageData)) {
          this.optionsData = response.pageData;
          this.dataLength = response.total;
          this.totalAmount = response.totalAmount;
        } else {
          this.optionsData = [];
          this.dataLength = 0;
          this.totalAmount = 0;
        }
      } catch (error) {
        this.optionsData = [];
        this.dataLength = 0;
        this.totalAmount = 0;
      }
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
.withdraw-list-table th,
.withdraw-list-table td {
  padding: 8px;
  vertical-align: middle;
  text-align: center; /* Căn giữa các checkbox */
}

input[type="checkbox"] {
  appearance: none; /* Loại bỏ kiểu dáng mặc định của checkbox */
  -webkit-appearance: none;
  -moz-appearance: none;
  width: 18px;
  height: 18px;
  border: 1px solid #afafaf;
  border-radius: 2px;
  outline: none;
  cursor: pointer;
  position: relative;
  background-color: white;
}

input[type="checkbox"]:checked {
  border-color: #28a745;
  background-color: white;
}

input[type="checkbox"]:checked::after {
  content: "\2714"; /* Ký hiệu dấu tick */
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  font-size: 12px;
  color: #28a745;
}

/* Thêm hiệu ứng hover */
input[type="checkbox"]:hover {
  border-color: #28a745; /* Viền xanh lá khi hover */
}

.amout {
  padding-left: 5px;
}

.span-amount {
  font-size: 12px;
  text-align: center;
}

.total-amount {
  background-color: #e5f3ff;
}
.account-number-column {
  max-width: 100px;

  overflow: hidden; /* Hide overflow */
  text-overflow: ellipsis; /* Display ellipsis (...) for overflowed text */
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
  max-height: 525px; /* Chiều cao tối đa của container */
  overflow-y: auto; /* Tạo thanh cuộn dọc */
}

.table-tbody {
  background-color: #fff;
}

table {
  width: 100%;
  table-layout: fixed;
}

/* CSS cho thẻ thead */
.thead {
  background-color: #f4f5f8;
  text-align: left;
  height: 35px;
  top: 0;
  z-index: 1;
  position: sticky;
  box-shadow: 0 2px 2px -1px rgba(0, 0, 0, 0.4);
  border-top: 1px solid #ccc !important;
  border-bottom: 1px solid #ccc;
}

.thead th {
  overflow: hidden;
  text-overflow: ellipsis;
  white-space: nowrap;
  padding: 8px;
  border-left: 1px solid #ccc;
  border-right: 1px solid #ccc;
  font-size: 14px;
  font-weight: bold;
  text-align: left;
}

/* CSS cho các ô td */
.withdraw-list-table td {
  border: 1px solid #ccc;
  padding: 5px;
  vertical-align: middle;
  overflow: visible;
  text-overflow: ellipsis;
  word-break: break-word;
  height: auto;
}

.withdraw-list-table td span {
  display: flex;
  width: 100%;
  word-break: break-word;
  margin: 0 auto;
  align-items: center;
}

/* Căn giữa text trong cột "Ngày hạch toán" và "Ngày chứng từ" */
.withdraw-list-table td.date-column span {
  display: flex;
  justify-content: center;
  align-items: center;
}

/* Căn phải text trong cột "Số tiền" */
.withdraw-list-table td.amount-column span {
  display: flex;
  justify-content: flex-end;
}

/* Căn trái text trong các cột còn lại */
.withdraw-list-table td.left-align-column {
  text-align: left;
}

/* Thêm vào các lớp cụ thể trong hàm getColumnClass */
.td-grid.date-column {
  text-align: center;
}

.td-grid.amount-column {
  text-align: right;
}
.withdraw-list-table td.description-column {
  width: 200px; /* Cung cấp chiều rộng tối thiểu */
  max-width: 300px; /* Giới hạn chiều rộng tối đa */
  white-space: nowrap; /* Ngăn không cho văn bản xuống dòng */
  overflow: hidden;
  text-overflow: ellipsis; /* Thêm dấu ba chấm nếu nội dung quá dài */
  text-align: left;
}

.td-grid.left-align-column {
  text-align: left;
}
/* Căn giữa text trong thẻ th của cột "Ngày hạch toán" và "Ngày chứng từ" */
.withdraw-list-table th.date-column {
  text-align: center;
}

/* Căn phải text trong thẻ th của cột "Số tiền" */
.withdraw-list-table th.amount-column {
  text-align: right;
}

/* Căn trái text trong các thẻ th của các cột còn lại */
.withdraw-list-table th.left-align-column {
  text-align: left;
}

/* Căn lề cho thẻ th của cột "Diễn giải" và đảm bảo cột này rộng hơn */
.withdraw-list-table th.description-column {
  text-align: left;
  max-width: 300px; /* Đảm bảo chiều rộng tối thiểu cho cột */
  width: 250px;
}

/* Style for the total amount row */
.total-label {
  text-align: left;
  padding-left: 10px;
}

.withdraw-list-table th {
  border-left: 1px solid #ccc;
  border-right: 1px solid #ccc;
  padding: 0px 8px 0px 8px;
  text-align: center;
  font-size: 12px;
  font-weight: bold;
}

.td-grid {
  height: 40px;
  text-align: center;
}

.withdraw-list-table td.td-grid.narrow-column {
  width: 170px;
  text-align: right !important;
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
  font-size: 12px;
  table-layout: auto;
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
  padding-left: 5px;
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

.sticky-column .dropdown-content {
  display: none;
  position: fixed;
  background-color: #fff;
  min-width: 100px;
  box-shadow: 0px 8px 16px 0px rgba(0, 0, 0, 0.2);
  z-index: 9999; /* Đặt giá trị z-index cao hơn */
  right: 0;
  top: auto; /* Đảm bảo không bị đè lên */
  top: 100%; /* Hiển thị phía trên nút */
  margin-bottom: 5px; /* Khoảng cách giữa dropdown và nút */
  border-radius: 2.5px;
}
.sticky-column .dropdown:hover .dropdown-content,
.sticky-column .dropbtn:focus + .dropdown-content {
  display: block;
}

.sticky-column .dropdown-content {
  position: absolute;
  right: 0;
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
